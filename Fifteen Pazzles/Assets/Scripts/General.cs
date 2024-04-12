using System;
using Entity;
using NUnit.Framework.Constraints;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace DefaultNamespace
{
    public class General : MonoBehaviour
    {
        private static readonly String TILE_TAG = "Tile";
        private static readonly int BOARD_HEIGHT = 4;
        private static readonly int BOARD_WIDTH = 4;

        public static readonly Vector3[] AVAILABLE_DIRECTIONS =
            { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };


        private Tile _emptyTile;
        private Tile[,] _tiles = new Tile[4, 4];

        [SerializeField] private LayerMask layerMask;

        private bool finished = false;

        private void Start()
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(TILE_TAG);

            Tile tile;
            int[] indexes;
            for (int i = 0; i < gameObjects.Length; ++i)
            {
                tile = gameObjects[i].GetComponent<Tile>();
                indexes = ExtractIndex(tile.transform.position);
                _tiles[indexes[0], indexes[1]] = tile;
            }

            _emptyTile = _tiles[3, 3];
            RandomShuffle(10);
        }

        private void RandomShuffle(int shuffleCount)
        {
            Tile tile;
            for (int i = 0; i < shuffleCount; i++)
            {
                tile = _emptyTile.GetRandomNeighbourTile(_tiles);
                SwapTiles(_emptyTile, tile);
            }
        }

        private void SwapTiles(Tile first, Tile second)
        {
            Vector3 tempPos;
            Vector3 firstPos = first.transform.position;
            Vector3 secondPos = second.transform.position;

            tempPos = firstPos;
            first.transform.position = secondPos;
            second.transform.position = tempPos;
        }

        private bool IsPazzleSolved()
        {
            Tile tile;
            for (int i = 0; i < BOARD_HEIGHT; ++i)
            {
                for (int j = 0; j < BOARD_WIDTH; ++j)
                {
                    tile = _tiles[i, j];
                    if (tile.transform.position != new Vector3(i + 1.5f, 0.1f, j + 1.5f))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static int[] ExtractIndex(Vector3 position)
        {
            int x = (int)(position.x - 1.5f);
            int z = (int)(position.z - 1.5f);
            return new[] { x, z };
        }

        private void Update()
        {
            if (finished)
            {
                return;
            }

            if (IsPazzleSolved())
            {
                Console.WriteLine("Pazzle is solved");
                finished = true;
            }
        }

        public void OnMouseDownClick(GameObject pazzle)
        {
            if (finished)
            {
                return;
            }

            Tile tile = pazzle.GetComponent<Tile>();
            if (tile != null && tile.TryMove(_emptyTile.transform.position))
            {
                SwapTiles(tile, _emptyTile);
            }
        }

        private Vector3 GetAvailableDirections(Vector3 pazzlePos)
        {
            foreach (var direction in AVAILABLE_DIRECTIONS)
            {
                if (!Physics.Raycast(pazzlePos, direction, 1.25f, layerMask))
                {
                    return direction;
                }
            }

            return Vector3.zero;
        }
    }
}