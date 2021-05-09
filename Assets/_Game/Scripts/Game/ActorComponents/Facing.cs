using System;
using UnityEngine;

namespace Tofunaut.GridRPG.Game.ActorComponents
{
    public class Facing : ActorComponent
    {
        public enum EDirection
        {
            East,
            South,
            West,
            North,
        }
        
        public EDirection Direction { get; private set; }

        private void Update()
        {
            var input = Actor.Input;
            if (input.Direction)
                Direction = Vector2ToDirection(input.Direction);
        }

        public static EDirection Vector2ToDirection(Vector2 v)
        {
            var highestDot = 0f;
            var index = 0;
            var vectors = new[]
            {
                Vector2.right,
                Vector2.down,
                Vector2.left,
                Vector2.up,
            };
            var directions = (EDirection[])Enum.GetValues(typeof(EDirection));
            for(var i = 0; i < vectors.Length; i++)
            {
                var dot = Vector2.Dot(v, vectors[i]);
                if (!(dot > highestDot)) 
                    continue;
                
                index = i;
                highestDot = dot;
            }

            return directions[index];
        }

        public static Vector2 DirectionToVector2(EDirection dir)
        {
            return dir switch
            {
                EDirection.East => Vector2.right,
                EDirection.South => Vector2.down,
                EDirection.West => Vector2.left,
                EDirection.North => Vector2.up,
                _ => Vector2.zero
            };
        }
    }
}