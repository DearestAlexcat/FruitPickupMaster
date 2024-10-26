using UnityEngine;

namespace Client
{
    static class GameInitialization
    {
        public static void FullInit()
        {
            InitializeUI();
        }

        static void InitializeUI()
        {
            var ui = Service<UI>.Get();

            if (ui == null)
            {
                ui = Object.Instantiate(Service<StaticData>.Get().UI);
                Service<UI>.Set(ui);
            }
        }
    }
}