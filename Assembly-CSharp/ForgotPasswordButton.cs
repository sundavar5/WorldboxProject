using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007B7 RID: 1975
public class ForgotPasswordButton : MonoBehaviour
{
	// Token: 0x06003E70 RID: 15984 RVA: 0x001B2798 File Offset: 0x001B0998
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.newStatus("");
		this.emailInput.gameObject.SetActive(true);
		this.emailBG.gameObject.SetActive(true);
		this.continueButton.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		this.forgotPasswordButton = base.gameObject.GetComponent<Button>();
		this.checking = false;
	}

	// Token: 0x06003E71 RID: 15985 RVA: 0x001B280F File Offset: 0x001B0A0F
	public void resetPassword()
	{
		this.checking = true;
		this.clearStatus();
	}

	// Token: 0x06003E72 RID: 15986 RVA: 0x001B281E File Offset: 0x001B0A1E
	private void Update()
	{
		this.forgotPasswordButton.interactable = !this.checking;
	}

	// Token: 0x06003E73 RID: 15987 RVA: 0x001B2834 File Offset: 0x001B0A34
	private void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.statusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.statusMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.statusMessage.text = pMessage;
	}

	// Token: 0x06003E74 RID: 15988 RVA: 0x001B2888 File Offset: 0x001B0A88
	private void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x04002D8B RID: 11659
	public GameObject emailBG;

	// Token: 0x04002D8C RID: 11660
	public InputField emailInput;

	// Token: 0x04002D8D RID: 11661
	public Text statusMessage;

	// Token: 0x04002D8E RID: 11662
	public Button continueButton;

	// Token: 0x04002D8F RID: 11663
	private Button forgotPasswordButton;

	// Token: 0x04002D90 RID: 11664
	private bool checking;
}
