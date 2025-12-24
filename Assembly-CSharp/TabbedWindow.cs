using System;
using UnityEngine;

// Token: 0x02000841 RID: 2113
public class TabbedWindow : MonoBehaviour
{
	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06004236 RID: 16950 RVA: 0x001C0950 File Offset: 0x001BEB50
	protected WindowMetaTabButtonsContainer tabs
	{
		get
		{
			return this.scroll_window.tabs;
		}
	}

	// Token: 0x06004237 RID: 16951 RVA: 0x001C095D File Offset: 0x001BEB5D
	protected void Awake()
	{
		this.scroll_window = base.transform.GetComponentInParent<ScrollWindow>();
		this.create();
	}

	// Token: 0x06004238 RID: 16952 RVA: 0x001C0976 File Offset: 0x001BEB76
	protected virtual void create()
	{
		this.tabs.init();
	}

	// Token: 0x06004239 RID: 16953 RVA: 0x001C0983 File Offset: 0x001BEB83
	internal virtual bool checkCancelWindow()
	{
		return false;
	}

	// Token: 0x0600423A RID: 16954 RVA: 0x001C0986 File Offset: 0x001BEB86
	public void showTab(WindowMetaTab pTab)
	{
		this.tabs.showTab(pTab);
	}

	// Token: 0x04003055 RID: 12373
	protected ScrollWindow scroll_window;
}
