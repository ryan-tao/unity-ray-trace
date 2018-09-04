using UnityEngine;

namespace RayTrace
{
    public class RayTraceUtility
    {
        public static Vector3 GetRandomPointInUnitSphere()
        {
            Vector3 t;
            t = 2f * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) - Vector3.one;
            t = t.normalized * Random.Range(0f, 1f);
            return t;
        }
    }
}
