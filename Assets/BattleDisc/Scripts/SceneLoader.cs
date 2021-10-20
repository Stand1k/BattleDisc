using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BattleDisc
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] private bool isAutoStart;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            if (isAutoStart) StartCoroutine(WaitForGame());
        }

        IEnumerator WaitForGame()
        {
            isAutoStart = false;
            yield return new WaitForSeconds(2.5f);
            StartCoroutine(LoadAsyncScene());
        }
        
        IEnumerator LoadAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Game");
            
            asyncLoad.allowSceneActivation = false;
            yield return (asyncLoad.progress > 0.9f);
            StartCoroutine(Loaded(asyncLoad));
        }

        public void LoadNextScene()
        {
            StartCoroutine(LoadAsyncScene());
        }

        IEnumerator Loaded(AsyncOperation sync)
        {
            yield return new WaitForSeconds(0.5f);
            sync.allowSceneActivation = true;
        }  
    }
}