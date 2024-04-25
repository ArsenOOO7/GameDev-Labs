using System.Collections;
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

        private void Start()
        {
            ManageCoroutine(true);
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
            while (true)
            {
                ComparePositions(firstRoad, secondRoad);
                ComparePositions(secondRoad, firstRoad);
                yield return null;
            }
        }

        private void ComparePositions(RoadController fRoad, RoadController sRoad)
        {
            var roadPos = fRoad.GetEndPosition();
            if (endTransform.position.x < roadPos.x)
            {
                fRoad.Move(sRoad.GetEndPosition());
            }
        }


        private void Update()
        {
        }
    }
}