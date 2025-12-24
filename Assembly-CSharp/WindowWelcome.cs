using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000803 RID: 2051
public class WindowWelcome : MonoBehaviour
{
	// Token: 0x06004065 RID: 16485 RVA: 0x001B81CC File Offset: 0x001B63CC
	public void Awake()
	{
		if (Config.isMobile)
		{
			this.text_tip = base.transform.FindRecursive("Text Mobile").GetComponent<LocalizedText>();
		}
		else
		{
			this.text_tip = base.transform.FindRecursive("Text PC").GetComponent<LocalizedText>();
		}
		this.top_text = base.transform.FindRecursive("Text Main").GetComponent<LocalizedText>();
	}

	// Token: 0x06004066 RID: 16486 RVA: 0x001B8233 File Offset: 0x001B6433
	public void nextTip()
	{
		this.text_tip.setKeyAndUpdate(LoadingScreen.getTipID());
	}

	// Token: 0x06004067 RID: 16487 RVA: 0x001B8248 File Offset: 0x001B6448
	private void Update()
	{
		if (Config.MODDED || Config.experimental_mode)
		{
			this.top_text.setKeyAndUpdate("mods_warning");
			this.top_text.GetComponent<Text>().color = Toolbox.color_red;
		}
		if (Time.frameCount % 30 == 0)
		{
			this.tldrCheck();
		}
	}

	// Token: 0x06004068 RID: 16488 RVA: 0x001B8298 File Offset: 0x001B6498
	private void tldrCheck()
	{
		if (this._content.GetComponentsInChildren<UiCreature>().Length > 0)
		{
			return;
		}
		AchievementLibrary.tldr.check(null);
	}

	// Token: 0x04002EA0 RID: 11936
	[SerializeField]
	private Transform _content;

	// Token: 0x04002EA1 RID: 11937
	private LocalizedText top_text;

	// Token: 0x04002EA2 RID: 11938
	private LocalizedText text_tip;
}
