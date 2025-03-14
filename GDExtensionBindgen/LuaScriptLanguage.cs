// This code was automatically generated by GDExtension C# Bindgen
using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace GDExtensionBindgen;

public class LuaScriptLanguage
{
	public static readonly StringName ClassName = "LuaScriptLanguage";

	protected ScriptLanguageExtension _object;

	public LuaScriptLanguage() : this(ClassName)
	{
	}
	protected LuaScriptLanguage(StringName @class) : this(ClassDB.Instantiate(@class))
	{
	}
	protected LuaScriptLanguage(Variant variant) : this((ScriptLanguageExtension) variant)
	{
	}
	protected LuaScriptLanguage([NotNull] ScriptLanguageExtension @object)
	{
		_object = @object;
	}

	public static implicit operator ScriptLanguageExtension(LuaScriptLanguage self) => self?._object;
	public static implicit operator Variant(LuaScriptLanguage self) => self?._object;
	public static explicit operator LuaScriptLanguage(Variant variant) => variant.AsGodotObject() != null ? new(variant) : null;

	#region Inherited Methods

	public void Free()
	{
		_object.Free();
	}

	public string GetClass()
	{
		return _object.GetClass();
	}

	public bool IsClass(string @class)
	{
		return _object.IsClass(@class);
	}

	public void Set(Godot.StringName @property, Variant @value)
	{
		_object.Set(@property, @value);
	}

	public Variant Get(Godot.StringName @property)
	{
		return _object.Get(@property);
	}

	public void SetIndexed(Godot.NodePath @property_path, Variant @value)
	{
		_object.SetIndexed(@property_path, @value);
	}

	public Variant GetIndexed(Godot.NodePath @property_path)
	{
		return _object.GetIndexed(@property_path);
	}

	public Godot.Collections.Array<Godot.Collections.Dictionary> GetPropertyList()
	{
		return _object.GetPropertyList();
	}

	public Godot.Collections.Array<Godot.Collections.Dictionary> GetMethodList()
	{
		return _object.GetMethodList();
	}

	public bool PropertyCanRevert(Godot.StringName @property)
	{
		return _object.PropertyCanRevert(@property);
	}

	public Variant PropertyGetRevert(Godot.StringName @property)
	{
		return _object.PropertyGetRevert(@property);
	}

	public void Notification(int @what, bool @reversed = false)
	{
		_object.Notification(@what, @reversed);
	}

	public override string ToString()
	{
		return _object.ToString();
	}

	public ulong GetInstanceId()
	{
		return _object.GetInstanceId();
	}

	public void SetScript(Variant @script)
	{
		_object.SetScript(@script);
	}

	public Variant GetScript()
	{
		return _object.GetScript();
	}

	public void SetMeta(Godot.StringName @name, Variant @value)
	{
		_object.SetMeta(@name, @value);
	}

	public void RemoveMeta(Godot.StringName @name)
	{
		_object.RemoveMeta(@name);
	}

	public Variant GetMeta(Godot.StringName @name, Variant @default = default)
	{
		return _object.GetMeta(@name, @default);
	}

	public bool HasMeta(Godot.StringName @name)
	{
		return _object.HasMeta(@name);
	}

	public Godot.Collections.Array<StringName> GetMetaList()
	{
		return _object.GetMetaList();
	}

	public void AddUserSignal(string @signal, Godot.Collections.Array @arguments = null)
	{
		@arguments ??= new();
		_object.AddUserSignal(@signal, @arguments);
	}

	public bool HasUserSignal(Godot.StringName @signal)
	{
		return _object.HasUserSignal(@signal);
	}

	public void RemoveUserSignal(Godot.StringName @signal)
	{
		_object.RemoveUserSignal(@signal);
	}

	public Godot.Error EmitSignal(Godot.StringName @signal, params Variant[] varargs)
	{
		return _object.EmitSignal(@signal, varargs);
	}

	public Variant Call(Godot.StringName @method, params Variant[] varargs)
	{
		return _object.Call(@method, varargs);
	}

	public Variant CallDeferred(Godot.StringName @method, params Variant[] varargs)
	{
		return _object.CallDeferred(@method, varargs);
	}

	public void SetDeferred(Godot.StringName @property, Variant @value)
	{
		_object.SetDeferred(@property, @value);
	}

	public Variant Callv(Godot.StringName @method, Godot.Collections.Array @arg_array)
	{
		return _object.Callv(@method, @arg_array);
	}

	public bool HasMethod(Godot.StringName @method)
	{
		return _object.HasMethod(@method);
	}

	public int GetMethodArgumentCount(Godot.StringName @method)
	{
		return _object.GetMethodArgumentCount(@method);
	}

	public bool HasSignal(Godot.StringName @signal)
	{
		return _object.HasSignal(@signal);
	}

	public Godot.Collections.Array<Godot.Collections.Dictionary> GetSignalList()
	{
		return _object.GetSignalList();
	}

	public Godot.Collections.Array<Godot.Collections.Dictionary> GetSignalConnectionList(Godot.StringName @signal)
	{
		return _object.GetSignalConnectionList(@signal);
	}

	public Godot.Collections.Array<Godot.Collections.Dictionary> GetIncomingConnections()
	{
		return _object.GetIncomingConnections();
	}

	public Godot.Error Connect(Godot.StringName @signal, Godot.Callable @callable, uint @flags = (uint)0)
	{
		return _object.Connect(@signal, @callable, @flags);
	}

	public void Disconnect(Godot.StringName @signal, Godot.Callable @callable)
	{
		_object.Disconnect(@signal, @callable);
	}

	public bool IsConnected(Godot.StringName @signal, Godot.Callable @callable)
	{
		return _object.IsConnected(@signal, @callable);
	}

	public bool HasConnections(Godot.StringName @signal)
	{
		return _object.HasConnections(@signal);
	}

	public void SetBlockSignals(bool @enable)
	{
		_object.SetBlockSignals(@enable);
	}

	public bool IsBlockingSignals()
	{
		return _object.IsBlockingSignals();
	}

	public void NotifyPropertyListChanged()
	{
		_object.NotifyPropertyListChanged();
	}

	public void SetMessageTranslation(bool @enable)
	{
		_object.SetMessageTranslation(@enable);
	}

	public bool CanTranslateMessages()
	{
		return _object.CanTranslateMessages();
	}

	public string Tr(Godot.StringName @message, Godot.StringName @context = null)
	{
		@context ??= "";
		return _object.Tr(@message, @context);
	}

	public string TrN(Godot.StringName @message, Godot.StringName @plural_message, int @n, Godot.StringName @context = null)
	{
		@context ??= "";
		return _object.TrN(@message, @plural_message, @n, @context);
	}

	public Godot.StringName GetTranslationDomain()
	{
		return _object.GetTranslationDomain();
	}

	public void SetTranslationDomain(Godot.StringName @domain)
	{
		_object.SetTranslationDomain(@domain);
	}

	public bool IsQueuedForDeletion()
	{
		return _object.IsQueuedForDeletion();
	}

	public void CancelFree()
	{
		_object.CancelFree();
	}

	#endregion

	#region Inherited Signals

	public event Action ScriptChanged
	{
		add
		{
			Connect("script_changed", Callable.From(value));
		}
		remove
		{
			Disconnect("script_changed", Callable.From(value));
		}
	}

	public event Action PropertyListChanged
	{
		add
		{
			Connect("property_list_changed", Callable.From(value));
		}
		remove
		{
			Disconnect("property_list_changed", Callable.From(value));
		}
	}

	#endregion
}
