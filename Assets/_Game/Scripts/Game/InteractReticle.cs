using DG.Tweening;
using Tofunaut.Core.Utilities;
using Tofunaut.GridRPG.Game.ActorComponents;
using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractReticle : MonoBehaviour
    {
        private const float FacingAnimDuration = 0.2f;
        private const float InteractingAlphaAnimDuration = 0.2f;
        private const float NotInteractingAlpha = 0.2f;
        private const float InteractingAlpha = 0.6f;
        private static readonly Color32 NeutralColor = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        
        private Actor _actor;
        private Facing _actorFacing;
        private Facing.EDirection _prevFacing;
        private Transform _t;
        private SpriteRenderer _spriteRenderer;
        private bool _wasInteracting;
        private float _currentAlpha;
        private Color _currentColor;

        private void Awake()
        {
            _actor = GetComponentInParent<Actor>();
            _actorFacing = GetComponentInParent<Facing>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _prevFacing = _actorFacing.Direction;
            _t = transform;
            _currentAlpha = NotInteractingAlpha;
            _currentColor = NeutralColor;
        }

        private void Update()
        {
            UpdateFacing();
            UpdateColor();
        }

        private void UpdateFacing()
        {
            var currentFacing = _actorFacing.Direction;
            if (currentFacing == _prevFacing) 
                return;
            
            _prevFacing = currentFacing;
            var currentAngle = Vector2.SignedAngle(_t.localPosition, Vector2.right) * Mathf.Deg2Rad;
            var toAngle = Vector2.SignedAngle(Facing.DirectionToVector2(currentFacing), Vector2.right) * Mathf.Deg2Rad;
            toAngle = currentAngle + MathfUtils.SmallestAngleDifferenceRad(currentAngle, toAngle);
                
            DOTween.To(() => currentAngle, x =>
            {
                currentAngle = x;
                _t.localPosition = new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(-currentAngle));
            }, toAngle, FacingAnimDuration);
        }

        private void UpdateColor()
        {
            var isInteracting = _actor.Input.Interacting;
            if (isInteracting != _wasInteracting)
            {
                _wasInteracting = isInteracting;
                var targetAlpha = isInteracting ? InteractingAlpha : NotInteractingAlpha;
                DOTween.To(() => _currentAlpha, x =>
                {
                    _currentAlpha = x;
                    _spriteRenderer.color = new Color(_currentColor.r, _currentColor.g, _currentColor.b, _currentAlpha);
                }, targetAlpha, InteractingAlphaAnimDuration);
            }
        }
    }
}