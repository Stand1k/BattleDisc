using UnityEngine;

namespace BattleDisc
{
    public class SwerveInput : MonoBehaviour
    {
        private float _lastFrameFingerPositionX;
        private float _lastFrameFingerPositionY;
        private float _moveFactorX;
        private float _moveFactorY;

        [SerializeField] private float swerveSpeed = 3f;
        [SerializeField] private float maxSwerveAmount = 3f;

        private float SwerveAmountX => Time.deltaTime * swerveSpeed * _moveFactorX;
        private float SwerveAmountZ => Time.deltaTime * swerveSpeed * _moveFactorY;

        private void Start()
        {
#if UNITY_EDITOR
            swerveSpeed *= 2;
            maxSwerveAmount *= 2;
#endif
        }

        void LateUpdate()
        {
            InputHandler();
        }
        
        public void InputHandler()
        {
            
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            if (Input.GetMouseButtonDown(0))
            {
                _lastFrameFingerPositionX = Input.mousePosition.x;
                _lastFrameFingerPositionY = Input.mousePosition.y;
            }
            else if (Input.GetMouseButton(0))
            {
                _moveFactorX = Input.mousePosition.x - _lastFrameFingerPositionX;
                _lastFrameFingerPositionX = Input.mousePosition.x;
                
                _moveFactorY = Input.mousePosition.y - _lastFrameFingerPositionY;
                _lastFrameFingerPositionY = Input.mousePosition.y;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _moveFactorX = 0f;
                _moveFactorY = 0f;
            }

#elif UNITY_ANDROID && !UNITY_EDITOR
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _lastFrameFingerPositionX = touch.position.x;
                        _lastFrameFingerPositionY = touch.position.y;
                        break;
                    case TouchPhase.Moved:
                        _moveFactorX = touch.position.x - _lastFrameFingerPositionX;
                        _lastFrameFingerPositionX = touch.position.x;

                        _moveFactorY = touch.position.y - _lastFrameFingerPositionY;
                        _lastFrameFingerPositionY = touch.position.y;
                        break;
                    case TouchPhase.Ended:
                        _moveFactorX = 0f;
                        _moveFactorY = 0f;
                        break;
                }
            }
#endif
        }

        public float GetNextXPosition()
        {
            return Mathf.Clamp(SwerveAmountX, -maxSwerveAmount, maxSwerveAmount);
        }
        
        public float GetNextZPosition()
        {
            return Mathf.Clamp(SwerveAmountZ, -maxSwerveAmount, maxSwerveAmount);
        }
    }
}