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
        public List<GameObject> confetti;
        public float confettiDisplayTime;
        public int confettiID;

        [Header("OTHER")]
        public float delayedCall;

        private void Awake()
        {
            uIAnimation.SetStartPosition("LevelStateText");
            uIAnimation.SetStartPosition("NextLevelButton");

            showCallbackWindow += () => buttonForward.enabled = true;
            hideCallbackWindow += () => buttonForward.enabled = false;
        }

        private IEnumerator DisplayWindow()
        {
            yield return new WaitForSeconds(delayedCall);

            confetti[confettiID].SetActive(true);

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
            try // Check MissingReferenceException 
            {
                confetti[confettiID].SetActive(false);
            }
            catch
            {
                //var ob = confetti.gameObject;
                Debug.Log("Confetti check MissingReferenceException");
            }

            base.OnDisable();
        }
    }
}