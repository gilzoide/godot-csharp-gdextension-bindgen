// This code was automatically generated by GDExtension C# Bindgen
using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace GDExtensionBindgen;

public class LuaCoroutine : LuaObject
{
	public new static readonly StringName ClassName = "LuaCoroutine";

	public LuaCoroutine() : base(ClassName)
	{
	}
	protected LuaCoroutine(StringName @class) : base(@class)
	{
	}
	protected LuaCoroutine(Variant variant) : base(variant)
	{
	}
	protected LuaCoroutine([NotNull] RefCounted @object) : base(@object)
	{
	}

	public static implicit operator RefCounted(LuaCoroutine self) => self?._object;
	public static implicit operator Variant(LuaCoroutine self) => self?._object;
	public static explicit operator LuaCoroutine(Variant variant) => new(variant);

	#region Enums

	public enum LuaCoroutineStatusEnum
	{
		StatusOk = 0,
		StatusYield = 1,
		StatusErrrun = 2,
		StatusErrsyntax = 3,
		StatusErrmem = 4,
		StatusErrerr = 5,
		StatusDead = -1,
	}

	#endregion

	#region Properties

	public int Status
	{
		get => (int)_object.Get("status");
	}

	#endregion

	#region Methods

	public LuaCoroutine.LuaCoroutineStatusEnum GetStatus()
	{
		return (LuaCoroutine.LuaCoroutineStatusEnum)(int)_object.Call("get_status");
	}

	public Godot.Variant Resumev(Godot.Collections.Array @arguments)
	{
		return (Godot.Variant)_object.Call("resumev", @arguments);
	}

	public Godot.Variant Resume(params Godot.Variant[] varargs)
	{
		return (Godot.Variant)_object.Call("resume", varargs);
	}

	public static LuaCoroutine Create(LuaFunction @function)
	{
		return (LuaCoroutine)Godot.ClassDB.ClassCallStatic(ClassName, "create", @function);
	}

	#endregion
}
