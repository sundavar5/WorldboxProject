using System;
using UnityEngine;

// Token: 0x020005DA RID: 1498
public class HelpButton : MonoBehaviour
{
	// Token: 0x0600314D RID: 12621 RVA: 0x00179AF8 File Offset: 0x00177CF8
	public void clickHelp()
	{
		string tLocale = PlayerConfig.dict["language"].stringVal;
		Analytics.LogEvent("open_help", true, true);
		string tLink;
		if (Application.platform == RuntimePlatform.Android)
		{
			tLink = "https://support.google.com/googleplay/answer/1050566?hl=" + tLocale;
		}
		else
		{
			tLink = string.Concat(new string[]
			{
				"https://support.apple.com/",
				tLocale,
				"-",
				tLocale,
				"/HT203005"
			});
		}
		Application.OpenURL(tLink);
	}
}
