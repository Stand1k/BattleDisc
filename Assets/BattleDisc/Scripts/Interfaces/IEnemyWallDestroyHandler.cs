using UnityEngine;

namespace BattleDisc
{
    public interface IEnemyWallDestroyHandler : IGlobalSubscriber
    {
        public void WallDestoy(GameObject gameObject);
    }
}