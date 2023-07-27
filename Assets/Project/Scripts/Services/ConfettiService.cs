using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfettiService
{
    [SerializeField] float delayedConfettiCall;
    [SerializeField] int confettiID;
    [SerializeField] List<GameObject> confetti;

    public async UniTask ShowConfetti()
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(delayedConfettiCall), ignoreTimeScale: false);

        confetti[confettiID].SetActive(true);
    }
}
