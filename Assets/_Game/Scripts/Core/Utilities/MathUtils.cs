using UnityEngine;

namespace Tofunaut.Core.Utilities
{
    public static class MathfUtils
    {
        public static float SmallestAngleDifferenceDeg(float fromDeg, float toDeg)
        {
            var diff = (toDeg - fromDeg + 180f) % 360f - 180f;
            return diff < -180f ? diff + 360f : diff;
        }
        
        public static float SmallestAngleDifferenceRad(float fromRad, float toRad)
        {
            return SmallestAngleDifferenceDeg(fromRad * Mathf.Rad2Deg, toRad * Mathf.Rad2Deg) * Mathf.Deg2Rad;
        }
    }
}