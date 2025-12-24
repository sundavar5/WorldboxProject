using System;
using System.IO;
using UnityEngine;

// Token: 0x020005B8 RID: 1464
[Serializable]
public class TrailerModeSettings
{
	// Token: 0x0600304F RID: 12367 RVA: 0x00175CB4 File Offset: 0x00173EB4
	public static void startEvent()
	{
		string tPath = Application.persistentDataPath + "/trailer_settings";
		TrailerModeSettings tMode;
		if (!File.Exists(tPath))
		{
			tMode = new TrailerModeSettings();
			string tSaveJson = JsonUtility.ToJson(tMode);
			tSaveJson = tSaveJson.Replace(",", ",\n");
			tSaveJson = tSaveJson.Replace("{", "{\n");
			tSaveJson = tSaveJson.Replace("}", "\n}");
			File.WriteAllText(tPath, tSaveJson);
		}
		else
		{
			tMode = JsonUtility.FromJson<TrailerModeSettings>(File.ReadAllText(tPath));
		}
		tMode.applyTrailerSettings();
	}

	// Token: 0x06003050 RID: 12368 RVA: 0x00175D34 File Offset: 0x00173F34
	public void applyTrailerSettings()
	{
		if (this.superOrcs)
		{
			AssetManager.actor_library.get("unit_orc").base_stats["damage"] = 10000f;
		}
		else
		{
			AssetManager.actor_library.get("unit_orc").base_stats["damage"] = 18f;
		}
		DebugConfig.setOption(DebugOption.FastSpawn, this.fastSpawn, true);
		DebugConfig.setOption(DebugOption.SonicSpeed, this.sonicSpeed, true);
		World.world.move_camera.camera_move_speed = this.cameraMoveSpeed;
		World.world.move_camera.camera_move_max = this.cameraMoveMax;
		World.world.move_camera.camera_zoom_speed = this.cameraZoomSpeed;
		Globals.TRAILER_MODE_USE_RESOURCES = this.cityUseResources;
	}

	// Token: 0x0400246D RID: 9325
	public bool cityUseResources = true;

	// Token: 0x0400246E RID: 9326
	public bool sonicSpeed = true;

	// Token: 0x0400246F RID: 9327
	public bool fastSpawn = true;

	// Token: 0x04002470 RID: 9328
	public float cameraMoveSpeed = 0.001f;

	// Token: 0x04002471 RID: 9329
	public float cameraMoveMax = 0.02f;

	// Token: 0x04002472 RID: 9330
	public float cameraZoomSpeed = 3.8f;

	// Token: 0x04002473 RID: 9331
	public bool superOrcs = true;
}
