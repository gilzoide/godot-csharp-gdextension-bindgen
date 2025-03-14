using Godot;
using System;
using GDExtensionBindgen;

public partial class GDExtensionTest : Node
{
	private LuaState lua = new LuaState();

	public override void _Ready()
	{
		lua.OpenLibraries();
		Variant result = lua.DoString("return 'Hello from Lua!'");
		GD.Print(result);
	}
}
