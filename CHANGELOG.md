# Changelog
## [Unreleased](https://github.com/gilzoide/godot-csharp-gdextension-bindgen/compare/0.3.1...HEAD)


## [0.3.1](https://github.com/gilzoide/godot-csharp-gdextension-bindgen/releases/tag/0.3.1)
### Fixed
- Files exported in the zip downloaded from GitHub


## [0.3.0](https://github.com/gilzoide/godot-csharp-gdextension-bindgen/releases/tag/0.3.0)
### Added
- Support for default values with types `float`, `StringName`, float Vector types, `Color`, all Packed Array types, `Transform2D` and `Transform3D`

### Changed
- Inherited enums and integer constants are not exposed in GDExtension classes, which fixes possible name clashes
- Private methods, whose name start with the prefix `_`, are not exposed anymore

### Fixed
- Only export `addons/csharp_gdextension_bindgen` folder to the Asset Library
- Enums where one of the constants is present in all constant names.
  Example: `Node.ProcessThreadMessages` and `FLAG_PROCESS_THREAD_MESSAGES`
- Only add the "Enum" suffix to enum types when their name would clash with another declaration, such as a signal, method or property.
  This was done to match Godot's C# binding generation logic.
- Inner type name from typed arrays
- Support for properties that register more than one class name in "class_name".
  In such case, the common parent class is used.
  Example: `CanvasItem.material` uses `CanvasItemMaterial,ShaderMaterial`.
- Names of Godot classes that don't exactly match their GDScript counterparts.
  The code replicates what Godot does in [modules/mono/utils/naming_utils.cpp](https://github.com/godotengine/godot/blob/4.3-stable/modules/mono/utils/naming_utils.cpp).
  Example: `MultiplayerAPI` -> `MultiplayerApi`


## [0.2.0](https://github.com/gilzoide/godot-csharp-gdextension-bindgen/releases/tag/0.2.0)
### Added
- [cli_entrypoint.gd](addons/csharp_gdextension_bindgen/cli_entrypoint.gd) script that can be used to generate bindings via command line.
  Usage:
  ```sh
  godot --headless --script addons/csharp_gdextension_bindgen/cli_entrypoint.gd -- [OUTPUT_DIR] [NAMESPACE]
  ```


## [0.1.0](https://github.com/gilzoide/godot-csharp-gdextension-bindgen/releases/tag/0.1.0)
### Added
- [csharp_gdextension_bindgen.gd](addons/csharp_gdextension_bindgen/csharp_gdextension_bindgen.gd) editor plugin that generates C# bindings for all GDExtension classes currently registered.
  To generate the bindings, enable the plugin and use the menu item `Project -> Tools -> Generate C# GDExtension Bindings`.
