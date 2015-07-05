using UnityEngine;
using System.Collections;
using Tiled;
using Tiled.Parser;

namespace Tiled.Builder {

    [AddComponentMenu("Tiled/MapBuilder")]
    [ExecuteInEditMode]
    public class MapBuilder : MonoBehaviour {

        [Tooltip("Exported map as .json")]
        public TextAsset mapJson;

        [Tooltip("Distance between tiles")]
        public Vector2 tileSize;

        [Tooltip("Path to sprite used to create tiles")]
        public string spriteResource;

        [Tooltip("Prefab used for individual tiles")]
        public GameObject tilePrefab;

        private Map map;
        public Map Map { get { return map; } }

        private GameObject tileHolder;
        private SpriteSheet spriteSheet;
        private static string tileHolderName = "Tiles";

        void Start() {
            initialize();
        }

        void Update() {
            initialize();
        }

        private void initialize() {
            
            tileHolder = GameObject.Find(tileHolderName);

            if (tileHolder != null) {
                return;
            }

            tileHolder = new GameObject();
            tileHolder.name = tileHolderName;

            map = new TiledMapLoader(
                new JSONMapParser())
                .Load(mapJson);

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

                        GameObject t = (GameObject)GameObject.Instantiate(
                            prefab,
                            new Vector3(x * tileSize.x, -y * tileSize.y, layer.Height),
                            layer.Rotation);

                        t.name = x + ", " + y + ": " + tilePrefab.name;

                        //  only set the custom tile if using the tile prefab
                        if (prefab == tilePrefab) {
                            SpriteRenderer renderer = t.GetComponentInChildren<SpriteRenderer>();
                            if (renderer != null) {
                                renderer.sprite = spriteSheet.sprites[d - 1];
                            }
                        }

                        t.transform.parent = holder.transform;
                    }

                    x++;

                    if (x >= map.Width) {
                        x = 0;
                        y++;
                    }
                }
            }
        }
    }
}
