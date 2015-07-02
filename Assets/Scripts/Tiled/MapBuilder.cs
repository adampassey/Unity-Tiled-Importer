using UnityEngine;
using System.Collections;
using Tiled;
using Tiled.Parser;

namespace Tiled.Builder {

    [AddComponentMenu("Tiled/MapBuilder")]
    public class MapBuilder : MonoBehaviour {

        [Tooltip("Exported map as .json")]
        public TextAsset mapJson;

        [Tooltip("Distance between tiles")]
        public Vector2 tileSize;

        [Tooltip("Path to sprite used to create tiles")]
        public string spriteResource;

        [Tooltip("Prefab used for individual tiles")]
        public GameObject tilePrefab;

        private GameObject tileHolder;
        private SpriteSheet spriteSheet;

        void Start() {

            tileHolder = new GameObject();
            tileHolder.name = "Tiles";

            Map map = new TiledMapLoader(
                new JSONMapParser())
                .Load(mapJson);

            spriteSheet = new SpriteSheet(spriteResource);

            CreateTiles(map);
        }

        private void CreateTiles(Map map) {

            foreach (Layer layer in map.Layers) {

                Debug.Log("Rendering layer: " + layer.Name);

                GameObject holder = new GameObject();
                holder.name = layer.Name;
                holder.transform.parent = tileHolder.transform;

                int x = 0, z = -map.Height;

                foreach (int d in layer.Data) {

                    if (d != 0) {

                        GameObject prefab = tilePrefab;

                        //  set a custom prefab if tile requires one
                        if (map.ObjectReferences.ContainsKey(d)) {
                            Debug.Log("Getting prefab at: " + d);
                            prefab = map.ObjectReferences[d];
                        }

                        GameObject t = (GameObject)GameObject.Instantiate(
                            prefab,
                            new Vector3(x * tileSize.x, layer.Height , -z * tileSize.y),
                            layer.Rotation);

                        t.name = x + ", " + z + ": " + tilePrefab.name;

                        //  only set the custom tile if using the tile prefab
                        if (prefab == tilePrefab) {
                            SpriteRenderer renderer = t.GetComponent<SpriteRenderer>();
                            if (renderer != null) {
                                renderer.sprite = spriteSheet.sprites[d - 1];
                            }
                        }

                        t.transform.parent = holder.transform;
                    }

                    x++;

                    if (x >= map.Width) {
                        x = 0;
                        z++;
                    }
                }
            }
        }
    }
}
