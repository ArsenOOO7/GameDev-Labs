using DefaultNamespace;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _points = 0f;
        private int _collectedTargets = 0;

        public int totalTargets;

        [SerializeField] private GameObject targetParent;

        private void Start()
        {
            totalTargets = targetParent.transform.childCount;
        }


        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag.Equals("Target"))
            {
                Destroy(collider.gameObject);
                ++_collectedTargets;
                AddPoints(200);
                CheckGameState();
            }

            if (collider.gameObject.CompareTag("IWalls"))
            {
                AddPoints(-250f);
            }
        }

        private void CheckGameState()
        {
            if (_collectedTargets >= totalTargets)
            {
                GlobalEventManager.FinishGame();
            }
        }

        public void AddPoints(float points)
        {
            _points += points;
            UIEventManager.ChangeScore(_points);
        }
    }
}