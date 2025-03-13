using Godot;
using System;
using GDExtensionBindgen;

public partial class GDExtensionTest : Node
{
	private LuaState lua = new LuaState();
	
	public override void _Ready()
	{
		lua.OpenLibraries();
		var result = lua.DoString("print('Oi'");
		GD.Print(result);
	}
}
