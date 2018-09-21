using UnityEngine;

namespace RayTrace
{
    public class Ray
    {
        public Vector3 Original { get; private set; }
        public Vector3 Direction { get; private set; }
        public Vector3 NormalizedDirection { get; private set; }

        public Ray(Vector3 o, Vector3 d)
        {
            Original = o;
            Direction = d;
            NormalizedDirection = Direction.normalized;
        }

        public Vector3 GetPoint(float t)
        {
            return Original + t * Direction;
        }
    }
}
