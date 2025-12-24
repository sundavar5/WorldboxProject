using System;
using UnityEngine;

// Token: 0x0200059E RID: 1438
public class UploadedMapDeleteProgressWindow : MonoBehaviour
{
	// Token: 0x06002FF1 RID: 12273 RVA: 0x00173EA0 File Offset: 0x001720A0
	private void OnEnable()
	{
		this.deletingOverlay.SetActive(false);
	}

	// Token: 0x06002FF2 RID: 12274 RVA: 0x00173EAE File Offset: 0x001720AE
	public void confirmDeletion()
	{
		this.deletingOverlay.SetActive(true);
	}

	// Token: 0x0400241C RID: 9244
	public GameObject deletingOverlay;
}
