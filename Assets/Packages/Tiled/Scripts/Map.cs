using UnityEngine;
using System.Collections.Generic;

namespace Tiled {

    public class Map {

        public enum Orientation {
            ORTHOGONAL,
            ISOMETRIC,
        }

        public enum RenderOrder {
            LEFT_UP,
            RIGHT_DOWN,
        }

        public Map() {

        }

        public Map(int width, int height, string orientation, string renderOrder, Dictionary<int, GameObject> references) {
            Width = width;
            Height = height;
            SetMapOrientation(orientation);
            SetRenderOrder(renderOrder);
            layers = new List<Layer>();
            objectReferences = references;
        }

        public Map(Dictionary<int, GameObject> references) {
            layers = new List<Layer>();
            objectReferences = references;
        }

        private string name;
        public string Name {
            get { return name; }
            set { name = value; }
        }

        private int height;
        public int Height {
            get { return height; }
            set { height = value; }
        }

        private int width;
        public int Width {
            get { return width; }
            set { width = value; }
        }

        private List<Layer> layers;
        public List<Layer> Layers {
            get { return layers; }
            set { layers = value; }
        }

        private Dictionary<int, GameObject> objectReferences;
        public Dictionary<int, GameObject> ObjectReferences {
            get { return objectReferences; }
        }

        private Orientation mapOrientation;
        public Orientation MapOrientation {
            get { return mapOrientation; }
            set { mapOrientation = value; }
        }
        public int TileHeight;
        public int TileWidth;
        public RenderOrder MapRenderOrder;

        public Orientation SetMapOrientation(string orientation) {
            if (orientation.Equals("orthogonal")) {
                MapOrientation = Orientation.ORTHOGONAL;
            } else if (orientation.Equals("isometric")) {
                MapOrientation = Orientation.ISOMETRIC;
            }
            return MapOrientation;
        }

        public RenderOrder SetRenderOrder(string renderOrder) {
            if (renderOrder.Equals("left-up")) {
                MapRenderOrder = RenderOrder.LEFT_UP;
            } else if (renderOrder.Equals("right-down")) {
                MapRenderOrder = RenderOrder.RIGHT_DOWN;
            }
            return MapRenderOrder;
        }
    }
}
