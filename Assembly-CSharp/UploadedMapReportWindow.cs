using System;
using UnityEngine;

// Token: 0x020005A5 RID: 1445
public class UploadedMapReportWindow : MonoBehaviour
{
	// Token: 0x06002FFD RID: 12285 RVA: 0x00173F40 File Offset: 0x00172140
	private void OnEnable()
	{
		this.reportOverlay.SetActive(false);
		this.reportButtons.SetActive(true);
		this.reportConfirmation.SetActive(false);
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x00173F66 File Offset: 0x00172166
	public void reportNSFW()
	{
		this.reportReason = "nsfw";
		this.confirmReport();
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x00173F79 File Offset: 0x00172179
	public void reportCrash()
	{
		this.reportReason = "crash";
		this.confirmReport();
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x00173F8C File Offset: 0x0017218C
	public void reportBroken()
	{
		this.reportReason = "broken";
		this.confirmReport();
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x00173F9F File Offset: 0x0017219F
	public void confirmReport()
	{
		this.reportButtons.SetActive(false);
		this.reportOverlay.SetActive(true);
		string text = this.reportReason;
	}

	// Token: 0x04002438 RID: 9272
	public GameObject reportOverlay;

	// Token: 0x04002439 RID: 9273
	public GameObject reportButtons;

	// Token: 0x0400243A RID: 9274
	public GameObject reportConfirmation;

	// Token: 0x0400243B RID: 9275
	private string reportReason = "";
}
