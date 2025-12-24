using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class GameStats : MonoBehaviour
{
	// Token: 0x06000DAB RID: 3499 RVA: 0x000BD860 File Offset: 0x000BBA60
	private void Start()
	{
		this.dataPath = Application.persistentDataPath + "/stats.json";
		this.loadData();
		if (this.data == null)
		{
			this.data = new GameStatsData();
		}
		else
		{
			this.checkDataForErrors();
		}
		this.saveTimer = new WorldTimer(30f, new Action(this.saveData));
		this.data.gameLaunches += 1L;
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000BD8D3 File Offset: 0x000BBAD3
	internal bool goodForAds()
	{
		return true;
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000BD8D8 File Offset: 0x000BBAD8
	private void saveData()
	{
		string tWhat = "Stats";
		bool hasError = false;
		string tPathSaveData = this.dataPath;
		string tTempPath = this.dataPath + ".tmp";
		try
		{
			if (!Directory.Exists(Application.persistentDataPath))
			{
				Directory.CreateDirectory(Application.persistentDataPath);
			}
		}
		catch (Exception message)
		{
			WorldTip.showNow("Error creating directory to save stats in! Check console for details", false, "top", 3f, "#F3961F");
			Debug.Log("Error creating directory: " + Application.persistentDataPath);
			Debug.Log(message);
		}
		try
		{
			using (FileStream fs = new FileStream(tTempPath, FileMode.Create, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					using (JsonWriter writer = new JsonTextWriter(sw))
					{
						new JsonSerializer().Serialize(writer, this.data);
					}
				}
			}
		}
		catch (IOException e)
		{
			if (Toolbox.IsDiskFull(e))
			{
				WorldTip.showNow("Error saving " + tWhat + " : Disk full!", false, "top", 3f, "#F3961F");
			}
			else
			{
				Debug.Log("Could not save " + tWhat + " due to hard drive / IO Error : ");
				Debug.Log(e);
				WorldTip.showNow("Error saving " + tWhat + " due to IOError! Check console for details", false, "top", 3f, "#F3961F");
			}
			hasError = true;
		}
		catch (Exception message2)
		{
			Debug.Log("Could not save " + tWhat + " due to error : ");
			Debug.Log(message2);
			WorldTip.showNow("Error saving " + tWhat + "! Check console for errors", false, "top", 3f, "#F3961F");
			hasError = true;
		}
		if (hasError)
		{
			if (File.Exists(tTempPath))
			{
				File.Delete(tTempPath);
			}
		}
		else
		{
			Toolbox.MoveSafely(tTempPath, tPathSaveData);
		}
		AchievementLibrary.life_is_a_sim.check(null);
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000BDAE0 File Offset: 0x000BBCE0
	private void checkDataForErrors()
	{
		if (double.IsNaN(this.data.gameTime) || double.IsInfinity(this.data.gameTime) || this.data.gameTime < 0.0)
		{
			Debug.Log(this.data.gameTime);
			Debug.LogError("Game time is NaN or Infinity! Resetting to 0");
			this.data.gameTime = 0.0;
		}
		if (this.data.creaturesBorn < 0L)
		{
			this.data.creaturesBorn = Math.Max(0L, this.data.creaturesDied - this.data.creaturesCreated);
		}
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x000BDB94 File Offset: 0x000BBD94
	private void loadData()
	{
		if (!File.Exists(this.dataPath))
		{
			return;
		}
		try
		{
			using (FileStream fs = new FileStream(this.dataPath, FileMode.Open, FileAccess.Read))
			{
				using (StreamReader sr = new StreamReader(fs))
				{
					using (JsonReader reader = new JsonTextReader(sr))
					{
						JsonSerializer serializer = new JsonSerializer();
						this.data = serializer.Deserialize<GameStatsData>(reader);
					}
				}
			}
		}
		catch (Exception message)
		{
			Debug.Log("exception caught when loading stats");
			Debug.LogError(message);
		}
		if (this.data == null)
		{
			Debug.LogError("(!) stats not has been loaded");
		}
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x000BDC5C File Offset: 0x000BBE5C
	public void updateStats(float pTime)
	{
		this.data.gameTime += (double)pTime;
		this.saveTimer.update(-1f);
	}

	// Token: 0x04000D95 RID: 3477
	internal GameStatsData data;

	// Token: 0x04000D96 RID: 3478
	private string dataPath;

	// Token: 0x04000D97 RID: 3479
	private WorldTimer saveTimer;
}
