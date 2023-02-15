using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Client
{
    public class WindowWinSimple : WindowBaseSimple
    {
        [Header("BUTTONS")]
        public Button buttonForward;

        [Header("CONFETTI")]
        public float confettiDisplayTime;
       

        private void Awake()
        {
            uIAnimation.SetStartPosition("LevelStateText");
            uIAnimation.SetStartPosition("NextLevelButton");

            showCallbackWindow += () => buttonForward.enabled = true;
            hideCallbackWindow += () => buttonForward.enabled = false;
        }

        private IEnumerator DisplayWindow()
        {
            InitWindow();

            yield return new WaitForSeconds(confettiDisplayTime);

            base.OnEnable();
        }

        void InitWindow()
        {
            buttonForward.enabled = false;
        }

        public override void OnEnable()
        {
            StartCoroutine(DisplayWindow());
        }

        public override void OnDisable()
        {
            base.OnDisable();
        }
    }
}