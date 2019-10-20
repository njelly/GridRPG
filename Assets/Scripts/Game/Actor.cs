////////////////////////////////////////////////////////////////////////////////
//
//  Actor (c) 2019 Tofunaut
//
//  Created by Nathaniel Ellingson for TofuUnity on 10/19/2019
//
////////////////////////////////////////////////////////////////////////////////

using UnityEngine;

namespace Tofunaut.GridRPG.Game
{
    public enum CardinalDirection
    {
        None = 0,
        North = 1 << 0,
        South = 1 << 1,
        East = 1 << 2,
        West = 1 << 3,
    }

    public abstract class Actor
    {
        public struct Input
        {
            public bool interact;
            public Vector2 direction;
        }

        abstract public void ProcessInput(Input input);

        public static CardinalDirection VectorToCardinalDirection(Vector2 v)
        {
            if(Mathf.Approximately(v.sqrMagnitude, 0f))
            {
                return CardinalDirection.None;
            }

            Vector2[] directions =
            {
                Vector2.right,
                Vector2.up,
                Vector2.left,
                Vector2.down,
            };

            float bestResult = float.MinValue;
            int index = -1;
            for(int i = 0; i < directions.Length; i++)
            {
                float alignedness = Vector2.Dot(directions[i], v);
                if (alignedness > bestResult)
                {
                    bestResult = alignedness;
                    index = i;
                }
            }

            switch (index)
            {
                case 0:
                    return CardinalDirection.East;
                case 1:
                    return CardinalDirection.North;
                case 2:
                    return CardinalDirection.West;
                case 3:
                    return CardinalDirection.South;
                default:
                    // this should never happen, this code has been tested
                    Debug.LogErrorFormat("failed to convert the vector {0} to a cardinal direction", v.ToString("F2"));
                    return CardinalDirection.None;
            }
        }
    }
}