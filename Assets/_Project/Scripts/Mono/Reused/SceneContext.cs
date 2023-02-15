using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Client
{ 
    public class SceneContext : MonoBehaviour
    {
        [field: SerializeField] public List<ConveyorElement> Conveyors { get; private set; }

        [HideInInspector] public int FruitMask { get; private set; }

        [field: SerializeField] public List<Unit> UnitsEntityInitializeList { get; private set; }

        [Header("CONFETTI")]
        [SerializeField] float delayedConfettiCall;
        [SerializeField] int confettiID;
        [SerializeField] List<GameObject> confetti;

        private void Awake()
        {
            FruitMask = LayerMask.GetMask("FruitMask");
        }

        public async UniTask ShowConfetti()
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(delayedConfettiCall), ignoreTimeScale: false);

            confetti[confettiID].SetActive(true);
        }
    }
}