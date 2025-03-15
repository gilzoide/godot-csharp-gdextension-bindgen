// This code was automatically generated by GDExtension C# Bindgen
using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace GDExtensionBindgen;

public class LuaCoroutine : LuaObject
{
	public LuaCoroutine() : base(NativeName)
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
	public static explicit operator LuaCoroutine(Variant variant) => variant.AsGodotObject() != null ? new(variant) : null;

	public new class PropertyName : LuaObject.PropertyName
	{
		public static readonly StringName Status = "status";
	}

	public new class MethodName : LuaObject.MethodName
	{
		public static readonly StringName GetStatus = "get_status";
		public static readonly StringName Resumev = "resumev";
		public static readonly StringName Resume = "resume";
		public static readonly StringName Create = "create";
	}

	public new class SignalName : LuaObject.SignalName
	{

	}

	private static readonly StringName NativeName = "LuaCoroutine";

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
		get => (int)_object.Get(PropertyName.Status);
	}

	#endregion

	#region Methods

	public LuaCoroutine.LuaCoroutineStatusEnum GetStatus()
	{
		return (LuaCoroutine.LuaCoroutineStatusEnum)(int)_object.Call(MethodName.GetStatus);
	}

	public Variant Resumev(Godot.Collections.Array @arguments)
	{
		return (Variant)_object.Call(MethodName.Resumev, @arguments);
	}

	public Variant Resume(params Variant[] varargs)
	{
		return (Variant)_object.Call(MethodName.Resume, varargs);
	}

	public static LuaCoroutine Create(LuaFunction @function)
	{
		return (LuaCoroutine)ClassDB.ClassCallStatic(NativeName, MethodName.Create, @function);
	}

	#endregion
}
