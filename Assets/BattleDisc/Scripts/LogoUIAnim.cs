using DG.Tweening;
using UnityEngine;

namespace Arkanoid
{
    public class LogoUIAnim : MonoBehaviour
    {
        private void Start()
        {
            var rectTransform = GetComponent<RectTransform>();
            var canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.DOFade(1f, 2f);
            rectTransform.DOShakePosition(2f, 20, 30, 5f);
        }
    }
}
