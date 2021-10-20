using System.Collections.Generic;
using UnityEngine;

namespace BattleDisc
{
    public class EnemyWallsService : MonoBehaviour, IEnemyWallDestroyHandler
    {
        private List<GameObject> _enemyWallsList;

        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        void Start()
        {
            _enemyWallsList = new List<GameObject>();
            
            for (int i = 0; i < transform.childCount; i++)
            {
                _enemyWallsList.Add(transform.GetChild(i).gameObject);
            }
        }

        public void WallDestoy(GameObject gameObject)
        {
            if (_enemyWallsList.Contains(gameObject))
            {
                _enemyWallsList.Remove(gameObject);
                Destroy(gameObject);

                if (_enemyWallsList.Count <= 0)
                {
                    EventBus.RaiseEvent<IGameWonHandle>(h => h.GameWon());
                }
            }
        }
        
        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}
