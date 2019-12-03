using UnityEngine;
using System.Collections.Generic;

namespace Tiled {

    public class Layer {

        public Layer() {
            data = new List<int>();
        }

        private List<int> data;
        public List<int> Data {
            get { return data; }
            set { data = value; }
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

        private Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        public Quaternion Rotation {
            get { return rotation; }
            set { rotation = value; }
        }
    }
}
