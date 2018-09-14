using UnityEngine;

namespace RayTrace
{
    public class Dielectirc : IMaterial
    {
        float eta;

        public Dielectirc(float eta)
        {
            this.eta = eta;
        }

        public bool Scatter(Ray rayIn, HitRecord record, ref Color attenuation, ref Ray scatteredRay)
        {
            Vector3 outwardNormal;
            attenuation = Color.white;
            var eta = 0f;
            if (Vector3.Dot(rayIn.NormalizedDirection, record.Normal) > 0)
            {
                outwardNormal = -record.Normal;
                eta = this.eta;
            }
            else
            {
                outwardNormal = record.Normal;
                eta = 1f / this.eta;
            }

            var refracted = Vector3.zero;
            if (RayTraceUtility.Refract(rayIn.NormalizedDirection, outwardNormal, eta, ref refracted))
            {
                scatteredRay = new Ray(record.Point, refracted);
                return true;
            }
            else
            {
                var reflected = RayTraceUtility.Reflect(rayIn.NormalizedDirection, record.Normal);
                scatteredRay = new Ray(record.Point, reflected);
                return false;
            }
        }
    }
}
