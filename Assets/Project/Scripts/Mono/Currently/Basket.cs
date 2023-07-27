using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] List<Transform> pointers;

        int index = 0;

        public async UniTask AddTo—art(Fruit fruit)
        {
            fruit.transform.parent = pointers[index];

            index = (index + 1) % pointers.Count;

            await UniTask.NextFrame();

            fruit.transform.localPosition = Vector3.zero;
        }
    }
}