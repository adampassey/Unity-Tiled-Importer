# Unity Tiled Importer
[Unity](https://unity3d.com/) Package for importing map data from the [Tiled Map Editor](http://www.mapeditor.org/).

### Usage

##### Installation

* Download the [Tiled Unity Package](https://github.com/adampassey/Unity-Tiled-Importer/blob/master/Tiled.unitypackage?raw=true). 
* From within Unity choose `Assets`, `Import Package`, `Custom Package`. Then select the downloaded package.
* Import all scripts.

##### Building

Once you've imported the package, you're ready to begin building your Tiled map in Unity! Create your map from within Tiled
and export it as `json`. _Unity Tiled Importer currently only supports map importing via `json`._

Create an empty `GameObject` that will be used as a `MapBuilder`. Attach a `MapBuilder` component by selecting `Add Component`,
`Tiled -> MapBuilder`. The `MapBuilder` component requires several properties to build the map:

* **Map Json** _(TextAsset)_: `json` export of your Tiled map data.
* **Tile Size** _(Vector2)_: Distance between each of your tiles. This will vary based on your individual project setup.
* **Sprite Resource** _(String)_: Path to your sprite tileset resource. The image must be imported into Unity in `Sprite Mode: Multiple`.
* **Tile Prefab** _(GameObject)_: Prefab for each tile.

Unity Tiled Importer will automatically build your map when the scene is built. Staying consistent with the default Unity2d
settings, the map is built on the `x`, `y` axis, with `z` being reserved for height.

##### Additional Configuration

The Unity Tiled Importer also supports some custom configuration, allowing you to:

* Use multiple map layers with specified height
* Setup different prefabs to be used on a per-tile basis
* Determine rotation of objects in a layer

Unity Tiled Importer uses [SimpleJSON](http://wiki.unity3d.com/index.php/SimpleJSON), which is somewhat limited in how
it parses the exported data from Tiled. Because of this, some of the custom configurations require a bit more work. 
These are the custom properties that are supported:

* Layers
  * **Rotation-x**: The rotation of the objects in the layer on the `x` axis
  * **Rotation-y**: The rotation of the objects in the layer on the `y` axis
  * **Rotation-z**: The rotation of the objects in the layer on the `z` axis
* Tiles
  * **Prefab**: Path to a prefab to be loaded instead of the default `Tile Prefab`
  * **ID**: A duplicate of the `ID` tile assigned by Tiled _(only needed if using the `Prefab` property)_

**To make a tile load a specific prefab**, add a custom property to that tile from within Tiled. The property should be 
named `Prefab`, with it's value being the path to the prefab from the `Resource` directory. Example: `Prefab: Prefabs/Wood`
will attempt to load the prefab: `Resources/Prefabs/Wood`. You will also need to add an additional custom property called
`ID`, which should be set to the _same value as the tile ID that Tiled creates automatically_. 

**Setting the rotation of objects in a layer is also fairly easy**. A layer just needs to be assigned one (or more) of
the three rotational properties to apply that rotation to all objects inside of it:
`Rotation-x`, `Rotation-y`, or `Rotation-z`. By default, all objects will be set to rotation of `Vector3(0, 0, 0)`.
