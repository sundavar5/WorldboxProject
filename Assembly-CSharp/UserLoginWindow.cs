using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007BF RID: 1983
public class UserLoginWindow : MonoBehaviour
{
	// Token: 0x06003EA2 RID: 16034 RVA: 0x001B2F64 File Offset: 0x001B1164
	public void Start()
	{
		this.checkState();
		if (PlayerConfig.dict["username"].stringVal != "")
		{
			this.inputTextUser.text = PlayerConfig.dict["username"].stringVal;
		}
	}

	// Token: 0x06003EA3 RID: 16035 RVA: 0x001B2FB8 File Offset: 0x001B11B8
	public void checkState()
	{
		Debug.Log("Check Login Window State");
		if (Auth.isLoggedIn)
		{
			if (Auth.displayName != "" && Auth.displayName != null)
			{
				Debug.Log("displayName found");
				this.usernameText.text = Auth.displayName;
			}
			else if (Auth.userName != "" && Auth.userName != null)
			{
				Debug.Log("userName found");
				this.usernameText.text = Auth.userName;
			}
			else
			{
				Debug.Log("emailAddress found");
				this.usernameText.text = Auth.emailAddress;
			}
			this.setLogout();
		}
		else
		{
			this.setLogin();
		}
		this.isLoggedIn = Auth.isLoggedIn;
	}

	// Token: 0x06003EA4 RID: 16036 RVA: 0x001B3074 File Offset: 0x001B1274
	public void Update()
	{
		if (this.isLoggedIn != Auth.isLoggedIn)
		{
			this.checkState();
		}
	}

	// Token: 0x06003EA5 RID: 16037 RVA: 0x001B308C File Offset: 0x001B128C
	public void setLoading()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "logging_in";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogin.SetActive(false);
		this.groupLogged.SetActive(false);
		this.groupLoading.SetActive(true);
	}

	// Token: 0x06003EA6 RID: 16038 RVA: 0x001B30E4 File Offset: 0x001B12E4
	public void setLogin()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "Login";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogged.SetActive(false);
		this.groupLoading.SetActive(false);
		this.groupLogin.SetActive(true);
	}

	// Token: 0x06003EA7 RID: 16039 RVA: 0x001B313C File Offset: 0x001B133C
	public void setLogout()
	{
		this.windowTitle.GetComponent<LocalizedText>().key = "welcome_worldnet";
		this.windowTitle.GetComponent<LocalizedText>().updateText(true);
		this.groupLogin.SetActive(false);
		this.groupLoading.SetActive(false);
		this.groupLogged.SetActive(true);
	}

	// Token: 0x06003EA8 RID: 16040 RVA: 0x001B3193 File Offset: 0x001B1393
	public void clearWindow(string pMessage = "...")
	{
		this.textMessage.text = pMessage;
		this.inputTextPassword.text = "";
		this.inputTextUser.text = "";
	}

	// Token: 0x06003EA9 RID: 16041 RVA: 0x001B31C4 File Offset: 0x001B13C4
	public void clearCredentials()
	{
		this.inputTextPassword.text = "";
		this.inputTextUser.text = "";
		if (PlayerConfig.dict["username"].stringVal != "")
		{
			this.inputTextUser.text = PlayerConfig.dict["username"].stringVal;
		}
	}

	// Token: 0x04002D9F RID: 11679
	public GameObject groupLogged;

	// Token: 0x04002DA0 RID: 11680
	public GameObject groupLogin;

	// Token: 0x04002DA1 RID: 11681
	public GameObject groupLoading;

	// Token: 0x04002DA2 RID: 11682
	public Text usernameText;

	// Token: 0x04002DA3 RID: 11683
	public Text windowTitle;

	// Token: 0x04002DA4 RID: 11684
	public InputField inputTextUser;

	// Token: 0x04002DA5 RID: 11685
	public InputField inputTextPassword;

	// Token: 0x04002DA6 RID: 11686
	public Text textMessage;

	// Token: 0x04002DA7 RID: 11687
	private bool isLoggedIn;
}
