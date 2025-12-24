using System;
using UnityEngine;

// Token: 0x0200082E RID: 2094
public class ButtonVote : MonoBehaviour
{
	// Token: 0x06004138 RID: 16696 RVA: 0x001BC7AF File Offset: 0x001BA9AF
	public void openLink()
	{
		Analytics.LogEvent("click_vote", true, true);
		if (Config.isAndroid)
		{
			Application.OpenURL("https://play.google.com/store/apps/details?id=com.mkarpenko.worldbox");
			return;
		}
		if (Config.isIos)
		{
			Application.OpenURL("https://itunes.apple.com/app/id1450941371");
		}
	}
}
