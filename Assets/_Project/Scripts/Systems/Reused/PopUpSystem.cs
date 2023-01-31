using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PopUpSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<Prefabs> _prefabs = default;
        private readonly EcsCustomInject<SceneContext> _sceneContext = default; 
        private readonly EcsCustomInject<SaveInJson> _saveInJson = default;

        private readonly EcsFilterInject<Inc<PopUpRequest>> _popUpFilter = default;

        public void Run(EcsSystems systems)
        {
            foreach (var it in _popUpFilter.Value)
            {
                var c = _popUpFilter.Pools.Inc1.Get(it);
                var popUP = Object.Instantiate<PopUpText>(_prefabs.Value.PlusOne, c.SpawnPosition, c.SpawnRotation, c.Parent);
                popUP.textUP.text = c.TextUP;

                _popUpFilter.Pools.Inc1.Del(it);
            }
        }
    }
}