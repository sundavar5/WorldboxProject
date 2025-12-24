using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000587 RID: 1415
public class DeleteWorldButton : MonoBehaviour
{
	// Token: 0x06002F01 RID: 12033 RVA: 0x0016C1D4 File Offset: 0x0016A3D4
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.deleteWorld));
		}
	}

	// Token: 0x06002F02 RID: 12034 RVA: 0x0016C202 File Offset: 0x0016A402
	private void deleteWorld()
	{
		ScrollWindow.showWindow("save_delete_confirm");
	}
}
