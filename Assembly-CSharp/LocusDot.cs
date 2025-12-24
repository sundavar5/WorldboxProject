using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006AD RID: 1709
public class LocusDot : MonoBehaviour
{
	// Token: 0x1700030B RID: 779
	// (get) Token: 0x060036A7 RID: 13991 RVA: 0x0018C4C5 File Offset: 0x0018A6C5
	internal Image status
	{
		get
		{
			return this._status;
		}
	}

	// Token: 0x060036A8 RID: 13992 RVA: 0x0018C4CD File Offset: 0x0018A6CD
	public void colorDot(Color pColor)
	{
		this._status.color = pColor;
	}

	// Token: 0x060036A9 RID: 13993 RVA: 0x0018C4DB File Offset: 0x0018A6DB
	public void colorDot(char pGeneticCode)
	{
		this.colorDot(NucleobaseHelper.getColor(pGeneticCode, false));
	}

	// Token: 0x04002876 RID: 10358
	[SerializeField]
	private Image _status;
}
