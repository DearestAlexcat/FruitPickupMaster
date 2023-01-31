
public class SaveData
{
    public int LevelIndex;
    public int[] LevelSortIndex = new int[5];

    public int ActualLevel => LevelSortIndex[index % LevelSortIndex.Length];

    public int Money;

    public int index = 0;

    public int TutorialIndex = 0;

    bool Check(int levelIndex)
    {
        for (int i = 0; i < LevelSortIndex.Length; i++)
        {
            if(LevelSortIndex[i] == levelIndex)
            {
                return true;
            }
        }

        return false;
    }

    public void SortLevels(Config config)
    {
        if (LevelIndex > config.levelDatas.Length - 1)
        {
            int tempIndex;
            int iterations = 30;

            do
            {
                iterations--;
                tempIndex = RandomRange(0, config.levelDatas.Length);
            } while (iterations > 0 && Check(tempIndex));

            LevelSortIndex[++index % LevelSortIndex.Length] = tempIndex;
        }
        else
        {
            LevelSortIndex[++index % LevelSortIndex.Length] = LevelIndex;
        }
    }

    private int RandomRange(int min, int max)
    {
        return (int)((new System.Random(System.DateTime.Now.Millisecond).Next(0, 32767) * (max - min)) / 32767f + min);
    }
}
