using UnityEngine;
using System.Collections.Generic;

namespace Tiled {

    public class Map {

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
    }
}
