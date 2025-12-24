using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x0200045A RID: 1114
public class GameProgress
{
	// Token: 0x06002638 RID: 9784 RVA: 0x0013890F File Offset: 0x00136B0F
	public static void init()
	{
		if (GameProgress.instance != null)
		{
			return;
		}
		Debug.Log("INIT Progress");
		GameProgress.instance = new GameProgress();
		GameProgress.instance.create();
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x00138938 File Offset: 0x00136B38
	public void create()
	{
		this.setNewDataPath();
		if (File.Exists(this.dataPath))
		{
			try
			{
				this.loadData();
				return;
			}
			catch (Exception)
			{
				this.initNewSave();
				return;
			}
		}
		this.initNewSave();
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x00138980 File Offset: 0x00136B80
	private void setNewDataPath()
	{
		this.dataPath = Application.persistentDataPath + "/worldboxProgress";
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x00138997 File Offset: 0x00136B97
	private void initNewSave()
	{
		this.data = new GameProgressData();
		GameProgress.saveData();
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x001389A9 File Offset: 0x00136BA9
	public static bool unlockAchievement(string pName)
	{
		if (GameProgress.instance == null)
		{
			return false;
		}
		if (string.IsNullOrEmpty(pName))
		{
			return false;
		}
		if (GameProgress.isAchievementUnlocked(pName))
		{
			return false;
		}
		GameProgress.instance.data.achievements.Add(pName);
		GameProgress.saveData();
		return true;
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x001389E4 File Offset: 0x00136BE4
	public static bool isAchievementUnlocked(string pName)
	{
		return GameProgress.instance != null && GameProgress.instance.data.achievements.Contains(pName);
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x00138A04 File Offset: 0x00136C04
	public static void saveData()
	{
		JsonSerializerSettings tSettings = new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
			Formatting = Formatting.Indented
		};
		string tEncodedData = Toolbox.encode(JsonConvert.SerializeObject(GameProgress.instance.data, tSettings));
		Toolbox.WriteSafely("Game Progress", GameProgress.instance.dataPath, ref tEncodedData);
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x00138A54 File Offset: 0x00136C54
	private void loadData()
	{
		if (!File.Exists(this.dataPath))
		{
			return;
		}
		string fileString = File.ReadAllText(this.dataPath);
		try
		{
			string tDecodedString = Toolbox.decode(fileString);
			if (!string.IsNullOrEmpty(tDecodedString))
			{
				fileString = tDecodedString;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			this.data = JsonConvert.DeserializeObject<GameProgressData>(fileString);
			this.data.setDefaultValues();
		}
		catch (Exception message)
		{
			Debug.LogError("Error loading game progress data from " + this.dataPath);
			Debug.LogError(message);
			this.initNewSave();
		}
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x00138AEC File Offset: 0x00136CEC
	public void debugClearAllAchievements()
	{
		this.data.achievements.Clear();
		GameProgress.saveData();
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x00138B04 File Offset: 0x00136D04
	public void unlockAllAchievements()
	{
		foreach (Achievement achievement in AssetManager.achievements.list)
		{
			GameProgress.unlockAchievement(achievement.id);
		}
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x00138B60 File Offset: 0x00136D60
	public void debugClearAll()
	{
		this.data.prepare();
		foreach (HashSet<string> hashSet in this.data.all_hashsets)
		{
			hashSet.Clear();
		}
		GameProgress.saveData();
	}

	// Token: 0x06002643 RID: 9795 RVA: 0x00138BC8 File Offset: 0x00136DC8
	public void debugUnlockAll()
	{
		foreach (ActorTrait actorTrait in AssetManager.traits.list)
		{
			actorTrait.unlock(false);
		}
		foreach (CultureTrait cultureTrait in AssetManager.culture_traits.list)
		{
			cultureTrait.unlock(false);
		}
		foreach (LanguageTrait languageTrait in AssetManager.language_traits.list)
		{
			languageTrait.unlock(false);
		}
		foreach (SubspeciesTrait subspeciesTrait in AssetManager.subspecies_traits.list)
		{
			subspeciesTrait.unlock(false);
		}
		foreach (ClanTrait clanTrait in AssetManager.clan_traits.list)
		{
			clanTrait.unlock(false);
		}
		foreach (ReligionTrait religionTrait in AssetManager.religion_traits.list)
		{
			religionTrait.unlock(false);
		}
		foreach (KingdomTrait kingdomTrait in AssetManager.kingdoms_traits.list)
		{
			kingdomTrait.unlock(false);
		}
		foreach (EquipmentAsset equipmentAsset in AssetManager.items.list)
		{
			equipmentAsset.unlock(false);
		}
		foreach (GeneAsset geneAsset in AssetManager.gene_library.list)
		{
			geneAsset.unlock(false);
		}
		foreach (ActorAsset actorAsset in AssetManager.actor_library.list)
		{
			actorAsset.unlock(false);
		}
		foreach (PlotAsset plotAsset in AssetManager.plots_library.list)
		{
			plotAsset.unlock(false);
		}
		GameProgress.saveData();
	}

	// Token: 0x04001CDE RID: 7390
	public static GameProgress instance;

	// Token: 0x04001CDF RID: 7391
	private string dataPath;

	// Token: 0x04001CE0 RID: 7392
	internal GameProgressData data;
}
