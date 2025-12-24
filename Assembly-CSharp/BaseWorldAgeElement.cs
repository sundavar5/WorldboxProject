using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000815 RID: 2069
public class BaseWorldAgeElement : MonoBehaviour
{
	// Token: 0x060040C2 RID: 16578 RVA: 0x001BB1A5 File Offset: 0x001B93A5
	private void Awake()
	{
		this.prepare();
	}

	// Token: 0x060040C3 RID: 16579 RVA: 0x001BB1AD File Offset: 0x001B93AD
	protected virtual void prepare()
	{
		this.button.onClick.AddListener(delegate()
		{
			WorldAgeElementAction worldAgeElementAction = this.click_callback;
			if (worldAgeElementAction == null)
			{
				return;
			}
			worldAgeElementAction(this);
		});
	}

	// Token: 0x060040C4 RID: 16580 RVA: 0x001BB1CB File Offset: 0x001B93CB
	public WorldAgeAsset getAsset()
	{
		return this.asset;
	}

	// Token: 0x060040C5 RID: 16581 RVA: 0x001BB1D3 File Offset: 0x001B93D3
	public virtual void setAge(WorldAgeAsset pAsset)
	{
		this.asset = pAsset;
		this._icon.sprite = this.asset.getSprite();
		this._tip_button.type = "world_age";
		this._tip_button.textOnClick = pAsset.id;
	}

	// Token: 0x060040C6 RID: 16582 RVA: 0x001BB214 File Offset: 0x001B9414
	public void setIconActiveColor(bool pState)
	{
		float tColorValue;
		if (pState)
		{
			tColorValue = 1f;
		}
		else
		{
			tColorValue = 0.55f;
		}
		Color tColor = new Color(tColorValue, tColorValue, tColorValue);
		this._icon.color = tColor;
	}

	// Token: 0x060040C7 RID: 16583 RVA: 0x001BB248 File Offset: 0x001B9448
	public void addClickCallback(WorldAgeElementAction pAction)
	{
		this.click_callback = (WorldAgeElementAction)Delegate.Combine(this.click_callback, pAction);
	}

	// Token: 0x060040C8 RID: 16584 RVA: 0x001BB261 File Offset: 0x001B9461
	public void removeClickCallback(WorldAgeElementAction pAction)
	{
		this.click_callback = (WorldAgeElementAction)Delegate.Remove(this.click_callback, pAction);
	}

	// Token: 0x060040C9 RID: 16585 RVA: 0x001BB27A File Offset: 0x001B947A
	public WorldAgeElementAction getClickCallback()
	{
		return this.click_callback;
	}

	// Token: 0x060040CA RID: 16586 RVA: 0x001BB282 File Offset: 0x001B9482
	public void clearClickCallbacks()
	{
		this.click_callback = null;
	}

	// Token: 0x060040CB RID: 16587 RVA: 0x001BB28B File Offset: 0x001B948B
	public Button getButton()
	{
		return this.button;
	}

	// Token: 0x04002EF9 RID: 12025
	[SerializeField]
	protected Button button;

	// Token: 0x04002EFA RID: 12026
	[SerializeField]
	protected TipButton _tip_button;

	// Token: 0x04002EFB RID: 12027
	[SerializeField]
	protected Image _icon;

	// Token: 0x04002EFC RID: 12028
	protected WorldAgeAsset asset;

	// Token: 0x04002EFD RID: 12029
	protected WorldAgeElementAction click_callback;
}
