using UnityEngine;
using UnityEngine.SceneManagement;

namespace Client
{
    [CreateAssetMenu]
    public class Levels : ScriptableObject
    {
        public string[] Scenes;

        public string this[int index]
        {
            get
            {
                return Scenes[index];
            }
        }

        public static void LoadCurrent()
        {
            var level = Progress.CurrentLevel;
            var staticData = Service<StaticData>.Get();

            var totalLevels = staticData.ThisLevels.Scenes.Length;

            if (level >= totalLevels)
            {
                level = level % totalLevels;
            }

            var levelName = staticData.ThisLevels.Scenes[level];
            SceneManager.LoadSceneAsync(levelName);
        }

        public static void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
