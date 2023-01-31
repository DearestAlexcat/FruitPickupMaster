# Extended systems for LeoEcsLite C# Entity Component System framework
Extended systems for [LeoECS Lite](https://github.com/Leopotam/ecslite).

> Tested on unity 2020.3 (not dependent on it) and contains assembly definition for compiling to separate assembly file for performance reason.

> **Important!** Don't forget to use `DEBUG` builds for development and `RELEASE` builds in production: all internal error checks / exception throwing works only in `DEBUG` builds and eleminated for performance reasons in `RELEASE`.

# Table of content
* [Socials](#socials)
* [Installation](#installation)
    * [As unity module](#as-unity-module)
    * [As source](#as-source)
* [Examples](#examples)
    * [Group systems](#group-systems)
    * [Autoremove components at point](#autoremove-components-at-point)
* [License](#license)

# Socials
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.ecslite.extendedsystems": "https://github.com/Leopotam/ecslite-extendedsystems.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.ecslite.extendedsystems": "https://github.com/Leopotam/ecslite-extendedsystems.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive from `Releases` page.

# Examples

## Group systems
`EcsGroupSystem` allows systems to be stored in enable/disable blocks: 
```csharp
// Nested to "Melee" group systems.
class MeleeSystem1 : IEcsRunSystem {
    public void Run (EcsSystems systems) { }
}
class MeleeSystem2 : IEcsRunSystem {
    public void Run (EcsSystems systems) { }
}

class MeleeGroupEnableSystem : IEcsRunSystem {
    public void Run (EcsSystems systems) {
        // We can enable "Melee" group with special event.
        var world = systems.GetWorld ();
        var entity = world.NewEntity ();
        ref var evt = ref world.GetPool<EcsGroupSystemState> ().Add (entity);
        evt.Name = "Melee";
        evt.State = true;
    }
}
...
// startup code.
var systems = new EcsSystems (new EcsWorld ());
systems
    // Adds disabled group "Melee" with 2 nested systems,
    // group-events will be stored in default world.
    .AddGroup ("Melee", false, null,
        new MeleeSystem1 (),
        new MeleeSystem2 ())
    // Other systems.
    .Add (new MeleeGroupEnableSystem ())
    .Init ();
```

## Autoremove components at point
`DelHere()` helper can be used to autoremove components at required point in execution sequence:
```csharp
var systems = new EcsSystems (new EcsWorld ());
systems
    .Add (new System1 ())
    .Add (new System2 ())
    // All C1 components will be removed here.
    .DelHere<C1> ()
    .Add (new System3 ())
    // All C2 components will be removed here.
    .DelHere<C2> ()
    .Add (new System4 ())
    .Init ();
```
> **Important!** if `DelHere()` removes components from custom world - `AddWorld()` call for this world should be placed before `DelHere()`.

# License
The software is released under the terms of the [MIT license](./LICENSE.md).

No personal support or any guarantees.
