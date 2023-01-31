// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class Vector2IntInspector : EcsComponentInspectorTyped<Vector2Int> {
        public override bool OnGuiTyped (string label, ref Vector2Int value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.Vector2IntField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}