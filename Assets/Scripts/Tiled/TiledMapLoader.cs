using UnityEngine;
using Tiled.Parser;

namespace Tiled {

    public class TiledMapLoader {

        private MapParser parser;

        private TiledMapLoader() { }

        public TiledMapLoader(MapParser mapParser) {
            parser = mapParser;
        }

        public Map Load(string path) {
            TextAsset textAsset = (TextAsset)Resources.Load(path);
            if (textAsset == null) {
                throw new UnityException("Cannot load map data from path: " + path);
            }

            return parser.Parse(textAsset.text);
        }
    }
}
