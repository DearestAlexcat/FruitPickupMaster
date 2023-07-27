using Cysharp.Threading.Tasks;
using DG.Tweening;
using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class ConveyorView : MonoBehaviour
    {
        [Header("COLLECT")]
        [SerializeField] int minCollect = 1;
        [SerializeField] int maxCollect = 5;
        [HideInInspector] int targetCollect;

        [Header("SPAWN")]
        [SerializeField] int poolSize = 10;
        [SerializeField] Transform spawnFruitPoint0;
        [SerializeField] Transform spawnFruitPoint1;
        public int quantityAtTime = 10;
        public float spawnDelay = 1f;

        [Header("CONVEYOR_ELEMENTS")]
        [SerializeField] List<Transform> scrollableSegments;
        [SerializeField] Transform finishPosition0;
        [SerializeField] Transform finishPosition1;

        [Header("MOVEMENT_SETTINGS")]
        [SerializeField] float speed = 1f;
        [SerializeField] bool isDirectionForward = true;

        [Header("PREFABS")]
        [SerializeField] List<Fruit> fruitsPrefabs;

        //OTHER
        [HideInInspector] public List<Pooler<Fruit>> fruitsPooler;
        
        [HideInInspector] public float time;

        private int firstSegment, lastSegment, conveyorEntity;
        
        public float Speed => speed;
        public int TargetCollect => targetCollect;
        public List<Fruit> FruitsPrefabs => fruitsPrefabs;
        

        private void Awake()
        {
            InitializeFruitsPooler();
            InitTargetCollect();

            firstSegment = 0;
            lastSegment = scrollableSegments.Count - 1;
        }
        public Vector3 GetActualFinishPosition()
        {
            return isDirectionForward ? finishPosition1.position : finishPosition0.position;
        }


        public void InitTargetCollect()
        {
            targetCollect = Random.Range(minCollect, maxCollect + 1);
        }

        private void RequestMoveSegment()
        {
            Service<EcsWorld>.Get().NewEntityRef<MoveSegmentReguest>().ConveyorEntity = conveyorEntity;
        }

        public void TranslateSegment()
        {
            if (isDirectionForward)
            {
                Vector3 endHook = scrollableSegments[firstSegment].transform.GetChild(1).position;
                Vector3 startHook = scrollableSegments[lastSegment].transform.GetChild(0).position;

                Vector3 pos = endHook + (scrollableSegments[lastSegment].position - startHook);
                scrollableSegments[lastSegment].position = pos;

                firstSegment--;
                lastSegment--;

                if (lastSegment < 0) lastSegment = scrollableSegments.Count - 1;
                if (firstSegment < 0) firstSegment = scrollableSegments.Count - 1;
            }
            else
            {
                Vector3 startHook = scrollableSegments[lastSegment].transform.GetChild(0).position;
                Vector3 endHook = scrollableSegments[firstSegment].transform.GetChild(1).position;

                Vector3 pos = startHook + (scrollableSegments[firstSegment].position - endHook);
                scrollableSegments[firstSegment].position = pos;

                lastSegment++;
                firstSegment++;

                if (firstSegment == scrollableSegments.Count) firstSegment = 0;
                if (lastSegment == scrollableSegments.Count) lastSegment = 0;
            }
        }

        public void MoveSegments()
        {
            Vector3 position = isDirectionForward ? finishPosition1.position : finishPosition0.position;

            for (int i = 0; i < scrollableSegments.Count; i++)
            {
                if (!DOTween.IsTweening(scrollableSegments[i]))
                {
                    scrollableSegments[i].DOComplete();
                    scrollableSegments[i].DOMove(position, speed)
                        .SetEase(Ease.Linear)
                        .SetSpeedBased()
                        .SetLink(scrollableSegments[i].gameObject)
                        .OnComplete(() => { RequestMoveSegment(); });
                }
            }
        }

        public void InitializeFruitsPooler()
        {
            fruitsPooler = new List<Pooler<Fruit>>();

            for (int i = 0; i < fruitsPrefabs.Count; i++)
            {
                fruitsPooler.Add(new Pooler<Fruit>(fruitsPrefabs[i], poolSize, transform));
            }
        }

        public async UniTask<Fruit> GetFruit()
        {
            int index = Random.Range(0, fruitsPooler.Count);

            Vector3 position = isDirectionForward ? spawnFruitPoint1.position : spawnFruitPoint0.position;

            Fruit f = fruitsPooler[index].Get(position, Random.rotation);
            f.PoolIndex = index;

            // wait until the object takes the correct position

            f.ThisRigidbody.isKinematic = true;
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.01f), ignoreTimeScale: false);

            position += Random.insideUnitSphere * Service<StaticData>.Get().shiftAmplitude; // spawn in sphere
            f.transform.position = position;

            f.ThisRigidbody.isKinematic = false;
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.01f), ignoreTimeScale: false);

            return f;
        }

        public void FreeFruit(Fruit fruit)
        {
            fruitsPooler[fruit.PoolIndex].Free(fruit);
        }
    }
}