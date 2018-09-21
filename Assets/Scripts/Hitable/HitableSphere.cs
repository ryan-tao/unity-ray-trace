using UnityEngine;

namespace RayTrace
{
    public class Sphere : IHitable
    {
        public Vector3 Center { get; private set; }
        public float Radius { get; private set; }
        public IMaterial Material { get; private set; }

        public Sphere(Vector3 center, float radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public bool Hit(Ray ray, float min, float max, ref HitRecord rec)
        {
            var oc = ray.Original - Center;
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = 2f * Vector3.Dot(ray.Direction, oc);
            var c = Vector3.Dot(oc, oc) - Radius * Radius;
            var discriminant = b * b - 4 * a * c;
            if (discriminant > 0)
            {
                float r = (-b - Mathf.Sqrt(discriminant)) / (2f * a);
                if (r > min && r < max)
                {
                    rec.Root = r;
                    rec.Point = ray.GetPoint(r);
                    rec.Normal = (rec.Point - Center).normalized;
                    rec.Material = Material;
                    return true;
                }

                r = (-b + Mathf.Sqrt(discriminant)) / (2f * a);
                if (r > min && r < max)
                {
                    rec.Root = r;
                    rec.Point = ray.GetPoint(r);
                    rec.Normal = (rec.Point - Center).normalized;
                    rec.Material = Material;
                    return true;
                }
            }

            return false;
        }
    }
}
