using System;
using System.Collections.Generic;

// Token: 0x02000417 RID: 1047
[Serializable]
public class WorldLaws
{
	// Token: 0x06002408 RID: 9224 RVA: 0x0012C8C4 File Offset: 0x0012AAC4
	public PlayerOptionData add(PlayerOptionData pData)
	{
		foreach (PlayerOptionData tData in this.list)
		{
			if (string.Equals(pData.name, tData.name))
			{
				this.dict.TryAdd(tData.name, tData);
				tData.on_switch = pData.on_switch;
				return tData;
			}
		}
		this.list.Add(pData);
		this.dict.Add(pData.name, pData);
		return pData;
	}

	// Token: 0x06002409 RID: 9225 RVA: 0x0012C968 File Offset: 0x0012AB68
	public void check()
	{
		this.init(true);
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x0012C974 File Offset: 0x0012AB74
	public void updateCaches()
	{
		foreach (WorldLawAsset worldLawAsset in AssetManager.world_laws_library.list)
		{
			worldLawAsset.updateCachedEnabled(this);
		}
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x0012C9CC File Offset: 0x0012ABCC
	public void init(bool pUpdateCaches = true)
	{
		if (this.list == null)
		{
			this.list = new List<PlayerOptionData>();
		}
		if (this.dict == null)
		{
			this.dict = new Dictionary<string, PlayerOptionData>();
		}
		foreach (WorldLawAsset tAsset in AssetManager.world_laws_library.list)
		{
			this.add(new PlayerOptionData(tAsset.id)
			{
				boolVal = tAsset.default_state,
				on_switch = tAsset.on_state_change
			});
		}
		foreach (WorldAgeAsset tAsset2 in AssetManager.era_library.list)
		{
			this.add(new PlayerOptionData(tAsset2.id)
			{
				boolVal = true
			});
		}
		if (pUpdateCaches)
		{
			this.updateCaches();
		}
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x0012CAD4 File Offset: 0x0012ACD4
	public bool isAgeEnabled(string pID)
	{
		return this.dict[pID].boolVal;
	}

	// Token: 0x0600240D RID: 9229 RVA: 0x0012CAE7 File Offset: 0x0012ACE7
	public void setAgeEnabled(string pID, bool pValue)
	{
		this.dict[pID].boolVal = pValue;
	}

	// Token: 0x0600240E RID: 9230 RVA: 0x0012CAFC File Offset: 0x0012ACFC
	public bool isEnabled(string pId)
	{
		PlayerOptionData tData;
		return this.dict.TryGetValue(pId, out tData) && tData.boolVal;
	}

	// Token: 0x0600240F RID: 9231 RVA: 0x0012CB24 File Offset: 0x0012AD24
	public void enable(string pID)
	{
		PlayerOptionData tData;
		if (!this.dict.TryGetValue(pID, out tData))
		{
			return;
		}
		tData.boolVal = true;
		this.updateCaches();
	}

	// Token: 0x040019F8 RID: 6648
	public List<PlayerOptionData> list;

	// Token: 0x040019F9 RID: 6649
	[NonSerialized]
	public Dictionary<string, PlayerOptionData> dict;
}
