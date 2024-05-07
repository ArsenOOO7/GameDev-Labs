using System.Collections.Generic;
using Game.Utils;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Collectable
{
    public class CollectableController : MonoBehaviour
    {
        [SerializeField] private GameObject chest;
        [SerializeField] private GameObject openChest;

        private Tilemap _tilemap;
        private Dictionary<Vector3Int, TileBase> tiles;

        private static int OpenedChests = 0;
        private static int AllChests = 0;

        private void Awake()
        {
            _tilemap = GetComponentInChildren<Tilemap>();
            tiles = _tilemap.GetTileDictionaryWithType<TileBase>();
            ++AllChests;
        }

        public bool MyTile()
        {
            return tiles.TryGetValue(_tilemap.GetCursorPosition(), out _);
        }

        public void OpenChest()
        {
            chest.SetActive(false);
            openChest.SetActive(true);
            ++OpenedChests;
        }

        public static bool AllChestCollected()
        {
            return AllChests <= OpenedChests;
        }
    }
}