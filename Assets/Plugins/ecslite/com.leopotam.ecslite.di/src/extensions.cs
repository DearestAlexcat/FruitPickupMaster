// -----------------------------------------------------------------------------------------
// The MIT License
// Dependency injection for LeoECS Lite https://github.com/Leopotam/ecslite-di
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// -----------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
#pragma warning disable CS0618

namespace Leopotam.EcsLite.Di {
#if DEBUG
    [Obsolete ("Use EcsWorldInject instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsWorldAttribute : Attribute {
        public readonly string World;

        public EcsWorldAttribute (string world = default) {
            World = world;
        }
    }

#if DEBUG
    [Obsolete ("Use EcsPoolInject<> instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsPoolAttribute : Attribute {
        public readonly string World;

        public EcsPoolAttribute (string world = default) {
            World = world;
        }
    }

#if DEBUG
    [Obsolete ("Use EcsFilterInject<> instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsFilterAttribute : Attribute {
        public readonly string World;
        public readonly Type Inc1;
        public readonly Type[] Others;

        public EcsFilterAttribute (Type includeType, params Type[] otherIncludes) : this (default, includeType, otherIncludes) { }

        public EcsFilterAttribute (string world, Type includeType, params Type[] otherIncludes) {
            World = world;
            Inc1 = includeType;
            Others = otherIncludes;
        }
    }

#if DEBUG
    [Obsolete ("Use EcsFilterInject<> instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsFilterExcludeAttribute : Attribute {
        public readonly Type Exc1;
        public readonly Type[] Others;

        public EcsFilterExcludeAttribute (Type excludeType, params Type[] otherExcludes) {
            Exc1 = excludeType;
            Others = otherExcludes;
        }
    }

#if DEBUG
    [Obsolete ("Use EcsSharedInject<> instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsSharedAttribute : Attribute { }

#if DEBUG
    [Obsolete ("Use EcsDataInject<> instead.")]
#endif
    [AttributeUsage (AttributeTargets.Field)]
    public sealed class EcsInjectAttribute : Attribute { }

    public static class Extensions {
        static readonly Type WorldType = typeof (EcsWorld);
        static readonly Type WorldAttrType = typeof (EcsWorldAttribute);
        static readonly Type PoolType = typeof (EcsPool<>);
        static readonly Type PoolAttrType = typeof (EcsPoolAttribute);
        static readonly Type FilterType = typeof (EcsFilter);
        static readonly Type FilterAttrType = typeof (EcsFilterAttribute);
        static readonly Type FilterExcAttrType = typeof (EcsFilterExcludeAttribute);
        static readonly Type SharedAttrType = typeof (EcsSharedAttribute);
        static readonly Type InjectAttrType = typeof (EcsInjectAttribute);
        static readonly MethodInfo WorldGetPoolMethod = typeof (EcsWorld).GetMethod (nameof (EcsWorld.GetPool));
        static readonly MethodInfo WorldFilterMethod = typeof (EcsWorld).GetMethod (nameof (EcsWorld.Filter));
        static readonly MethodInfo MaskIncMethod = typeof (EcsWorld.Mask).GetMethod (nameof (EcsWorld.Mask.Inc));
        static readonly MethodInfo MaskExcMethod = typeof (EcsWorld.Mask).GetMethod (nameof (EcsWorld.Mask.Exc));
        static readonly Dictionary<Type, MethodInfo> GetPoolMethodsCache = new Dictionary<Type, MethodInfo> (256);
        static readonly Dictionary<Type, MethodInfo> FilterMethodsCache = new Dictionary<Type, MethodInfo> (256);
        static readonly Dictionary<Type, MethodInfo> MaskIncMethodsCache = new Dictionary<Type, MethodInfo> (256);
        static readonly Dictionary<Type, MethodInfo> MaskExcMethodsCache = new Dictionary<Type, MethodInfo> (256);

        public static EcsSystems Inject (this EcsSystems systems, params object[] injects) {
            if (injects == null) { injects = Array.Empty<object> (); }
            IEcsSystem[] allSystems = null;
            var systemsCount = systems.GetAllSystems (ref allSystems);
            var shared = systems.GetShared<object> ();
            var sharedType = shared?.GetType ();

            for (var i = 0; i < systemsCount; i++) {
                var system = allSystems[i];
                foreach (var f in system.GetType ().GetFields (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)) {
                    // skip statics.
                    if (f.IsStatic) { continue; }
                    // EcsWorld.
                    if (InjectWorld (f, system, systems)) { continue; }
                    // EcsPool.
                    if (InjectPool (f, system, systems)) { continue; }
                    // EcsFilter.
                    if (InjectFilter (f, system, systems)) { continue; }
                    // Shared.
                    if (InjectShared (f, system, shared, sharedType)) { continue; }
                    // Inject.
                    if (InjectCustomData (f, system, injects)) { continue; }
                    // EcsWorldInject, EcsFilterInject, EcsPoolInject, EcsSharedInject.
                    if (InjectBuiltIns (f, system, systems)) { continue; }
                    // EcsDataInject.
                    if (InjectCustoms (f, system, injects)) { continue; }
                }
            }

            return systems;
        }

        static MethodInfo GetGenericGetPoolMethod (Type componentType) {
            if (!GetPoolMethodsCache.TryGetValue (componentType, out var pool)) {
                pool = WorldGetPoolMethod.MakeGenericMethod (componentType);
                GetPoolMethodsCache[componentType] = pool;
            }
            return pool;
        }

        static MethodInfo GetGenericFilterMethod (Type componentType) {
            if (!FilterMethodsCache.TryGetValue (componentType, out var filter)) {
                filter = WorldFilterMethod.MakeGenericMethod (componentType);
                FilterMethodsCache[componentType] = filter;
            }
            return filter;
        }

        static MethodInfo GetGenericMaskIncMethod (Type componentType) {
            if (!MaskIncMethodsCache.TryGetValue (componentType, out var inc)) {
                inc = MaskIncMethod.MakeGenericMethod (componentType);
                MaskIncMethodsCache[componentType] = inc;
            }
            return inc;
        }

        static MethodInfo GetGenericMaskExcMethod (Type componentType) {
            if (!MaskExcMethodsCache.TryGetValue (componentType, out var exc)) {
                exc = MaskExcMethod.MakeGenericMethod (componentType);
                MaskExcMethodsCache[componentType] = exc;
            }
            return exc;
        }

        static bool InjectWorld (FieldInfo fieldInfo, IEcsSystem system, EcsSystems systems) {
            if (fieldInfo.FieldType == WorldType) {
                if (Attribute.IsDefined (fieldInfo, WorldAttrType)) {
                    var worldAttr = (EcsWorldAttribute) Attribute.GetCustomAttribute (fieldInfo, WorldAttrType);
                    fieldInfo.SetValue (system, systems.GetWorld (worldAttr.World));
                }
                return true;
            }
            return false;
        }

        static bool InjectPool (FieldInfo fieldInfo, IEcsSystem system, EcsSystems systems) {
            if (fieldInfo.FieldType.IsGenericType && fieldInfo.FieldType.GetGenericTypeDefinition () == PoolType) {
                if (Attribute.IsDefined (fieldInfo, PoolAttrType)) {
                    var poolAttr = (EcsPoolAttribute) Attribute.GetCustomAttribute (fieldInfo, PoolAttrType);
                    var world = systems.GetWorld (poolAttr.World);
                    var componentTypes = fieldInfo.FieldType.GetGenericArguments ();
                    fieldInfo.SetValue (system, GetGenericGetPoolMethod (componentTypes[0]).Invoke (world, null));
                }
                return true;
            }
            return false;
        }

        static bool InjectFilter (FieldInfo fieldInfo, IEcsSystem system, EcsSystems systems) {
            if (fieldInfo.FieldType == FilterType) {
                if (Attribute.IsDefined (fieldInfo, FilterAttrType)) {
                    var filterAttr = (EcsFilterAttribute) Attribute.GetCustomAttribute (fieldInfo, FilterAttrType);
                    var world = systems.GetWorld (filterAttr.World);
                    var mask = (EcsWorld.Mask) GetGenericFilterMethod (filterAttr.Inc1).Invoke (world, null);
                    if (filterAttr.Others != null) {
                        foreach (var type in filterAttr.Others) {
                            GetGenericMaskIncMethod (type).Invoke (mask, null);
                        }
                    }
                    if (Attribute.IsDefined (fieldInfo, FilterExcAttrType)) {
                        var filterExcAttr = (EcsFilterExcludeAttribute) Attribute.GetCustomAttribute (fieldInfo, FilterExcAttrType);
                        GetGenericMaskExcMethod (filterExcAttr.Exc1).Invoke (mask, null);
                        if (filterExcAttr.Others != null) {
                            foreach (var type in filterExcAttr.Others) {
                                GetGenericMaskExcMethod (type).Invoke (mask, null);
                            }
                        }
                    }
                    fieldInfo.SetValue (system, mask.End ());
                }
                return true;
            }
            return false;
        }

        static bool InjectShared (FieldInfo fieldInfo, IEcsSystem system, object shared, Type sharedType) {
            if (shared != null && Attribute.IsDefined (fieldInfo, SharedAttrType)) {
                if (fieldInfo.FieldType.IsAssignableFrom (sharedType)) {
                    fieldInfo.SetValue (system, shared);
                }
                return true;
            }
            return false;
        }

        static bool InjectCustomData (FieldInfo fieldInfo, IEcsSystem system, object[] injects) {
            if (injects.Length > 0 && Attribute.IsDefined (fieldInfo, InjectAttrType)) {
                foreach (var inject in injects) {
                    if (fieldInfo.FieldType.IsInstanceOfType (inject)) {
                        fieldInfo.SetValue (system, inject);
                        break;
                    }
                }
                return true;
            }
            return false;
        }

        static bool InjectBuiltIns (FieldInfo fieldInfo, IEcsSystem system, EcsSystems systems) {
            if (typeof (IEcsDataInject).IsAssignableFrom (fieldInfo.FieldType)) {
                var instance = (IEcsDataInject) fieldInfo.GetValue (system);
                instance.Fill (systems);
                fieldInfo.SetValue (system, instance);
                return true;
            }
            return false;
        }

        static bool InjectCustoms (FieldInfo fieldInfo, IEcsSystem system, object[] injects) {
            if (typeof (IEcsCustomDataInject).IsAssignableFrom (fieldInfo.FieldType)) {
                var instance = (IEcsCustomDataInject) fieldInfo.GetValue (system);
                instance.Fill (injects);
                fieldInfo.SetValue (system, instance);
                return true;
            }
            return false;
        }
    }
}