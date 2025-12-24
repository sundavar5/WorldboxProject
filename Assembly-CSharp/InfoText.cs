using System;
using UnityEngine;

// Token: 0x02000465 RID: 1125
public class InfoText : MonoBehaviour
{
	// Token: 0x06002679 RID: 9849 RVA: 0x00139CFE File Offset: 0x00137EFE
	private void Start()
	{
		this.text.gameObject.GetComponent<Renderer>().sortingOrder = 1000;
		this.shadow.gameObject.GetComponent<Renderer>().sortingOrder = 999;
	}

	// Token: 0x0600267A RID: 9850 RVA: 0x00139D34 File Offset: 0x00137F34
	public void setText(string pText)
	{
		this.text.text = pText;
		this.shadow.text = pText;
	}

	// Token: 0x04001D00 RID: 7424
	public TextMesh text;

	// Token: 0x04001D01 RID: 7425
	public TextMesh shadow;
}
