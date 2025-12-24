using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200058C RID: 1420
public class LoadWorldButton : MonoBehaviour
{
	// Token: 0x06002F13 RID: 12051 RVA: 0x0016C450 File Offset: 0x0016A650
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.loadWorld));
		}
	}

	// Token: 0x06002F14 RID: 12052 RVA: 0x0016C47E File Offset: 0x0016A67E
	private void loadWorld()
	{
		if (SaveManager.getCurrentMeta().saveVersion == 15)
		{
			ErrorWindow.errorMessage = "No, abandon it.";
			ScrollWindow.get("error_with_reason").clickShow(false, false);
			return;
		}
		ScrollWindow.showWindow("save_load_confirm");
	}
}
