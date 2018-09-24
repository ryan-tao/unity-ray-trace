using UnityEngine;

namespace RayTrace
{
    public class RayTraceUtility
    {
        public static Vector3 GetRandomPointInUnitDisk()
        {
            Vector3 t;
            t = 2f * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0f) - new Vector3(1f, 1f, 0f);
            t = t.normalized * Random.Range(0f, 1f);
            return t;
        }

        public static Vector3 GetRandomPointInUnitSphere()
        {
            Vector3 t;
            t = 2f * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) - Vector3.one;
            t = t.normalized * Random.Range(0f, 1f);
            return t;
        }

        public static Vector3 Reflect(Vector3 vectorIn, Vector3 normal)
        {
            return vectorIn - 2 * Vector3.Dot(vectorIn, normal) * normal;
        }

        public static bool Refract(Vector3 vectorIn, Vector3 normal, float eta, ref Vector3 vectorOut)
        {
            vectorIn = vectorIn.normalized;
            var idotn = Vector3.Dot(vectorIn, normal);
            var k = 1 - eta * eta * (1f - idotn * idotn);
            if (k < 0)
            {
                vectorOut = Vector3.zero;
                return false;
            }

            var a = eta * idotn + Mathf.Sqrt(k);
            vectorOut = eta * vectorIn - a * normal;
            return true;
        }

        public static float Schlick(float cosi, float eta)
        {
            var r0 = (eta - 1f) / (eta + 1);
            r0 *= r0;
            return r0 + (1f - r0) * Mathf.Pow(1 - cosi, 5f);
        }
    }
}
