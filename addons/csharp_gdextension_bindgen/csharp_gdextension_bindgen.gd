## GDExtension to C# binding generator
##
## The C# classes generated are not attached scripts, but rather wrappers that
## forward execution to a GodotObject using dynamic calls.
##
## Use the "Project -> Tools -> Generate C# GDExtension Bindings" menu item to
## generate C# bindings from GDExtension.
##
## TODO: support constant integers that are not part of any enum
## TODO: cache StringNames used by class
@tool
extends EditorPlugin


const MENU_ITEM_NAME = "Generate C# GDExtension Bindings"
const GENERATED_NAMESPACE = "GDExtensionBindgen"
const GENERATED_SCRIPTS_FOLDER = "res://GDExtensionBindgen"


func _enter_tree():
	add_tool_menu_item(MENU_ITEM_NAME, _run)


func _exit_tree():
	remove_tool_menu_item(MENU_ITEM_NAME)


func generate_csharp_script(cls_name: StringName):
	var class_is_extension = _is_extension_class(cls_name)
	var class_is_editor_only = _is_editor_extension_class(cls_name)
	var parent_class = ClassDB.get_parent_class(cls_name)
	var parent_class_is_extension = _is_extension_class(parent_class)
	var no_inheritance = parent_class_is_extension
	var engine_class = _first_non_extension_parent(cls_name)

	var regions = PackedStringArray()

	# StringName cache for class name
	regions.append("public {maybe_new}static readonly StringName ClassName = \"{cls_name}\";".format({
		cls_name = cls_name,
		maybe_new = "new " if parent_class_is_extension else "",
	}))

	# Engine object used for calling engine methods
	if not parent_class_is_extension:
		regions.append("protected %s _object;" % parent_class)

	# Constructors
	var ctor_fmt
	if parent_class_is_extension:
		ctor_fmt = """
			public {cls_name}() : base(ClassName)
			{
			}
			protected {cls_name}(StringName @class) : base(@class)
			{
			}
			protected {cls_name}(Variant variant) : base(variant)
			{
			}
			protected {cls_name}([NotNull] {engine_class} @object) : base(@object)
			{
			}
		"""
	else:
		ctor_fmt = """
			public {cls_name}() : this(ClassName)
			{
			}
			protected {cls_name}(StringName @class) : this(ClassDB.Instantiate(@class))
			{
			}
			protected {cls_name}(Variant variant) : this(({engine_class}) variant)
			{
			}
			protected {cls_name}([NotNull] {engine_class} @object)
			{
				_object = @object;
			}
		"""
	var ctor = ctor_fmt.dedent().format({
		cls_name = cls_name,
		engine_class = engine_class,
	}).strip_edges()
	regions.append(ctor)

	var casts = """
		public static implicit operator {engine_class}({cls_name} self) => self?._object;
		public static implicit operator Variant({cls_name} self) => self?._object;
		public static explicit operator {cls_name}(Variant variant) => variant.AsGodotObject() != null ? new(variant) : null;
	""".dedent().format({
		cls_name = cls_name,
		engine_class = engine_class,
	}).strip_edges()
	regions.append(casts)

	# ENUMS
	var enums = PackedStringArray()
	for enum_name in ClassDB.class_get_enum_list(cls_name, true):
		enums.append(_generate_enum(cls_name, enum_name))
	if not enums.is_empty():
		regions.append("#region Enums")
		regions.append("\n\n".join(enums))
		regions.append("#endregion")

	# PROPERTIES
	var properties = PackedStringArray()
	for property in ClassDB.class_get_property_list(cls_name, true):
		if property["usage"] & (PROPERTY_USAGE_GROUP | PROPERTY_USAGE_SUBGROUP):
			continue
		properties.append(_generate_property(cls_name, property))
	if not properties.is_empty():
		regions.append("#region Properties")
		regions.append("\n\n".join(properties))
		regions.append("#endregion")

	var inherited_properties = PackedStringArray()
	if not parent_class_is_extension:
		for inherited_class in _get_parent_classes(cls_name):
			for property in ClassDB.class_get_property_list(inherited_class, true):
				if property["usage"] & (PROPERTY_USAGE_GROUP | PROPERTY_USAGE_SUBGROUP):
					continue
				inherited_properties.append(_generate_property(inherited_class, property))
	if not inherited_properties.is_empty():
		regions.append("#region Inherited Properties")
		regions.append("\n\n".join(inherited_properties))
		regions.append("#endregion")

	# METHODS
	var methods = PackedStringArray()
	for method in ClassDB.class_get_method_list(cls_name, true):
		if method["flags"] & (METHOD_FLAG_VIRTUAL | METHOD_FLAG_VIRTUAL_REQUIRED):
			continue
		methods.append(_generate_method(cls_name, method))
	if not methods.is_empty():
		regions.append("#region Methods")
		regions.append("\n\n".join(methods))
		regions.append("#endregion")

	var inherited_methods = PackedStringArray()
	if not parent_class_is_extension:
		for inherited_class in _get_parent_classes(cls_name):
			for method in ClassDB.class_get_method_list(inherited_class, true):
				if method["flags"] & (METHOD_FLAG_VIRTUAL | METHOD_FLAG_VIRTUAL_REQUIRED):
					continue
				inherited_methods.append(_generate_method(inherited_class, method))
	if not inherited_methods.is_empty():
		regions.append("#region Inherited Methods")
		regions.append("\n\n".join(inherited_methods))
		regions.append("#endregion")

	# SIGNALS
	var signals = PackedStringArray()
	for sig in ClassDB.class_get_signal_list(cls_name, true):
		signals.append(_generate_signal(cls_name, sig))
	if not signals.is_empty():
		regions.append("#region Signals")
		regions.append("\n\n".join(signals))
		regions.append("#endregion")

	var inherited_signals = PackedStringArray()
	if not parent_class_is_extension:
		for inherited_class in _get_parent_classes(cls_name):
			for method in ClassDB.class_get_signal_list(inherited_class, true):
				inherited_signals.append(_generate_signal(inherited_class, method))
	if not inherited_signals.is_empty():
		regions.append("#region Inherited Signals")
		regions.append("\n\n".join(inherited_signals))
		regions.append("#endregion")

	var code = """
		// This code was automatically generated by GDExtension C# Bindgen
		using System;
		using System.Diagnostics.CodeAnalysis;
		using Godot;

		namespace {GENERATED_NAMESPACE};

		public class {cls_name}{inheritance}
		{
		{regions}
		}
	""".dedent().format({
		GENERATED_NAMESPACE = GENERATED_NAMESPACE,
		cls_name = cls_name,
		inheritance = " : " + parent_class if parent_class_is_extension else "",
		regions = "\n\n".join(regions).indent("\t"),
	}).strip_edges()

	if class_is_editor_only:
		code = """
		#if TOOLS
		{code}
		#endif
		""".dedent().format({
			code = code,
		}).strip_edges()

	code += "\n"

	if not DirAccess.dir_exists_absolute(GENERATED_SCRIPTS_FOLDER):
		DirAccess.make_dir_recursive_absolute(GENERATED_SCRIPTS_FOLDER)

	var new_script = FileAccess.open(GENERATED_SCRIPTS_FOLDER.path_join(cls_name + ".cs"), FileAccess.WRITE)
	new_script.store_string(code)


func _run():
	var classes = ClassDB.get_class_list()
	for cls_name in classes:
		if _is_extension_class(cls_name):
			generate_csharp_script(cls_name)


static func _generate_enum(cls_name: StringName, enum_name: StringName) -> String:
	var constants = PackedStringArray()
	for constant_name in ClassDB.class_get_enum_constants(cls_name, enum_name, true):
		var constant_value = ClassDB.class_get_integer_constant(cls_name, constant_name)
		constants.append(constant_name.to_pascal_case() + " = " + str(constant_value) + ",")

	return """
		{flags}
		public enum {enum_name}Enum
		{
		{constants}
		}
	""".dedent().format({
		flags = "[Flags]" if ClassDB.is_class_enum_bitfield(cls_name, enum_name) else "",
		enum_name = enum_name,
		constants = "\n".join(constants).indent("\t"),
	}).strip_edges()


static func _generate_property(cls_name: StringName, property: Dictionary) -> String:
	var property_name = property["name"]
	var csharp_property_name = property_name.to_pascal_case()
	var property_type = _get_property_type(property)

	var getset = PackedStringArray()

	var getter = ClassDB.class_get_property_getter(cls_name, property_name)
	if getter:
		if _is_extension_class(cls_name):
			getset.append("get => {get_cast}_object.Get(\"{property_name}\");".format({
				get_cast = _property_get_cast(property),
				property_name = property_name,
			}))
		else:
			getset.append("get => _object.{csharp_property_name};".format({
				csharp_property_name = csharp_property_name,
			}))

	var setter = ClassDB.class_get_property_setter(cls_name, property_name)
	if setter:
		if _is_extension_class(cls_name):
			getset.append("set => _object.Set(\"{property_name}\", {set_cast}value);".format({
				set_cast = _property_set_cast(property),
				property_name = property_name,
			}))
		else:
			getset.append("set => _object.{csharp_property_name} = value;".format({
				csharp_property_name = csharp_property_name,
			}))

	return """
		public {property_type} {csharp_property_name}
		{
		{getset}
		}
	""".dedent().format({
		property_type = property_type,
		csharp_property_name = csharp_property_name,
		getset = "\n".join(getset).indent("\t"),
	}).strip_edges()


static func _generate_method(cls_name: StringName, method: Dictionary) -> String:
	var is_static = method["flags"] & METHOD_FLAG_STATIC
	var return_type = _get_method_return_type(method["name"], method["return"])
	var method_name = method["name"]
	var csharp_method_name = method_name.to_pascal_case()

	var arg_types = PackedStringArray()
	var arg_names = PackedStringArray()

	var args = PackedStringArray()
	for argument in method["args"]:
		var arg_type = _get_property_type(argument)
		var arg_name = "@" + argument["name"]
		# hardcode type that cannot be known from reflection in GDScript
		if method["name"] == "connect" and arg_name == "@flags":
			arg_type = "uint"
		args.append("{arg_type} {arg_name}".format({
			arg_type = arg_type,
			arg_name = arg_name,
		}))
		arg_types.append(arg_type)
		if _property_is_enum(argument):
			arg_names.append("(int)" + arg_name)
		else:
			arg_names.append(arg_name)

	var implementation = PackedStringArray()
	var default_args = method["default_args"]
	var i = args.size() - default_args.size()
	for default_value in default_args:
		if default_value == null:
			default_value = "default"
		elif default_value is int and arg_types[i] != "int":
			default_value = ("(%s)" % arg_types[i]) + str(default_value)
		elif default_value is String:
			default_value = '"%s"' % default_value
		elif default_value is StringName:
			implementation.append('%s ??= "%s";' % [arg_names[i], default_value])
			default_value = "null"
		elif default_value is Array:
			assert(default_value.is_empty(), "Populated Array not supported yet! " + str(default_value)) # TODO: support populated array as default value
			implementation.append("%s ??= new();" % arg_names[i])
			default_value = "null"
		elif default_value is Dictionary:
			assert(default_value.is_empty(), "Populated Dictionary not supported yet! " + str(default_value)) # TODO: support populated dictionary as default value
			implementation.append("%s ??= new();" % arg_names[i])
			default_value = "null"
		args[i] += " = " + str(default_value)
		i += 1

	if method["flags"] & METHOD_FLAG_VARARG:
		args.append("params Variant[] varargs")
		arg_names.append("varargs")

	if _is_extension_class(cls_name):
		arg_names.insert(0, "\"%s\"" % method_name)
		if is_static:
			implementation.append("{maybe_return}ClassDB.ClassCallStatic(ClassName, {arg_names});".format({
				arg_names = ", ".join(arg_names),
				maybe_return = "return " + _property_get_cast(method["return"]) if return_type != "void" else "",
			}))
		else:
			implementation.append("{maybe_return}_object.Call({arg_names});".format({
				arg_names = ", ".join(arg_names),
				maybe_return = "return " + _property_get_cast(method["return"]) if return_type != "void" else "",
			}))
	else:
		if is_static:
			implementation.append("{maybe_return}{engine_class}.{method_name}({arg_names});".format({
				arg_names = ", ".join(arg_names),
				engine_class = _first_non_extension_parent(cls_name),
				method_name = csharp_method_name,
				maybe_return = "return " if return_type != "void" else "",
			}))
		else:
			implementation.append("{maybe_return}_object.{method_name}({arg_names});".format({
				arg_names = ", ".join(arg_names),
				method_name = csharp_method_name,
				maybe_return = "return " if return_type != "void" else "",
			}))

	return """
		public {maybe_static}{maybe_override}{return_type} {cs_method_name}({args})
		{
		{implementation}
		}
	""".dedent().format({
		args = ", ".join(args),
		cs_method_name = csharp_method_name,
		implementation = "\n".join(implementation).indent("\t"),
		maybe_override = "override " if csharp_method_name == "ToString" else "",
		maybe_static = "static " if is_static else "",
		return_type = return_type,
	}).strip_edges()


static func _generate_signal(cls_name: StringName, sig: Dictionary):
	var signal_name = sig["name"]
	var csharp_signal_name = signal_name.to_pascal_case()
	var return_type = _get_method_return_type(signal_name, sig["return"])

	var arg_types = PackedStringArray()
	for argument in sig["args"]:
		var arg_type = _get_property_type(argument)
		arg_types.append(arg_type)

	var delegate_type
	if return_type == "void":
		if not arg_types.is_empty():
			delegate_type = "Action<{arg_types}>".format({
				arg_types = ", ".join(arg_types)
			})
		else:
			delegate_type = "Action"
	else:
		arg_types.append(return_type)
		delegate_type = "Func<{arg_types}>".format({
			arg_types = ", ".join(arg_types)
		})

	return """
		public event {delegate_type} {cs_signal_name}
		{
			add
			{
				Connect(\"{signal_name}\", Callable.From(value));
			}
			remove
			{
				Disconnect(\"{signal_name}\", Callable.From(value));
			}
		}
	""".dedent().format({
		delegate_type = delegate_type,
		cs_signal_name = csharp_signal_name,
		signal_name = signal_name,
	}).strip_edges()


static func _property_is_enum(property: Dictionary) -> bool:
	return property["usage"] & (PROPERTY_USAGE_CLASS_IS_ENUM | PROPERTY_USAGE_CLASS_IS_BITFIELD)


static func _get_property_type(property: Dictionary) -> String:
	match property["type"]:
		TYPE_NIL:
			return "Variant"
		TYPE_BOOL:
			return "bool"
		TYPE_INT:
			if _property_is_enum(property):
				var enum_name = property["class_name"]
				if enum_name == "Error":
					return "Godot.Error"
				return enum_name + "Enum"
			return "int"
		TYPE_STRING:
			return "string"
		TYPE_VECTOR2I:
			return "Godot.Vector2I"
		TYPE_RECT2I:
			return "Godot.Rect2I"
		TYPE_VECTOR3I:
			return "Godot.Vector3I"
		TYPE_VECTOR4I:
			return "Godot.Vector4I"
		TYPE_AABB:
			return "Godot.Aabb"
		TYPE_RID:
			return "Godot.Rid"
		TYPE_OBJECT:
			if property["class_name"] and property["class_name"] != "Object":
				return property["class_name"]
			else:
				return "GodotObject"
		TYPE_ARRAY:
			if property["hint"] & PROPERTY_HINT_ARRAY_TYPE:
				return "Godot.Collections.Array<%s>" % _get_mapped_variant_type(property["hint_string"])
			else:
				return "Godot.Collections.Array"
		TYPE_DICTIONARY:
			return "Godot.Collections.Dictionary"
		TYPE_PACKED_BYTE_ARRAY:
			return "byte[]"
		TYPE_PACKED_INT32_ARRAY:
			return "int[]"
		TYPE_PACKED_INT64_ARRAY:
			return "long[]"
		TYPE_PACKED_FLOAT32_ARRAY:
			return "float[]"
		TYPE_PACKED_FLOAT64_ARRAY:
			return "double[]"
		TYPE_PACKED_STRING_ARRAY:
			return "string[]"
		TYPE_PACKED_VECTOR2_ARRAY:
			return "Godot.Vector2[]"
		TYPE_PACKED_VECTOR3_ARRAY:
			return "Godot.Vector3[]"
		TYPE_PACKED_VECTOR4_ARRAY:
			return "Godot.Vector4[]"
		TYPE_PACKED_COLOR_ARRAY:
			return "Godot.Color[]"
		var t:
			return "Godot." + type_string(t)


static func _get_mapped_variant_type(variant_type_name: String) -> String:
	var _type_map = {
		"Variant": "Variant",
		"bool": "bool",
		"int": "long",
		"float": "double" if OS.has_feature("double") else "float",
		"String": "string",
		"StringName": "StringName",
		"Dictionary": "Godot.Collections.Dictionary",
	}
	return _type_map[variant_type_name]


static func _property_get_cast(property: Dictionary):
	var property_type = _get_property_type(property)
	if _property_is_enum(property):
		return "(%s)(int)" % property_type
	else:
		return "(%s)" % property_type


static func _property_set_cast(property: Dictionary):
	if _property_is_enum(property):
		return "(int)"
	else:
		return ""


static func _is_extension_class(cls_name: StringName) -> bool:
	return ClassDB.class_get_api_type(cls_name) in [
		ClassDB.APIType.API_EXTENSION,
		ClassDB.APIType.API_EDITOR_EXTENSION,
	]


static func _is_editor_extension_class(cls_name: StringName) -> bool:
	return ClassDB.class_get_api_type(cls_name) == ClassDB.APIType.API_EDITOR_EXTENSION


static func _first_non_extension_parent(cls_name: StringName) -> StringName:
	while _is_extension_class(cls_name):
		cls_name = ClassDB.get_parent_class(cls_name)
	return cls_name


static func _get_method_return_type(method_name: StringName, method_return: Dictionary) -> String:
	# hardcode type that cannot be known from reflection in GDScript
	if method_name == "get_instance_id":
		return "ulong"

	if method_return["type"] == TYPE_NIL:
		if method_return["usage"] & PROPERTY_USAGE_NIL_IS_VARIANT:
			return "Variant"
		else:
			return "void"
	else:
		return _get_property_type(method_return)


static func _get_parent_classes(cls_name: StringName) -> Array[StringName]:
	var parent_classes = [] as Array[StringName]
	while true:
		cls_name = ClassDB.get_parent_class(cls_name)
		parent_classes.append(cls_name)
		if cls_name == "Object":
			break
	return parent_classes
