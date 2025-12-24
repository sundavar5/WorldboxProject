using System;
using UnityEngine;

// Token: 0x0200040D RID: 1037
public static class PixelDetector
{
	// Token: 0x060023E5 RID: 9189 RVA: 0x0012BAF4 File Offset: 0x00129CF4
	public static bool GetSpritePixelColorUnderMousePointer(MonoBehaviour mono, out Vector2Int pVector)
	{
		pVector = new Vector2Int(-1, -1);
		Vector2 mousePos = Input.mousePosition;
		Vector2 viewportPos = Camera.main.ScreenToViewportPoint(mousePos);
		if (viewportPos.x <= 0f || viewportPos.x >= 1f || viewportPos.y <= 0f || viewportPos.y >= 1f)
		{
			return false;
		}
		Ray ray;
		try
		{
			ray = Camera.main.ViewportPointToRay(viewportPos);
		}
		catch (Exception)
		{
			return false;
		}
		return PixelDetector.IntersectsSprite(mono, ray, out pVector);
	}

	// Token: 0x060023E6 RID: 9190 RVA: 0x0012BB98 File Offset: 0x00129D98
	private static bool IntersectsSprite(MonoBehaviour mono, Ray ray, out Vector2Int pVector)
	{
		SpriteRenderer spriteRenderer = mono.GetComponent<SpriteRenderer>();
		pVector = new Vector2Int(-1, -1);
		if (spriteRenderer == null)
		{
			return false;
		}
		Sprite sprite = spriteRenderer.sprite;
		if (sprite == null)
		{
			return false;
		}
		Texture2D texture = sprite.texture;
		if (texture == null)
		{
			return false;
		}
		if (sprite.packed && sprite.packingMode == SpritePackingMode.Tight)
		{
			Debug.LogError("SpritePackingMode.Tight atlas packing is not supported!");
			return false;
		}
		Plane plane = new Plane(mono.transform.forward, mono.transform.position);
		float rayIntersectDist;
		if (!plane.Raycast(ray, out rayIntersectDist))
		{
			return false;
		}
		Vector3 vector = spriteRenderer.worldToLocalMatrix.MultiplyPoint3x4(ray.origin + ray.direction * rayIntersectDist);
		Rect textureRect = sprite.textureRect;
		float pixelsPerUnit = sprite.pixelsPerUnit;
		float halfRealTexWidth = (float)texture.width * 0f;
		float halfRealTexHeight = (float)texture.height * 0f;
		int texPosX = (int)(vector.x * pixelsPerUnit + halfRealTexWidth);
		int texPosY = (int)(vector.y * pixelsPerUnit + halfRealTexHeight);
		if (texPosX < 0 || (float)texPosX < textureRect.x || texPosX >= Mathf.FloorToInt(textureRect.xMax))
		{
			return false;
		}
		if (texPosY < 0 || (float)texPosY < textureRect.y || texPosY >= Mathf.FloorToInt(textureRect.yMax))
		{
			return false;
		}
		pVector = new Vector2Int(texPosX, texPosY);
		return true;
	}
}
