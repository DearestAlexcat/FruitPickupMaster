# Dependency injection for LeoEcsLite C# Entity Component System framework
Dependency injection for [LeoECS Lite](https://github.com/Leopotam/ecslite).

> Tested on unity 2020.3 (not dependent on it) and contains assembly definition for compiling to separate assembly file for performance reason.

> **Important!** Don't forget to use `DEBUG` builds for development and `RELEASE` builds in production: all internal error checks / exception throwing works only in `DEBUG` builds and eleminated for performance reasons in `RELEASE`.

# Table of content
* [Socials](#socials)
* [Installation](#installation)
    * [As unity module](#as-unity-module)
    * [As source](#as-source)
* [Integration to startup](#integration-to-startup)
* [Injectors](#injectors)
    * [EcsWorldInject](#ecsworldinject)
    * [EcsPoolInject](#ecspoolinject)
    * [EcsFilterInject](#ecsfilterinject)
    * [EcsSharedInject](#ecssharedinject)
    * [EcsCustomInject](#ecscustominject)
* [License](#license)

# Socials
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)

# Installation

## As unity module
This repository can be installed as unity module directly from git url. In this way new line should be added to `Packages/manifest.json`:
```
"com.leopotam.ecslite.di": "https://github.com/Leopotam/ecslite-di.git",
```
By default last released version will be used. If you need trunk / developing version then `develop` name of branch should be added after hash:
```
"com.leopotam.ecslite.di": "https://github.com/Leopotam/ecslite-di.git#develop",
```

## As source
If you can't / don't want to use unity modules, code can be downloaded as sources archive from `Releases` page.

# Integration to startup
```csharp
var systems = new EcsSystems (new EcsWorld ());
systems
    .Add (new System1 ())
    .AddWorld (new EcsWorld (), "events")
    // ...
    // Inject() method should be placed after
    // all systems/worlds registration and
    // before Init().
    .Inject ()
    .Init ();
```

# Injectors

## EcsWorldInject
```csharp
class TestSystem : IEcsRunSystem {
    // field will be injected with default world instance.
    readonly EcsWorldInject _defaultWorld = default;
    
    // field will be injected with "events" world instance.
    readonly EcsWorldInject _eventsWorld = "events";

    public void Run (EcsSystems systems) {
        // all injected fields can be used here.
        // _defaultWorld.Value.xxx
        // _eventsWorld.Value.xxx
    }
}
```

## EcsPoolInject
```csharp
class TestSystem : IEcsRunSystem {
    // field will be injected with pool from default world instance.
    readonly EcsPoolInject<C1> _c1Pool = default;
    
    // field will be injected with pool from "events" world instance.
    readonly EcsPoolInject<C1> _c1EventsPool = "events";

    public void Run (EcsSystems systems) {
        // all injected fields can be used here.
        // _c1Pool.Value.xxx
        // _c1EventsPool.Value.xxx
    }
}
```

## EcsFilterInject
```csharp
class TestSystem : IEcsRunSystem {
    // field will be injected with filter (C1 included)
    // from default world instance.
    readonly EcsFilterInject<Inc<C1>> _filter1 = default;
    
    // field will be injected with filter (C1,C2 included)
    // from default world instance.
    readonly EcsFilterInject<Inc<C1, C2>> _filter2 = default;
    
    // field will be injected with filter (C1 included, C2 excluded)
    // from default world instance.
    readonly EcsFilter<Inc<C1>, Exc<C2>> _filter11 = default;
    
    // field will be injected with filter (C1 included, C2 excluded)
    // from "events" world instance.
    readonly EcsFilter<Inc<C1>, Exc<C2>> _eventsFilter11 = "events";

    public void Run (EcsSystems systems) {
        // all injected fields can be used here.
        // _filter1.Value.xxx
        // _filter2.Value.xxx
        // _filter11.Value.xxx
        // _eventsFilter11.Value.xxx
        // even included pools:
        // EcsPool<C1> pool1 = _filter2.Pools.Inc1;
        // EcsPool<C2> pool2 = _filter2.Pools.Inc2;
    }
}
```
**Important:** `Inc<>` supports up to 8 constraints, `Exc<>` supports up to 4 constraints.
If you need more constraints, you can create new constraint type inside project.
For example, `Inc<>` with 10 constraints:
```csharp
public struct Inc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : IEcsInclude
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct
    where T7 : struct
    where T8 : struct
    where T9 : struct
    where T10 : struct {
    public EcsPool<T1> Inc1;
    public EcsPool<T2> Inc2;
    public EcsPool<T3> Inc3;
    public EcsPool<T4> Inc4;
    public EcsPool<T5> Inc5;
    public EcsPool<T6> Inc6;
    public EcsPool<T7> Inc7;
    public EcsPool<T8> Inc8;
    public EcsPool<T9> Inc9;
    public EcsPool<T10> Inc10;
        
    public EcsWorld.Mask Fill (EcsWorld world) {
        Inc1 = world.GetPool<T1> ();
        Inc2 = world.GetPool<T2> ();
        Inc3 = world.GetPool<T3> ();
        Inc4 = world.GetPool<T4> ();
        Inc5 = world.GetPool<T5> ();
        Inc6 = world.GetPool<T6> ();
        Inc7 = world.GetPool<T7> ();
        Inc8 = world.GetPool<T8> ();
        Inc9 = world.GetPool<T9> ();
        Inc10 = world.GetPool<T10> ();
        return world
            .Filter<T1> ()
            .Inc<T2> ()
            .Inc<T3> ()
            .Inc<T4> ()
            .Inc<T5> ()
            .Inc<T6> ()
            .Inc<T7> ()
            .Inc<T8> ()
            .Inc<T9> ()
            .Inc<T10> ();
    }
}
```
Similar, `Exc<>` with 6 constraints:
```csharp
public struct Exc<T1, T2, T3, T4, T5, T6> : IEcsExclude
    where T1 : struct
    where T2 : struct
    where T3 : struct
    where T4 : struct
    where T5 : struct
    where T6 : struct {
    public EcsWorld.Mask Fill (EcsWorld.Mask mask) {
        return mask.Exc<T1> ()
            .Exc<T2> ()
            .Exc<T3> ()
            .Exc<T4> ()
            .Exc<T5> ()
            .Exc<T6> ();
    }
}
```

## EcsSharedInject
```csharp
class TestSystem : IEcsRunSystem {
    // field will be injected with GetShared() instance from EcsSystems.
    readonly EcsSharedInject<Shared> _shared = default;

    public void Run (EcsSystems systems) {
        // all injected fields can be used here.
        // _shared.Value.xxx
    }
}
```

## EcsCustomInject
```csharp
systems
    .Add (new TestSystem ())
    .Inject (new CustomData1 (), new CustomData2 ())
    .Init ();
// ...
class TestSystem : IEcsRunSystem {
    // field will be injected with instance from EcsSystems.Inject() call.
    readonly EcsCustomInject<CustomData1> _custom1 = default;
    
    // field will be injected with instance from EcsSystems.Inject() call.
    readonly EcsCustomInject<CustomData2> _custom2 = default;

    public void Run (EcsSystems systems) {
        // all injected fields can be used here.
        // _custom1.Value.xxx
        // _custom2.Value.xxx
    }
}
```

# License
The software is released under the terms of the [MIT license](./LICENSE.md).

No personal support or any guarantees.
