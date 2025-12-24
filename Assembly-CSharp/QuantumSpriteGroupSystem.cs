using System;
using UnityEngine;

// Token: 0x020002ED RID: 749
public class QuantumSpriteGroupSystem : SpriteGroupSystem<QuantumSprite>
{
	// Token: 0x06001C27 RID: 7207 RVA: 0x001005B9 File Offset: 0x000FE7B9
	public void create(QuantumSpriteAsset pAsset)
	{
		this._path = pAsset.id_prefab;
		this._asset = pAsset;
		this.create();
		base.transform.name = pAsset.id;
	}

	// Token: 0x06001C28 RID: 7208 RVA: 0x001005E5 File Offset: 0x000FE7E5
	public QuantumSpriteCacheData getCacheData(int pSize)
	{
		if (this._cache_data == null)
		{
			this._cache_data = new QuantumSpriteCacheData(pSize);
		}
		else
		{
			this._cache_data.checkSize(pSize);
		}
		return this._cache_data;
	}

	// Token: 0x06001C29 RID: 7209 RVA: 0x0010060F File Offset: 0x000FE80F
	public override void deactivate(QuantumSprite pObject)
	{
	}

	// Token: 0x06001C2A RID: 7210 RVA: 0x00100611 File Offset: 0x000FE811
	public override void checkActiveAction(QuantumSprite pObject)
	{
	}

	// Token: 0x06001C2B RID: 7211 RVA: 0x00100614 File Offset: 0x000FE814
	protected override QuantumSprite createNew()
	{
		QuantumSprite tMark = base.createNew();
		if (this._asset.create_object != null)
		{
			this._asset.create_object(this._asset, tMark);
		}
		return tMark;
	}

	// Token: 0x06001C2C RID: 7212 RVA: 0x00100650 File Offset: 0x000FE850
	public override void create()
	{
		base.create();
		this.prefab = Resources.Load<QuantumSprite>("civ/" + this._path);
		if (this.prefab == null)
		{
			Debug.LogError("Missing Prefab " + this._path);
		}
	}

	// Token: 0x04001588 RID: 5512
	private string _path;

	// Token: 0x04001589 RID: 5513
	private QuantumSpriteAsset _asset;

	// Token: 0x0400158A RID: 5514
	private QuantumSpriteCacheData _cache_data;
}
