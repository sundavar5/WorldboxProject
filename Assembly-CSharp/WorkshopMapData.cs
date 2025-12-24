using System;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x0200080A RID: 2058
public class WorkshopMapData
{
	// Token: 0x06004083 RID: 16515 RVA: 0x001B9F78 File Offset: 0x001B8178
	public static WorkshopMapData currentMapToWorkshop()
	{
		WorkshopMapData workshopMapData = new WorkshopMapData();
		string tSavePath = SaveManager.generateWorkshopPath("");
		SavedMap mapData = SaveManager.saveWorldToDirectory(tSavePath, true, true);
		workshopMapData.meta_data_map = mapData.getMeta();
		workshopMapData.preview_image_path = tSavePath + "preview.png";
		workshopMapData.main_path = tSavePath;
		return workshopMapData;
	}

	// Token: 0x04002EB2 RID: 11954
	public string main_path;

	// Token: 0x04002EB3 RID: 11955
	public string preview_image_path;

	// Token: 0x04002EB4 RID: 11956
	public Sprite sprite_small_preview;

	// Token: 0x04002EB5 RID: 11957
	public MapMetaData meta_data_map;

	// Token: 0x04002EB6 RID: 11958
	public WorkshopMapMetaData meta_data_workshop;

	// Token: 0x04002EB7 RID: 11959
	public Steamworks.Ugc.Item workshop_item;
}
