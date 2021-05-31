using Tofunaut.Core.Utilities;
using UnityEngine;

namespace Tofunaut.GridRPG.Game.ActorComponents
{
    public class Mover : ActorComponent
    {
        private const float HesitationDuration = 0.1f;
        
        public bool IsMoving { get; private set; }

        [SerializeField] private float _moveSpeed;

        private Transform _t;
        private Vector2 _targetPosition;
        private Facing.EDirection _prevMoveDirection;

        protected override void Awake()
        {
            base.Awake();
            
            _t = transform;
        }

        private void Update()
        {
            var currentMoveDirection = Facing.Vector2ToDirection(Actor.Input.Direction);
            if (!IsMoving && Actor.Input.Direction && Actor.Input.Direction.SignalDuration > HesitationDuration)
            {
                var potentialTargetPos = Actor.Coord + Facing.DirectionToVector2(currentMoveDirection);
                if (!CanMoveToPosition(potentialTargetPos))
                    return;
                
                _prevMoveDirection = currentMoveDirection;
                _targetPosition = potentialTargetPos;
                IsMoving = true;
            }

            // do nothing if we're still not moving
            if (!IsMoving) 
                return;
            
            var moveDistance = _moveSpeed * Time.deltaTime;
            var toTarget = _targetPosition - (Vector2)_t.position;
                
            // check if we've reached our target
            if (toTarget.sqrMagnitude < moveDistance * moveDistance)
            {
                var isStillMoving = false;
                
                // continue moving if Actor.Input.Direction is held
                if (Actor.Input.Direction)
                {
                    var prevTargetPosition = _targetPosition;
                    var potentialTargetPosition = Actor.Coord + Facing.DirectionToVector2(Facing.Vector2ToDirection(Actor.Input.Direction));
                    if(CanMoveToPosition(potentialTargetPosition))
                    {
                        _targetPosition = potentialTargetPosition;
                        toTarget = _targetPosition - (Vector2)_t.position;
                        
                        // if we're changing direction, snap to the target, then move the remaining distance to the new target
                        if (_prevMoveDirection != currentMoveDirection)
                        {
                            moveDistance -= (prevTargetPosition - (Vector2)_t.position).magnitude;
                            _t.position = prevTargetPosition;
                            _prevMoveDirection = currentMoveDirection;
                        }
                    
                        isStillMoving = true;
                    }
                }
                
                // stop moving
                if(!isStillMoving)
                {
                    _t.position = _targetPosition;
                    moveDistance = 0f;
                    IsMoving = false;
                }
            }

            // step to the target position
            _t.position += (Vector3)toTarget.normalized * moveDistance;
        }

        private bool CanMoveToPosition(Vector2 position)
        {
            if (GameContext.MapManager.CurrentMap.SolidTileMap.HasTile((Vector3Int)position.RoundToVector2Int() - new Vector3Int(1, 1, 0)))
                return false;
            
            var results = new Collider2D[1];
            var numResults = Physics2D.OverlapCircleNonAlloc(position, 0.4f, results, LayerMask.GetMask("Actor"));
            return numResults == 0;
        }
    }
}