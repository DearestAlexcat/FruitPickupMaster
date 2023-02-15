using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    [System.Serializable]
    public class ConveyorElement
    {
        [Header("SPAWN")]
        public int poolSize = 10;
        public float spawnDelay = 1f;
        public Transform spawnPoint;
        [HideInInspector] public float time;

        [Header("CONVEYOR_OBJ")]
        public GameObject conveyorObj;
        public Transform conveyorLine;
        public MeshRenderer ScrollingConveyorLine;

        [Header("MOVEMENT_SETTINGS")]
        public float Speed;
        public float FruitSpeed;

        [Header("PREFABS")]
        public List<Fruit> fruitsPrefabs = new List<Fruit>();
        [HideInInspector] public List<Pooler<Fruit>> fruitsPooler = new List<Pooler<Fruit>>();

        [Header("LINKS")]
        public PlayerUnit unit;

        public void InitializeFruits()
        {
            for (int i = 0; i < fruitsPrefabs.Count; i++)
            {
                fruitsPooler.Add(new Pooler<Fruit>(fruitsPrefabs[i], poolSize, conveyorObj.transform));
            }
        }

        public Fruit GetFruit()
        {
            int index = Random.Range(0, fruitsPooler.Count);
            Fruit f = fruitsPooler[index].Get(spawnPoint.position, Quaternion.identity);
            f.PoolIndex = index;
            return f;
        }

        public void FreeFruit(Fruit fruit)
        {
            fruitsPooler[fruit.PoolIndex].Free(fruit);
        }
    }
}