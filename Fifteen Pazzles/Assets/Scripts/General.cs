using System;
using DefaultNamespace.Util;
using Entity;
using UnityEngine;

namespace DefaultNamespace
{
    public class General : MonoBehaviour
    {
        private static readonly String TILE_TAG = "Tile";

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
                indexes = PositionUtils.ExtractIndex(tile.transform.position);
                _tiles[indexes[0], indexes[1]] = tile;
            }

            _emptyTile = _tiles[3, 3];
            ColorUtils.FillMaterials(_tiles);
            PositionUtils.RandomShuffle(_tiles, _emptyTile, 30);
        }

        private bool IsPazzleSolved()
        {
            Tile tile;
            for (int i = 0; i < PositionUtils.BOARD_HEIGHT; ++i)
            {
                for (int j = 0; j < PositionUtils.BOARD_WIDTH; ++j)
                {
                    tile = _tiles[i, j];
                    if (tile.transform.position != new Vector3(i + PositionUtils.ERROR_X, 0.1f, j + PositionUtils.ERROR_Z))
                    {
                        return false;
                    }
                }
            }

            return true;
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
                PositionUtils.SwapTiles(tile, _emptyTile);
            }
        }
    }
}