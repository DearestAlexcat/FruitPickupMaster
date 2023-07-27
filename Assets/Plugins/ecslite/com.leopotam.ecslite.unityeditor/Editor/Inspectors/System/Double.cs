// ----------------------------------------------------------------------------
// The MIT License
// UnityEditor integration https://github.com/Leopotam/ecslite-unityeditor
// for LeoECS Lite https://github.com/Leopotam/ecslite
// Copyright (c) 2021-2022 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEditor;

namespace Leopotam.EcsLite.UnityEditor.Inspectors {
    sealed class DoubleInspector : EcsComponentInspectorTyped<double> {
        public override bool OnGuiTyped (string label, ref double value, EcsEntityDebugView entityView) {
            var newValue = EditorGUILayout.DoubleField (label, value);
            if (System.Math.Abs (newValue - value) < double.Epsilon) { return false; }
            value = newValue;
            return true;
        }
    }
}