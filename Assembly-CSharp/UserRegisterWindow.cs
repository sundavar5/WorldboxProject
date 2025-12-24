using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007C0 RID: 1984
public class UserRegisterWindow : MonoBehaviour
{
	// Token: 0x06003EAB RID: 16043 RVA: 0x001B3238 File Offset: 0x001B1438
	public void Start()
	{
		this.checkState();
	}

	// Token: 0x06003EAC RID: 16044 RVA: 0x001B3240 File Offset: 0x001B1440
	private void OnEnable()
	{
		this.checkState();
	}

	// Token: 0x06003EAD RID: 16045 RVA: 0x001B3248 File Offset: 0x001B1448
	public void RegisterNewAccount(string username, string password, string email)
	{
		UserRegisterWindow._username = username;
		UserRegisterWindow._password = password;
		UserRegisterWindow._email = email;
	}

	// Token: 0x06003EAE RID: 16046 RVA: 0x001B325C File Offset: 0x001B145C
	public void registerAccountCallback(string errorReason)
	{
		Config.lockGameControls = false;
	}

	// Token: 0x06003EAF RID: 16047 RVA: 0x001B3264 File Offset: 0x001B1464
	public void checkState()
	{
		Debug.Log("Check Register Window State");
		if (Auth.isLoggedIn)
		{
			this.setSuccess();
			return;
		}
		this.setPage1();
		this.blockRegister1Button();
		this.blockRegister2Button();
	}

	// Token: 0x06003EB0 RID: 16048 RVA: 0x001B3290 File Offset: 0x001B1490
	public void setSuccess()
	{
		Config.lockGameControls = false;
		this.page2.SetActive(false);
		this.page1.SetActive(false);
		this.creationPage.SetActive(false);
		this.successPage.SetActive(true);
	}

	// Token: 0x06003EB1 RID: 16049 RVA: 0x001B32C8 File Offset: 0x001B14C8
	public void setPage2()
	{
		Config.lockGameControls = false;
		this.page1.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(false);
		this.page2.SetActive(true);
	}

	// Token: 0x06003EB2 RID: 16050 RVA: 0x001B3300 File Offset: 0x001B1500
	public void setPage1()
	{
		Config.lockGameControls = false;
		this.page2.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(false);
		this.page1.SetActive(true);
		InputField inputField = this.inputTextUsername;
		if (!string.IsNullOrEmpty((inputField != null) ? inputField.text : null))
		{
			RegisterUsername.runUsernameCheck(this.inputTextUsername);
		}
	}

	// Token: 0x06003EB3 RID: 16051 RVA: 0x001B3367 File Offset: 0x001B1567
	public void setCreation()
	{
		Config.lockGameControls = true;
		this.page1.SetActive(false);
		this.page2.SetActive(false);
		this.successPage.SetActive(false);
		this.creationPage.SetActive(true);
	}

	// Token: 0x06003EB4 RID: 16052 RVA: 0x001B339F File Offset: 0x001B159F
	public void blockRegister1Button()
	{
		this.usernameCheckButton.GetComponent<CanvasGroup>().alpha = 0.2f;
		this.usernameCheckButton.interactable = false;
	}

	// Token: 0x06003EB5 RID: 16053 RVA: 0x001B33C2 File Offset: 0x001B15C2
	public void unblockRegister1Button()
	{
		this.usernameCheckButton.GetComponent<CanvasGroup>().alpha = 1f;
		this.usernameCheckButton.interactable = true;
	}

	// Token: 0x06003EB6 RID: 16054 RVA: 0x001B33E5 File Offset: 0x001B15E5
	public void blockRegister2Button()
	{
		this.emailCheckButton.GetComponent<CanvasGroup>().alpha = 0.2f;
		this.emailCheckButton.interactable = false;
	}

	// Token: 0x06003EB7 RID: 16055 RVA: 0x001B3408 File Offset: 0x001B1608
	public void unblockRegister2Button()
	{
		this.emailCheckButton.GetComponent<CanvasGroup>().alpha = 1f;
		this.emailCheckButton.interactable = true;
	}

	// Token: 0x06003EB8 RID: 16056 RVA: 0x001B342C File Offset: 0x001B162C
	public void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.textMessage.GetComponent<LocalizedText>().key = pMessage;
			this.textMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.textMessage.text = pMessage;
	}

	// Token: 0x06003EB9 RID: 16057 RVA: 0x001B3480 File Offset: 0x001B1680
	public void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x06003EBA RID: 16058 RVA: 0x001B348D File Offset: 0x001B168D
	public void blockRegisterButton()
	{
	}

	// Token: 0x04002DA8 RID: 11688
	public GameObject page1;

	// Token: 0x04002DA9 RID: 11689
	public GameObject page2;

	// Token: 0x04002DAA RID: 11690
	public GameObject successPage;

	// Token: 0x04002DAB RID: 11691
	public GameObject creationPage;

	// Token: 0x04002DAC RID: 11692
	public Button usernameCheckButton;

	// Token: 0x04002DAD RID: 11693
	public Button emailCheckButton;

	// Token: 0x04002DAE RID: 11694
	public InputField inputTextUsername;

	// Token: 0x04002DAF RID: 11695
	public InputField inputTextEmail;

	// Token: 0x04002DB0 RID: 11696
	public InputField inputTextPassword;

	// Token: 0x04002DB1 RID: 11697
	public Text textMessage;

	// Token: 0x04002DB2 RID: 11698
	private static string _email = "";

	// Token: 0x04002DB3 RID: 11699
	private static string _password = "";

	// Token: 0x04002DB4 RID: 11700
	private static string _username = "";
}
