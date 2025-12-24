using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020007F1 RID: 2033
public class TabTogglesGroup : MonoBehaviour
{
	// Token: 0x06003FD6 RID: 16342 RVA: 0x001B649A File Offset: 0x001B469A
	public TabToggle getCurrentButton()
	{
		return this._current_toggle;
	}

	// Token: 0x06003FD7 RID: 16343 RVA: 0x001B64A4 File Offset: 0x001B46A4
	public void clearButtons()
	{
		foreach (TabToggleContainer tabToggleContainer in this._toggles)
		{
			tabToggleContainer.gameObject.SetActive(false);
		}
	}

	// Token: 0x06003FD8 RID: 16344 RVA: 0x001B64FC File Offset: 0x001B46FC
	public void tryAddButton(string pIcon, string pTooltip, TabToggleAction pShowAction, TabToggleAction pAction)
	{
		if (this.switchButton(pTooltip, true))
		{
			return;
		}
		this.addButton(pIcon, pTooltip, pShowAction, pAction);
	}

	// Token: 0x06003FD9 RID: 16345 RVA: 0x001B6514 File Offset: 0x001B4714
	public bool switchButton(string pTooltip, bool pEnabled)
	{
		foreach (TabToggleContainer tButton in this._toggles)
		{
			if (tButton.gameObject.name == pTooltip)
			{
				tButton.gameObject.SetActive(pEnabled);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003FDA RID: 16346 RVA: 0x001B6588 File Offset: 0x001B4788
	public void addButton(string pIcon, string pTooltip, TabToggleAction pShowAction, TabToggleAction pAction)
	{
		TabToggleContainer tContainer = Object.Instantiate<TabToggleContainer>(Resources.Load<TabToggleContainer>("ui/TabToggleGeneric"), base.transform);
		TabToggle componentInChildren = tContainer.GetComponentInChildren<TabToggle>();
		PowerButton tPowerButton = componentInChildren.GetComponent<PowerButton>();
		tPowerButton.icon.sprite = SpriteTextureLoader.getSprite(pIcon);
		tPowerButton.GetComponent<TipButton>().textOnClick = pTooltip;
		tPowerButton.GetComponent<TipButton>().textOnClickDescription = pTooltip + "_description";
		componentInChildren.icon = tPowerButton.icon;
		componentInChildren.select_action = new TabToggleClearAction(this.selectAction);
		componentInChildren.action = pAction;
		componentInChildren.post_action = pShowAction;
		componentInChildren.gameObject.name = pTooltip;
		tContainer.gameObject.name = pTooltip;
		this._toggles.Add(tContainer);
	}

	// Token: 0x06003FDB RID: 16347 RVA: 0x001B663C File Offset: 0x001B483C
	private void selectAction(TabToggle pToggle)
	{
		foreach (TabToggleContainer tButton in this._toggles)
		{
			if (!(tButton.toggle == pToggle))
			{
				tButton.toggle.unselect();
			}
		}
		this._current_toggle = pToggle;
	}

	// Token: 0x06003FDC RID: 16348 RVA: 0x001B66A8 File Offset: 0x001B48A8
	internal void enableFirst()
	{
		if (this._toggles.Count == 0)
		{
			return;
		}
		this.selectAction(null);
		foreach (TabToggleContainer tButton in this._toggles)
		{
			if (tButton.gameObject.activeSelf)
			{
				tButton.toggle.click();
				break;
			}
		}
	}

	// Token: 0x04002E4C RID: 11852
	private readonly List<TabToggleContainer> _toggles = new List<TabToggleContainer>();

	// Token: 0x04002E4D RID: 11853
	private TabToggle _current_toggle;
}
