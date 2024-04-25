using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class RoadController : MonoBehaviour
    {
        private float _length;
        private float _partLength;
        private Coroutine _coroutine;

        [SerializeField] private float speed;

        private void Start()
        {
            _length = transform.localScale.x;
            _partLength = _length / 2;
            ManageCoroutine(true);
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
                transform.position = new Vector3(currentPos.x + (Time.deltaTime * speed), currentPos.y, currentPos.z);
                yield return null;
            }
        }

        public void Move(Vector3 position)
        {
            var currentPos = transform.position;
            transform.position = new Vector3(position.x - _partLength, currentPos.y, currentPos.z);
        }

        public Vector3 GetEndPosition()
        {
            var currentPos = transform.position;
            return new Vector3(currentPos.x - _partLength, currentPos.y, currentPos.z);
        }

        public Vector3 GetStartPosition()
        {
            var currentPos = transform.position;
            return new Vector3(currentPos.x + _partLength, currentPos.y, currentPos.z);
        }
    }
}