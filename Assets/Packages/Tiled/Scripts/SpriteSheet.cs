using UnityEngine;
using System.Collections;

namespace Tiled {

    public class SpriteSheet {

        public Sprite[] sprites;

        public SpriteSheet(string spritePath) {
            try {
                sprites = Resources.LoadAll<Sprite>(spritePath);
            }
            catch (UnityException e) {
                Debug.LogError("Unable to load sprites. " + e);
            }
        }

    }
}
