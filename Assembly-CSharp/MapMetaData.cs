using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

// Token: 0x0200058D RID: 1421
[Serializable]
public class MapMetaData
{
	// Token: 0x06002F16 RID: 12054 RVA: 0x0016C4BC File Offset: 0x0016A6BC
	public void prepareForSave()
	{
		this.modded = Config.MODDED;
		if (this.modded)
		{
			this.modsActive = new List<string>(ModLoader.getModsLoaded());
		}
		this.timestamp = Epoch.Current();
	}

	// Token: 0x06002F17 RID: 12055 RVA: 0x0016C4EC File Offset: 0x0016A6EC
	public string toJson()
	{
		return JsonConvert.SerializeObject(this, new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
		});
	}

	// Token: 0x0400230F RID: 8975
	[NonSerialized]
	public string temp_date_string = "";

	// Token: 0x04002310 RID: 8976
	public int saveVersion;

	// Token: 0x04002311 RID: 8977
	public int width;

	// Token: 0x04002312 RID: 8978
	public int height;

	// Token: 0x04002313 RID: 8979
	public MapStats mapStats;

	// Token: 0x04002314 RID: 8980
	public int cities;

	// Token: 0x04002315 RID: 8981
	public int units;

	// Token: 0x04002316 RID: 8982
	public int population;

	// Token: 0x04002317 RID: 8983
	public int structures;

	// Token: 0x04002318 RID: 8984
	public int mobs;

	// Token: 0x04002319 RID: 8985
	public int vegetation;

	// Token: 0x0400231A RID: 8986
	public long deaths;

	// Token: 0x0400231B RID: 8987
	public int kingdoms;

	// Token: 0x0400231C RID: 8988
	public int buildings;

	// Token: 0x0400231D RID: 8989
	public int equipment;

	// Token: 0x0400231E RID: 8990
	public int books;

	// Token: 0x0400231F RID: 8991
	public int wars;

	// Token: 0x04002320 RID: 8992
	public int alliances;

	// Token: 0x04002321 RID: 8993
	public int families;

	// Token: 0x04002322 RID: 8994
	public int clans;

	// Token: 0x04002323 RID: 8995
	public int cultures;

	// Token: 0x04002324 RID: 8996
	public int religions;

	// Token: 0x04002325 RID: 8997
	public int languages;

	// Token: 0x04002326 RID: 8998
	public int subspecies;

	// Token: 0x04002327 RID: 8999
	public int favorites;

	// Token: 0x04002328 RID: 9000
	public int favorite_items;

	// Token: 0x04002329 RID: 9001
	public bool cursed;

	// Token: 0x0400232A RID: 9002
	[DefaultValue(false)]
	public bool modded;

	// Token: 0x0400232B RID: 9003
	[DefaultValue(null)]
	public List<string> modsActive = new List<string>();

	// Token: 0x0400232C RID: 9004
	public double timestamp;
}
