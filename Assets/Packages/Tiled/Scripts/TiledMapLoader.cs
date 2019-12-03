using UnityEngine;
using Tiled.Parser;

namespace Tiled {

    public class TiledMapLoader {

        private MapParser parser;

        private TiledMapLoader() { }

        public TiledMapLoader(MapParser mapParser) {
            parser = mapParser;
        }

        public Map Load(TextAsset asset) {
            if (asset == null) {
                throw new UnityException("Must set Map data as text asset.");
            }

            return parser.Parse(asset.text);
        }
    }
}
