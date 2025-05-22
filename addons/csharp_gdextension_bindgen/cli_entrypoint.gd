extends SceneTree


const CSharpGDExtensionBindgen = preload("csharp_gdextension_bindgen.gd")


func _initialize():
	var args = OS.get_cmdline_user_args()
	var output_dir = CSharpGDExtensionBindgen.GENERATED_SCRIPTS_FOLDER if args.is_empty() else args[0]
	CSharpGDExtensionBindgen.generate_gdextension_csharp_scripts(output_dir)
	quit()
