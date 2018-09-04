using UnityEngine;

namespace RayTrace
{
	public interface IMaterial
	{
        bool Scatter(Ray rayIn, HitRecord record, ref Color attenuation, ref Ray scatteredRay);
	}
}
