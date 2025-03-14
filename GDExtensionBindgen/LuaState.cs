// This code was automatically generated by GDExtension C# Bindgen
using System;
using System.Diagnostics.CodeAnalysis;
using Godot;

namespace GDExtensionBindgen;

public class LuaState
{
	public static readonly StringName ClassName = "LuaState";

	protected RefCounted _object;

	public LuaState() : this(ClassName)
	{
	}
	protected LuaState(StringName @class) : this(Godot.ClassDB.Instantiate(@class))
	{
	}
	protected LuaState(Variant variant) : this((RefCounted) variant)
	{
	}
	protected LuaState([NotNull] RefCounted @object)
	{
		_object = @object;
	}

	public static implicit operator RefCounted(LuaState self) => self?._object;
	public static implicit operator Variant(LuaState self) => self?._object;
	public static explicit operator LuaState(Variant variant) => new(variant);

	#region Enums

	[Flags]
	public enum LibraryEnum
	{
		LuaBase = 1,
		LuaPackage = 2,
		LuaCoroutine = 4,
		LuaString = 8,
		LuaOs = 16,
		LuaMath = 32,
		LuaTable = 64,
		LuaDebug = 128,
		LuaBit32 = 256,
		LuaIo = 512,
		LuaFfi = 1024,
		LuaJit = 2048,
		LuaUtf8 = 4096,
		LuaAllLibs = 8191,
		GodotVariant = 8192,
		GodotUtilityFunctions = 16384,
		GodotSingletons = 32768,
		GodotClasses = 65536,
		GodotEnums = 131072,
		GodotAllLibs = 253952,
	}

	#endregion

	#region Properties

	public LuaTable Globals
	{
		get => (LuaTable)_object.Get("globals");
	}

	public LuaTable Registry
	{
		get => (LuaTable)_object.Get("registry");
	}

	#endregion

	#region Methods

	public void OpenLibraries(LuaState.LibraryEnum @libraries = (LuaState.LibraryEnum)262143)
	{
		_object.Call("open_libraries", (int)@libraries);
	}

	public LuaTable CreateTable(Godot.Collections.Dictionary @initial_values = null)
	{
		@initial_values ??= new();
		return (LuaTable)_object.Call("create_table", @initial_values);
	}

	public Godot.Variant LoadString(string @chunk, string @chunkname = "", LuaTable @env = default)
	{
		return (Godot.Variant)_object.Call("load_string", @chunk, @chunkname, @env);
	}

	public Godot.Variant LoadFile(string @filename, int @buffer_size = 1024, LuaTable @env = default)
	{
		return (Godot.Variant)_object.Call("load_file", @filename, @buffer_size, @env);
	}

	public Godot.Variant DoString(string @chunk, string @chunkname = "", LuaTable @env = default)
	{
		return (Godot.Variant)_object.Call("do_string", @chunk, @chunkname, @env);
	}

	public Godot.Variant DoFile(string @filename, int @buffer_size = 1024, LuaTable @env = default)
	{
		return (Godot.Variant)_object.Call("do_file", @filename, @buffer_size, @env);
	}

	public LuaTable GetGlobals()
	{
		return (LuaTable)_object.Call("get_globals");
	}

	public LuaTable GetRegistry()
	{
		return (LuaTable)_object.Call("get_registry");
	}

	#endregion

	#region Inherited Methods

	public bool InitRef()
	{
		return _object.InitRef();
	}

	public bool Reference()
	{
		return _object.Reference();
	}

	public bool Unreference()
	{
		return _object.Unreference();
	}

	public int GetReferenceCount()
	{
		return _object.GetReferenceCount();
	}

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

	public void Set(Godot.StringName @property, Godot.Variant @value)
	{
		_object.Set(@property, @value);
	}

	public Godot.Variant Get(Godot.StringName @property)
	{
		return _object.Get(@property);
	}

	public void SetIndexed(Godot.NodePath @property_path, Godot.Variant @value)
	{
		_object.SetIndexed(@property_path, @value);
	}

	public Godot.Variant GetIndexed(Godot.NodePath @property_path)
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

	public Godot.Variant PropertyGetRevert(Godot.StringName @property)
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

	public void SetScript(Godot.Variant @script)
	{
		_object.SetScript(@script);
	}

	public Godot.Variant GetScript()
	{
		return _object.GetScript();
	}

	public void SetMeta(Godot.StringName @name, Godot.Variant @value)
	{
		_object.SetMeta(@name, @value);
	}

	public void RemoveMeta(Godot.StringName @name)
	{
		_object.RemoveMeta(@name);
	}

	public Godot.Variant GetMeta(Godot.StringName @name, Godot.Variant @default = default)
	{
		return _object.GetMeta(@name, @default);
	}

	public bool HasMeta(Godot.StringName @name)
	{
		return _object.HasMeta(@name);
	}

	public Godot.Collections.Array<Godot.StringName> GetMetaList()
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

	public Godot.Error EmitSignal(Godot.StringName @signal, params Godot.Variant[] varargs)
	{
		return _object.EmitSignal(@signal, varargs);
	}

	public Godot.Variant Call(Godot.StringName @method, params Godot.Variant[] varargs)
	{
		return _object.Call(@method, varargs);
	}

	public Godot.Variant CallDeferred(Godot.StringName @method, params Godot.Variant[] varargs)
	{
		return _object.CallDeferred(@method, varargs);
	}

	public void SetDeferred(Godot.StringName @property, Godot.Variant @value)
	{
		_object.SetDeferred(@property, @value);
	}

	public Godot.Variant Callv(Godot.StringName @method, Godot.Collections.Array @arg_array)
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

	#region Signals

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
