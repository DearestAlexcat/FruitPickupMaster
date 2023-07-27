using System.Runtime.InteropServices;
using UnityEngine;

namespace Client
{
    [System.Serializable]
    public class LevelTask
    {
        [SerializeField] bool includeTask = true;
        public bool IncludeTask => includeTask;

        [HideInInspector] public int targetPoolIndex;

        [HideInInspector] public int targetCollect;

        public (string, Sprite) GetTask(ConveyorView c)
        {
            targetPoolIndex = Random.Range(0, c.FruitsPrefabs.Count);
            targetCollect = c.TargetCollect;

            string name = c.FruitsPrefabs[targetPoolIndex].name;

            return (targetCollect.ToString(), Service<StaticData>.Get().GetIcon(name));
        }

        public bool CheckÑorrectnessÑhoice(int poolIndex)
        {
            return poolIndex == targetPoolIndex;
        }

        public int TaskIndex => targetPoolIndex;

        public bool CheckTaskComplete()
        {
            return includeTask ? targetCollect == 0 : false;
        }
    }
}