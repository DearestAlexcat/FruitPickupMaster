using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Client 
{
    sealed class PopupSystem : IEcsRunSystem 
    {
        private readonly EcsCustomInject<StaticData> _staticData = default;
        private readonly EcsFilterInject<Inc<PopUpRequest>> _popUpFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var it in _popUpFilter.Value)
            {
                var c = _popUpFilter.Pools.Inc1.Get(it);

                var popUP = Object.Instantiate<PopupText>(_staticData.Value.popup, c.SpawnPosition, c.SpawnRotation, c.Parent);
                popUP.textUP.text = c.TextUP;

                _popUpFilter.Pools.Inc1.Del(it);
            }
        }
    }
}