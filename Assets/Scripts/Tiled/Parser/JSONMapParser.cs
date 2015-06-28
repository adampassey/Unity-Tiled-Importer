using UnityEngine;
using System.Collections.Generic;
using Tiled;
using SimpleJSON;

namespace Tiled.Parser {

    public class JSONMapParser : MapParser {

        public Map Parse(string text) {

            JSONNode json = JSON.Parse(text);

            Map map = new Map(CreateReferences(json));

            map.Height = json["height"].AsInt;
            map.Width = json["width"].AsInt;

            foreach (JSONNode layerNode in json["layers"].AsArray) {
                Layer layer = new Layer();
                layer.Name = layerNode["name"];
                layer.Height = layerNode["properties"]["Height"].AsInt;

                if (layerNode["properties"]["Rotation"].Value != null) {
                    layer.Rotation = Quaternion.Euler(new Vector3(0, 0, layerNode["properties"]["Rotation"].AsInt));
                }

                foreach (JSONNode dataNode in layerNode["data"].AsArray) {
                    layer.Data.Add(dataNode.AsInt);
                }

                map.Layers.Add(layer);
            }

            return map;
        }

        //  create all the references required to create the map
        //  hasn't been tested with multiple tilesets
        //  https://github.com/bjorn/tiled/issues/605
        private Dictionary<int, GameObject> CreateReferences(JSONNode json) {

            Dictionary<int, GameObject> references = new Dictionary<int, GameObject>();

            foreach (JSONNode node in json["tilesets"].AsArray) {

                int firstgid = node["firstgid"].AsInt;

                for (int i = 0; i < node["tileproperties"].Count; i++) {

                    string prefabPath = node["tileproperties"][i]["Prefab"].Value;
                    GameObject prefab = Resources.Load<GameObject>(prefabPath);

                    if (prefab == null) {
                        throw new UnityException("Cannot load prefab at: " + prefabPath);
                    }

                    references.Add(i + firstgid, prefab);
                }
            }

            return references;
        }
    }
}
