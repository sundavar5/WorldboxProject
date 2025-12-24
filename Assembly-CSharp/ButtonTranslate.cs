using System;
using UnityEngine;

// Token: 0x0200082B RID: 2091
public class ButtonTranslate : MonoBehaviour
{
	// Token: 0x06004132 RID: 16690 RVA: 0x001BC74F File Offset: 0x001BA94F
	public void openLink()
	{
		Analytics.LogEvent("click_translate", true, true);
		Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSeL8sirqSFbHa_dHipgu-2QiRSNHqEn2l7ApodM8qD5xm010A/viewform");
	}
}
