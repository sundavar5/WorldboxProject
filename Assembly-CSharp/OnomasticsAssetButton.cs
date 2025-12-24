using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200071B RID: 1819
public class OnomasticsAssetButton : MonoBehaviour
{
	// Token: 0x060039F9 RID: 14841 RVA: 0x0019B94C File Offset: 0x00199B4C
	private void Awake()
	{
		this.create();
		DraggableLayoutElement tDraggableLayoutElement;
		if (base.TryGetComponent<DraggableLayoutElement>(out tDraggableLayoutElement))
		{
			DraggableLayoutElement draggableLayoutElement = tDraggableLayoutElement;
			draggableLayoutElement.start_being_dragged = (Action<DraggableLayoutElement>)Delegate.Combine(draggableLayoutElement.start_being_dragged, new Action<DraggableLayoutElement>(this.onStartDrag));
		}
	}

	// Token: 0x060039FA RID: 14842 RVA: 0x0019B98C File Offset: 0x00199B8C
	protected virtual void onStartDrag(DraggableLayoutElement pOriginalElement)
	{
		OnomasticsAssetButton tOriginalButton = pOriginalElement.GetComponent<OnomasticsAssetButton>();
		this.setupButton(tOriginalButton.onomastics_asset, tOriginalButton._get_current_onomastics_data);
	}

	// Token: 0x060039FB RID: 14843 RVA: 0x0019B9B2 File Offset: 0x00199BB2
	public void setupButton(OnomasticsAsset pAsset, GetCurrentOnomasticsData pDelegate)
	{
		this.loadAsset(pAsset);
		this.setOnomasticsGetter(pDelegate);
		this.checkSpriteButtonColor();
	}

	// Token: 0x060039FC RID: 14844 RVA: 0x0019B9C8 File Offset: 0x00199BC8
	public RectTransform getRect()
	{
		return base.GetComponent<RectTransform>();
	}

	// Token: 0x060039FD RID: 14845 RVA: 0x0019B9D0 File Offset: 0x00199BD0
	private void Update()
	{
		this.checkSpriteButtonColor();
	}

	// Token: 0x060039FE RID: 14846 RVA: 0x0019B9D8 File Offset: 0x00199BD8
	public bool isGroupType()
	{
		return this.onomastics_asset.isGroupType();
	}

	// Token: 0x060039FF RID: 14847 RVA: 0x0019B9E8 File Offset: 0x00199BE8
	private bool doesGroupHaveContent()
	{
		if (this._get_current_onomastics_data == null)
		{
			return true;
		}
		OnomasticsData tData = this._get_current_onomastics_data();
		return tData != null && this.onomastics_asset != null && (!this.isGroupType() || !tData.isGroupEmpty(this.onomastics_asset.id));
	}

	// Token: 0x06003A00 RID: 14848 RVA: 0x0019BA36 File Offset: 0x00199C36
	public void checkSpriteButtonColor()
	{
		if (this.doesGroupHaveContent())
		{
			this.image.color = Color.white;
			return;
		}
		this.image.color = Color.gray;
	}

	// Token: 0x06003A01 RID: 14849 RVA: 0x0019BA61 File Offset: 0x00199C61
	public void setOnomasticsGetter(GetCurrentOnomasticsData pDelegate)
	{
		this._get_current_onomastics_data = pDelegate;
	}

	// Token: 0x06003A02 RID: 14850 RVA: 0x0019BA6C File Offset: 0x00199C6C
	private void Start()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(delegate
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showTooltip();
		}, true);
	}

	// Token: 0x06003A03 RID: 14851 RVA: 0x0019BA97 File Offset: 0x00199C97
	public void loadAsset(OnomasticsAsset pAsset)
	{
		this.onomastics_asset = pAsset;
		this.image.sprite = this.onomastics_asset.getSprite();
	}

	// Token: 0x06003A04 RID: 14852 RVA: 0x0019BAB8 File Offset: 0x00199CB8
	public void showTooltip()
	{
		if (!this.tooltip_enabled)
		{
			return;
		}
		this.tooltipBuilder();
		base.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		base.transform.DOKill(false);
		base.transform.DOScale(1f, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x06003A05 RID: 14853 RVA: 0x0019BB1D File Offset: 0x00199D1D
	private void tooltipBuilder()
	{
		Tooltip.show(this, "onomastics_asset", new TooltipData
		{
			onomastics_asset = this.onomastics_asset,
			onomastics_data = this._get_current_onomastics_data()
		});
	}

	// Token: 0x06003A06 RID: 14854 RVA: 0x0019BB4C File Offset: 0x00199D4C
	private void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		this.button = base.GetComponent<Button>();
		this.image = base.transform.Find("TiltEffect/icon").GetComponent<Image>();
	}

	// Token: 0x06003A07 RID: 14855 RVA: 0x0019BB85 File Offset: 0x00199D85
	private void OnDestroy()
	{
		base.transform.DOKill(false);
	}

	// Token: 0x04002AE6 RID: 10982
	private bool _created;

	// Token: 0x04002AE7 RID: 10983
	internal Image image;

	// Token: 0x04002AE8 RID: 10984
	internal bool tooltip_enabled = true;

	// Token: 0x04002AE9 RID: 10985
	internal Button button;

	// Token: 0x04002AEA RID: 10986
	public OnomasticsAsset onomastics_asset;

	// Token: 0x04002AEB RID: 10987
	public OnomasticsActionUpdate onomastics_action_update;

	// Token: 0x04002AEC RID: 10988
	private GetCurrentOnomasticsData _get_current_onomastics_data;
}
