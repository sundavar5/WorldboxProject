using System;
using UnityEngine;

// Token: 0x020005E6 RID: 1510
public class LoadingScreenOut : MonoBehaviour
{
	// Token: 0x0600318D RID: 12685 RVA: 0x0017AE59 File Offset: 0x00179059
	private void Update()
	{
		this.canvasGroup.alpha -= Time.deltaTime * 2f;
		if (this.canvasGroup.alpha <= 0f)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x04002569 RID: 9577
	public CanvasGroup canvasGroup;
}
