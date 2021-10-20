using UnityEngine;

namespace BattleDisc
{
    public class PlayerMovement : MonoBehaviour, IGameWonHandle
    {
        [SerializeField] private SwerveInput swerveInput;

        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        void Start()
        {
            swerveInput = GetComponent<SwerveInput>();
        }

        void Update()
        {
            var position = transform.localPosition;

            var nextXPosition = position.x + swerveInput.GetNextXPosition();
            var nextZPosition = position.z + swerveInput.GetNextZPosition();
            
            Vector3 clampedMousePos = new Vector3(
                Mathf.Clamp(nextXPosition, -3.3f, 3.3f), 
                position.y,
                Mathf.Clamp(nextZPosition, -5.5f, -1f)
                );

            transform.localPosition = clampedMousePos;
        }

        public void GameWon()
        {
            enabled = false;
        }
        
        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}

