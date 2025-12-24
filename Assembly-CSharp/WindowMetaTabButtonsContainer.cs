using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000802 RID: 2050
public class WindowMetaTabButtonsContainer : MonoBehaviour
{
	// Token: 0x0600404C RID: 16460 RVA: 0x001B7BF0 File Offset: 0x001B5DF0
	private void Awake()
	{
		this.init();
	}

	// Token: 0x0600404D RID: 16461 RVA: 0x001B7BF8 File Offset: 0x001B5DF8
	public void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		if (WindowMetaTabButtonsContainer._tab_button_on == null)
		{
			WindowMetaTabButtonsContainer._tab_button_on = SpriteTextureLoader.getSprite("ui/tab_button_vertical_selected");
			WindowMetaTabButtonsContainer._tab_button_off = SpriteTextureLoader.getSprite("ui/tab_button_vertical");
		}
		if (this._tabs.Count > 0 && this.tab_default == null)
		{
			this.tab_default = this._tabs[0];
		}
		this._scroll_window = base.transform.GetComponentInParent<ScrollWindow>();
		WindowMetaTab[] tListTabs = base.transform.GetComponentsInChildren<WindowMetaTab>(true);
		if (tListTabs.Length == 0)
		{
			return;
		}
		foreach (WindowMetaTab tTabButton in tListTabs)
		{
			this._tabs.Add(tTabButton);
			tTabButton.container = this;
		}
		this.refillTabsWithContent();
	}

	// Token: 0x0600404E RID: 16462 RVA: 0x001B7CBE File Offset: 0x001B5EBE
	private void OnEnable()
	{
		this.refillTabsWithContent();
		this.initialTabAction();
	}

	// Token: 0x0600404F RID: 16463 RVA: 0x001B7CCC File Offset: 0x001B5ECC
	private void OnDisable()
	{
		this.hideAllTabContent();
	}

	// Token: 0x06004050 RID: 16464 RVA: 0x001B7CD4 File Offset: 0x001B5ED4
	private void refillTabsWithContent()
	{
		this._tabs_with_content.Clear();
		foreach (WindowMetaTab tTabButton in this._tabs)
		{
			if (tTabButton.gameObject.activeSelf && tTabButton.getState() && tTabButton.tab_elements.Count != 0)
			{
				this._tabs_with_content.Add(tTabButton);
			}
		}
	}

	// Token: 0x06004051 RID: 16465 RVA: 0x001B7D5C File Offset: 0x001B5F5C
	public Transform addTabContent(WindowMetaTab pTabButton, Transform pContent)
	{
		string tId = pTabButton.name;
		if (!this._tabs.Contains(pTabButton))
		{
			Debug.LogError("[addTabContent] Tab " + tId + " not found in window " + this._scroll_window.name);
			return null;
		}
		if (pContent == null)
		{
			return null;
		}
		pTabButton.tab_elements.Add(pContent);
		return pContent;
	}

	// Token: 0x06004052 RID: 16466 RVA: 0x001B7DB8 File Offset: 0x001B5FB8
	public Transform addTabContent(string pTabId, Transform pContent)
	{
		WindowMetaTab tTabButton = this._tabs.Find((WindowMetaTab c) => c.name == pTabId);
		if (tTabButton == null)
		{
			Debug.LogError("[addTabContent] Tab " + pTabId + " not found in window " + this._scroll_window.name);
			return null;
		}
		return this.addTabContent(tTabButton, pContent);
	}

	// Token: 0x06004053 RID: 16467 RVA: 0x001B7E22 File Offset: 0x001B6022
	internal void removeTab(WindowMetaTab pTab)
	{
		this._tabs.Remove(pTab);
		if (this._tab_last == pTab)
		{
			this.startTabAction(this.tab_default, false);
		}
	}

	// Token: 0x06004054 RID: 16468 RVA: 0x001B7E4C File Offset: 0x001B604C
	public void initialTabAction()
	{
		if (this.tab_default == null)
		{
			return;
		}
		if (this._tab_last == null)
		{
			this.tab_default.doAction();
			return;
		}
		this._tab_last.doAction();
	}

	// Token: 0x06004055 RID: 16469 RVA: 0x001B7E82 File Offset: 0x001B6082
	public void showTab(WindowMetaTab pTabButton, bool pSkipActionIfSame = false)
	{
		this.startTabAction(pTabButton, pSkipActionIfSame);
	}

	// Token: 0x06004056 RID: 16470 RVA: 0x001B7E8C File Offset: 0x001B608C
	public void showTab(WindowMetaTab pTabButton)
	{
		this.startTabAction(pTabButton, false);
	}

	// Token: 0x06004057 RID: 16471 RVA: 0x001B7E98 File Offset: 0x001B6098
	public void showTab(string pId)
	{
		foreach (WindowMetaTab tTab in this._tabs)
		{
			if (!(tTab.name != pId))
			{
				this.showTab(tTab);
				tTab.checkShowWorldTip();
				break;
			}
		}
	}

	// Token: 0x06004058 RID: 16472 RVA: 0x001B7F04 File Offset: 0x001B6104
	public void startTabAction(WindowMetaTab pTab, bool pSkipIfSame = false)
	{
		if (pTab.destroyed)
		{
			return;
		}
		if (pSkipIfSame && this.isActiveTab(pTab))
		{
			return;
		}
		this._tab_last = pTab;
		this.hideAllTabContent();
		this.enableTab(this._tab_last);
		foreach (Transform tTabContent in pTab.tab_elements)
		{
			if (!(tTabContent == null))
			{
				tTabContent.gameObject.SetActive(true);
			}
		}
		TabShowAction on_tab_show = this._on_tab_show;
		if (on_tab_show != null)
		{
			on_tab_show(this._tab_last);
		}
		this._scroll_window.resetScroll();
	}

	// Token: 0x06004059 RID: 16473 RVA: 0x001B7FB8 File Offset: 0x001B61B8
	public bool isActiveTab(WindowMetaTab pTab)
	{
		return this._tab_last == pTab;
	}

	// Token: 0x0600405A RID: 16474 RVA: 0x001B7FC6 File Offset: 0x001B61C6
	protected void enableTab(WindowMetaTab pTabButton)
	{
		pTabButton.GetComponent<Image>().sprite = WindowMetaTabButtonsContainer._tab_button_on;
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x001B7FD8 File Offset: 0x001B61D8
	public void hideAllTabContent()
	{
		this.disableTabs();
		foreach (WindowMetaTab windowMetaTab in this._tabs)
		{
			foreach (Transform tTabContent in windowMetaTab.tab_elements)
			{
				if (!(tTabContent == null))
				{
					tTabContent.gameObject.SetActive(false);
				}
			}
		}
		TabHideAction on_tab_hide = this._on_tab_hide;
		if (on_tab_hide != null)
		{
			on_tab_hide();
		}
		Tooltip.hideTooltipNow();
	}

	// Token: 0x0600405C RID: 16476 RVA: 0x001B8090 File Offset: 0x001B6290
	protected void disableTabs()
	{
		foreach (WindowMetaTab windowMetaTab in this._tabs)
		{
			windowMetaTab.GetComponent<Image>().sprite = WindowMetaTabButtonsContainer._tab_button_off;
		}
	}

	// Token: 0x0600405D RID: 16477 RVA: 0x001B80EC File Offset: 0x001B62EC
	public List<WindowMetaTab> getContentTabs()
	{
		this._tabs_with_content.Sort((WindowMetaTab p1, WindowMetaTab p2) => p1.transform.GetSiblingIndex().CompareTo(p2.transform.GetSiblingIndex()));
		return this._tabs_with_content;
	}

	// Token: 0x0600405E RID: 16478 RVA: 0x001B811E File Offset: 0x001B631E
	public WindowMetaTab getActiveTab()
	{
		if (this._tab_last != null)
		{
			return this._tab_last;
		}
		return this.tab_default;
	}

	// Token: 0x0600405F RID: 16479 RVA: 0x001B813B File Offset: 0x001B633B
	public void reloadActiveTab()
	{
		this.getActiveTab().doAction();
	}

	// Token: 0x06004060 RID: 16480 RVA: 0x001B8148 File Offset: 0x001B6348
	public void addTabShowCallback(TabShowAction pCallback)
	{
		this._on_tab_show = (TabShowAction)Delegate.Combine(this._on_tab_show, pCallback);
	}

	// Token: 0x06004061 RID: 16481 RVA: 0x001B8161 File Offset: 0x001B6361
	public void removeTabShowCallback(TabShowAction pCallback)
	{
		this._on_tab_show = (TabShowAction)Delegate.Remove(this._on_tab_show, pCallback);
	}

	// Token: 0x06004062 RID: 16482 RVA: 0x001B817A File Offset: 0x001B637A
	public void addTabHideCallback(TabHideAction pCallback)
	{
		this._on_tab_hide = (TabHideAction)Delegate.Combine(this._on_tab_hide, pCallback);
	}

	// Token: 0x06004063 RID: 16483 RVA: 0x001B8193 File Offset: 0x001B6393
	public void removeTabHideCallback(TabHideAction pCallback)
	{
		this._on_tab_hide = (TabHideAction)Delegate.Remove(this._on_tab_hide, pCallback);
	}

	// Token: 0x04002E96 RID: 11926
	private static Sprite _tab_button_on;

	// Token: 0x04002E97 RID: 11927
	private static Sprite _tab_button_off;

	// Token: 0x04002E98 RID: 11928
	public WindowMetaTab tab_default;

	// Token: 0x04002E99 RID: 11929
	private ScrollWindow _scroll_window;

	// Token: 0x04002E9A RID: 11930
	private List<WindowMetaTab> _tabs = new List<WindowMetaTab>();

	// Token: 0x04002E9B RID: 11931
	private readonly List<WindowMetaTab> _tabs_with_content = new List<WindowMetaTab>();

	// Token: 0x04002E9C RID: 11932
	private WindowMetaTab _tab_last;

	// Token: 0x04002E9D RID: 11933
	private TabShowAction _on_tab_show;

	// Token: 0x04002E9E RID: 11934
	private TabHideAction _on_tab_hide;

	// Token: 0x04002E9F RID: 11935
	private bool _initialized;
}
