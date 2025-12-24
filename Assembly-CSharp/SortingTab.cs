using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005FE RID: 1534
public class SortingTab : MonoBehaviour
{
	// Token: 0x06003260 RID: 12896 RVA: 0x0017E906 File Offset: 0x0017CB06
	public SortButton getCurrentButton()
	{
		return this._current_sort_button;
	}

	// Token: 0x06003261 RID: 12897 RVA: 0x0017E910 File Offset: 0x0017CB10
	public void clearButtons()
	{
		foreach (SortButtonContainer sortButtonContainer in this._buttons)
		{
			sortButtonContainer.gameObject.SetActive(false);
		}
	}

	// Token: 0x06003262 RID: 12898 RVA: 0x0017E968 File Offset: 0x0017CB68
	public SortButton tryAddButton(string pIcon, string pTooltip, SortButtonAction pShowAction, SortButtonAction pAction)
	{
		if (this.switchButton(pTooltip, true))
		{
			return null;
		}
		return this.addButton(pIcon, pTooltip, pShowAction, pAction);
	}

	// Token: 0x06003263 RID: 12899 RVA: 0x0017E984 File Offset: 0x0017CB84
	public bool switchButton(string pTooltip, bool pEnabled)
	{
		foreach (SortButtonContainer tButton in this._buttons)
		{
			if (tButton.gameObject.name == pTooltip)
			{
				tButton.gameObject.SetActive(pEnabled);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06003264 RID: 12900 RVA: 0x0017E9F8 File Offset: 0x0017CBF8
	public SortButton addButton(string pIcon, string pTooltip, SortButtonAction pShowAction, SortButtonAction pAction)
	{
		SortButtonContainer tContainer = Object.Instantiate<SortButtonContainer>(Resources.Load<SortButtonContainer>("ui/SortButtonGeneric"), base.transform);
		SortButton tSortButton = tContainer.GetComponentInChildren<SortButton>();
		PowerButton tPowerButton = tSortButton.GetComponent<PowerButton>();
		tPowerButton.icon.sprite = SpriteTextureLoader.getSprite(pIcon);
		tPowerButton.GetComponent<TipButton>().textOnClick = pTooltip;
		tSortButton.icon = tPowerButton.icon;
		tSortButton.select_action = new SortButtonClearAction(this.selectAction);
		tSortButton.action = pAction;
		tSortButton.post_action = pShowAction;
		tSortButton.gameObject.name = pTooltip;
		tContainer.gameObject.name = pTooltip;
		this._buttons.Add(tContainer);
		if (this.scrollable)
		{
			tSortButton.gameObject.AddComponent<ScrollableButton>();
		}
		return tSortButton;
	}

	// Token: 0x06003265 RID: 12901 RVA: 0x0017EAAC File Offset: 0x0017CCAC
	private void selectAction(SortButton pButton)
	{
		foreach (SortButtonContainer tButton in this._buttons)
		{
			if (!(tButton.sort_button == pButton))
			{
				tButton.sort_button.turnOff();
			}
		}
		this._current_sort_button = pButton;
	}

	// Token: 0x06003266 RID: 12902 RVA: 0x0017EB18 File Offset: 0x0017CD18
	internal void enableFirstIfNone()
	{
		if (this._buttons.Count == 0)
		{
			return;
		}
		foreach (SortButtonContainer tButton in this._buttons)
		{
			if (tButton.gameObject.activeSelf && tButton.sort_button == this._current_sort_button)
			{
				return;
			}
		}
		this.selectAction(null);
		foreach (SortButtonContainer tButton2 in this._buttons)
		{
			if (tButton2.gameObject.activeSelf)
			{
				tButton2.sort_button.click();
				break;
			}
		}
	}

	// Token: 0x04002618 RID: 9752
	public bool scrollable;

	// Token: 0x04002619 RID: 9753
	private readonly List<SortButtonContainer> _buttons = new List<SortButtonContainer>();

	// Token: 0x0400261A RID: 9754
	private SortButton _current_sort_button;
}
