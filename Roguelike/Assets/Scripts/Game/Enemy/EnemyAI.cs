using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Event;
using Game.Player;
using Game.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

namespace Game.Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [Header("Search")] [SerializeField] private float searchDistance;

        [SerializeField] private float chaseDistance;
        [SerializeField] private float searchDelay;
        [SerializeField] private float chaseDelay;

        [Header("Interaction")] [SerializeField]
        private float attackDistance = 1.5f;

        [SerializeField] private float hitDistance = 2f;
        [SerializeField] private float damage = 25;

        [SerializeField] private Tilemap[] forbiddenTilemaps;
        [SerializeField] private Tilemap walkable;

        private Dictionary<Vector3Int, TileBase> vectorTileMap;
        private Dictionary<Vector3Int, TileBase> vectorObstaclesTileMap;

        private Vector3Int _startPosition;
        private GameObject _player;
        private PlayerController _playerController;

        private bool _canMove;
        private Coroutine _moveDelayCoroutine;
        [SerializeField] private float _moveDelay;

        [SerializeField] private LayerMask interactableLayerMask;
        
        
        private void Awake()
        {
            _startPosition = walkable.WorldToCell(transform.position);
            transform.position = walkable.GetCellCenterWorld(_startPosition);
            GlobalEventManager.OnGameStart.AddListener(() =>
            {
                transform.position = walkable.GetCellCenterWorld(_startPosition);
                _canMove = true;
            });
            
            GlobalEventManager.OnGameStop.AddListener(() => _canMove = false);
            GlobalEventManager.OnTick.AddListener(() =>
            {
                if (_player == null)
                {
                    SearchPlayer();
                }
                else
                {
                    ChasePlayer();
                }
            });
            
            vectorTileMap = walkable.GetTileDictionaryWithType<TileBase>();
            vectorObstaclesTileMap = new Dictionary<Vector3Int, TileBase>();

            foreach (var forbiddenTilemap in forbiddenTilemaps)
            {
                vectorObstaclesTileMap.AddRange(forbiddenTilemap.GetTileDictionaryWithType<TileBase>());
            }
        }

        private void ChasePlayer()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToPlayer < chaseDistance)
            {
                TryMoveToPlayer();
            }
        }

        private void SearchPlayer()
        {
            var target = Physics2D.OverlapCircleAll(transform.position, searchDistance)
                .ToList()
                .Find(t => t.CompareTag("Player"));
            if (target != null)
            {
                _player = target.gameObject;
                _playerController = _player.GetComponent<PlayerController>();
                ChasePlayer();
            }
        }

        public void AllowMoving(bool allow)
        {
            _canMove = allow;
        }

        private void TryMoveToPlayer()
        {
            var playerPositon = walkable.WorldToCell(_player.transform.position);
            var position = walkable.WorldToCell(transform.position);
            var moveDirection = Vector3.Normalize((playerPositon - position));

            var distance = Vector3Int.Distance(playerPositon, position);
            if (distance <= 1)
            {
                if (Random.Range(0, 100) < 60)
                {
                    _playerController.TakeDamage(damage);
                }
                return;
            }
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, distance, interactableLayerMask);
            if (hit.collider == null)
            {
                var movePosition = position + moveDirection;
                var movePositionInt =
                    new Vector3Int(Mathf.RoundToInt(movePosition.x), Mathf.RoundToInt(movePosition.y), 0);
                if (vectorTileMap.TryGetValue(movePositionInt, out _)
                    && !vectorObstaclesTileMap.TryGetValue(movePositionInt, out _)
                    && movePositionInt != playerPositon)
                {
                    transform.position = walkable.GetCellCenterWorld(movePositionInt);
                    _canMove = false;
                    StartMoveDelay();
                }
            }
        }

        private void StartMoveDelay() => _moveDelayCoroutine ??= StartCoroutine(MoveDelay());
        private void StopMoveDelay() => StopMyCoroutine(ref _moveDelayCoroutine);

        private IEnumerator MoveDelay()
        {
            yield return new WaitForSeconds(_moveDelay);
            _canMove = true;
            StopMoveDelay();
        }

        private void StopMyCoroutine(ref Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}