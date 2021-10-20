using UnityEngine;

namespace BattleDisc
{
    public class PlayPrefsUtils : Singleton<PlayPrefsUtils>
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        
        public void SetEnemySpeed()
        {
            PlayerPrefs.SetFloat("EnemySpeed", GetEnemySpeed() + 1f);
        }

        public float GetEnemySpeed()
        {
            return PlayerPrefs.HasKey("EnemySpeed") ? PlayerPrefs.GetFloat("EnemySpeed") : 2f;
        }

        public void SetNextLvl()
        {
            PlayerPrefs.SetInt("CurrentLvl", GetCurrentLvl() + 1);
        }

        public int GetCurrentLvl()
        {
            return PlayerPrefs.HasKey("CurrentLvl") ? PlayerPrefs.GetInt("CurrentLvl") : 1;
        }
    }
}