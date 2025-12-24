using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000603 RID: 1539
public class SwitchButton : MonoBehaviour
{
	// Token: 0x0600328D RID: 12941 RVA: 0x0017FB8F File Offset: 0x0017DD8F
	public void setEnabled(bool pValue)
	{
		if (pValue)
		{
			base.GetComponent<CanvasGroup>().alpha = 1f;
			return;
		}
		base.GetComponent<CanvasGroup>().alpha = 0.5f;
	}

	// Token: 0x04002632 RID: 9778
	public Color color_on = Color.white;

	// Token: 0x04002633 RID: 9779
	public Color color_off = Color.gray;

	// Token: 0x04002634 RID: 9780
	public Text text;

	// Token: 0x04002635 RID: 9781
	public Image icon;
}
