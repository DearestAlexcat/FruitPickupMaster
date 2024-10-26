using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class ConveyorView : MonoBehaviour
    {
        public bool isGeneral;

        [Header("Spawn settings")]
        public float spawnDelay = 1f;
        public int quantityAtTime = 10;
        public int quantityAtInit = 10;

        [SerializeField] int poolSize = 10;
        [SerializeField] Transform spawnFruitPoint0;
        [SerializeField] Transform spawnFruitPoint1;

        [Header("Conveyor elements")]
        [SerializeField] List<Transform> scrollableSegments;
        [SerializeField] Transform finishPosition0;
        [SerializeField] Transform finishPosition1;

        [Header("Mevement settings")]
        [SerializeField] float speed = 1f;
        [SerializeField] bool isDirectionForward = true;

        [Header("Fruit prefabs")]
        [SerializeField] List<Fruit> fruitsPrefabs;

        List<Pooler<Fruit>> fruitsPooler;

        private int firstSegment, lastSegment;
        
        public float Speed => speed;

        private void Awake()
        {
            InitializeFruitsPooler();

            lastSegment = scrollableSegments.Count - 1;
        }

        public void InitializeFruitsPooler()
        {
            fruitsPooler = new();
            for (int i = 0; i < fruitsPrefabs.Count; i++)
            {
                fruitsPooler.Add(new Pooler<Fruit>(fruitsPrefabs[i], poolSize, transform));
            }
        }

        public string GetTaskName(int targetPoolIndex)
        {
            return fruitsPrefabs[targetPoolIndex].name;
        }

        public int GetRandomPoolIndex()
        {
            return Random.Range(0, fruitsPrefabs.Count);
        }

        public void ReturnToStart()
        {
            Vector3 startHook = scrollableSegments[lastSegment].transform.GetChild(0).position;
            Vector3 endHook = scrollableSegments[firstSegment].transform.GetChild(1).position;

            if (isDirectionForward)
            {
                scrollableSegments[lastSegment].position = endHook + (scrollableSegments[lastSegment].position - startHook);

                firstSegment--;
                lastSegment--;

                if (lastSegment < 0) lastSegment = scrollableSegments.Count - 1;
                if (firstSegment < 0) firstSegment = scrollableSegments.Count - 1;
            }
            else
            {
                scrollableSegments[firstSegment].position = startHook + (scrollableSegments[firstSegment].position - endHook);

                lastSegment++;
                firstSegment++;

                if (firstSegment == scrollableSegments.Count) firstSegment = 0;
                if (lastSegment == scrollableSegments.Count) lastSegment = 0;
            }
        }

        private bool PointInCircle(Vector3 a, Vector3 b, float r) // Бред, изменить подход. Избавиться от MoveTowards
        {
            return (b.x - a.x) * (b.x - a.x) + (b.z - a.z) * (b.z - a.z) <= r * r;
        }

        public void Move()
        {
            var endValue = isDirectionForward ? finishPosition1.position : finishPosition0.position;
            bool tostart = false;

            for (int i = 0; i < scrollableSegments.Count; i++)
            {
                scrollableSegments[i].transform.position = Vector3.MoveTowards(scrollableSegments[i].position, endValue, speed * Time.deltaTime);
                if (PointInCircle(scrollableSegments[i].transform.position, endValue, 0.5f))
                    tostart = true;
            }

            if (tostart)
                ReturnToStart();
        }

        public Fruit GetFruit(float t)
        {
            int poolIndex = Random.Range(0, fruitsPooler.Count);

            Fruit fruit = fruitsPooler[poolIndex].Get(Vector3.Lerp(spawnFruitPoint0.position, spawnFruitPoint1.position, t), Random.rotation);

            fruit.PoolIndex = poolIndex;
            fruit.transform.position += Random.insideUnitSphere * Service<StaticData>.Get().shiftAmplitude;

            return fruit;
        }

        public Fruit GetFruit()
        {
            int poolIndex = Random.Range(0, fruitsPooler.Count);

            Vector3 position = isDirectionForward ? spawnFruitPoint1.position : spawnFruitPoint0.position;
            
            Fruit fruit = fruitsPooler[poolIndex].Get(position, Random.rotation);

            fruit.PoolIndex = poolIndex;
            fruit.transform.position += Random.insideUnitSphere * Service<StaticData>.Get().shiftAmplitude;

            return fruit;
        }
       
        public void FreeFruit(Fruit fruit)
        {
            fruitsPooler[fruit.PoolIndex].Free(fruit);
        }

        public Vector3 GetStartPosition()
        {
            return isDirectionForward ? finishPosition0.position : finishPosition1.position;
        }

        public Vector3 GetFinishPosition()
        {
            return isDirectionForward ? finishPosition1.position : finishPosition0.position;
        }
    }
}