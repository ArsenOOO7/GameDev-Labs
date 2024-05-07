using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Game.Obstacle
{
    public class ObstacleController : MonoBehaviour
    {
        [SerializeField] private Tilemap barrierTileMap;
        [SerializeField] private Tilemap patternTileMap;
        [SerializeField] private TileBase baseBarrier;
        [SerializeField] private TileBase patternTileBase;
        [SerializeField] private float barrierSpawnChance;

        private Vector3Int[] _fieldPosition;


        private void Awake()
        {
            _fieldPosition = patternTileMap.GetTilePositionWithType<TileBase>(patternTileBase.GetType());
        }

        private void Start()
        {
            RandomizeBarriers();
        }

        private void RandomizeBarriers()
        {
            List<Vector3> positions = new List<Vector3>();
            foreach (Vector3Int fieldPos in _fieldPosition)
            {
                if (Random.Range(0, 100) > barrierSpawnChance)
                {
                    positions.Add(barrierTileMap.CellToWorld(fieldPos));
                }
            }
            AddBarriersToTileMap(positions.ToArray());
        }

        private void AddBarriersToTileMap(Vector3[] positions)
        {
            foreach (Vector3 position in positions)
            {
                Vector3Int gridPosition = barrierTileMap.WorldToCell(position);
                barrierTileMap.SetTile(gridPosition, baseBarrier);
            }
        }

        public void DestroyTile(Vector3 position)
        {
            Vector3Int gridPosition = barrierTileMap.WorldToCell(position);
            barrierTileMap.SetTile(gridPosition, null);
        }
    }
}