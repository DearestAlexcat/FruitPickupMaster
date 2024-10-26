using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfettiService
{
    [SerializeField] float delayedConfettiCall;
    [SerializeField] int confettiIndex;
    [SerializeField] List<GameObject> confetti;

    public async UniTask ShowConfetti()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(delayedConfettiCall), ignoreTimeScale: false);
        confetti[confettiIndex].SetActive(true);
    }
}
