using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020007FB RID: 2043
public class WindowListElementBase<TMetaObject, TData> : MonoBehaviour, IPointerMoveHandler, IEventSystemHandler where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x0600401E RID: 16414 RVA: 0x001B76AF File Offset: 0x001B58AF
	private void Awake()
	{
		this.create();
	}

	// Token: 0x0600401F RID: 16415 RVA: 0x001B76B7 File Offset: 0x001B58B7
	private void create()
	{
		this.initMonoFields();
		this.initTooltip();
	}

	// Token: 0x06004020 RID: 16416 RVA: 0x001B76C8 File Offset: 0x001B58C8
	protected virtual void initMonoFields()
	{
		if (this._main_banner == null)
		{
			BannerGeneric<TMetaObject, TData>[] tMainBanners = base.gameObject.transform.FindAllRecursive((Transform p) => p.gameObject.activeInHierarchy);
			if (tMainBanners.Length == 1)
			{
				this._main_banner = tMainBanners[0];
				return;
			}
			string str = "WindowListElementBase: Failed to auto-find main banner. Assign manually. Found : ";
			string str2 = tMainBanners.Length.ToString();
			string str3 = " of type ";
			Type typeFromHandle = typeof(BannerGeneric<TMetaObject, TData>);
			Debug.LogError(str + str2 + str3 + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
		}
	}

	// Token: 0x06004021 RID: 16417 RVA: 0x001B7758 File Offset: 0x001B5958
	private void initTooltip()
	{
		base.GetComponent<Button>().OnHoverOut(delegate()
		{
			Tooltip.hideTooltip();
		});
	}

	// Token: 0x06004022 RID: 16418 RVA: 0x001B7784 File Offset: 0x001B5984
	public void click()
	{
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
		{
			this.tooltipAction();
			return;
		}
		MetaType tType = this.meta_object.getMetaType();
		MetaTypeAsset tMetaTypeAsset = AssetManager.meta_type_library.getAsset(tType);
		tMetaTypeAsset.set_selected(this.meta_object);
		if (tMetaTypeAsset.get_selected() == null)
		{
			return;
		}
		ScrollWindow.showWindow(tMetaTypeAsset.window_name);
	}

	// Token: 0x06004023 RID: 16419 RVA: 0x001B77F4 File Offset: 0x001B59F4
	internal virtual void show(TMetaObject pObject)
	{
		this.meta_object = pObject;
		this.loadBanner();
		this.toggleFavorited(this.meta_object.isFavorite());
		if (this._icon_species != null)
		{
			this._icon_species.sprite = this.getActorAsset().getSpriteIcon();
		}
	}

	// Token: 0x06004024 RID: 16420 RVA: 0x001B7848 File Offset: 0x001B5A48
	protected virtual void loadBanner()
	{
		this._main_banner.load(this.meta_object);
	}

	// Token: 0x06004025 RID: 16421 RVA: 0x001B7860 File Offset: 0x001B5A60
	protected virtual void tooltipAction()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06004026 RID: 16422 RVA: 0x001B7867 File Offset: 0x001B5A67
	public void toggleFavorited(bool pState)
	{
		if (this._icon_favorite != null)
		{
			this._icon_favorite.SetActive(pState);
		}
	}

	// Token: 0x06004027 RID: 16423 RVA: 0x001B7883 File Offset: 0x001B5A83
	protected virtual void OnDisable()
	{
		this.meta_object = default(TMetaObject);
	}

	// Token: 0x06004028 RID: 16424 RVA: 0x001B7891 File Offset: 0x001B5A91
	public void OnPointerMove(PointerEventData pData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (Tooltip.anyActive())
		{
			return;
		}
		this.tooltipAction();
	}

	// Token: 0x06004029 RID: 16425 RVA: 0x001B78A9 File Offset: 0x001B5AA9
	protected virtual ActorAsset getActorAsset()
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002E87 RID: 11911
	[HideInInspector]
	public TMetaObject meta_object;

	// Token: 0x04002E88 RID: 11912
	[SerializeField]
	private BannerGeneric<TMetaObject, TData> _main_banner;

	// Token: 0x04002E89 RID: 11913
	[SerializeField]
	private GameObject _icon_favorite;

	// Token: 0x04002E8A RID: 11914
	[SerializeField]
	private Image _icon_species;
}
