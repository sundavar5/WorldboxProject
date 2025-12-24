using System;
using UnityEngine;

// Token: 0x0200080E RID: 2062
public class WorkshopOpenSteamWorkshop : MonoBehaviour
{
	// Token: 0x06004092 RID: 16530 RVA: 0x001BA51F File Offset: 0x001B871F
	public void playWorkShopMap()
	{
		Application.OpenURL(string.Format("steam://url/SteamWorkshopPage/{0}", 1206560U));
	}

	// Token: 0x06004093 RID: 16531 RVA: 0x001BA53A File Offset: 0x001B873A
	public void openWorkShopAgreement()
	{
		Application.OpenURL("steam://url/CommunityFilePage/" + WorkshopOpenSteamWorkshop.fileID);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04002ECD RID: 11981
	public static string fileID;
}
