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
                    return attenuation * GetColorFromHitRecord(scatterdRay, list, depth + 1);
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
			var camera = new RayTraceCamera();
			var colors = new Color[width * height];
			var hitableList = new HitableList();
			hitableList.List.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(new Color(0.8f, 0.3f, 0.3f))));
			hitableList.List.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f, new Lambertian(new Color(0.8f, 0.8f, 0f))));
            hitableList.List.Add(new Sphere(new Vector3(1, 0, -1f), 0.5f, new Metal(new Color(0.8f, 0.6f, 0.2f))));
            hitableList.List.Add(new Sphere(new Vector3(-1, 0, -1f), 0.5f, new Dielectirc(1.5f)));

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
	}
}
