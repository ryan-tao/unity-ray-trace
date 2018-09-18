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
            var dotDN = Vector3.Dot(rayIn.NormalizedDirection, record.Normal);
            if ( dotDN > 0)
            {
                outwardNormal = -record.Normal;
                eta = this.eta;
                cosi = eta * dotDN;
            }
            else
            {
                outwardNormal = record.Normal;
                eta = 1f / this.eta;
                cosi = -dotDN;
            }

            var refracted = Vector3.zero;
            if (RayTraceUtility.Refract(rayIn.NormalizedDirection, outwardNormal, eta, ref refracted))
            {
                reflectProb = RayTraceUtility.Schlick(cosi, eta);
            }
            else
            {
                reflectProb = 1f;
            }

            if (Random.Range(0f, 1f) <= reflectProb)
            {
                var reflected = RayTraceUtility.Reflect(rayIn.NormalizedDirection, record.Normal);
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
