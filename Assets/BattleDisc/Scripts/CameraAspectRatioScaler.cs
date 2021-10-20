using UnityEngine;

namespace BattleDisc
{
    public class CameraAspectRatioScaler : MonoBehaviour
    {
        [SerializeField] private Vector2 referenceResolution;
        [SerializeField] private Vector3 zoomFactor = Vector3.one;

        [HideInInspector] public Vector3 originPosition;

        void Start()
        {
            originPosition = transform.position;
        }

        void Update()
        {
            if (referenceResolution.y == 0 || referenceResolution.x == 0)
                return;

            var refRatio = referenceResolution.x / referenceResolution.y;
            var ratio = (float) Screen.width / (float) Screen.height;

            transform.position = originPosition + transform.forward * (1f - refRatio / ratio) * zoomFactor.z
                                                + transform.right * (1f - refRatio / ratio) * zoomFactor.x
                                                + transform.up * (1f - refRatio / ratio) * zoomFactor.y;
        }
    }
}