using UnityEngine;
using System.Collections;
using Tiled;
using Tiled.Parser;

namespace Tiled.Builder {

    [AddComponentMenu("Tiled/MapBuilder")]
    [ExecuteInEditMode]
    public class MapBuilder : MonoBehaviour {

        [Tooltip("Map file as .json")]
        public TextAsset mapFile;

        [Tooltip("Distance between tiles")]
        public Vector2 tileSize;

        [Tooltip("Path to sprite used to create tiles")]
        public string spriteResource;

        [Tooltip("Prefab used for individual tiles")]
        public GameObject tilePrefab;

        public Map map;
        public Map Map { get { return map; } }

        private GameObject tileHolder;
        private SpriteSheet spriteSheet;
        private static readonly string tileHolderName = "Tiles";

        void Start() {
            initialize();
        }

        void Update() {
            initialize();
        }

        private void initialize() {

            if (map == null) {
                Debug.Log("Loading map data");
                map = new TiledMapLoader(new JSONMapParser()).Load(mapFile);

                MapContainer.Map = map;
            }

            tileHolder = GameObject.Find(tileHolderName);

            if (tileHolder != null) {
                return;
            }

            tileHolder = new GameObject();
            tileHolder.name = tileHolderName;

            spriteSheet = new SpriteSheet(spriteResource);

            createTiles(map);
        }

        private void createTiles(Map map) {

            foreach (Layer layer in map.Layers) {

                Debug.Log("Rendering layer: " + layer.Name);

                GameObject holder = new GameObject();
                holder.name = layer.Name;
                holder.transform.parent = tileHolder.transform;

                int x = 0, y = -map.Height;

                foreach (int d in layer.Data) {

                    if (d != 0) {

                        GameObject prefab = tilePrefab;

                        //  set a custom prefab if tile requires one
                        if (map.ObjectReferences.ContainsKey(d)) {
                            prefab = map.ObjectReferences[d];
                        }

                        Vector3 pos = Vector3.zero;

                        if(map.MapOrientation.Equals(Map.Orientation.ORTHOGONAL)) {
                            pos = orthogonalPos(x, y, layer.Height);
                        } else if (map.MapOrientation.Equals(Map.Orientation.ISOMETRIC)) {
                            pos = isometricPos(x, y, layer.Height);
                        }

                        GameObject t = (GameObject)GameObject.Instantiate(
                            prefab,
                            pos,
                            layer.Rotation);

                        t.name = x + ", " + y + ": " + tilePrefab.name;

                        //  only set the custom tile if using the tile prefab
                        if (prefab == tilePrefab) {
                            SpriteRenderer renderer = t.GetComponentInChildren<SpriteRenderer>();
                            if (renderer != null) {
                                renderer.sprite = spriteSheet.sprites[d - 1];
                            }
                        }

                        t.transform.SetParent(holder.transform);
                    }

                    x++;

                    if (x >= map.Width) {
                        x = 0;
                        y++;
                    }
                }
            }
        }

        private Vector3 orthogonalPos(int x, int y, int layerHeight) {
            return new Vector3(x * tileSize.x, -y * tileSize.y, layerHeight);
        }

        private Vector3 isometricPos(int x, int y, int layerHeight) {
            return new Vector3(x - y, (x + y) / 2f, layerHeight);
        }
    }
}
