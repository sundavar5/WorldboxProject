using System;
using UnityEngine;

// Token: 0x02000808 RID: 2056
public class WorkshopHelpers : MonoBehaviour
{
	// Token: 0x0600407D RID: 16509 RVA: 0x001B9E74 File Offset: 0x001B8074
	public void openCurrentMapInWorkshop()
	{
		Application.OpenURL("steam://url/CommunityFilePage/" + SaveManager.currentWorkshopMapData.workshop_item.Id.ToString());
	}

	// Token: 0x0600407E RID: 16510 RVA: 0x001B9EAD File Offset: 0x001B80AD
	public void openUploadWorld()
	{
		SaveManager.clearCurrentSelectedWorld();
		ScrollWindow.showWindow("steam_workshop_upload_world");
	}

	// Token: 0x0600407F RID: 16511 RVA: 0x001B9EBE File Offset: 0x001B80BE
	public void openBrowseWorlds()
	{
		SaveManager.clearCurrentSelectedWorld();
		ScrollWindow.showWindow("steam_workshop_browse");
	}

	// Token: 0x04002EAC RID: 11948
	public const string color_own_map = "#3DDEFF";

	// Token: 0x04002EAD RID: 11949
	public const string color_other_map = "#FF9B1C";
}
