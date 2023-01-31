//using Facebook.Unity;
//using GameAnalyticsSDK;

namespace Client.Services
{
    public class AnalyticsManager
    {
        public AnalyticsManager()
        {
            //GameAnalytics.Initialize();
            FBInit();
        }

        private void FBInit()
        {
            //if (FB.IsInitialized)
            //{
            //    FB.ActivateApp();
            //}
            //else
            //{
            //    FB.Init(() =>
            //    {
            //        FB.ActivateApp();
            //    });
            //}
        }

        public void LevelStartGA(int CurrentLevelIndex)
        {
            var message = $"Level Start - {CurrentLevelIndex + 1}";
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, message);
        }

        public void LevelWinGA(int CurrentLevelIndex, int deaths)
        {
            var message = $"Level Win - {CurrentLevelIndex + 1}. Deaths per level - {deaths}";
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, message);
        }
       
        public void LevelLoseGA(int CurrentLevelIndex)
        {
            var message = $"Level Lose - {CurrentLevelIndex + 1}";
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, message);
        }
    }
}