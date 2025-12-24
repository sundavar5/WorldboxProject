using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000597 RID: 1431
public class SaveWorldButton : MonoBehaviour
{
	// Token: 0x06002FD8 RID: 12248 RVA: 0x00172D7C File Offset: 0x00170F7C
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.saveWorld));
		}
	}

	// Token: 0x06002FD9 RID: 12249 RVA: 0x00172DAA File Offset: 0x00170FAA
	private void saveWorld()
	{
		ScrollWindow.showWindow("save_world_confirm");
	}
}
