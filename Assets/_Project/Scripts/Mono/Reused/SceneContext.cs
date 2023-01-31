using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SceneContext : MonoBehaviour
{
    [field: SerializeField] public Camera Camera { get; set; }
    [field: SerializeField] public WindowWinSimple WinWindow { get; set; }
    [field: SerializeField] public UISmoothBlackout blackout { get; set; }
    [field: SerializeField] public UIAnimation ThisUIAnimation { get; set; }
    [field: SerializeField] public SliderHelper LevelProgress { get; set; }
    [field: SerializeField] public TMPTextHelper LevelLabel { get; set; }

    [HideInInspector] public int FruitMask { get; private set; }

    private void Awake()
    {
        FruitMask = LayerMask.GetMask("FruitMask");
    }
}
