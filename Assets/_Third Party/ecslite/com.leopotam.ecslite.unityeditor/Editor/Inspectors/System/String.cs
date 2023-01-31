// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class StringInspector : EcsComponentInspectorTyped<string> {
        public override bool IsNullAllowed () {
            return true;
        }

        public override bool OnGuiTyped (string label, ref string value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.TextField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}