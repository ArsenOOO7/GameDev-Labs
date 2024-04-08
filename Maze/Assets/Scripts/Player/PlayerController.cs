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
                _points += 200;
                UIEventManager.ChangeScore(_points);
                CheckGameState();
            }
        }

        private void CheckGameState()
        {
            if (_collectedTargets >= totalTargets)
            {
                GlobalEventManager.FinishGame();
            }
        }
    }
}