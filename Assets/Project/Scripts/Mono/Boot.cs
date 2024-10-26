using System.Collections;
using UnityEngine;

namespace Client
{
    class Boot : MonoBehaviour
    {
        [SerializeField] StaticData staticData;

        IEnumerator Start()
        {
            Service<StaticData>.Set(staticData);
            GameInitialization.FullInit();
            
            yield return null;

            Levels.LoadCurrent();
        }
    }
}