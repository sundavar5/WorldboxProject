using System;
using UnityEngine;

// Token: 0x020007F7 RID: 2039
public class WindowBackground : MonoBehaviour
{
	// Token: 0x06003FFB RID: 16379 RVA: 0x001B6B25 File Offset: 0x001B4D25
	private void Start()
	{
		this.group = base.GetComponent<CanvasGroup>();
	}

	// Token: 0x06003FFC RID: 16380 RVA: 0x001B6B34 File Offset: 0x001B4D34
	private void Update()
	{
		if (ScrollWindow.isWindowActive() && this.group.alpha < 1f)
		{
			this.group.alpha += Time.deltaTime * 5f;
			return;
		}
		if (!ScrollWindow.isWindowActive() && this.group.alpha > 0f)
		{
			this.group.alpha -= Time.deltaTime * 5f;
		}
	}

	// Token: 0x04002E64 RID: 11876
	private CanvasGroup group;
}
