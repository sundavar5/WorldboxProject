using System;
using UnityEngine;

// Token: 0x0200075A RID: 1882
public class SelectedNano<TNanoObject> : SelectedNanoBase where TNanoObject : NanoObject, IFavoriteable
{
	// Token: 0x17000379 RID: 889
	// (get) Token: 0x06003B9B RID: 15259 RVA: 0x001A0FE7 File Offset: 0x0019F1E7
	protected virtual TNanoObject nano_object
	{
		get
		{
			return SelectedObjects.getSelectedNanoObject() as TNanoObject;
		}
	}

	// Token: 0x06003B9C RID: 15260 RVA: 0x001A0FF8 File Offset: 0x0019F1F8
	protected override void Awake()
	{
		base.Awake();
		this._container_traits = base.GetComponentInChildren<ISelectedContainerTrait>();
		if (this._container_banners_parent != null)
		{
			this._container_banners = this._container_banners_parent.GetComponentInChildren<ISelectedTabBanners<TNanoObject>>();
		}
	}

	// Token: 0x06003B9D RID: 15261 RVA: 0x001A102B File Offset: 0x0019F22B
	private void OnDisable()
	{
		this.clearLastObject();
	}

	// Token: 0x06003B9E RID: 15262 RVA: 0x001A1033 File Offset: 0x0019F233
	public override void update()
	{
		if (this._last_nano.isRekt())
		{
			this._last_nano = default(TNanoObject);
		}
		this.updateElements(this.nano_object);
	}

	// Token: 0x06003B9F RID: 15263 RVA: 0x001A1060 File Offset: 0x0019F260
	protected virtual void updateElements(TNanoObject pNano)
	{
		if (pNano.isRekt())
		{
			this.clearLastObject();
			return;
		}
		base.updateFavoriteIcon(pNano.isFavorite());
		if (this.isNanoChanged(pNano))
		{
			this.updateElementsOnChange(pNano);
		}
		this.updateElementsAlways(pNano);
		this._last_dirty_stats = pNano.getStatsDirtyVersion();
		this._last_nano = pNano;
	}

	// Token: 0x06003BA0 RID: 15264 RVA: 0x001A10C1 File Offset: 0x0019F2C1
	protected virtual void updateElementsOnChange(TNanoObject pNano)
	{
		this.showStatsGeneral(pNano);
		this.updateTraits();
		this.updateBanners(pNano);
		this.checkAchievements(pNano);
		World.world.selected_buttons.clearHighlightedButton();
	}

	// Token: 0x06003BA1 RID: 15265 RVA: 0x001A10ED File Offset: 0x0019F2ED
	protected virtual void checkAchievements(TNanoObject pNano)
	{
	}

	// Token: 0x06003BA2 RID: 15266 RVA: 0x001A10EF File Offset: 0x0019F2EF
	protected virtual void updateElementsAlways(TNanoObject pNano)
	{
		this.recalcTabSize();
	}

	// Token: 0x06003BA3 RID: 15267 RVA: 0x001A10F7 File Offset: 0x0019F2F7
	protected virtual void showStatsGeneral(TNanoObject pNano)
	{
	}

	// Token: 0x06003BA4 RID: 15268 RVA: 0x001A10FC File Offset: 0x0019F2FC
	private void updateBanners(TNanoObject pNano)
	{
		if (this._container_banners == null)
		{
			return;
		}
		this._container_banners.update(pNano);
		if (this._container_banners.countVisibleBanners() > 0)
		{
			this._container_banners_parent.gameObject.SetActive(true);
			return;
		}
		this._container_banners_parent.gameObject.SetActive(false);
	}

	// Token: 0x06003BA5 RID: 15269 RVA: 0x001A114F File Offset: 0x0019F34F
	protected virtual void updateTraits()
	{
		if (this._container_traits == null)
		{
			return;
		}
		this._container_traits.update(this.nano_object);
	}

	// Token: 0x06003BA6 RID: 15270 RVA: 0x001A1170 File Offset: 0x0019F370
	protected virtual void clearLastObject()
	{
		this._last_nano = default(TNanoObject);
		this._last_dirty_stats = -1;
	}

	// Token: 0x06003BA7 RID: 15271 RVA: 0x001A1188 File Offset: 0x0019F388
	protected void recalcTabSize()
	{
		ScrollRectExtended tScrollRect = PowerTabController.instance.scrollRect;
		if (tScrollRect.isDragged() || Mathf.Abs(tScrollRect.velocity.x) > 1f)
		{
			this._powers_tab.sortButtons();
			return;
		}
		float tCurrentNormalizedPosition;
		if (!tScrollRect.isHorizontalScrollAvailable())
		{
			tCurrentNormalizedPosition = 0f;
		}
		else
		{
			tCurrentNormalizedPosition = tScrollRect.horizontalNormalizedPosition;
		}
		Vector2 tVelocity = tScrollRect.velocity;
		if (this._powers_tab.recalc())
		{
			this._powers_tab.sortButtons();
		}
		tScrollRect.horizontalNormalizedPosition = tCurrentNormalizedPosition;
		tScrollRect.velocity = tVelocity;
	}

	// Token: 0x06003BA8 RID: 15272 RVA: 0x001A120F File Offset: 0x0019F40F
	protected bool isNanoChanged(TNanoObject pNano)
	{
		return pNano.getStatsDirtyVersion() != this._last_dirty_stats || pNano != this._last_nano;
	}

	// Token: 0x04002BB1 RID: 11185
	[SerializeField]
	private GameObject _container_banners_parent;

	// Token: 0x04002BB2 RID: 11186
	private ISelectedContainerTrait _container_traits;

	// Token: 0x04002BB3 RID: 11187
	private ISelectedTabBanners<TNanoObject> _container_banners;

	// Token: 0x04002BB4 RID: 11188
	private int _last_dirty_stats;

	// Token: 0x04002BB5 RID: 11189
	private TNanoObject _last_nano;
}
