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
            var cosi = 0f;
            var reflectProb = 0f;
            if (Vector3.Dot(rayIn.Direction, record.Normal) > 0)
            {
                outwardNormal = -record.Normal;
                eta = this.eta;
                cosi = eta * Vector3.Dot(rayIn.NormalizedDirection, record.Normal);
            }
            else
            {
                outwardNormal = record.Normal;
                eta = 1f / this.eta;
                cosi = -Vector3.Dot(rayIn.NormalizedDirection, record.Normal);
            }

            var refracted = Vector3.zero;
            if (RayTraceUtility.Refract(rayIn.Direction, outwardNormal, eta, ref refracted))
            {
                reflectProb = RayTraceUtility.Schlick(cosi, eta);
            }
            else
            {
                reflectProb = 1f;
            }

            if (Random.Range(0f, 1f) <= reflectProb)
            {
                var reflected = RayTraceUtility.Reflect(rayIn.Direction, record.Normal);
                scatteredRay = new Ray(record.Point, reflected);
            }
            else
            {
                scatteredRay = new Ray(record.Point, refracted);
            }

            return true;
        }
    }
}
