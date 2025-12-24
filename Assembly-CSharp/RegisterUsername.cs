using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007BA RID: 1978
public class RegisterUsername : MonoBehaviour
{
	// Token: 0x06003E88 RID: 16008 RVA: 0x001B2BB7 File Offset: 0x001B0DB7
	private void OnEnable()
	{
		RegisterUsername.usernameOK = false;
		RegisterUsername.termsOK = false;
	}

	// Token: 0x06003E89 RID: 16009 RVA: 0x001B2BC5 File Offset: 0x001B0DC5
	public void usernameCheck(InputField pUsername)
	{
		RegisterUsername.runUsernameCheck(pUsername);
	}

	// Token: 0x06003E8A RID: 16010 RVA: 0x001B2BD0 File Offset: 0x001B0DD0
	public static void runUsernameCheck(InputField pUsername)
	{
		RegisterUsername.<runUsernameCheck>d__4 <runUsernameCheck>d__;
		<runUsernameCheck>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<runUsernameCheck>d__.pUsername = pUsername;
		<runUsernameCheck>d__.<>1__state = -1;
		<runUsernameCheck>d__.<>t__builder.Start<RegisterUsername.<runUsernameCheck>d__4>(ref <runUsernameCheck>d__);
	}

	// Token: 0x06003E8B RID: 16011 RVA: 0x001B2C07 File Offset: 0x001B0E07
	public void termsCheck(bool pTermsEnabled)
	{
		RegisterUsername.termsOK = pTermsEnabled;
		RegisterUsername.unblockRegisterButton();
	}

	// Token: 0x06003E8C RID: 16012 RVA: 0x001B2C14 File Offset: 0x001B0E14
	private static void blockRegisterButton()
	{
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().blockRegister1Button();
		}
	}

	// Token: 0x06003E8D RID: 16013 RVA: 0x001B2C31 File Offset: 0x001B0E31
	private static void unblockRegisterButton()
	{
		if (!RegisterUsername.usernameOK || !RegisterUsername.termsOK)
		{
			RegisterUsername.blockRegisterButton();
			return;
		}
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().unblockRegister1Button();
		}
	}

	// Token: 0x06003E8E RID: 16014 RVA: 0x001B2C62 File Offset: 0x001B0E62
	private static void newStatus(string pMessage)
	{
		if (RegisterUsername.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().newStatus(pMessage);
		}
	}

	// Token: 0x06003E8F RID: 16015 RVA: 0x001B2C80 File Offset: 0x001B0E80
	private static bool registerWindowExists()
	{
		return ScrollWindow.get("register") != null && ScrollWindow.get("register").GetComponent<UserRegisterWindow>() != null;
	}

	// Token: 0x06003E90 RID: 16016 RVA: 0x001B2CAB File Offset: 0x001B0EAB
	private static void clearStatus()
	{
		RegisterUsername.newStatus("");
	}

	// Token: 0x04002D94 RID: 11668
	private static bool usernameOK;

	// Token: 0x04002D95 RID: 11669
	private static bool termsOK;
}
