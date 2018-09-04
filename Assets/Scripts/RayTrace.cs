using UnityEngine;
using UnityEngine.UI;

namespace RayTrace
{
	public class RayTrace : MonoBehaviour
	{
		const int width = 800;
		const int height = 400;
		Texture2D texture;

		[SerializeField]
		RawImage image;
		
		// Use this for initialization
		void Start()
		{
			texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
			texture.SetPixels(HitMetalSphere.CreateColorFromHitRecord(width, height));
			texture.Apply();

			image.texture = texture;
		}

		void OnDestroy()
		{
			Destroy(texture);
			texture = null;
		}
	}
}
