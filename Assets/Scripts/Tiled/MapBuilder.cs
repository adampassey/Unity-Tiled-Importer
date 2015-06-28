using UnityEngine;
using System.Collections;
using Tiled;
using Tiled.Parser;

namespace Tiled.Builder {

    [AddComponentMenu("Tiled/MapBuilder")]
    public class MapBuilder : MonoBehaviour {

        public string mapJson;
        public Vector2 tileSize;

        private GameObject tileHolder;

        void Start() {

            tileHolder = new GameObject();
            tileHolder.name = "Tiles";

            Map map = new TiledMapLoader(
                new JSONMapParser())
                .Load(mapJson);

            CreateTiles(map);
        }

        private void CreateTiles(Map map) {

            foreach (Layer layer in map.Layers) {

                Debug.Log("Rendering layer: " + layer.Name);

                GameObject holder = new GameObject();
                holder.name = layer.Name;
                holder.transform.parent = tileHolder.transform;

                int x = 0, y = -map.Height;

                foreach (int d in layer.Data) {

                    if (d != 0) {

                        Debug.Log(map.ObjectReferences[d]);

                        break;

                        GameObject prefab = map.ObjectReferences[d];

                        GameObject t = (GameObject)GameObject.Instantiate(
                            prefab,
                            new Vector3(x * tileSize.x, -y * tileSize.y, layer.Height),
                            layer.Rotation);

                        t.name = x + ", " + y + ": " + prefab.name;

                        SpriteRenderer renderer = t.GetComponent<SpriteRenderer>();
                        renderer.sortingOrder = Mathf.Abs(y * (int)tileSize.y) + layer.Height;

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
