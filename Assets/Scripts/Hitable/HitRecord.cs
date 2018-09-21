using UnityEngine;

namespace RayTrace
{
    public class HitRecord
    {
        public float Root;
        public Vector3 Point;
        public Vector3 Normal;
        public IMaterial Material;
    }
}
