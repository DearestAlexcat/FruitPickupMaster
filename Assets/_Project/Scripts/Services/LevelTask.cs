using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelTask
{
    [SerializeField] bool includeTask = true;
    public bool IncludeTask => includeTask;

    [SerializeField] int minCollect = 1;
    [SerializeField] int maxCollect = 5;

    [HideInInspector] public int targetPoolIndex;
    [HideInInspector] public int currentCollect;
    [HideInInspector] public int targetCollect;

    public string GetTask(ConveyorElement c)
    {
        targetPoolIndex = Random.Range(0, c.fruitsPrefabs.Count);
        targetCollect = Random.Range(minCollect, maxCollect + 1);

        string name = c.fruitsPrefabs[targetPoolIndex].name;

        if (targetCollect == 1)
        {
            if (name != "Apple")
            {
                name = name.TrimEnd('e');
            }

            name = name.TrimEnd('s');
        }

        return "Collect " + targetCollect + " " + name;
    }

    public bool CheckÑorrectnessÑhoice(int poolIndex)
    {
        return poolIndex == targetPoolIndex;
    }

    public void IncrementCollect()
    {
        Client.EcsStartup.Instance.SceneContext.LevelProgress.ChangeValueWithText(++currentCollect);
    }

    public bool CheckLevelComplete()
    {
        return currentCollect >= targetCollect;
    }
}