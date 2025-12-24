using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007B9 RID: 1977
public class RegisterDetails : MonoBehaviour
{
	// Token: 0x06003E7C RID: 15996 RVA: 0x001B2A34 File Offset: 0x001B0C34
	private void OnEnable()
	{
		RegisterDetails.checkButton();
	}

	// Token: 0x06003E7D RID: 15997 RVA: 0x001B2A3B File Offset: 0x001B0C3B
	public void emailCheck(InputField pEmail)
	{
		RegisterDetails.runEmailCheck(pEmail);
	}

	// Token: 0x06003E7E RID: 15998 RVA: 0x001B2A44 File Offset: 0x001B0C44
	public static void runEmailCheck(InputField pEmail)
	{
		string email = pEmail.text;
		RegisterDetails.emailValid = false;
		Debug.Log("Name: " + email);
		if (!Auth.isValidEmail(email))
		{
			RegisterDetails.newStatus("InvalidEmail");
			RegisterDetails.checkButton();
			Debug.Log("Not valid");
			return;
		}
		RegisterDetails.clearStatus();
		RegisterDetails.emailValid = true;
		RegisterDetails.checkButton();
	}

	// Token: 0x06003E7F RID: 15999 RVA: 0x001B2AA0 File Offset: 0x001B0CA0
	public void passwordCheck(InputField pEmail)
	{
		RegisterDetails.runPasswordCheck(pEmail);
	}

	// Token: 0x06003E80 RID: 16000 RVA: 0x001B2AA8 File Offset: 0x001B0CA8
	public static void runPasswordCheck(InputField pPassword)
	{
		string password = pPassword.text;
		RegisterDetails.passwordValid = false;
		Debug.Log("Pass: " + password);
		if (password.Length < 6)
		{
			RegisterDetails.newStatus("ShortPassword");
			RegisterDetails.checkButton();
			Debug.Log("Not valid");
			return;
		}
		RegisterDetails.clearStatus();
		RegisterDetails.passwordValid = true;
		RegisterDetails.checkButton();
	}

	// Token: 0x06003E81 RID: 16001 RVA: 0x001B2B05 File Offset: 0x001B0D05
	private static void checkButton()
	{
		if (RegisterDetails.emailValid && RegisterDetails.passwordValid)
		{
			RegisterDetails.unblockRegisterButton();
			return;
		}
		RegisterDetails.blockRegisterButton();
	}

	// Token: 0x06003E82 RID: 16002 RVA: 0x001B2B20 File Offset: 0x001B0D20
	private static void blockRegisterButton()
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().blockRegister2Button();
		}
	}

	// Token: 0x06003E83 RID: 16003 RVA: 0x001B2B3D File Offset: 0x001B0D3D
	private static void unblockRegisterButton()
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().unblockRegister2Button();
		}
	}

	// Token: 0x06003E84 RID: 16004 RVA: 0x001B2B5A File Offset: 0x001B0D5A
	private static void newStatus(string pMessage)
	{
		if (RegisterDetails.registerWindowExists())
		{
			ScrollWindow.get("register").GetComponent<UserRegisterWindow>().newStatus(pMessage);
		}
	}

	// Token: 0x06003E85 RID: 16005 RVA: 0x001B2B78 File Offset: 0x001B0D78
	private static bool registerWindowExists()
	{
		return ScrollWindow.get("register") != null && ScrollWindow.get("register").GetComponent<UserRegisterWindow>() != null;
	}

	// Token: 0x06003E86 RID: 16006 RVA: 0x001B2BA3 File Offset: 0x001B0DA3
	private static void clearStatus()
	{
		RegisterDetails.newStatus("");
	}

	// Token: 0x04002D92 RID: 11666
	private static bool emailValid;

	// Token: 0x04002D93 RID: 11667
	private static bool passwordValid;
}
