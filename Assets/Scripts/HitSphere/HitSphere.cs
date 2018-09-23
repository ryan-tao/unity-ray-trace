using UnityEngine;

namespace RayTrace
{
    public class HitSphere
    {
        const int MaxDepth = 50;
        const int SamplingNumber = 10;

        static Color GetColorFromHitRecord(Ray ray, HitableList list, int depth)
        {
            var record = new HitRecord();
            if (list.Hit(ray, 0.001f, float.MaxValue, ref record))
            {
                var scatterdRay = new Ray(Vector3.zero, Vector3.zero);
                var attenuation = Color.black;
                if (depth < MaxDepth && record.Material.Scatter(ray, record, ref attenuation, ref scatterdRay))
                {
                    var c = GetColorFromHitRecord(scatterdRay, list, depth + 1);
                    return new Color(c.r * attenuation.r, c.g * attenuation.g, c.b * attenuation.b);
                }
                else
                {
                    return Color.black;
                }
            }

            var t = (ray.NormalizedDirection.y + 1f) * 0.5f;
            return (1 - t) * Color.white + t * new Color(0.5f, 0.7f, 1f);
        }

        public static Color[] CreateColorFromHitRecord(int width, int height)
        {
            var camPos = new Vector3(10f, 2f, -3f);
            var camLookAt = new Vector3(0f, 1f, 0f);
            var focusDist = (camLookAt - camPos).magnitude;
            var camera = new RayTraceCamera(camPos, camLookAt, Vector3.up, 40f, 2f, 0f, focusDist);

            var colors = new Color[width * height];
            var hitableList = GetHitableListFromScene();

            for (int j = height - 1; j >= 0; j--)
            {
                for (int i = 0; i < width; i++)
                {
                    var color = new Color();
                    for (int s = 0; s < SamplingNumber; s++)
                    {
                        var u = (i + Random.Range(0, 1f)) / width;
                        var v = (j + Random.Range(0, 1f)) / height;
                        var r = camera.GetRay(u, v);
                        color += GetColorFromHitRecord(r, hitableList, 2);
                    }
                    color /= SamplingNumber;
                    colors[i + j * width] = new Color(Mathf.Sqrt(color.r), Mathf.Sqrt(color.g), Mathf.Sqrt(color.b), 1f);
                }
            }

            return colors;
        }

        static HitableList GetHitableListFromScene()
        {
            var hitableList = new HitableList();
            hitableList.List.Add(new Sphere(new Vector3(0f, -1000f, 0f), 1000f, new Lambertian(new Color(0.5f, 0.5f, 0.5f))));
            for (int a = -4; a < 4; a++)
            {
                for (var b = -4; b < 4; b++)
                {
                    var chooseMat = Random.Range(0f, 1f);
                    var center = new Vector3(a + 0.9f * Random.Range(0f, 1f), 0.2f, b + 0.9f * Random.Range(0f, 1f));

                    if (Vector3.Distance(center, new Vector3(4f, 0.2f, 4f)) > 0.9f)
                    {
                        if (chooseMat < 0.8f)
                        {
                            hitableList.List.Add(new Sphere(center, 0.2f, new Lambertian(GetRandomColor())));
                        }
                        else if (chooseMat < 0.95f)
                        {
                            hitableList.List.Add(new Sphere(center, 0.2f, new Metal(GetRandomMatalColor())));
                        }
                        else
                        {
                            hitableList.List.Add(new Sphere(center, 0.2f, new Dielectirc(1.5f)));
                        }
                    }
                }
            }

            hitableList.List.Add(new Sphere(new Vector3(0f, 1f, 0f), 1f, new Dielectirc(1.5f)));
            hitableList.List.Add(new Sphere(new Vector3(-4f, 1f, 0f), 1f, new Lambertian(new Color(0.4f, 0.2f, 0.5f))));
            hitableList.List.Add(new Sphere(new Vector3(4f, 1f, 0f), 1f, new Metal(new Color(0.7f, 0.6f, 0.5f))));
            return hitableList;
        }

        static HitableList GetHitableListFromSimpleScene()
        {
            var hitableList = new HitableList();
            hitableList.List.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Color(0.8f, 0.3f, 0.3f))));
            hitableList.List.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f, new Lambertian(new Color(0.8f, 0.8f, 0f))));
            hitableList.List.Add(new Sphere(new Vector3(1, 0, -1f), 0.5f, new Metal(new Color(0.8f, 0.6f, 0.2f))));
            hitableList.List.Add(new Sphere(new Vector3(-1, 0, -1f), 0.5f, new Dielectirc(1.2f)));
            return hitableList;
        }

        static Color GetRandomColor()
        {
            return new Color(
                Random.Range(0f, 1f) * Random.Range(0f, 1f),
                Random.Range(0f, 1f) * Random.Range(0f, 1f),
                Random.Range(0f, 1f) * Random.Range(0f, 1f));
        }

        static Color GetRandomMatalColor()
        {
            return new Color(
                0.5f * (1f + Random.Range(0f, 1f)),
                0.5f * (1f + Random.Range(0f, 1f)),
                0.5f * (1f + Random.Range(0f, 1f)));
        }
    }
}
