using UnityEngine;

namespace RayTrance
{
	public class HitSphere
	{
		const int SamplingNumber = 10;

		static Vector3 GetRandomPointInUnitSphere()
		{
			Vector3 t;
			t = 2f * new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) - Vector3.one;
			t = t.normalized * Random.Range(0f, 1f);
			return t;
		}

		static Color GetColorFromHitRecord(Ray ray, HitableList list)
		{
			var rec = new HitRecord();
			if (list.Hit(ray, 0.001f, float.MaxValue, ref rec))
			{
				var target = rec.Point + rec.Normal + GetRandomPointInUnitSphere();
				return 0.5f * GetColorFromHitRecord(new Ray(rec.Point, target - rec.Point), list);
			}

			//var t = (ray.NormalizedDirection.y + 1f) * 0.5f;
			//return (1 - t) * Color.white + t * new Color(0.5f, 0.7f, 1f);
			return Color.yellow;
		}

		public static Color[] CreateColorFromHitRecord(int width, int height)
		{
			var camera = new RayTraceCamera();
			var colors = new Color[width * height];
			var hitableList = new HitableList();
			hitableList.List.Add(new Sphere(new Vector3(0, 0, -1), 0.5f));
			hitableList.List.Add(new Sphere(new Vector3(0, -100.5f, -1), 100f));
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
