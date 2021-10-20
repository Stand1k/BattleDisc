using System;
using System.Collections;
using BattleDisc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Arkanoid
{
    public class BallService : MonoBehaviour, IGameWonHandle
    {
        private BallData _ballData;

        [SerializeField] private Vector3 direction;
        [SerializeField] private float flySpeed;
        [SerializeField] private Transform player;
        [SerializeField] private Transform enemy;

        private LineDrawer _lineDrawer;
        private Transform _transform;
        private Material _ballMaterial;

        private void Awake()
        {
            EventBus.Subscribe(this);
        }

        private void Start()
        {
            _ballData = new BallData(BallType.Player, BallState.Aim);
            _transform = transform;
            player = GameObject.Find("Player").transform;
            enemy = GameObject.Find("Enemy").transform;
            _lineDrawer = GetComponent<LineDrawer>();
            _ballMaterial = GetComponent<MeshRenderer>().material;
        }

        private void Update()
        {
            if (_ballData.BallState == BallState.Active)
            {
                _lineDrawer.enabled = false;
                _transform.Translate(direction * flySpeed * Time.deltaTime, Space.Self);
            }
            else if (_ballData.BallState == BallState.Aim && _ballData.BallType == BallType.Player)
            {
                _lineDrawer.enabled = true;
            }
        }

        private void LateUpdate()
        {
            if (Input.GetMouseButtonUp(0) && 
                _ballData.BallType == BallType.Player && 
                _ballData.BallState == BallState.Aim)
            {
                _ballData.BallState = BallState.Active;

                direction = new Vector3(
                    (player.transform.position.x - transform.position.x ) / 3f,
                    0f,
                    (player.transform.position.z - transform.position.z) / 3f
                );
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.CompareTag("Player") && _ballData.BallType == BallType.Enemy)
            {
                PlayerBallTranstion(collision.transform);
            }

            if (collision.CompareTag("Enemy") && _ballData.BallType == BallType.Player)
            {
                StartCoroutine(StartEnemyAim());
                EnemyBallTransition(collision.transform);
            }

            if (collision.CompareTag("Wall"))
            {
                direction = MathUtils.BounceDirection(direction, collision.transform);
            }

            if (collision.CompareTag("EnemyWall") && _ballData.BallType == BallType.Player)
            {
                EventBus.RaiseEvent<IEnemyWallDestroyHandler>(h => 
                    h.WallDestoy(collision.gameObject)
                    );
                PlayerBallTranstion(player);
            }
        }

        private IEnumerator StartEnemyAim()
        {
            _ballData.BallState = BallState.Aim;
            yield return new WaitForSeconds(1f);
            direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-2f, -1f));
            _ballData.BallState = BallState.Active;
        }

        private void EnemyBallTransition(Transform collisionTransform)
        {
            _ballMaterial.color = Color.red;
            var collisionPosition = collisionTransform.position;
            var localPosition = transform.localPosition;
            _ballData.BallState = BallState.Aim;
            _ballData.BallType = BallType.Enemy;
            localPosition = new Vector3(collisionPosition.x, localPosition.y,
                collisionPosition.z + 1.5f);
            transform.localPosition = localPosition;
        }

        private void PlayerBallTranstion(Transform collisionTransform)
        {
            _ballMaterial.color  = Color.green;
            var collisionPosition = collisionTransform.position;
            var localPosition = transform.localPosition;
            _ballData.BallState = BallState.Aim;
            _ballData.BallType = BallType.Player;
            localPosition = new Vector3(collisionPosition.x, localPosition.y,
                collisionPosition.z - 1.5f);
            transform.localPosition = localPosition;
        }

        public void GameWon()
        {
            PlayPrefsUtils.Instance.SetEnemySpeed();
            PlayPrefsUtils.Instance.SetNextLvl();
            enabled = false;
        }

        private void OnDestroy()
        {
            EventBus.Unsubscribe(this);
        }
    }
}