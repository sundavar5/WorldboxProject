using System;
using UnityEngine;
using UnityEngine.UI;
using WorldBoxConsole;

// Token: 0x02000612 RID: 1554
public class VersionTextUpdater : MonoBehaviour
{
	// Token: 0x060032ED RID: 13037 RVA: 0x001811C8 File Offset: 0x0017F3C8
	private void Start()
	{
		if (this.addText)
		{
			this.text.text = "version: " + Application.version + "-" + Config.versionCodeText;
			if (!string.IsNullOrEmpty(Config.gitCodeText))
			{
				Text text = this.text;
				text.text = text.text + "@" + Config.gitCodeText;
				return;
			}
		}
		else
		{
			string tPlatform = Application.platform.ToString().ToLower();
			tPlatform = tPlatform.Replace("player", "");
			this.text.text = string.Concat(new string[]
			{
				tPlatform,
				" ",
				Application.version,
				"-",
				Config.versionCodeText
			});
			if (!string.IsNullOrEmpty(Config.gitCodeText))
			{
				Text text2 = this.text;
				text2.text = text2.text + "@" + Config.gitCodeText;
			}
			try
			{
				if (!string.IsNullOrEmpty(RequestHelper.salt) && RequestHelper.salt != "err")
				{
					Text text3 = this.text;
					text3.text = text3.text + " (" + RequestHelper.salt.Substring(0, 2) + ")";
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x0018132C File Offset: 0x0017F52C
	private void Update()
	{
		if (this.errored)
		{
			return;
		}
		if (!this.modded && Config.MODDED)
		{
			this.text.color = Color.yellow;
			this.modded = true;
		}
		if (LogHandler.errorNum > 0 || Console.hasErrors())
		{
			if (this.modded)
			{
				this.text.color = Color.cyan;
			}
			else
			{
				this.text.color = Color.red;
			}
			this.errored = true;
			return;
		}
	}

	// Token: 0x04002699 RID: 9881
	public bool addText = true;

	// Token: 0x0400269A RID: 9882
	public Text text;

	// Token: 0x0400269B RID: 9883
	private bool errored;

	// Token: 0x0400269C RID: 9884
	private bool modded;
}
