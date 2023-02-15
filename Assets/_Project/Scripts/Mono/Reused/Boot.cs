//using System.Collections;
using UnityEngine;

namespace Client
{
    class Boot : MonoBehaviour
    {
        [SerializeField] StaticData staticData;

        void Awake()
        {
            Service<StaticData>.Set(staticData);
            //GameInitialization.FullInit();
            // yield return null;

            Levels.LoadCurrentWithSkip();
        }
    }
}