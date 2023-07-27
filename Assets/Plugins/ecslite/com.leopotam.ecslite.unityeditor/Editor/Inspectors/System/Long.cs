// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class LongInspector : EcsComponentInspectorTyped<long> {
        public override bool OnGuiTyped (string label, ref long value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.LongField (label, value);
            if (newValue == value) { return false; }
            value = newValue;
            return true;
        }
    }
}