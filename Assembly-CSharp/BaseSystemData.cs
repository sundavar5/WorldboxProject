using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;
using SQLite;
using UnityEngine.Scripting;
using UnityPools;

// Token: 0x0200021B RID: 539
[Serializable]
public abstract class BaseSystemData : IDisposable
{
	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06001350 RID: 4944 RVA: 0x000D83A9 File Offset: 0x000D65A9
	// (set) Token: 0x06001351 RID: 4945 RVA: 0x000D83B1 File Offset: 0x000D65B1
	[PrimaryKey]
	[NotNull]
	[JsonProperty(Order = -1, DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(-1L)]
	public long id { get; set; } = -1L;

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06001352 RID: 4946 RVA: 0x000D83BA File Offset: 0x000D65BA
	// (set) Token: 0x06001353 RID: 4947 RVA: 0x000D83C2 File Offset: 0x000D65C2
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string name { get; set; }

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06001354 RID: 4948 RVA: 0x000D83CB File Offset: 0x000D65CB
	// (set) Token: 0x06001355 RID: 4949 RVA: 0x000D83D3 File Offset: 0x000D65D3
	public bool custom_name { get; set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06001356 RID: 4950 RVA: 0x000D83DC File Offset: 0x000D65DC
	// (set) Token: 0x06001357 RID: 4951 RVA: 0x000D83E4 File Offset: 0x000D65E4
	[DefaultValue(-1L)]
	public long name_culture_id { get; set; } = -1L;

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06001358 RID: 4952 RVA: 0x000D83ED File Offset: 0x000D65ED
	// (set) Token: 0x06001359 RID: 4953 RVA: 0x000D83F5 File Offset: 0x000D65F5
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(0.0)]
	public double created_time { get; set; }

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x0600135A RID: 4954 RVA: 0x000D83FE File Offset: 0x000D65FE
	// (set) Token: 0x0600135B RID: 4955 RVA: 0x000D8406 File Offset: 0x000D6606
	[DefaultValue(0.0)]
	public double died_time { get; set; }

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x0600135C RID: 4956 RVA: 0x000D840F File Offset: 0x000D660F
	// (set) Token: 0x0600135D RID: 4957 RVA: 0x000D8417 File Offset: 0x000D6617
	[DefaultValue(false)]
	public bool favorite { get; set; }

	// Token: 0x170000FC RID: 252
	// (set) Token: 0x0600135E RID: 4958 RVA: 0x000D8420 File Offset: 0x000D6620
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[UnityEngine.Scripting.Preserve]
	[DefaultValue(1)]
	[Obsolete("Use created_time instead")]
	public int age
	{
		set
		{
			if (value < 1)
			{
				return;
			}
			if (this.created_time > 0.0)
			{
				return;
			}
			this.created_time = (double)((float)(-1 * value) * 60f);
		}
	}

	// Token: 0x0600135F RID: 4959 RVA: 0x000D844C File Offset: 0x000D664C
	public void cloneCustomDataFrom(BaseSystemData pTarget)
	{
		if (pTarget.custom_data_int != null)
		{
			foreach (KeyValuePair<string, int> tItem in pTarget.custom_data_int.dict)
			{
				this.set(tItem.Key, tItem.Value);
			}
		}
		if (pTarget.custom_data_long != null)
		{
			foreach (KeyValuePair<string, long> tItem2 in pTarget.custom_data_long.dict)
			{
				this.set(tItem2.Key, tItem2.Value);
			}
		}
		if (pTarget.custom_data_float != null)
		{
			foreach (KeyValuePair<string, float> tItem3 in pTarget.custom_data_float.dict)
			{
				this.set(tItem3.Key, tItem3.Value);
			}
		}
		if (pTarget.custom_data_bool != null)
		{
			foreach (KeyValuePair<string, bool> tItem4 in pTarget.custom_data_bool.dict)
			{
				this.set(tItem4.Key, tItem4.Value);
			}
		}
		if (pTarget.custom_data_string != null)
		{
			foreach (KeyValuePair<string, string> tItem5 in pTarget.custom_data_string.dict)
			{
				this.set(tItem5.Key, tItem5.Value);
			}
		}
		if (pTarget.custom_data_flags != null)
		{
			foreach (string tItem6 in pTarget.custom_data_flags)
			{
				this.addFlag(tItem6);
			}
		}
	}

	// Token: 0x06001360 RID: 4960 RVA: 0x000D8684 File Offset: 0x000D6884
	public Dictionary<string, string> debug()
	{
		Dictionary<string, string> tDict = new Dictionary<string, string>();
		if (this.custom_data_int != null)
		{
			foreach (KeyValuePair<string, int> tItem in this.custom_data_int.dict)
			{
				tDict.Add(tItem.Key, tItem.Value.ToString());
			}
		}
		if (this.custom_data_long != null)
		{
			foreach (KeyValuePair<string, long> tItem2 in this.custom_data_long.dict)
			{
				tDict.Add(tItem2.Key, tItem2.Value.ToString());
			}
		}
		if (this.custom_data_float != null)
		{
			foreach (KeyValuePair<string, float> tItem3 in this.custom_data_float.dict)
			{
				tDict.Add(tItem3.Key, tItem3.Value.ToString(CultureInfo.InvariantCulture));
			}
		}
		if (this.custom_data_bool != null)
		{
			foreach (KeyValuePair<string, bool> tItem4 in this.custom_data_bool.dict)
			{
				tDict.Add(tItem4.Key, tItem4.Value.ToString());
			}
		}
		if (this.custom_data_string != null)
		{
			foreach (KeyValuePair<string, string> tItem5 in this.custom_data_string.dict)
			{
				tDict.Add(tItem5.Key, tItem5.Value);
			}
		}
		if (this.custom_data_flags != null)
		{
			foreach (string tItem6 in this.custom_data_flags)
			{
				tDict.Add("Flag", tItem6);
			}
		}
		return tDict;
	}

	// Token: 0x06001361 RID: 4961 RVA: 0x000D88EC File Offset: 0x000D6AEC
	public void save()
	{
		this.checkInt();
		this.checkLong();
		this.checkFloat();
		this.checkBool();
		this.checkString();
		this.checkFlags();
	}

	// Token: 0x06001362 RID: 4962 RVA: 0x000D8912 File Offset: 0x000D6B12
	public void checkInt()
	{
		CustomDataContainer<int> customDataContainer = this.custom_data_int;
		if (customDataContainer != null && customDataContainer.dict.Count == 0)
		{
			this.custom_data_int.Dispose();
			this.custom_data_int = null;
		}
	}

	// Token: 0x06001363 RID: 4963 RVA: 0x000D8942 File Offset: 0x000D6B42
	public void checkLong()
	{
		CustomDataContainer<long> customDataContainer = this.custom_data_long;
		if (customDataContainer != null && customDataContainer.dict.Count == 0)
		{
			this.custom_data_long.Dispose();
			this.custom_data_long = null;
		}
	}

	// Token: 0x06001364 RID: 4964 RVA: 0x000D8972 File Offset: 0x000D6B72
	public void checkFloat()
	{
		CustomDataContainer<float> customDataContainer = this.custom_data_float;
		if (customDataContainer != null && customDataContainer.dict.Count == 0)
		{
			this.custom_data_float.Dispose();
			this.custom_data_float = null;
		}
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x000D89A2 File Offset: 0x000D6BA2
	public void checkBool()
	{
		CustomDataContainer<bool> customDataContainer = this.custom_data_bool;
		if (customDataContainer != null && customDataContainer.dict.Count == 0)
		{
			this.custom_data_bool.Dispose();
			this.custom_data_bool = null;
		}
	}

	// Token: 0x06001366 RID: 4966 RVA: 0x000D89D2 File Offset: 0x000D6BD2
	public void checkString()
	{
		CustomDataContainer<string> customDataContainer = this.custom_data_string;
		if (customDataContainer != null && customDataContainer.dict.Count == 0)
		{
			this.custom_data_string.Dispose();
			this.custom_data_string = null;
		}
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x000D8A02 File Offset: 0x000D6C02
	public void checkFlags()
	{
		if (this.custom_data_flags == null)
		{
			return;
		}
		if (this.custom_data_flags.Count == 0)
		{
			UnsafeCollectionPool<HashSet<string>, string>.Release(this.custom_data_flags);
			this.custom_data_flags = null;
		}
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x000D8A2C File Offset: 0x000D6C2C
	public void load()
	{
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x000D8A2E File Offset: 0x000D6C2E
	public void get(string pKey, out int pResult, int pDefault = 0)
	{
		if (this.custom_data_int == null || !this.custom_data_int.TryGetValue(pKey, out pResult))
		{
			pResult = pDefault;
		}
	}

	// Token: 0x0600136A RID: 4970 RVA: 0x000D8A4A File Offset: 0x000D6C4A
	public void get(string pKey, out long pResult, long pDefault = 0L)
	{
		if (this.custom_data_long == null || !this.custom_data_long.TryGetValue(pKey, out pResult))
		{
			pResult = pDefault;
		}
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x000D8A66 File Offset: 0x000D6C66
	public void get(string pKey, out float pResult, float pDefault = 0f)
	{
		if (this.custom_data_float == null || !this.custom_data_float.TryGetValue(pKey, out pResult))
		{
			pResult = pDefault;
		}
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x000D8A82 File Offset: 0x000D6C82
	public void get(string pKey, out string pResult, string pDefault = null)
	{
		if (this.custom_data_string == null || !this.custom_data_string.TryGetValue(pKey, out pResult))
		{
			pResult = pDefault;
		}
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x000D8A9E File Offset: 0x000D6C9E
	public void get(string pKey, out bool pResult, bool pDefault = false)
	{
		if (this.custom_data_bool == null || !this.custom_data_bool.TryGetValue(pKey, out pResult))
		{
			pResult = pDefault;
		}
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x000D8ABA File Offset: 0x000D6CBA
	public int set(string pKey, int pData)
	{
		if (this.custom_data_int == null)
		{
			this.custom_data_int = new CustomDataContainer<int>();
		}
		this.custom_data_int[pKey] = pData;
		return pData;
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x000D8ADD File Offset: 0x000D6CDD
	public long set(string pKey, long pData)
	{
		if (this.custom_data_long == null)
		{
			this.custom_data_long = new CustomDataContainer<long>();
		}
		this.custom_data_long[pKey] = pData;
		return pData;
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x000D8B00 File Offset: 0x000D6D00
	public float set(string pKey, float pData)
	{
		if (this.custom_data_float == null)
		{
			this.custom_data_float = new CustomDataContainer<float>();
		}
		this.custom_data_float[pKey] = pData;
		return pData;
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x000D8B23 File Offset: 0x000D6D23
	public string set(string pKey, string pData)
	{
		if (this.custom_data_string == null)
		{
			this.custom_data_string = new CustomDataContainer<string>();
		}
		this.custom_data_string[pKey] = pData;
		return pData;
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x000D8B46 File Offset: 0x000D6D46
	public bool set(string pKey, bool pData)
	{
		if (this.custom_data_bool == null)
		{
			this.custom_data_bool = new CustomDataContainer<bool>();
		}
		this.custom_data_bool[pKey] = pData;
		return pData;
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x000D8B6C File Offset: 0x000D6D6C
	public void change(string pKey, int pValue, int pMin = 0, int pMax = 1000)
	{
		int tPoints;
		this.get(pKey, out tPoints, 0);
		tPoints += pValue;
		if (tPoints < pMin)
		{
			tPoints = pMin;
		}
		if (tPoints > pMax)
		{
			tPoints = pMax;
		}
		this.set(pKey, tPoints);
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x000D8B9E File Offset: 0x000D6D9E
	public void removeInt(string pKey)
	{
		if (this.custom_data_int == null)
		{
			return;
		}
		this.custom_data_int.Remove(pKey);
		this.checkInt();
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x000D8BBB File Offset: 0x000D6DBB
	public void removeLong(string pKey)
	{
		if (this.custom_data_long == null)
		{
			return;
		}
		this.custom_data_long.Remove(pKey);
		this.checkLong();
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x000D8BD8 File Offset: 0x000D6DD8
	public void removeFloat(string pKey)
	{
		if (this.custom_data_float == null)
		{
			return;
		}
		this.custom_data_float.Remove(pKey);
		this.checkFloat();
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x000D8BF5 File Offset: 0x000D6DF5
	public void removeString(string pKey)
	{
		if (this.custom_data_string == null)
		{
			return;
		}
		this.custom_data_string.Remove(pKey);
		this.checkString();
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x000D8C12 File Offset: 0x000D6E12
	public void removeBool(string pKey)
	{
		if (this.custom_data_bool == null)
		{
			return;
		}
		this.custom_data_bool.Remove(pKey);
		this.checkBool();
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x000D8C2F File Offset: 0x000D6E2F
	public bool addFlag(string pID)
	{
		if (this.custom_data_flags == null)
		{
			this.custom_data_flags = UnsafeCollectionPool<HashSet<string>, string>.Get();
		}
		return this.custom_data_flags.Add(pID);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x000D8C50 File Offset: 0x000D6E50
	public bool hasFlag(string pID)
	{
		HashSet<string> hashSet = this.custom_data_flags;
		return hashSet != null && hashSet.Contains(pID);
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x000D8C64 File Offset: 0x000D6E64
	public void removeFlag(string pID)
	{
		if (this.custom_data_flags == null)
		{
			return;
		}
		this.custom_data_flags.Remove(pID);
		this.checkFlags();
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x000D8C84 File Offset: 0x000D6E84
	public virtual void Dispose()
	{
		CustomDataContainer<int> customDataContainer = this.custom_data_int;
		if (customDataContainer != null)
		{
			customDataContainer.Dispose();
		}
		CustomDataContainer<long> customDataContainer2 = this.custom_data_long;
		if (customDataContainer2 != null)
		{
			customDataContainer2.Dispose();
		}
		CustomDataContainer<float> customDataContainer3 = this.custom_data_float;
		if (customDataContainer3 != null)
		{
			customDataContainer3.Dispose();
		}
		CustomDataContainer<bool> customDataContainer4 = this.custom_data_bool;
		if (customDataContainer4 != null)
		{
			customDataContainer4.Dispose();
		}
		CustomDataContainer<string> customDataContainer5 = this.custom_data_string;
		if (customDataContainer5 != null)
		{
			customDataContainer5.Dispose();
		}
		this.custom_data_int = null;
		this.custom_data_long = null;
		this.custom_data_float = null;
		this.custom_data_bool = null;
		this.custom_data_string = null;
		HashSet<string> hashSet = this.custom_data_flags;
		if (hashSet != null)
		{
			hashSet.Clear();
		}
		this.checkFlags();
		List<NameEntry> list = this.past_names;
		if (list != null)
		{
			list.Clear();
		}
		this.past_names = null;
	}

	// Token: 0x170000FD RID: 253
	[JsonIgnore]
	[Ignore]
	public float this[string pKey]
	{
		get
		{
			float tResult;
			this.get(pKey, out tResult, 0f);
			return tResult;
		}
		set
		{
			this.set(pKey, value);
		}
	}

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x0600137F RID: 4991 RVA: 0x000D8D60 File Offset: 0x000D6F60
	[JsonIgnore]
	public string obsidian_name_id
	{
		get
		{
			return this.name + "(" + this.id.ToString() + ")";
		}
	}

	// Token: 0x0400117E RID: 4478
	[DefaultValue(null)]
	public List<NameEntry> past_names;

	// Token: 0x04001183 RID: 4483
	[DefaultValue(null)]
	public CustomDataContainer<int> custom_data_int;

	// Token: 0x04001184 RID: 4484
	[DefaultValue(null)]
	public CustomDataContainer<long> custom_data_long;

	// Token: 0x04001185 RID: 4485
	[DefaultValue(null)]
	public CustomDataContainer<float> custom_data_float;

	// Token: 0x04001186 RID: 4486
	[DefaultValue(null)]
	public CustomDataContainer<bool> custom_data_bool;

	// Token: 0x04001187 RID: 4487
	[DefaultValue(null)]
	public CustomDataContainer<string> custom_data_string;

	// Token: 0x04001188 RID: 4488
	[DefaultValue(null)]
	public HashSet<string> custom_data_flags;

	// Token: 0x04001189 RID: 4489
	[JsonIgnore]
	public bool from_db;
}
