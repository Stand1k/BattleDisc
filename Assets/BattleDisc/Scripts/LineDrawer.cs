using UnityEngine;

namespace BattleDisc
{
    public class LineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform[] points;

        private void OnEnable()
        {
            lineRenderer.gameObject.SetActive(true);
        }

        void Update()
        {
            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, points[i].localPosition);
            }
        }
        
        private void OnDisable()
        {
            for (int i = 0; i < points.Length; i++)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
            }
            
            lineRenderer.gameObject.SetActive(false);
        }
    }
}
