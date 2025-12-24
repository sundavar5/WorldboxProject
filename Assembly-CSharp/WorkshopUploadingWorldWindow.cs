using System;
using Steamworks.Data;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000811 RID: 2065
public class WorkshopUploadingWorldWindow : MonoBehaviour
{
	// Token: 0x0600409D RID: 16541 RVA: 0x001BA67C File Offset: 0x001B887C
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		WorkshopUploadingWorldWindow.needsWorkshopAgreement = false;
		this.errorImage.gameObject.SetActive(false);
		this.doneButton.gameObject.SetActive(false);
		this.workshopAgreementButton.gameObject.SetActive(false);
		this.statusMessage.text = LocalizedTextManager.getText("uploading_your_world", null, false);
		this.loadingImage.gameObject.SetActive(true);
		this.doneImage.gameObject.SetActive(false);
		this.bar.gameObject.SetActive(true);
		this.percents.gameObject.SetActive(true);
		this.mask.gameObject.SetActive(true);
		this.barParent.SetActive(true);
		this.bar.transform.localScale = new Vector3(0f, 1f, 1f);
		WorkshopUploadingWorldWindow.uploading = true;
		SteamSDK.steamInitialized.Then(() => WorkshopMaps.uploadMap()).Then(delegate()
		{
			this.progressBarUpdate();
			WorkshopUploadingWorldWindow.uploading = false;
			this.doneButton.gameObject.SetActive(true);
			this.statusMessage.text = LocalizedTextManager.getText("world_uploaded", null, false);
			this.loadingImage.gameObject.SetActive(false);
			this.doneImage.gameObject.SetActive(true);
			if (WorkshopUploadingWorldWindow.needsWorkshopAgreement)
			{
				this.statusMessage.text = LocalizedTextManager.getText("workshop_agreement", null, false);
				this.workshopAgreementButton.SetActive(true);
			}
			else
			{
				string str = "steam://url/CommunityFilePage/";
				PublishedFileId uploaded_file_id = WorkshopMaps.uploaded_file_id;
				Application.OpenURL(str + uploaded_file_id.ToString());
			}
			this.barParent.SetActive(false);
			this.bar.gameObject.SetActive(false);
			this.percents.gameObject.SetActive(false);
			this.mask.gameObject.SetActive(false);
		}).Catch(delegate(Exception e)
		{
			this.statusMessage.text = LocalizedTextManager.getText("upload_error", null, false) + "\n( " + e.Message.ToString() + " )";
			WorkshopUploadingWorldWindow.uploading = false;
			Debug.LogError(e.Message.ToString());
			this.doneButton.gameObject.SetActive(true);
			this.doneImage.gameObject.SetActive(false);
			this.loadingImage.gameObject.SetActive(false);
			this.errorImage.gameObject.SetActive(true);
		});
	}

	// Token: 0x0600409E RID: 16542 RVA: 0x001BA7B8 File Offset: 0x001B89B8
	private void Update()
	{
		if (WorkshopUploadingWorldWindow.uploading || this.percents.isActiveAndEnabled)
		{
			this.progressBarUpdate();
		}
	}

	// Token: 0x0600409F RID: 16543 RVA: 0x001BA7D4 File Offset: 0x001B89D4
	private void progressBarUpdate()
	{
		float progress = WorkshopMaps.uploadProgress;
		float tVal = this.bar.transform.localScale.x;
		if (this.bar.transform.localScale.x < progress)
		{
			tVal = this.bar.transform.localScale.x + Time.deltaTime;
			if (tVal > progress || progress > 0.75f)
			{
				tVal = progress;
			}
			this.bar.transform.localScale = new Vector3(tVal, 1f, 1f);
			this.percents.text = Mathf.CeilToInt(tVal * 100f).ToString() + " %";
			return;
		}
		this.percents.text = Mathf.CeilToInt(progress * 100f).ToString() + " %";
	}

	// Token: 0x04002ED2 RID: 11986
	public Button doneButton;

	// Token: 0x04002ED3 RID: 11987
	public UnityEngine.UI.Image loadingImage;

	// Token: 0x04002ED4 RID: 11988
	public UnityEngine.UI.Image doneImage;

	// Token: 0x04002ED5 RID: 11989
	public UnityEngine.UI.Image errorImage;

	// Token: 0x04002ED6 RID: 11990
	public GameObject barParent;

	// Token: 0x04002ED7 RID: 11991
	public Text statusMessage;

	// Token: 0x04002ED8 RID: 11992
	public Text percents;

	// Token: 0x04002ED9 RID: 11993
	public UnityEngine.UI.Image bar;

	// Token: 0x04002EDA RID: 11994
	public UnityEngine.UI.Image mask;

	// Token: 0x04002EDB RID: 11995
	public static bool uploading;

	// Token: 0x04002EDC RID: 11996
	public static bool needsWorkshopAgreement;

	// Token: 0x04002EDD RID: 11997
	public GameObject workshopAgreementButton;
}
