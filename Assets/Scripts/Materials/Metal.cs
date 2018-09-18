using UnityEngine;

namespace RayTrace
{
    public class Metal : IMaterial
    {
        Color albedo;

        public Metal(Color c)
        {
            albedo = c;
        }

        public bool Scatter(Ray rayIn, HitRecord record, ref Color attenuation, ref Ray scatteredRay)
        {
            var target = RayTraceUtility.Reflect(rayIn.NormalizedDirection, record.Normal);
            scatteredRay = new Ray(record.Point, target);
            attenuation = albedo;
            return Vector3.Dot(record.Normal, scatteredRay.Direction) > 0;
        }
    }
}
