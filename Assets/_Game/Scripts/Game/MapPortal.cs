using System;
using Tofunaut.GridRPG.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Tofunaut.GridRPG.Game
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MapPortal : MonoBehaviour
    {
        public MapData ConnectedMap => _connectedMap;
        
        [SerializeField] private MapData _connectedMap;

        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("MapPortal");
        }
    }
}