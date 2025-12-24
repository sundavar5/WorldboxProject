using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200080F RID: 2063
public class WorkshopPlayMap : MonoBehaviour
{
	// Token: 0x06004095 RID: 16533 RVA: 0x001BA564 File Offset: 0x001B8764
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.playWorkShopMap));
		}
	}

	// Token: 0x06004096 RID: 16534 RVA: 0x001BA592 File Offset: 0x001B8792
	public void playWorkShopMap()
	{
		ScrollWindow.showWindow("save_load_confirm");
	}
}
