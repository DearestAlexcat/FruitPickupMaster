using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class Basket : MonoBehaviour
    {
        [SerializeField] List<Transform> pointers;

        int index = 0;

        public async UniTask AddToСart(Fruit fruit)
        {
            fruit.transform.parent = pointers[index];

            index = (index + 1) % pointers.Count;

            // Если место занято, то освобождаем его удалив прошлый объект

            await UniTask.NextFrame();

            fruit.transform.localPosition = Vector3.zero;
        }
    }
}