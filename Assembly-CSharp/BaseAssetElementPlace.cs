using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200054C RID: 1356
public class BaseAssetElementPlace<TAsset, TAssetElement> : MonoBehaviour where TAsset : Asset where TAssetElement : BaseDebugAssetElement<TAsset>
{
	// Token: 0x06002C38 RID: 11320 RVA: 0x0015C334 File Offset: 0x0015A534
	public void clear()
	{
		if (!this.has_element)
		{
			return;
		}
		this.layout_element.minHeight = this.element.rect_transform.rect.height;
		Object.Destroy(this.element_game_object_cache);
		this.element_game_object_cache = null;
		this.element = default(TAssetElement);
		this.has_element = false;
	}

	// Token: 0x06002C39 RID: 11321 RVA: 0x0015C398 File Offset: 0x0015A598
	public void setData(TAsset pAsset, TAssetElement pPrefab)
	{
		if (this.has_element)
		{
			this.clear();
		}
		this.layout_element.minHeight = -1f;
		TAssetElement tAssetElement = Object.Instantiate<TAssetElement>(pPrefab, this.rect_transform);
		tAssetElement.setData(pAsset);
		tAssetElement.rect_transform.localScale = Vector3.one;
		this.element = tAssetElement;
		this.element_game_object_cache = tAssetElement.gameObject;
		this.has_element = true;
	}

	// Token: 0x040021E5 RID: 8677
	public GameObject game_object_cache;

	// Token: 0x040021E6 RID: 8678
	public RectTransform rect_transform;

	// Token: 0x040021E7 RID: 8679
	public LayoutElement layout_element;

	// Token: 0x040021E8 RID: 8680
	public bool has_element;

	// Token: 0x040021E9 RID: 8681
	public TAssetElement element;

	// Token: 0x040021EA RID: 8682
	public GameObject element_game_object_cache;

	// Token: 0x040021EB RID: 8683
	public bool allowed_for_search = true;
}
