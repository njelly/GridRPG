using Cinemachine;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public class GameCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [SerializeField] private CinemachineConfiner _cinemachineConfiner;

        public void SetTarget(Transform target)
        {
            _cinemachineVirtualCamera.Follow = target;
        }

        public void SetBounds(PolygonCollider2D boundsCollider)
        {
            _cinemachineConfiner.m_BoundingShape2D = boundsCollider;
        }
    }
}