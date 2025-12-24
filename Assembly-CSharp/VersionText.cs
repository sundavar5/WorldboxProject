using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007C2 RID: 1986
public class VersionText : MonoBehaviour
{
	// Token: 0x06003EBE RID: 16062 RVA: 0x001B34BF File Offset: 0x001B16BF
	private void Awake()
	{
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x06003EBF RID: 16063 RVA: 0x001B34CD File Offset: 0x001B16CD
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.text.GetComponent<LocalizedText>().updateText(true);
	}

	// Token: 0x06003EC0 RID: 16064 RVA: 0x001B34E8 File Offset: 0x001B16E8
	private void Update()
	{
		if (this.text == null)
		{
			return;
		}
		this.text.text = this.text.text.Replace("$old_version$", this.oldText(Config.gv));
		this.text.text = this.text.text.Replace("$new_version$", this.newText(VersionCheck.onlineVersion));
	}

	// Token: 0x06003EC1 RID: 16065 RVA: 0x001B355A File Offset: 0x001B175A
	private string oldText(string pText)
	{
		return "<color=#FF0000>" + pText + "</color>";
	}

	// Token: 0x06003EC2 RID: 16066 RVA: 0x001B356C File Offset: 0x001B176C
	private string newText(string pText)
	{
		return "<color=#00FF00>" + pText + "</color>";
	}

	// Token: 0x04002DBB RID: 11707
	internal Text text;
}
