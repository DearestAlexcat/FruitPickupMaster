using UnityEngine;

namespace Client
{ 
    public class UI : MonoBehaviour
    {
        [field: SerializeField] public WindowWinSimple WinWindow { get; set; }
        [field: SerializeField] public UISmoothBlackout blackout { get; set; }
        [field: SerializeField] public UIAnimation ThisUIAnimation { get; set; }
        [field: SerializeField] public SliderHelper LevelProgress { get; set; }
        [field: SerializeField] public TMPTextHelper LevelLabel { get; set; }
    }
}