using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public static class TaskExtension
{
    public static Dictionary<string, int> groupCoroutines = new Dictionary<string, int>();

    public static void AttachTaskToGroup(this UniTask action, string groupName)
    {
        if (!groupCoroutines.ContainsKey(groupName))
        {
            groupCoroutines.Add(groupName, 0);
        }

        groupCoroutines[groupName]++;
        DoParallel(action, groupName).Forget();
    }

    public static async UniTask DoParallel(UniTask action, string groupName)
    {
        await action;
        groupCoroutines[groupName]--;
    }

    public static bool CheckGroupProcessing(string groupName)
    {
        return groupCoroutines.ContainsKey(groupName) && groupCoroutines[groupName] > 0;
    }
}
