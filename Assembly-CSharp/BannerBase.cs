using System;
using DG.Tweening;
using UnityEngine;

// Token: 0x020005C7 RID: 1479
public abstract class BannerBase : MonoBehaviour, IBanner, IBaseMono, IRefreshElement
{
	// Token: 0x17000288 RID: 648
	// (get) Token: 0x06003081 RID: 12417 RVA: 0x00176F7B File Offset: 0x0017517B
	protected virtual MetaType meta_type
	{
		get
		{
			throw new NotImplementedException(base.GetType().Name);
		}
	}

	// Token: 0x17000289 RID: 649
	// (get) Token: 0x06003082 RID: 12418 RVA: 0x00176F8D File Offset: 0x0017518D
	public MetaCustomizationAsset meta_asset
	{
		get
		{
			return AssetManager.meta_customization_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x1700028A RID: 650
	// (get) Token: 0x06003083 RID: 12419 RVA: 0x00176F9F File Offset: 0x0017519F
	public MetaTypeAsset meta_type_asset
	{
		get
		{
			return AssetManager.meta_type_library.getAsset(this.meta_type);
		}
	}

	// Token: 0x1700028B RID: 651
	// (get) Token: 0x06003084 RID: 12420 RVA: 0x00176FB1 File Offset: 0x001751B1
	// (set) Token: 0x06003085 RID: 12421 RVA: 0x00176FC3 File Offset: 0x001751C3
	internal int option_1
	{
		get
		{
			return this.meta_asset.option_1_get();
		}
		set
		{
			this.meta_asset.option_1_set(value);
		}
	}

	// Token: 0x1700028C RID: 652
	// (get) Token: 0x06003086 RID: 12422 RVA: 0x00176FD6 File Offset: 0x001751D6
	// (set) Token: 0x06003087 RID: 12423 RVA: 0x00176FE8 File Offset: 0x001751E8
	internal int option_2
	{
		get
		{
			return this.meta_asset.option_2_get();
		}
		set
		{
			this.meta_asset.option_2_set(value);
		}
	}

	// Token: 0x1700028D RID: 653
	// (get) Token: 0x06003088 RID: 12424 RVA: 0x00176FFB File Offset: 0x001751FB
	// (set) Token: 0x06003089 RID: 12425 RVA: 0x0017700D File Offset: 0x0017520D
	internal int color
	{
		get
		{
			return this.meta_asset.color_get();
		}
		set
		{
			this.meta_asset.color_set(value);
		}
	}

	// Token: 0x0600308A RID: 12426 RVA: 0x00177020 File Offset: 0x00175220
	public virtual void load(NanoObject pObject)
	{
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x00177022 File Offset: 0x00175222
	public virtual NanoObject GetNanoObject()
	{
		return null;
	}

	// Token: 0x0600308C RID: 12428 RVA: 0x00177028 File Offset: 0x00175228
	public void jump(float pSpeed = 0.1f, bool pSilent = false)
	{
		float tStart = base.transform.localPosition.y;
		this._sequence.Kill(false);
		this._sequence = DOTween.Sequence();
		this._sequence.Append(base.transform.DOLocalMoveY(tStart + 5f, pSpeed, false));
		this._sequence.Append(base.transform.DOLocalMoveY(tStart, pSpeed, false));
		this._sequence.AppendCallback(delegate
		{
			if (!pSilent)
			{
				SoundBox.click();
			}
		});
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x001770BC File Offset: 0x001752BC
	private void OnDisable()
	{
		this._sequence.Kill(false);
	}

	// Token: 0x0600308E RID: 12430 RVA: 0x001770CA File Offset: 0x001752CA
	public string getName()
	{
		return this.GetNanoObject().name;
	}

	// Token: 0x0600308F RID: 12431 RVA: 0x001770D7 File Offset: 0x001752D7
	public virtual void showTooltip()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003091 RID: 12433 RVA: 0x001770E6 File Offset: 0x001752E6
	Transform IBaseMono.get_transform()
	{
		return base.transform;
	}

	// Token: 0x06003092 RID: 12434 RVA: 0x001770EE File Offset: 0x001752EE
	GameObject IBaseMono.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x06003093 RID: 12435 RVA: 0x001770F6 File Offset: 0x001752F6
	T IBaseMono.GetComponent<T>()
	{
		return base.GetComponent<T>();
	}

	// Token: 0x040024D7 RID: 9431
	private Sequence _sequence;
}
