# UnityEditor integration for LeoEcsLite C# Entity Component System framework
Unity editor integration for [LeoECS Lite](https://github.com/Leopotam/ecslite).

> Tested on unity 2020.3 (dependent on it) and contains assembly definition for compiling to separate assembly file for performance reason.

> **Important!** Don't forget that this module developed to work only inside unity editor and under `UNITY_EDITOR` definition.

# Table of content
* [Socials](#socials)
* [Installation](#installation)
    * [As unity module](#as-unity-module)
    * [As source](#as-source)
* [Integration](#integration)
    * [From code](#from-code)
    * [From UI](#from-ui)
* [License](#license)
* [FAQ](#faq)

# Socials
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.ecslite.unityeditor": "https://github.com/Leopotam/ecslite-unityeditor.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.ecslite.unityeditor": "https://github.com/Leopotam/ecslite-unityeditor.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be cloned or downloaded as archive from `releases` page.

# Integration

## From code
```csharp
// ecs-startup code:
void Start () {        
    _systems = new EcsSystems (new EcsWorld ());
    _systems
        .Add (new TestSystem1 ())
#if UNITY_EDITOR
        // add debug systems for custom worlds here, for example:
        // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
        .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
        .Init ();
}

void Update () {
    // you should call Run() for EcsWorldDebugSystem container EcsSystems,
    // otherwise names will not be updated with wastes of internal allocations.
    _systems.Run();
}
```

> **Important!** By default component names will be baked to GameObject name on each change in delayed manner - its why you should call `EcsSystems.Run()` for systems group that contains `EcsWorldDebugSystem`.
> If you don't need this info (for performance reason for example), you can disable it with passing `bakeComponentsInName = false` parameter in ctor of `EcsWorldDebugSystem`.


## From UI
Some code can be generated automatically from unity editor main menu "Assets / Create / LeoECS Lite".

# License
The software is released under the terms of the [MIT license](./LICENSE.md).

No personal support or any guarantees.

# FAQ

### I want to use `IEcsPreInitSystem`-system with creating entities inside, but editor integration throws exception. What I'm doing wrong?

Example:
```csharp
class IssueRepro : MonoBehaviour {
    struct Comp { }

    class Sys : IEcsPreInitSystem, IEcsInitSystem {
        public void PreInit (EcsSystems systems) {
            var world = systems.GetWorld ();
            var e = world.NewEntity ();
            world.GetPool<Comp> ().Add (e);
        }

        public void Init (EcsSystems systems) {
            var world = systems.GetWorld ();
            var pool = world.GetPool<Comp> ();

            foreach (var e in world.Filter<Comp> ().End ()) {
                pool.Del (e);
            }
        }
    }
    
    EcsSystems _systems;

    void Awake () {
        _systems = new EcsSystems (new EcsWorld ());
        _systems
            .Add (new Sys ())
#if UNITY_EDITOR
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
#endif
            .Init ();
    }
    
    void Update () {
        _systems?.Run (); // throws NullReferenceException() here.
    }
    
    void OnDestroy () {
        if (_systems != null) {
            _systems.Destroy ();
            _systems.GetWorld ().Destroy ();
            _systems = null;
        }
    }
}
```
Main problem in this situation - editor update system (`EUS`) uses same integration way - as `IEcsPreInitSystem` and dont know anything about world changes before it.
If you really want to create data in `PreInit()` method (that not recommended), you can extract `EUS` to separate `EcsSystems` group:
```csharp
    EcsSystems _systems;
#if UNITY_EDITOR
    EcsSystems _editorSystems;
#endif
    void Awake () {
        _systems = new EcsSystems (new EcsWorld ());
#if UNITY_EDITOR
        // create separate EcsSystems group for editor systems only.
        _editorSystems = new EcsSystems (_systems.GetWorld ());
        _editorSystems
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
            .Init ();
#endif
        _systems
            .Add (new Sys ())
            .Init ();
    }
    
    void Update () {
        _systems?.Run ();
#if UNITY_EDITOR
        // process editor systems group after standard logic. 
        _editorSystems?.Run ();
#endif
    }
    
    void OnDestroy () {
#if UNITY_EDITOR
        // cleanup editor systems group.
        if (_editorSystems != null) {
            _editorSystems.Destroy ();
            _editorSystems = null;
        }
#endif
        if (_systems != null) {
            _systems.Destroy ();
            _systems.GetWorld ().Destroy ();
            _systems = null;
        }
    }
```

### I can't edit my custom type, but can builtin simple types. How can I do it?

With custom inspector drawer:
```csharp
public struct C2 {
    public string Name;
}

// somewhere in project - it will be found automatically.
#if UNITY_EDITOR
sealed class C2Inspector : EcsComponentInspectorTyped<C2> {
    public override bool OnGuiTyped (string label, ref C2 value, EcsEntityDebugView entityView) {
        EditorGUILayout.LabelField ($"Super C2 component", EditorStyles.boldLabel);
        var newValue = EditorGUILayout.TextField ("Name", value.Name);
        EditorGUILayout.HelpBox ($"Hello, {value.Name}", MessageType.Info);
        // If value not changed - just return false.
        if (newValue == value.Name) { return false; }
        // Otherwise - overwrite value and return true.
        value.Name = newValue;
        return true;
    }
}
#endif
```
It works for components and component fields.