using System;
using Game.Collectable;
using Game.Enemy;
using Game.Obstacle;
using Game.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Player
{
    public class PlayerActionController : MonoBehaviour
    {
        [SerializeField] private CollectableController[] collectables;
        [SerializeField] private Tilemap walkable;
        [SerializeField] private LayerMask enemy, interactable;
        [SerializeField] private float damage = 30f;

        private void Update()
        {
            /*while (true)
            {
                while (true)
                {
                    while (true)
                    {
                        while (true)
                        {
                            break;
                        }
                        break;
                    }
                    break;
                }
                break;
            }*/
            TryAttack();
        }

        private void TryAttack()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                var cursorPosition = walkable.GetCursorPosition();
                var position = walkable.WorldToCell(transform.position);
                var distance = Vector3.Distance(cursorPosition, position);
                var moveDirection = Vector3.Normalize((cursorPosition - position));

                if (distance <= 1)
                {
                    var hit = Physics2D.Raycast(transform.position, moveDirection, distance, enemy);
                    if (hit.collider != null)
                    {
                        var enemyController = hit.collider.GetComponent<EnemyController>();
                        enemyController.TakeDamage(damage);
                        return;
                    }
                    
                    hit = Physics2D.Raycast(transform.position, moveDirection, distance, interactable);
                    if (hit.collider != null && hit.collider.GetType().Equals(typeof(TilemapCollider2D)))
                    {
                        //sheet, no shit - Pomircovana
                        var tileObstacle = hit.collider.GetComponentInParent<ObstacleController>();
                        tileObstacle.DestroyTile(cursorPosition);
                        return;
                    }
                    
                    foreach (var collectableController in collectables)
                    {
                        if (collectableController.MyTile())
                        {
                            collectableController.OpenChest();
                            return;
                        }
                    }                    
                }
            }
        }
    }
}