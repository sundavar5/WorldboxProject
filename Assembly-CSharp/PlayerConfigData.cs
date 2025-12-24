using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x0200047D RID: 1149
[Preserve]
[Serializable]
public class PlayerConfigData
{
	// Token: 0x06002769 RID: 10089 RVA: 0x0013F28C File Offset: 0x0013D48C
	public void initData()
	{
		PlayerConfig.dict.Clear();
		foreach (OptionAsset tAsset in AssetManager.options_library.list)
		{
			if (tAsset.id[0] != '_')
			{
				PlayerOptionData tData = new PlayerOptionData(tAsset.id);
				if (tAsset.type == OptionType.Bool)
				{
					tData.boolVal = tAsset.default_bool;
				}
				else if (tAsset.type == OptionType.String)
				{
					tData.stringVal = tAsset.default_string;
				}
				else if (tAsset.type == OptionType.Int)
				{
					tData.intVal = tAsset.default_int;
				}
				if (Config.isMobile && tAsset.override_bool_mobile)
				{
					tData.boolVal = tAsset.default_bool_mobile;
				}
				this.add(tData);
			}
		}
	}

	// Token: 0x0600276A RID: 10090 RVA: 0x0013F370 File Offset: 0x0013D570
	public PlayerOptionData get(string pKey)
	{
		foreach (PlayerOptionData tData in this.list)
		{
			if (string.Equals(pKey, tData.name))
			{
				return tData;
			}
		}
		return null;
	}

	// Token: 0x0600276B RID: 10091 RVA: 0x0013F3D4 File Offset: 0x0013D5D4
	public PlayerOptionData add(PlayerOptionData pData)
	{
		foreach (PlayerOptionData tData in this.list)
		{
			if (string.Equals(pData.name, tData.name))
			{
				PlayerConfig.dict.Add(tData.name, tData);
				return tData;
			}
		}
		this.list.Add(pData);
		PlayerConfig.dict.Add(pData.name, pData);
		return pData;
	}

	// Token: 0x0600276C RID: 10092 RVA: 0x0013F468 File Offset: 0x0013D668
	public string toJson()
	{
		string result;
		using (StringBuilderPool sb = new StringBuilderPool(8192))
		{
			using (StringWriter sw = new StringWriter(sb.string_builder, CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonWriter = new JsonTextWriter(sw))
				{
					JsonHelper.writer.Serialize(jsonWriter, this, typeof(PlayerConfigData));
				}
				result = sw.ToString();
			}
		}
		return result;
	}

	// Token: 0x04001D94 RID: 7572
	[DefaultValue(5)]
	public int nextReward = 5;

	// Token: 0x04001D95 RID: 7573
	[DefaultValue("")]
	public string powerReward = "";

	// Token: 0x04001D96 RID: 7574
	[DefaultValue("")]
	public string lastReward = "";

	// Token: 0x04001D97 RID: 7575
	[DefaultValue(-1.0)]
	public double nextAdTimestamp = -1.0;

	// Token: 0x04001D98 RID: 7576
	public List<RewardedPower> rewardedPowers = new List<RewardedPower>();

	// Token: 0x04001D99 RID: 7577
	public List<PlayerOptionData> list = new List<PlayerOptionData>();

	// Token: 0x04001D9A RID: 7578
	[Preserve]
	[Obsolete("use GameProgressData.achievements instead")]
	public List<string> achievements = new List<string>();

	// Token: 0x04001D9B RID: 7579
	[Preserve]
	[Obsolete("use GameProgressData.unlocked_traits instead")]
	public List<string> unlocked_traits = new List<string>();

	// Token: 0x04001D9C RID: 7580
	public List<string> trait_editor_gamma = new List<string>();

	// Token: 0x04001D9D RID: 7581
	[DefaultValue(RainState.Add)]
	public RainState trait_editor_gamma_state;

	// Token: 0x04001D9E RID: 7582
	public List<string> trait_editor_omega = new List<string>();

	// Token: 0x04001D9F RID: 7583
	[DefaultValue(RainState.Add)]
	public RainState trait_editor_omega_state;

	// Token: 0x04001DA0 RID: 7584
	public List<string> trait_editor_delta = new List<string>();

	// Token: 0x04001DA1 RID: 7585
	[DefaultValue(RainState.Add)]
	public RainState trait_editor_delta_state;

	// Token: 0x04001DA2 RID: 7586
	public List<string> equipment_editor = new List<string>();

	// Token: 0x04001DA3 RID: 7587
	[DefaultValue(RainState.Add)]
	public RainState equipment_editor_state;

	// Token: 0x04001DA4 RID: 7588
	[DefaultValue(-1)]
	public int favorite_world = -1;

	// Token: 0x04001DA5 RID: 7589
	internal string worldnet = "";

	// Token: 0x04001DA6 RID: 7590
	public bool premium;

	// Token: 0x04001DA7 RID: 7591
	public bool valCheck2025;

	// Token: 0x04001DA8 RID: 7592
	public bool magicCheck2025;

	// Token: 0x04001DA9 RID: 7593
	public bool fireworksCheck2025;

	// Token: 0x04001DAA RID: 7594
	public int saveVersion = 1;

	// Token: 0x04001DAB RID: 7595
	public int lastRateID;

	// Token: 0x04001DAC RID: 7596
	public bool tutorialFinished;

	// Token: 0x04001DAD RID: 7597
	[DefaultValue(true)]
	public bool pPossible0507 = true;

	// Token: 0x04001DAE RID: 7598
	public bool premiumDisabled;

	// Token: 0x04001DAF RID: 7599
	public bool clearDebugOnStart;

	// Token: 0x04001DB0 RID: 7600
	public bool testAds;
}
