using System;
using UnityEngine;

// Token: 0x0200082C RID: 2092
public class ButtonTwitter : MonoBehaviour
{
	// Token: 0x06004134 RID: 16692 RVA: 0x001BC76F File Offset: 0x001BA96F
	public void openLink()
	{
		Analytics.LogEvent("open_link_twitter", true, true);
		Application.OpenURL("http://twitter.com/mixamko");
	}
}
