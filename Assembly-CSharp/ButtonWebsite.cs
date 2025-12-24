using System;
using UnityEngine;

// Token: 0x0200082F RID: 2095
public class ButtonWebsite : MonoBehaviour
{
	// Token: 0x0600413A RID: 16698 RVA: 0x001BC7E8 File Offset: 0x001BA9E8
	public void openLink()
	{
		Analytics.LogEvent("open_link_website", true, true);
		Application.OpenURL("https://www.superworldbox.com/");
	}

	// Token: 0x0600413B RID: 16699 RVA: 0x001BC800 File Offset: 0x001BAA00
	public void openLinkLSFLW2()
	{
		Analytics.LogEvent("open_link_lsflw2", true, true);
		Application.OpenURL("https://apps.apple.com/app/apple-store/id1397453494?pt=117120454&ct=worldbox&mt=8");
	}

	// Token: 0x0600413C RID: 16700 RVA: 0x001BC818 File Offset: 0x001BAA18
	public void openPatchLog()
	{
		Analytics.LogEvent("open_link_changelog", true, true);
		Application.OpenURL("https://www.superworldbox.com/changelog");
	}
}
