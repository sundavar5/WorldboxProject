using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000457 RID: 1111
public class ErrorWindow : MonoBehaviour
{
	// Token: 0x0600262E RID: 9774 RVA: 0x001387FD File Offset: 0x001369FD
	private void OnEnable()
	{
		this.errorText.text = ErrorWindow.errorMessage;
	}

	// Token: 0x04001CDC RID: 7388
	public Text errorText;

	// Token: 0x04001CDD RID: 7389
	public static string errorMessage;
}
