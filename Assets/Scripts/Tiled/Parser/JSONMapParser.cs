using UnityEngine;
using System.Collections.Generic;
using Tiled;
using SimpleJSON;

namespace Tiled.Parser {

    public class JSONMapParser : MapParser {

        public Map Parse(string text) {

            JSONNode json = JSON.Parse(text);

            Map map = new Map(createReferences(json));
            map.Layers = createLayers(json["layers"]);

            map.Height = json["height"].AsInt;
            map.Width = json["width"].AsInt;

            return map;
        }

        //  create all the prefab references required to create
        //  the map. Should be used for creating custom objects
        //  not tiles.
        private Dictionary<int, GameObject> createReferences(JSONNode json) {

            Dictionary<int, GameObject> references = new Dictionary<int, GameObject>();

            foreach (JSONNode node in json["tilesets"].AsArray) {

                int firstgid = node["firstgid"].AsInt;

                for (int i = 0; i < node["tileproperties"].Count; i++) {

                    string prefabPath = node["tileproperties"][i]["Prefab"].Value;
                    GameObject prefab = Resources.Load<GameObject>(prefabPath);

                    if (prefab == null) {
                        throw new UnityException("Cannot load prefab at: " + prefabPath);
                    }

                    references.Add(node["tileproperties"][i]["ID"].AsInt + firstgid, prefab);
                }
            }

            return references;
        }

        //  create all layers
        private List<Layer> createLayers(JSONNode json) {

            List<Layer> layers = new List<Layer>();

            foreach (JSONNode layerNode in json.AsArray) {
                Layer layer = new Layer();
                layer.Name = layerNode["name"];
                layer.Height = layerNode["properties"]["Height"].AsInt;

                Vector3 rotation = Vector3.zero;

                if (layerNode["properties"]["Rotation-x"].Value != null) {
                    rotation.x = layerNode["properties"]["Rotation-x"].AsFloat;
                }

                if (layerNode["properties"]["Rotation-y"].Value != null) {
                    rotation.y = layerNode["properties"]["Rotation-y"].AsFloat;
                }

                if (layerNode["properties"]["Rotation-z"].Value != null) {
                    rotation.z = layerNode["properties"]["Rotation-z"].AsFloat;
                }

                layer.Rotation = Quaternion.Euler(rotation);

                foreach (JSONNode dataNode in layerNode["data"].AsArray) {
                    layer.Data.Add(dataNode.AsInt);
                }

                layers.Add(layer);
            }

            return layers;
        }
    }
}
