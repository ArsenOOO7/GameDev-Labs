using Entity;
using UnityEngine;

namespace DefaultNamespace.Util
{
    public class PositionUtils : MonoBehaviour
    {
        
        public static readonly int BOARD_HEIGHT = 4;
        public static readonly int BOARD_WIDTH = 4;

        public static readonly float ERROR_X = 1.5f;
        public static readonly float ERROR_Z = 1.5f;
        
        public static void RandomShuffle(Tile[,] tiles, Tile emptyTile, int iterations)
        {
            Tile tile;
            for (int i = 0; i < iterations; i++)
            {
                tile = emptyTile.GetRandomNeighbourTile(tiles);
                SwapTiles(emptyTile, tile);
            }
        }

        public static void SwapTiles(Tile first, Tile second)
        {
            Vector3 tempPos;
            Vector3 firstPos = first.transform.position;
            Vector3 secondPos = second.transform.position;

            tempPos = firstPos;
            first.transform.position = secondPos;
            second.transform.position = tempPos;
        }

        public static int[] ExtractIndex(Vector3 position)
        {
            int x = (int)(position.x - ERROR_X);
            int z = (int)(position.z - ERROR_Z);
            return new[] { x, z };
        }
    }
}