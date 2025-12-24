using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000810 RID: 2064
public class WorkshopUploadWorldButton : MonoBehaviour
{
	// Token: 0x06004098 RID: 16536 RVA: 0x001BA5A8 File Offset: 0x001B87A8
	private void Start()
	{
		Button tButton;
		if (base.TryGetComponent<Button>(out tButton))
		{
			tButton.onClick.AddListener(new UnityAction(this.uploadWorldToWorkshop));
		}
	}

	// Token: 0x06004099 RID: 16537 RVA: 0x001BA5D6 File Offset: 0x001B87D6
	private void OnEnable()
	{
		this.quickError.SetActive(false);
	}

	// Token: 0x0600409A RID: 16538 RVA: 0x001BA5E4 File Offset: 0x001B87E4
	private void uploadWorldToWorkshop()
	{
		this.quickError.SetActive(false);
		if (string.IsNullOrWhiteSpace(this.title.text))
		{
			this.errorMessage.text = "Give your world a name!";
			this.quickError.SetActive(true);
			return;
		}
		if (string.IsNullOrWhiteSpace(this.description.text))
		{
			this.errorMessage.text = "Give your world a description!";
			this.quickError.SetActive(true);
			return;
		}
		ScrollWindow.showWindow("steam_workshop_uploading");
	}

	// Token: 0x0600409B RID: 16539 RVA: 0x001BA665 File Offset: 0x001B8865
	public void closeError()
	{
		this.quickError.SetActive(false);
	}

	// Token: 0x04002ECE RID: 11982
	public Text title;

	// Token: 0x04002ECF RID: 11983
	public Text description;

	// Token: 0x04002ED0 RID: 11984
	public GameObject quickError;

	// Token: 0x04002ED1 RID: 11985
	public Text errorMessage;
}
