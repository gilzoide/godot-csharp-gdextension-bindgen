// This code was automatically generated by GDExtension C# Bindgen
using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace GDExtensionBindgen;

public class LuaUserdata : LuaTable
{
	public new static readonly StringName ClassName = "LuaUserdata";

	public LuaUserdata() : base(ClassName)
	{
	}
	protected LuaUserdata(StringName @class) : base(@class)
	{
	}
	protected LuaUserdata(Variant variant) : base(variant)
	{
	}
	protected LuaUserdata([NotNull] RefCounted @object) : base(@object)
	{
	}

	public static implicit operator RefCounted(LuaUserdata self) => self?._object;
	public static implicit operator Variant(LuaUserdata self) => self?._object;
	public static explicit operator LuaUserdata(Variant variant) => new(variant);
}