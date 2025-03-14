using Godot;
using System;
using GDExtensionBindgen;

public partial class GDExtensionTest : Node
{
	private readonly LuaState lua = new();

	public override void _Ready()
	{
		lua.OpenLibraries();
		Variant result = lua.DoString("return 'Hello from Lua!'");
		GD.Print(result);
	}
}
