using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

// Token: 0x02000260 RID: 608
public class OnomasticsData : IDisposable
{
	// Token: 0x060016D4 RID: 5844 RVA: 0x000E473F File Offset: 0x000E293F
	public void setTemplateData(IReadOnlyList<string> pTemplateData)
	{
		this._template_data.Clear();
		this._template_data.AddRange(pTemplateData);
	}

	// Token: 0x060016D5 RID: 5845 RVA: 0x000E4758 File Offset: 0x000E2958
	public List<string> getFullTemplateData()
	{
		return this._template_data;
	}

	// Token: 0x060016D6 RID: 5846 RVA: 0x000E4760 File Offset: 0x000E2960
	public List<string> getCurrentSubgroup()
	{
		return this._current_subgroup;
	}

	// Token: 0x060016D7 RID: 5847 RVA: 0x000E4768 File Offset: 0x000E2968
	public void addToTemplateData(string pID)
	{
		this._template_data.Add(pID);
	}

	// Token: 0x060016D8 RID: 5848 RVA: 0x000E4776 File Offset: 0x000E2976
	public void shuffleAllCards()
	{
		this._template_data.Shuffle<string>();
	}

	// Token: 0x060016D9 RID: 5849 RVA: 0x000E4783 File Offset: 0x000E2983
	public void clearTemplateData()
	{
		this._template_data.Clear();
	}

	// Token: 0x060016DA RID: 5850 RVA: 0x000E4790 File Offset: 0x000E2990
	public string getGroupString(string pGroupID)
	{
		OnomasticsDataGroup tGroup;
		if (this.tryGetGroup(pGroupID, out tGroup))
		{
			return tGroup.characters_string;
		}
		return null;
	}

	// Token: 0x060016DB RID: 5851 RVA: 0x000E47B0 File Offset: 0x000E29B0
	public bool isGroupEmpty(string pGroupID)
	{
		OnomasticsDataGroup tGroup = this.getGroup(pGroupID);
		return this.isGroupEmpty(tGroup);
	}

	// Token: 0x060016DC RID: 5852 RVA: 0x000E47CC File Offset: 0x000E29CC
	public bool isGroupEmpty(OnomasticsDataGroup pGroup)
	{
		return pGroup == null || pGroup.isEmpty();
	}

	// Token: 0x060016DD RID: 5853 RVA: 0x000E47DC File Offset: 0x000E29DC
	public OnomasticsDataGroup getGroup(string pGroupID)
	{
		OnomasticsDataGroup tGroup;
		this.groups.TryGetValue(pGroupID, out tGroup);
		return tGroup;
	}

	// Token: 0x060016DE RID: 5854 RVA: 0x000E47F9 File Offset: 0x000E29F9
	public bool tryGetGroup(string pGroupID, out OnomasticsDataGroup pGroup)
	{
		return this.groups.TryGetValue(pGroupID, out pGroup) && !pGroup.isEmpty();
	}

	// Token: 0x060016DF RID: 5855 RVA: 0x000E4818 File Offset: 0x000E2A18
	public string getRandomPartFromGroup(string pGroupID)
	{
		OnomasticsDataGroup tGroup;
		if (this.tryGetGroup(pGroupID, out tGroup))
		{
			return tGroup.characters.GetRandom<string>();
		}
		return string.Empty;
	}

	// Token: 0x060016E0 RID: 5856 RVA: 0x000E4844 File Offset: 0x000E2A44
	private bool saveGroup(string pGroupID, string pCharacters)
	{
		bool tNewChange = true;
		OnomasticsDataGroup tGroup;
		if (!this.groups.TryGetValue(pGroupID, out tGroup))
		{
			tGroup = new OnomasticsDataGroup();
			this.groups.Add(pGroupID, tGroup);
			tNewChange = false;
		}
		string tOldValue = tGroup.characters_string;
		pCharacters = pCharacters.Replace("\n", " ");
		pCharacters = pCharacters.Replace("\r", " ");
		while (pCharacters.Contains("  "))
		{
			pCharacters = pCharacters.Replace("  ", " ");
		}
		pCharacters = pCharacters.Trim(new char[]
		{
			'\n',
			'\r',
			' '
		});
		if (pCharacters.Length == 0)
		{
			tGroup.characters_string = string.Empty;
			tGroup.characters = null;
		}
		else
		{
			string[] tSplitGroup = pCharacters.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			Array.Sort<string>(tSplitGroup, StringComparer.OrdinalIgnoreCase);
			tGroup.characters_string = string.Join(' ', tSplitGroup);
			tGroup.characters = tSplitGroup;
		}
		if (string.Equals(tOldValue, tGroup.characters_string, StringComparison.OrdinalIgnoreCase))
		{
			tNewChange = false;
		}
		return tNewChange;
	}

	// Token: 0x060016E1 RID: 5857 RVA: 0x000E4938 File Offset: 0x000E2B38
	public void cloneFrom(OnomasticsData pOriginalData)
	{
		this._template_data.AddRange(pOriginalData._template_data);
		foreach (KeyValuePair<string, OnomasticsDataGroup> tPair in pOriginalData.groups)
		{
			this.setGroup(tPair.Key, tPair.Value.characters_string);
		}
	}

	// Token: 0x060016E2 RID: 5858 RVA: 0x000E49B0 File Offset: 0x000E2BB0
	public void setDebugTest()
	{
		this.loadFromShortTemplate("0312|0:pl p s l d b;1:mp rp rt b;2:kin tin le ee;3:a e i o u y oo");
	}

	// Token: 0x060016E3 RID: 5859 RVA: 0x000E49C0 File Offset: 0x000E2BC0
	internal static bool hasCheckOnTheRight(OnomasticsData pData, int pIndex)
	{
		List<string> tCurrentSubgroup = pData.getCurrentSubgroup();
		int tNewIndex = pIndex + 1;
		if (tNewIndex >= tCurrentSubgroup.Count)
		{
			return false;
		}
		string tAssetIDRight = tCurrentSubgroup[tNewIndex];
		return AssetManager.onomastics_library.get(tAssetIDRight).check_delegate != null;
	}

	// Token: 0x060016E4 RID: 5860 RVA: 0x000E4A00 File Offset: 0x000E2C00
	internal static bool passesCheckOnTheRight(OnomasticsData pData, StringBuilderPool pLocalNameBuilder, string pLastPart, int pIndex, ActorSex pSex)
	{
		List<string> tCurrentSubgroup = pData.getCurrentSubgroup();
		int tNewIndex = pIndex + 1;
		if (tNewIndex >= tCurrentSubgroup.Count)
		{
			return true;
		}
		string tAssetIDRight = tCurrentSubgroup[tNewIndex];
		OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tAssetIDRight);
		bool tResult = tAsset.check_delegate(tAsset, pData, pLocalNameBuilder, pLastPart, tNewIndex, pSex);
		if (tResult && OnomasticsData.hasCheckOnTheRight(pData, tNewIndex))
		{
			return OnomasticsData.passesCheckOnTheRight(pData, pLocalNameBuilder, pLastPart, tNewIndex, pSex);
		}
		return tResult;
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000E4A68 File Offset: 0x000E2C68
	public string generateName(ActorSex pSex = ActorSex.None, int pCalls = 0, long? pSeed = null)
	{
		if (pCalls > 100)
		{
			return "Rebr";
		}
		if (pSex == ActorSex.None)
		{
			if (Randy.randomBool())
			{
				pSex = ActorSex.Female;
			}
			else
			{
				pSex = ActorSex.Male;
			}
		}
		string tLastPart = string.Empty;
		if (pSeed != null)
		{
			Randy.resetSeed(pSeed.Value);
		}
		string result;
		using (StringBuilderPool tLocalNameBuilder = new StringBuilderPool())
		{
			using (ListPool<string> tSubGroup = this.getSubgroup(this._template_data))
			{
				this._current_subgroup.Clear();
				this._current_subgroup.AddRange(tSubGroup);
				int tLength = this._current_subgroup.Count;
				bool tIsUpperExists = false;
				for (int i = 0; i < tLength; i++)
				{
					string tGroupID = this._current_subgroup[i];
					OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tGroupID);
					if (!OnomasticsData.hasCheckOnTheRight(this, i) || OnomasticsData.passesCheckOnTheRight(this, tLocalNameBuilder, tLastPart, i, pSex))
					{
						OnomasticsAssetType type = tAsset.type;
						if (type <= OnomasticsAssetType.Special)
						{
							OnomasticsNameMakerDelegate namemaker_delegate = tAsset.namemaker_delegate;
							string tPart = (namemaker_delegate != null) ? namemaker_delegate(tAsset, this, tLocalNameBuilder, tLastPart, i, pSex) : null;
							if (tPart != null && tPart.Length > 0)
							{
								tLastPart = tPart;
								tLocalNameBuilder.Append(tPart);
							}
							if (tAsset.is_upper)
							{
								tIsUpperExists = true;
							}
						}
					}
				}
				tLocalNameBuilder.Remove(new char[]
				{
					','
				});
				tLocalNameBuilder.TrimEnd(new char[]
				{
					' ',
					'-'
				});
				if (tLocalNameBuilder.Length > 30)
				{
					tLocalNameBuilder.Cut(0, 30);
				}
				if (tLocalNameBuilder.Length == 0)
				{
					result = this.generateName(pSex, ++pCalls, null);
				}
				else if (Blacklist.checkBlackList(tLocalNameBuilder))
				{
					result = this.generateName(pSex, ++pCalls, null);
				}
				else
				{
					if (tIsUpperExists)
					{
						tLocalNameBuilder.ToUpperInvariant();
					}
					else
					{
						tLocalNameBuilder.ToTitleCase();
					}
					result = tLocalNameBuilder.ToString();
				}
			}
		}
		return result;
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000E4C70 File Offset: 0x000E2E70
	private ListPool<string> getSubgroup(List<string> pTemplateData)
	{
		int tSubgroups = OnomasticsData.countDividers(pTemplateData) + 1;
		ListPool<string> tResult;
		if (tSubgroups > 1)
		{
			int tSubgroupIndex = Randy.randomInt(0, tSubgroups);
			tResult = this.getSubgroupByIndex(tSubgroupIndex);
		}
		else
		{
			tResult = new ListPool<string>(pTemplateData);
		}
		return tResult;
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000E4CA4 File Offset: 0x000E2EA4
	private ListPool<string> getSubgroupByIndex(int pIndex)
	{
		ListPool<string> tResult = new ListPool<string>();
		int tSubgroupIndex = 0;
		foreach (string tID in this._template_data)
		{
			if (tID == "divider")
			{
				tSubgroupIndex++;
				if (tSubgroupIndex > pIndex)
				{
					break;
				}
			}
			else if (tSubgroupIndex == pIndex)
			{
				tResult.Add(tID);
			}
		}
		return tResult;
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000E4D1C File Offset: 0x000E2F1C
	private static int countDividers(List<string> pTemplateData)
	{
		int tResult = 0;
		using (List<string>.Enumerator enumerator = pTemplateData.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current == "divider")
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x000E4D78 File Offset: 0x000E2F78
	public static string convertToCultureTitleCase(string pString)
	{
		string tResult = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(pString.ToLower());
		if (tResult.Contains('\''))
		{
			tResult = OnomasticsData.capitalizeAfterApostrophe(tResult);
		}
		return tResult;
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x000E4DB0 File Offset: 0x000E2FB0
	private static string capitalizeAfterApostrophe(string pInput)
	{
		TextInfo tTextInfo = CultureInfo.InvariantCulture.TextInfo;
		char[] tResult = pInput.ToCharArray();
		bool tCapitalizeNext = false;
		for (int i = 0; i < tResult.Length; i++)
		{
			if (tCapitalizeNext && char.IsLetter(tResult[i]))
			{
				tResult[i] = tTextInfo.ToUpper(tResult[i]);
				tCapitalizeNext = false;
			}
			if (tResult[i] == '\'')
			{
				tCapitalizeNext = true;
			}
		}
		return new string(tResult);
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x000E4E0C File Offset: 0x000E300C
	public string getShortTemplate()
	{
		string result;
		using (StringBuilderPool tBuilder = new StringBuilderPool())
		{
			foreach (string tID in this._template_data)
			{
				OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tID);
				tBuilder.Append(tAsset.short_id);
			}
			tBuilder.Append('|');
			bool tFirstGroup = true;
			foreach (KeyValuePair<string, OnomasticsDataGroup> tPair in from p in this.groups
			orderby AssetManager.onomastics_library.get(p.Key).short_id
			select p)
			{
				if (!tPair.Value.isEmpty())
				{
					if (!tFirstGroup)
					{
						tBuilder.Append(';');
					}
					OnomasticsAsset tAsset2 = AssetManager.onomastics_library.get(tPair.Key);
					tBuilder.Append(tAsset2.short_id);
					tBuilder.Append(':');
					tBuilder.Append(tPair.Value.characters_string.ToLower());
					tFirstGroup = false;
				}
			}
			result = tBuilder.ToString();
		}
		return result;
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x000E4F88 File Offset: 0x000E3188
	public bool templateIsValid(string pShortTemplate)
	{
		return !string.IsNullOrEmpty(pShortTemplate) && pShortTemplate.Contains('|') && pShortTemplate.Split('|', StringSplitOptions.None).Length == 2;
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x000E4FB4 File Offset: 0x000E31B4
	public void loadFromShortTemplate(string pShortTemplate)
	{
		if (string.IsNullOrEmpty(pShortTemplate))
		{
			pShortTemplate = "|";
		}
		if (!pShortTemplate.Contains('|'))
		{
			throw new ArgumentException("Invalid template format: (NS) " + pShortTemplate);
		}
		pShortTemplate = pShortTemplate.Trim(new char[]
		{
			'\n',
			'\r',
			' ',
			'"',
			'`'
		});
		if (pShortTemplate.Contains('\n'))
		{
			throw new ArgumentException("Invalid template format: (NL) " + pShortTemplate);
		}
		if (pShortTemplate.Contains('\r'))
		{
			throw new ArgumentException("Invalid template format: (NL) " + pShortTemplate);
		}
		string[] tParts = pShortTemplate.Split('|', StringSplitOptions.None);
		if (tParts.Length != 2)
		{
			throw new ArgumentException(string.Format("Invalid template format: (L{0}) ", tParts.Length) + pShortTemplate);
		}
		char[] tTemplateIDS = tParts[0].ToCharArray();
		using (ListPool<string> tList = new ListPool<string>(tTemplateIDS.Length))
		{
			foreach (char tShortID in tTemplateIDS)
			{
				OnomasticsAsset tAsset = AssetManager.onomastics_library.getByShortId(tShortID.ToString());
				if (tAsset == null)
				{
					throw new ArgumentException(string.Format("Invalid template format: (0{0}) ", tShortID) + pShortTemplate);
				}
				tList.Add(tAsset.id);
			}
			this.setTemplateData(tList);
			string[] array2 = tParts[1].Split(';', StringSplitOptions.None);
			this.groups.Clear();
			string[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				string[] tGroupDetails = array3[i].Split(':', StringSplitOptions.None);
				if (tGroupDetails.Length == 2)
				{
					string tGroupShortId = tGroupDetails[0];
					string tCharacters = tGroupDetails[1];
					OnomasticsAsset tAsset2 = AssetManager.onomastics_library.getByShortId(tGroupShortId);
					if (tAsset2 == null)
					{
						throw new ArgumentException("Invalid template format: (G" + tGroupShortId + ") " + pShortTemplate);
					}
					this.saveGroup(tAsset2.id, tCharacters);
				}
			}
		}
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x000E5184 File Offset: 0x000E3384
	public bool isGendered()
	{
		foreach (string tID in this._template_data)
		{
			OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tID);
			if (tAsset.id == "sex_male" || tAsset.id == "sex_female")
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x000E5208 File Offset: 0x000E3408
	public bool isEmpty()
	{
		return this._template_data.Count == 0;
	}

	// Token: 0x060016F0 RID: 5872 RVA: 0x000E5218 File Offset: 0x000E3418
	public bool setGroup(string pID, string pString)
	{
		return this.saveGroup(pID, pString);
	}

	// Token: 0x060016F1 RID: 5873 RVA: 0x000E5222 File Offset: 0x000E3422
	public void Dispose()
	{
		this.groups.Clear();
		this.groups = null;
		this._template_data.Clear();
		this._template_data = null;
		this._current_subgroup.Clear();
		this._current_subgroup = null;
	}

	// Token: 0x040012CE RID: 4814
	public const char SEPARATOR_GROUPS = '|';

	// Token: 0x040012CF RID: 4815
	public const char SEPARATOR_PARTS = ';';

	// Token: 0x040012D0 RID: 4816
	public const char SEPARATOR_PART_CONTAINER = ':';

	// Token: 0x040012D1 RID: 4817
	public const int MAX_NAME_LENGTH = 30;

	// Token: 0x040012D2 RID: 4818
	public Dictionary<string, OnomasticsDataGroup> groups = new Dictionary<string, OnomasticsDataGroup>();

	// Token: 0x040012D3 RID: 4819
	private List<string> _template_data = new List<string>();

	// Token: 0x040012D4 RID: 4820
	private List<string> _current_subgroup = new List<string>();

	// Token: 0x040012D5 RID: 4821
	public const string DEFAULT_NAME_FAILED = "Rebr";
}
