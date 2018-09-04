using UnityEngine;

namespace RayTrace
{
    public class Lambertian : IMaterial
    {
        Color albedo;

        public Lambertian(Color c)
        {
            albedo = c;
        }

        public bool Scatter(Ray rayIn, HitRecord record, ref Color attenuation, ref Ray scatteredRay)
        {
            var target = record.Point + record.Normal + RayTraceUtility.GetRandomPointInUnitSphere();
            scatteredRay = new Ray(record.Point, target - record.Point);
            attenuation = albedo;
            return true;
        }
    }
}
