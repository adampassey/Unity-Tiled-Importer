using UnityEngine;
using System.Collections;
using Tiled;
using Tiled.Parser;

namespace Tiled.Builder {

    [AddComponentMenu("Tiled/MapBuilder")]
    public class MapBuilder : MonoBehaviour {

        public TextAsset mapJson;
        public Vector2 tileSize;

        public string spriteResource;
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
            Debug.Log(spriteSheet.sprites.Length);

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

                        GameObject t = (GameObject)GameObject.Instantiate(
                            tilePrefab,
                            new Vector3(x * tileSize.x, -y * tileSize.y, layer.Height),
                            layer.Rotation);

                        t.name = x + ", " + y + ": " + tilePrefab.name;

                        SpriteRenderer renderer = t.GetComponent<SpriteRenderer>();
                        renderer.sprite = spriteSheet.sprites[d-1];

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
