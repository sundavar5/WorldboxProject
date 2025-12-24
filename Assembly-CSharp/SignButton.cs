using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007BB RID: 1979
public class SignButton : MonoBehaviour
{
	// Token: 0x06003E92 RID: 16018 RVA: 0x001B2CC0 File Offset: 0x001B0EC0
	public void tryLogin()
	{
		this.clearStatus();
		this.loginEmail = this.textName.text;
		this.loginPassword = this.textPassword.text;
		this.loginUsername = "";
		if (this.loginEmail == "" || this.loginPassword == "")
		{
			this.errorStatus("EmailPasswordEmpty");
			return;
		}
		if (Auth.isValidEmail(this.loginEmail))
		{
			this.userLoginWindow.setLoading();
			this.continueLogin();
			return;
		}
		this.loginUsername = this.textName.text;
		this.loginEmail = "";
		if (!Username.isValid(this.loginUsername))
		{
			this.errorStatus("InvalidUsername");
			return;
		}
		PlayerConfig.dict["username"].stringVal = this.loginUsername;
		PlayerConfig.saveData();
		Login.GetEmailForUsername(this.loginUsername, this.loginPassword, new Action<string, string>(this.emailLoginCallback));
		this.userLoginWindow.setLoading();
	}

	// Token: 0x06003E93 RID: 16019 RVA: 0x001B2DCB File Offset: 0x001B0FCB
	public void continueLogin()
	{
	}

	// Token: 0x06003E94 RID: 16020 RVA: 0x001B2DCD File Offset: 0x001B0FCD
	public void emailLoginCallback(string returnedEmail, string errorReason)
	{
		if (errorReason != "")
		{
			this.userLoginWindow.setLogin();
			this.errorStatus(errorReason);
			return;
		}
		this.loginEmail = returnedEmail;
		this.continueLogin();
	}

	// Token: 0x06003E95 RID: 16021 RVA: 0x001B2DFC File Offset: 0x001B0FFC
	private void errorStatus(string pMessage)
	{
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textStatusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textStatusMessage.GetComponent<LocalizedText>().updateText(true);
		}
		else
		{
			this.textStatusMessage.text = pMessage;
		}
		this.textStatusMessage.color = Toolbox.makeColor("#FF8686");
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x001B2E58 File Offset: 0x001B1058
	private void goodStatus(string pMessage)
	{
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textStatusMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textStatusMessage.GetComponent<LocalizedText>().updateText(true);
		}
		else
		{
			this.textStatusMessage.text = pMessage;
		}
		this.textStatusMessage.color = Toolbox.makeColor("#95DD5D");
	}

	// Token: 0x06003E97 RID: 16023 RVA: 0x001B2EB2 File Offset: 0x001B10B2
	private void clearStatus()
	{
		this.textStatusMessage.text = "";
	}

	// Token: 0x04002D96 RID: 11670
	public UserLoginWindow userLoginWindow;

	// Token: 0x04002D97 RID: 11671
	public InputField textName;

	// Token: 0x04002D98 RID: 11672
	public InputField textPassword;

	// Token: 0x04002D99 RID: 11673
	public Text textStatusMessage;

	// Token: 0x04002D9A RID: 11674
	private string loginEmail;

	// Token: 0x04002D9B RID: 11675
	private string loginPassword;

	// Token: 0x04002D9C RID: 11676
	private string loginUsername;
}
