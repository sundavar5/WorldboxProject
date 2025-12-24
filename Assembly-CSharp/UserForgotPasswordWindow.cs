using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007BE RID: 1982
public class UserForgotPasswordWindow : MonoBehaviour
{
	// Token: 0x06003E9F RID: 16031 RVA: 0x001B2F25 File Offset: 0x001B1125
	public void Start()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.forgotPasswordButton.gameObject.SetActive(true);
	}

	// Token: 0x06003EA0 RID: 16032 RVA: 0x001B2F40 File Offset: 0x001B1140
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.forgotPasswordButton.gameObject.SetActive(true);
	}

	// Token: 0x04002D9E RID: 11678
	public Button forgotPasswordButton;
}
