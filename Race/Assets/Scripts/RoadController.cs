using System.Collections;
using Event;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private float length;
        private float _partLength;
        private Coroutine _coroutine;

        private float _speed = 10;

        private void Start()
        {
            _partLength = length / 2;
            ManageCoroutine(true);
            RoadEventManager.OnPlayerSpeedChanges.AddListener(newSpeed => _speed = newSpeed);
        }

        private void ManageCoroutine(bool start)
        {
            if (start)
            {
                _coroutine ??= StartCoroutine(MoveRoad());
            }
            else
            {
                if (_coroutine != null)
                {
                    StopCoroutine(_coroutine);
                    _coroutine = null;
                }
            }
        }

        private IEnumerator MoveRoad()
        {
            while (true)
            {
                var currentPos = transform.position;
                transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z - (Time.deltaTime * _speed));
                yield return null;
            }
        }

        public void Move(Vector3 position)
        {
            var currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y, position.z + _partLength);
        }

        public Vector3 GetEndPosition()
        {
            var currentPos = transform.position;
            return new Vector3(currentPos.x, currentPos.y, currentPos.z + _partLength);
        }
    }
}