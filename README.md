# C# GDExtension Bindgen
[![Godot Asset Library page](https://img.shields.io/static/v1?logo=godotengine&label=asset%20library&color=478CBF&message=0.2.0)](https://godotengine.org/asset-library/asset/3832)

Automatic C# bindings generator for GDExtension classes (Godot 4.4+)


## How to use
1. Import your GDExtensions
2. Enable the `C# GDExtension Bindgen` plugin in `Project Settings... -> Plugins` tab
3. Click in the menu item `Project -> Tools -> Generate C# GDExtension Bindings` whenever you want to regenerate C# bindings
4. Enjoy 🍾


## How does it work?
This editor plugin contains the script [csharp_gdextension_bindgen.gd](addons/csharp_gdextension_bindgen/csharp_gdextension_bindgen.gd) with the whole C# code generation.
It uses the reflection provided by [ClassDB](https://docs.godotengine.org/en/stable/classes/class_classdb.html) to generate bindings to enums, constants, properties, methods and signals for all classes registered using the GDExtension API.

The generated C# classes are in the `GDExtensionBindgen` namespace, so you'll need to add `using GDExtensionBindgen;` before using them.

The generated classes do not inherit from any `GodotObject` subclass because they are not C# scripts attached to the native objects, but rather a thin C# wrapper with strongly typed definitions of all members registered. Most of the time this addon tries to generate code that matches what Godot generates for C# for engine classes, e.g. you'll see `StringName`s cached using the `PropertyName`, `MethodName` and `SignalName` nested classes.


## Caveats
- Since generated classes do not inherit `GodotObject`, there's no easy way to create C# scripts that specify the wrapped GDExtension classes as their base class


## Generating bindings from the CLI
This addon also contains the [cli_entrypoint.gd](addons/csharp_gdextension_bindgen/cli_entrypoint.gd) script that can be used to generate bindings via command line:
```sh
# Generates C# bindings for all GDExtension classes
# Optionally pass the desired output directory and C# namespace
godot --headless --script addons/csharp_gdextension_bindgen/cli_entrypoint.gd -- [OUTPUT_DIR] [NAMESPACE]
```

This can be used by GDExtension projects themselves as a way to distribute pre-generated C# bindings.


## TODO
- [X] support constant integers that are not part of any enum
- [X] get/set properties inherited from engine class directly without Get/Set
- [X] support signals
- [X] submit to the Asset Library
- [X] cache StringNames
- [ ] support packed arrays as method default values
- [ ] support non-empty Arrays and Dictionaries as method default values
