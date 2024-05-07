using System.Collections;
using System.Collections.Generic;
using Game.Collectable;
using Game.Enemy;
using Game.Event;
using Game.Level;
using Game.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

namespace Game.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask interactable;

        [SerializeField] private Tilemap[] forbiddenTilemaps;
        [SerializeField] private Tilemap walkable;
        [SerializeField] private Tilemap finish;

        private Dictionary<Vector3Int, TileBase> vectorTileMap;
        private Dictionary<Vector3Int, TileBase> vectorObstaclesTileMap;
        private Dictionary<Vector3Int, TileBase> finishTileMap;

        private bool _canMove;

        private Coroutine _moveDelayCoroutine;
        [SerializeField] private float _moveDelay;

        private Vector3Int _startPosition;

        private void Awake()
        {
            vectorTileMap = walkable.GetTileDictionaryWithType<TileBase>();
            vectorObstaclesTileMap = new Dictionary<Vector3Int, TileBase>();

            foreach (var forbiddenTilemap in forbiddenTilemaps)
            {
                vectorObstaclesTileMap.AddRange(forbiddenTilemap.GetTileDictionaryWithType<TileBase>());
            }

            finishTileMap = finish.GetTileDictionaryWithType<TileBase>();

            _startPosition = walkable.WorldToCell(transform.position);
            transform.position = _startPosition;
            GlobalEventManager.OnGameStart.AddListener(() =>
                transform.position = walkable.GetCellCenterWorld(_startPosition));
        }

        private void Start()
        {
            _canMove = true;
            transform.position = walkable.CellToWorld(new Vector3Int(1, 1));
            GlobalEventManager.Start();
        }

        private void Update()
        {
            if (GameStateManager.IsStopped)
            {
                return;
            }

            TryMove();
        }

        private void TryMove()
        {
            if (Input.GetKey(KeyCode.Mouse1) && _canMove)
            {
                var cursorPosition = walkable.GetCursorPosition();
                var playerPosition = walkable.WorldToCell(transform.position);

                if (cursorPosition.Equals(playerPosition))
                {
                    return;
                }
                
                var moveDirection = Vector3.Normalize((cursorPosition - playerPosition));

                var distance = Vector3Int.Distance(cursorPosition, playerPosition);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, distance, interactable);
                if (hit.collider == null)
                {
                    var movePosition = playerPosition + moveDirection;
                    var movePositionInt = new Vector3Int(Mathf.RoundToInt(movePosition.x),
                        Mathf.RoundToInt(movePosition.y), 0);
                    if (vectorTileMap.TryGetValue(movePositionInt, out _)
                        && !vectorObstaclesTileMap.TryGetValue(movePositionInt, out _))
                    {
                        transform.position = walkable.GetCellCenterWorld(movePositionInt);
                        _canMove = false;
                        StartMoveDelay();
                        GlobalEventManager.Tick();
                    }

                    if (finishTileMap.TryGetValue(movePositionInt, out _)
                        && CollectableController.AllChestCollected()
                        && EnemyController.AllEnemiesDestroyed())
                    {
                        LevelController.Instance.ChangeLevel();
                    }
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