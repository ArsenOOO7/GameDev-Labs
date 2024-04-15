using System;
using Entity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Util
{
    public class ColorUtils : MonoBehaviour
    {
        private static readonly string[] MAT_NAMES =
            { "Pazzles/Zeleny", "Pazzles/Red", "Pazzles/Blue", "Pazzles/Vadym" };

        public static void FillMaterials(Tile[,] tiles)
        {
            for (int i = 0; i < PositionUtils.BOARD_HEIGHT; ++i)
            {
                for (int j = 0; j < PositionUtils.BOARD_WIDTH; ++j)
                {
                    FillColor(tiles[i, j]);
                }
            }
        }

        private static void FillColor(Tile tile)
        {
            string matName = GetRandomMatName();
            Material material = Resources.Load<Material>(matName);
            if (material != null)
            {
                tile.GetComponentInChildren<MeshRenderer>().material = material;
            }
            else
            {
                Console.WriteLine("Material is not found! (" + matName + ")");
            }
        }

        private static string GetRandomMatName()
        {
            return MAT_NAMES[Random.Range(0, MAT_NAMES.Length)];
        }
    }
}