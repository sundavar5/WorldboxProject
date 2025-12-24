using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200062D RID: 1581
public class ArchitectMoodButton : MonoBehaviour
{
	// Token: 0x06003398 RID: 13208 RVA: 0x0018385F File Offset: 0x00181A5F
	private void Awake()
	{
		this.button.onClick.AddListener(delegate()
		{
			ArchitectMoodAction click_callback = this._click_callback;
			if (click_callback == null)
			{
				return;
			}
			click_callback(this);
		});
	}

	// Token: 0x06003399 RID: 13209 RVA: 0x0018387D File Offset: 0x00181A7D
	public ArchitectMood getAsset()
	{
		return this._asset;
	}

	// Token: 0x0600339A RID: 13210 RVA: 0x00183885 File Offset: 0x00181A85
	public virtual void setAsset(ArchitectMood pAsset)
	{
		this._asset = pAsset;
		this._icon.sprite = this._asset.getSprite();
		this._tip_button.textOnClick = pAsset.getLocaleID();
	}

	// Token: 0x0600339B RID: 13211 RVA: 0x001838B5 File Offset: 0x00181AB5
	public void toggleSelectedButton(bool pState)
	{
		if (this._selected != null)
		{
			this._selected.color = Toolbox.makeColor(this._asset.color_main);
			this._selected.enabled = pState;
		}
	}

	// Token: 0x0600339C RID: 13212 RVA: 0x001838EC File Offset: 0x00181AEC
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

	// Token: 0x0600339D RID: 13213 RVA: 0x00183920 File Offset: 0x00181B20
	public void addClickCallback(ArchitectMoodAction pAction)
	{
		this._click_callback = (ArchitectMoodAction)Delegate.Combine(this._click_callback, pAction);
	}

	// Token: 0x04002713 RID: 10003
	[SerializeField]
	protected Button button;

	// Token: 0x04002714 RID: 10004
	[SerializeField]
	protected TipButton _tip_button;

	// Token: 0x04002715 RID: 10005
	[SerializeField]
	protected Image _icon;

	// Token: 0x04002716 RID: 10006
	[SerializeField]
	private Image _selected;

	// Token: 0x04002717 RID: 10007
	private ArchitectMood _asset;

	// Token: 0x04002718 RID: 10008
	private ArchitectMoodAction _click_callback;
}
