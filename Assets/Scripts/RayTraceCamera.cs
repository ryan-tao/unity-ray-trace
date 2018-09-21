using UnityEngine;

namespace RayTrace
{
    public class RayTraceCamera
    {
        public Vector3 Position;
        public Vector3 LowerLeft;
        public Vector3 Horizontal;
        public Vector3 Vertical;

        public RayTraceCamera(Vector3 position, Vector3 lookAt, Vector3 up, float fov, float aspect)
        {
            var radian = fov * Mathf.PI / 180f;
            var halfHeight = Mathf.Tan(radian * 0.5f);
            var halfWidth = aspect * halfHeight;

            Position = position;
            var w = (lookAt - position).normalized;
            var u = Vector3.Cross(up, w).normalized;
            var v = Vector3.Cross(w, u).normalized;

            LowerLeft = position + w - halfWidth * u - halfHeight * v;
            Horizontal = halfWidth * 2 * u;
            Vertical = halfHeight * 2 * v;
        }

        public Ray GetRay(float u, float v)
        {
            return new Ray(Position, LowerLeft + u * Horizontal + v * Vertical - Position);
        }
    }
}
