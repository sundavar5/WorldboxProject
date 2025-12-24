using System;
using UnityEngine;

// Token: 0x0200082D RID: 2093
public class ButtonVersionUpdate : MonoBehaviour
{
	// Token: 0x06004136 RID: 16694 RVA: 0x001BC78F File Offset: 0x001BA98F
	public void openLink()
	{
		Analytics.LogEvent("open_link_version", true, true);
		Application.OpenURL("https://www.superworldbox.com/");
	}
}
