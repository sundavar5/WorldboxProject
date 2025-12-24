using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000575 RID: 1397
public class MultiBannerPool
{
	// Token: 0x06002DCF RID: 11727 RVA: 0x00165624 File Offset: 0x00163824
	public MultiBannerPool(Transform pPoolContainer)
	{
		this._pool_banners = new Dictionary<string, ObjectPoolGenericMono<MonoBehaviour>>();
		this._pool_container = pPoolContainer;
		GameObject tNewArea = new GameObject("PrefabArea", new Type[]
		{
			typeof(RectTransform)
		});
		tNewArea.transform.SetParent(this._pool_container);
		this._prefab_area = tNewArea.transform;
		this._prefab_area.gameObject.SetActive(false);
	}

	// Token: 0x06002DD0 RID: 11728 RVA: 0x00165698 File Offset: 0x00163898
	public IBanner getNext(NanoObject pObject)
	{
		string tBannerType = pObject.getType();
		MetaCustomizationAsset tMetaAsset = AssetManager.meta_customization_library.get(tBannerType);
		ObjectPoolGenericMono<MonoBehaviour> tPoolElements;
		if (!this._pool_banners.TryGetValue(tBannerType, out tPoolElements))
		{
			GameObject tBannerArea = new GameObject("BannerArea " + tBannerType, new Type[]
			{
				typeof(RectTransform)
			});
			tBannerArea.transform.SetParent(this._pool_container, false);
			MonoBehaviour tPrefabItem = (MonoBehaviour)tMetaAsset.get_banner(tMetaAsset, pObject, this._prefab_area);
			tPrefabItem.gameObject.name = tBannerType;
			this._pool_banners.Add(tBannerType, new ObjectPoolGenericMono<MonoBehaviour>(tPrefabItem, tBannerArea.transform));
			tPoolElements = this._pool_banners[tBannerType];
		}
		return tPoolElements.getNext() as IBanner;
	}

	// Token: 0x06002DD1 RID: 11729 RVA: 0x00165759 File Offset: 0x00163959
	public void release(IBanner pItem)
	{
		this.getItemPool(pItem).release(pItem as MonoBehaviour, true);
	}

	// Token: 0x06002DD2 RID: 11730 RVA: 0x0016576E File Offset: 0x0016396E
	public void resetParent(IBanner pItem)
	{
		this.getItemPool(pItem).resetParent(pItem as MonoBehaviour);
	}

	// Token: 0x06002DD3 RID: 11731 RVA: 0x00165784 File Offset: 0x00163984
	private ObjectPoolGenericMono<MonoBehaviour> getItemPool(IBanner pItem)
	{
		MetaCustomizationAsset tCurrentMetaAsset = pItem.meta_asset;
		ObjectPoolGenericMono<MonoBehaviour> tBannerPool;
		if (this._pool_banners.TryGetValue(tCurrentMetaAsset.id, out tBannerPool))
		{
			return tBannerPool;
		}
		return null;
	}

	// Token: 0x06002DD4 RID: 11732 RVA: 0x001657B0 File Offset: 0x001639B0
	public void clear()
	{
		foreach (ObjectPoolGenericMono<MonoBehaviour> objectPoolGenericMono in this._pool_banners.Values)
		{
			objectPoolGenericMono.clear(true);
			objectPoolGenericMono.resetParent();
		}
	}

	// Token: 0x040022BB RID: 8891
	private Dictionary<string, ObjectPoolGenericMono<MonoBehaviour>> _pool_banners;

	// Token: 0x040022BC RID: 8892
	private Transform _pool_container;

	// Token: 0x040022BD RID: 8893
	private Transform _prefab_area;
}
