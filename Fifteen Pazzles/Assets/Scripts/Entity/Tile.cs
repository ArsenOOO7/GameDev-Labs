using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entity
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] public bool empty;

        public bool TryMove(Vector3 position)
        {
            var distance = Vector3.Distance(position, transform.position);
            return distance > 0 && distance < (1 + .001f);
        }


        public bool IsEmpty()
        {
            return empty;
        }


        public Tile GetRandomNeighbourTile(Tile[,] tiles)
        {
            List<Tile> neighbours = new List<Tile>();
            foreach (Tile tile in tiles)
            {
                if (TryMove(tile.transform.position))
                {
                    neighbours.Add(tile);
                }
            }

            int randomIndex = Random.Range(0, neighbours.Count);
            return neighbours[randomIndex];
        }
    }
}