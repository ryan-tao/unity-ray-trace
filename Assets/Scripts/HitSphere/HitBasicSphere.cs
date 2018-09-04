using UnityEngine;

namespace RayTrace
{
	public class HitBasicSphere
	{
		const int SamplingNumber = 10;

		static Color GetColorFromHitRecord(Ray ray, HitableList list)
		{
			var rec = new HitRecord();
			if (list.Hit(ray, 0.001f, float.MaxValue, ref rec))
			{
				var target = rec.Point + rec.Normal + RayTraceUtility.GetRandomPointInUnitSphere();
				return 0.5f * GetColorFromHitRecord(new Ray(rec.Point, target - rec.Point), list);
			}

            var t = (ray.NormalizedDirection.y + 1f) * 0.5f;
            return (1 - t) * Color.white + t * new Color(0.5f, 0.7f, 1f);
        }

		public static Color[] CreateColorFromHitRecord(int width, int height)
		{
			var camera = new RayTraceCamera();
			var colors = new Color[width * height];
			var hitableList = new HitableList();
			hitableList.List.Add(new Sphere(new Vector3(0, 0, -1), 0.5f, new Lambertian(Color.white)));
			hitableList.List.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f, new Lambertian(Color.white)));
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
						color += GetColorFromHitRecord(r, hitableList);
					}
					color /= SamplingNumber;
					colors[i + j * width] = new Color(Mathf.Sqrt(color.r), Mathf.Sqrt(color.g), Mathf.Sqrt(color.b), 1f);
				}
			}

			return colors;
		}
	}
}
