// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;
using UnityEngine;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class QuaternionInspector : EcsComponentInspectorTyped<Quaternion> {
        public override bool OnGuiTyped (string label, ref Quaternion value, EcsEntityDebugView entityView) {
            var eulerAngles = value.eulerAngles;
            var newValue = EditorGUILayout.Vector3Field (label, eulerAngles);
            if (newValue == eulerAngles) { return false; }
            value = Quaternion.Euler (newValue);
            return true;
        }
    }
}