using UnityEngine;

namespace BattleDisc
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float timer = 0.1f;
        
        private Transform _wayPoint;
        private float _timerValue;
        private Vector3 _wayPointPosition;
    
        void Start()
        {
            speed = PlayPrefsUtils.Instance.GetEnemySpeed();
            _timerValue = timer;
            _wayPoint = GameObject.Find("Ball").transform;
        }

        void Update()
        {
            if(_timerValue > 0)
            {
                _timerValue -= Time.deltaTime;
            }

            if (_timerValue <= 0)
            {
                UpdatePosition();
                _timerValue = timer;
            }

            var localPosition = transform.localPosition;
            localPosition = Vector3.MoveTowards(localPosition, 
                new Vector3(_wayPointPosition.x, localPosition.y,localPosition.z), 
                speed * Time.deltaTime);
            transform.localPosition = localPosition;
        }
    
        void UpdatePosition()
        {
            _wayPointPosition = _wayPoint.localPosition;
        }
    }
}
