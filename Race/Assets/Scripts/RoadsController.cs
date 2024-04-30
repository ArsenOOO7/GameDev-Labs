using System.Collections;
using Event;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class RoadsController : MonoBehaviour
    {
        [SerializeField] private RoadController firstRoad;
        [SerializeField] private RoadController secondRoad;

        [FormerlySerializedAs("endPosition")] [SerializeField]
        private Transform endTransform;

        private Coroutine _coroutine;
        
        private bool _gameFinished = false;


        private void Start()
        {
            ManageCoroutine(true);
            GlobalEventManager.OnGameStop.AddListener(() =>
            {
                _gameFinished = true;
            });
        }

        private void ManageCoroutine(bool start)
        {
            if (start)
            {
                _coroutine ??= StartCoroutine(MoveRoads());
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

        private IEnumerator MoveRoads()
        {
            while (!_gameFinished)
            {
                ComparePositions(firstRoad, secondRoad);
                ComparePositions(secondRoad, firstRoad);
                yield return null;
            }
            ManageCoroutine(false);
        }

        private void ComparePositions(RoadController fRoad, RoadController sRoad)
        {
            var roadPos = fRoad.GetEndPosition();
            if (endTransform.position.z > roadPos.z)
            {
                fRoad.Move(sRoad.GetEndPosition());
            }
        }


        private void Update()
        {
        }
    }
}