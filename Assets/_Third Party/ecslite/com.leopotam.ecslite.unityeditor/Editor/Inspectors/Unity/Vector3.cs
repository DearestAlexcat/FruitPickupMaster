// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class Vector3Inspector : EcsComponentInspectorTyped<Vector3> {
        public override bool OnGuiTyped (string label, ref Vector3 value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.Vector3Field (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}