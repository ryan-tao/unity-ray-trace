using UnityEngine;

namespace RayTrace
{
    public class RayTraceCamera
    {
        public Vector3 Position;
        public Vector3 LowerLeft;
        public Vector3 Horizontal;
        public Vector3 Vertical;

        float lensRadius;
        Vector3 u, v;

        public RayTraceCamera(Vector3 position, Vector3 lookAt, Vector3 up, float fov, float aspect, float aperture, float focusDist)
        {
            lensRadius = aperture / 2f;
            var radian = fov * Mathf.PI / 180f;
            var halfHeight = Mathf.Tan(radian * 0.5f);
            var halfWidth = aspect * halfHeight;

            Position = position;
            var w = (lookAt - position).normalized;
            u = Vector3.Cross(up, w).normalized;
            v = Vector3.Cross(w, u).normalized;

            LowerLeft = position + (w - halfWidth * u - halfHeight * v) * focusDist;
            Horizontal = halfWidth * 2 * focusDist * u;
            Vertical = halfHeight * 2 * focusDist * v;
        }

        public Ray GetRay(float s, float t)
        {
            var lensPos = lensRadius * RayTraceUtility.GetRandomPointInUnitDisk();
            var offset = lensPos.x * u + lensPos.y * v;
            return new Ray(Position + offset, LowerLeft + s * Horizontal + t * Vertical - Position - offset);
        }
    }
}
