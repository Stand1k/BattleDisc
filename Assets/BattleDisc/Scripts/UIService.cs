using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleDisc
{
    public class UIService : MonoBehaviour, IGameWonHandle
    {
        [SerializeField] private Canvas gameOverCanvas;
        [SerializeField] private CanvasGroup blurBackground;    
        [SerializeField] private Button nextReloadButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private TMP_Text lvlText;

        private void Awake()
        {
            EventBus.Subscribe(this);
            pauseButton.onClick.AddListener(() => PauseButtonToggle());
            lvlText.text = "Lvl: " + PlayPrefsUtils.Instance.GetCurrentLvl();
        }

        public void GameWon()
        {
            nextReloadButton.GetComponentInChildren<TMP_Text>().text = "Next";
            GameOverMenu();
        }

        public void PauseButtonToggle()
        {
            if (Time.timeScale == 1f)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }

        private void GameOverMenu()
        {   
            pauseButton.gameObject.SetActive(false);
            gameOverCanvas.enabled = true;
            exitButton.onClick.AddListener(() => Application.Quit());
            nextReloadButton.onClick.AddListener(() => SceneLoader.Instance.LoadNextScene());
            blurBackground.DOFade(1f, 1f);
            nextReloadButton.transform.DOLocalMoveX(0f, 1f);
            exitButton.transform.DOLocalMoveX(0f, 1f);
        }
        
        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}