using System;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
public class RegisterButton : MonoBehaviour
{
	// Token: 0x06003E76 RID: 15990 RVA: 0x001B289D File Offset: 0x001B0A9D
	public void usernameCheck()
	{
		Debug.Log("Name:  " + this.userRegisterWindow.inputTextUsername.text);
		this.userRegisterWindow.setPage2();
	}

	// Token: 0x06003E77 RID: 15991 RVA: 0x001B28CC File Offset: 0x001B0ACC
	public void tryRegister()
	{
		this.clearStatus();
		Debug.Log("Name:  " + this.userRegisterWindow.inputTextUsername.text);
		Debug.Log("Email: " + this.userRegisterWindow.inputTextEmail.text);
		string username = this.userRegisterWindow.inputTextUsername.text;
		string email = this.userRegisterWindow.inputTextEmail.text;
		string password = this.userRegisterWindow.inputTextPassword.text;
		if (email == "" || password == "")
		{
			this.newStatus("EmailPasswordEmpty");
			return;
		}
		if (!Auth.isValidEmail(email))
		{
			this.newStatus("InvalidEmail");
			return;
		}
		if (password.Length < 6)
		{
			this.newStatus("ShortPassword");
			return;
		}
		this.userRegisterWindow.RegisterNewAccount(username, password, email);
	}

	// Token: 0x06003E78 RID: 15992 RVA: 0x001B29AD File Offset: 0x001B0BAD
	private void sendVerification()
	{
		Debug.Log("send verification");
	}

	// Token: 0x06003E79 RID: 15993 RVA: 0x001B29BC File Offset: 0x001B0BBC
	private void newStatus(string pMessage)
	{
		Debug.Log("new status " + pMessage);
		if (LocalizedTextManager.stringExists(pMessage))
		{
			this.userRegisterWindow.textMessage.GetComponent<LocalizedText>().key = pMessage;
			this.userRegisterWindow.textMessage.GetComponent<LocalizedText>().updateText(true);
			return;
		}
		this.userRegisterWindow.textMessage.text = pMessage;
	}

	// Token: 0x06003E7A RID: 15994 RVA: 0x001B2A1F File Offset: 0x001B0C1F
	private void clearStatus()
	{
		this.newStatus("");
	}

	// Token: 0x04002D91 RID: 11665
	public UserRegisterWindow userRegisterWindow;
}
