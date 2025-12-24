using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

// Token: 0x02000148 RID: 328
public class OnomasticsLibrary : AssetLibrary<OnomasticsAsset>
{
	// Token: 0x060009DB RID: 2523 RVA: 0x00091687 File Offset: 0x0008F887
	public override void init()
	{
		base.init();
		this.addGroups();
		this.addDice();
		this.addUnique();
		this.addSpecialCharacters();
		this.addVowelConsonantSpecials();
		this.addSexes();
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x000916B4 File Offset: 0x0008F8B4
	private void addUnique()
	{
		this.add(new OnomasticsAsset
		{
			id = "clone_last",
			short_id = 'l',
			color_text = "#EB2931",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
			{
				if (pStringBuilder.Length == 0)
				{
					return string.Empty;
				}
				return this.getLastWord(pStringBuilder);
			},
			affects_left_word = true
		});
		OnomasticsAsset onomasticsAsset = new OnomasticsAsset();
		onomasticsAsset.id = "coin_flip";
		onomasticsAsset.short_id = 'f';
		onomasticsAsset.color_text = "#F5C308";
		onomasticsAsset.type = OnomasticsAssetType.Special;
		onomasticsAsset.check_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool _, string _, int _, ActorSex _) => Randy.randomBool());
		onomasticsAsset.affects_left = true;
		this.add(onomasticsAsset);
		this.add(new OnomasticsAsset
		{
			id = "mirror",
			short_id = 'm',
			color_text = "#FFFFFF",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
			{
				if (pStringBuilder.Length == 0)
				{
					return string.Empty;
				}
				return this.getLastWord(pStringBuilder).Reverse();
			},
			affects_left_word = true
		});
		OnomasticsAsset onomasticsAsset2 = new OnomasticsAsset();
		onomasticsAsset2.id = "wild_6";
		onomasticsAsset2.short_id = 'w';
		onomasticsAsset2.color_text = "#A1B1A2";
		onomasticsAsset2.type = OnomasticsAssetType.Special;
		onomasticsAsset2.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
		{
			string result;
			using (ListPool<string> tGroups = new ListPool<string>(pData.groups.Count))
			{
				foreach (KeyValuePair<string, OnomasticsDataGroup> tPair in pData.groups)
				{
					if (!tPair.Value.isEmpty() && AssetManager.onomastics_library.get(tPair.Key).group_id < 6)
					{
						tGroups.Add(tPair.Key);
					}
				}
				if (tGroups.Count == 0)
				{
					result = string.Empty;
				}
				else
				{
					string tRandomID = OnomasticsLibrary.GetRandom<string>(tGroups);
					OnomasticsAsset tGroupAsset = AssetManager.onomastics_library.get(tRandomID);
					result = tGroupAsset.namemaker_delegate(tGroupAsset, pData, pStringBuilder, pLastPart, pIndex, pSex);
				}
			}
			return result;
		};
		this.add(onomasticsAsset2);
		OnomasticsAsset onomasticsAsset3 = new OnomasticsAsset();
		onomasticsAsset3.id = "domino";
		onomasticsAsset3.short_id = 'o';
		onomasticsAsset3.color_text = "#7FA9BC";
		onomasticsAsset3.type = OnomasticsAssetType.Special;
		onomasticsAsset3.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
		{
			string tFinalString = "";
			for (int i = 0; i < 6; i++)
			{
				string tID = string.Format("group_{0}", i + 1);
				OnomasticsAsset tGroupAsset = AssetManager.onomastics_library.get(tID);
				tFinalString += tGroupAsset.namemaker_delegate(tGroupAsset, pData, pStringBuilder, pLastPart, pIndex, pSex);
			}
			return tFinalString;
		};
		this.add(onomasticsAsset3);
		OnomasticsAsset onomasticsAsset4 = new OnomasticsAsset();
		onomasticsAsset4.id = "repeater";
		onomasticsAsset4.short_id = 'r';
		onomasticsAsset4.color_text = "#F3AB11";
		onomasticsAsset4.type = OnomasticsAssetType.Special;
		onomasticsAsset4.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
		{
			if (pIndex == 0)
			{
				return string.Empty;
			}
			int tNewIndex = pIndex - 1;
			List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
			OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
			if (tAsset.is_divider)
			{
				return string.Empty;
			}
			while (tAsset.namemaker_delegate == null)
			{
				tNewIndex--;
				if (tNewIndex < 0)
				{
					return string.Empty;
				}
				tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
				if (tAsset.is_divider)
				{
					return string.Empty;
				}
			}
			if (tAsset.namemaker_delegate == null)
			{
				return string.Empty;
			}
			string tFinalString = "";
			for (int i = 0; i < 2; i++)
			{
				if (!OnomasticsData.hasCheckOnTheRight(pData, tNewIndex) || OnomasticsData.passesCheckOnTheRight(pData, pStringBuilder, pLastPart, tNewIndex, pSex))
				{
					tFinalString += tAsset.namemaker_delegate(tAsset, pData, pStringBuilder, pLastPart, tNewIndex, pSex);
				}
			}
			return tFinalString;
		};
		onomasticsAsset4.affects_left = true;
		this.add(onomasticsAsset4);
		this.add(new OnomasticsAsset
		{
			id = "upper",
			short_id = 'u',
			color_text = "#B2FF75",
			is_upper = true,
			type = OnomasticsAssetType.Special,
			affects_everything = true
		});
		OnomasticsAsset onomasticsAsset5 = new OnomasticsAsset();
		onomasticsAsset5.id = "backspace";
		onomasticsAsset5.short_id = 'b';
		onomasticsAsset5.color_text = "#FF6A43";
		onomasticsAsset5.type = OnomasticsAssetType.Special;
		onomasticsAsset5.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
		{
			if (pStringBuilder.Length == 0)
			{
				return string.Empty;
			}
			pStringBuilder.Remove(pStringBuilder.Length - 1, 1);
			return string.Empty;
		};
		this.add(onomasticsAsset5);
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x00091950 File Offset: 0x0008FB50
	private void addVowelConsonantSpecials()
	{
		OnomasticsAsset onomasticsAsset = new OnomasticsAsset();
		onomasticsAsset.id = "consonant_separator";
		onomasticsAsset.short_id = 'c';
		onomasticsAsset.color_text = "#657CCE";
		onomasticsAsset.type = OnomasticsAssetType.Special;
		onomasticsAsset.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string _, int pIndex, ActorSex _)
		{
			if (pIndex == 0)
			{
				return string.Empty;
			}
			int tNewIndex = pIndex - 1;
			List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
			OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
			if (!tAsset.isGroupType())
			{
				return string.Empty;
			}
			if (pData.isGroupEmpty(tAsset.id))
			{
				return string.Empty;
			}
			ConsonantSeparator.addRandomVowels(pStringBuilder, pData.getGroup(tAsset.id).characters);
			return string.Empty;
		};
		onomasticsAsset.check_delegate = new OnomasticsCheckDelegate(this.blockGroupOnLeft);
		onomasticsAsset.affects_left = true;
		onomasticsAsset.affects_left_word = true;
		onomasticsAsset.affects_left_group_only = true;
		this.add(onomasticsAsset);
		OnomasticsAsset onomasticsAsset2 = new OnomasticsAsset();
		onomasticsAsset2.id = "vowel_separator";
		onomasticsAsset2.short_id = 'v';
		onomasticsAsset2.color_text = "#FF68C5";
		onomasticsAsset2.type = OnomasticsAssetType.Special;
		onomasticsAsset2.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string _, int pIndex, ActorSex _)
		{
			if (pIndex == 0)
			{
				return string.Empty;
			}
			int tNewIndex = pIndex - 1;
			List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
			OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
			if (!tAsset.isGroupType())
			{
				return string.Empty;
			}
			if (pData.isGroupEmpty(tAsset.id))
			{
				return string.Empty;
			}
			VowelSeparator.addRandomConsonants(pStringBuilder, pData.getGroup(tAsset.id).characters);
			return string.Empty;
		};
		onomasticsAsset2.check_delegate = new OnomasticsCheckDelegate(this.blockGroupOnLeft);
		onomasticsAsset2.affects_left = true;
		onomasticsAsset2.affects_left_word = true;
		onomasticsAsset2.affects_left_group_only = true;
		this.add(onomasticsAsset2);
		this.add(new OnomasticsAsset
		{
			id = "vowel_duplicator",
			short_id = 'd',
			color_text = "#D9B03A",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
			{
				if (pStringBuilder.Length == 0)
				{
					return string.Empty;
				}
				ValueTuple<int, int> lastWordBounds = this.getLastWordBounds(pStringBuilder, false);
				int tStart = lastWordBounds.Item1;
				int tLength = lastWordBounds.Item2;
				string empty;
				using (ListPool<int> tVowelIndexes = VowelSeparator.findAllSingleVowels(pStringBuilder, tStart, tLength))
				{
					if (tVowelIndexes.Count == 0)
					{
						empty = string.Empty;
					}
					else
					{
						int tRandomIndex = OnomasticsLibrary.GetRandom<int>(tVowelIndexes);
						pStringBuilder.Insert(tRandomIndex, pStringBuilder[tRandomIndex]);
						empty = string.Empty;
					}
				}
				return empty;
			},
			affects_left_word = true
		});
		this.add(new OnomasticsAsset
		{
			id = "consonant_duplicator",
			short_id = 'D',
			color_text = "#2E76AD",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
			{
				if (pStringBuilder.Length == 0)
				{
					return string.Empty;
				}
				ValueTuple<int, int> lastWordBounds = this.getLastWordBounds(pStringBuilder, false);
				int tStart = lastWordBounds.Item1;
				int tLength = lastWordBounds.Item2;
				string empty;
				using (ListPool<int> tConsonantIndexes = ConsonantSeparator.findAllSingleConsonants(pStringBuilder, tStart, tLength))
				{
					if (tConsonantIndexes.Count == 0)
					{
						empty = string.Empty;
					}
					else
					{
						int tRandomIndex = OnomasticsLibrary.GetRandom<int>(tConsonantIndexes);
						pStringBuilder.Insert(tRandomIndex, pStringBuilder[tRandomIndex]);
						empty = string.Empty;
					}
				}
				return empty;
			},
			affects_left_word = true
		});
		this.add(new OnomasticsAsset
		{
			id = "vowel_replacer",
			short_id = 'e',
			color_text = "#FF68C5",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string _, int pIndex, ActorSex _)
			{
				if (pIndex == 0)
				{
					return string.Empty;
				}
				int tNewIndex = pIndex - 1;
				List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
				OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
				if (!tAsset.isGroupType())
				{
					return string.Empty;
				}
				if (pData.isGroupEmpty(tAsset.id))
				{
					return string.Empty;
				}
				ValueTuple<int, int> lastWordBounds = this.getLastWordBounds(pStringBuilder, false);
				int tStart = lastWordBounds.Item1;
				int tLength = lastWordBounds.Item2;
				string empty;
				using (ListPool<int> tVowels = VowelSeparator.findAllVowels(pStringBuilder, tStart, tLength))
				{
					if (tVowels.Count == 0)
					{
						empty = string.Empty;
					}
					else
					{
						string tRandomString = OnomasticsLibrary.GetRandom<string>(pData.getGroup(tAsset.id).characters);
						int tRandomIndex = OnomasticsLibrary.GetRandom<int>(tVowels);
						pStringBuilder.Remove(tRandomIndex, 1);
						pStringBuilder.Insert(tRandomIndex, tRandomString);
						empty = string.Empty;
					}
				}
				return empty;
			},
			check_delegate = new OnomasticsCheckDelegate(this.blockGroupOnLeft),
			affects_left = true,
			affects_left_word = true,
			affects_left_group_only = true
		});
		this.add(new OnomasticsAsset
		{
			id = "consonant_replacer",
			short_id = 'E',
			color_text = "#A8A8A8",
			type = OnomasticsAssetType.Special,
			namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData pData, StringBuilderPool pStringBuilder, string _, int pIndex, ActorSex _)
			{
				if (pIndex == 0)
				{
					return string.Empty;
				}
				int tNewIndex = pIndex - 1;
				List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
				OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
				if (!tAsset.isGroupType())
				{
					return string.Empty;
				}
				if (pData.isGroupEmpty(tAsset.id))
				{
					return string.Empty;
				}
				ValueTuple<int, int> lastWordBounds = this.getLastWordBounds(pStringBuilder, false);
				int tStart = lastWordBounds.Item1;
				int tLength = lastWordBounds.Item2;
				string empty;
				using (ListPool<int> tConsonants = ConsonantSeparator.findAllConsonants(pStringBuilder, tStart, tLength))
				{
					if (tConsonants.Count == 0)
					{
						empty = string.Empty;
					}
					else
					{
						string tRandomString = OnomasticsLibrary.GetRandom<string>(pData.getGroup(tAsset.id).characters);
						int tRandomIndex = OnomasticsLibrary.GetRandom<int>(tConsonants);
						pStringBuilder.Remove(tRandomIndex, 1);
						pStringBuilder.Insert(tRandomIndex, tRandomString);
						empty = string.Empty;
					}
				}
				return empty;
			},
			check_delegate = new OnomasticsCheckDelegate(this.blockGroupOnLeft),
			affects_left = true,
			affects_left_word = true,
			affects_left_group_only = true
		});
		OnomasticsAsset onomasticsAsset3 = new OnomasticsAsset();
		onomasticsAsset3.id = "vowel_requirer";
		onomasticsAsset3.short_id = 'V';
		onomasticsAsset3.color_text = "#FBFDC2";
		onomasticsAsset3.type = OnomasticsAssetType.Special;
		onomasticsAsset3.check_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _) => pStringBuilder.Length != 0 && VowelSeparator.isVowel(pStringBuilder[pStringBuilder.Length - 1]));
		onomasticsAsset3.affects_left = true;
		this.add(onomasticsAsset3);
		OnomasticsAsset onomasticsAsset4 = new OnomasticsAsset();
		onomasticsAsset4.id = "consonant_requirer";
		onomasticsAsset4.short_id = 'C';
		onomasticsAsset4.color_text = "#CAB2A8";
		onomasticsAsset4.type = OnomasticsAssetType.Special;
		onomasticsAsset4.check_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _) => pStringBuilder.Length != 0 && ConsonantSeparator.isConsonant(pStringBuilder[pStringBuilder.Length - 1]));
		onomasticsAsset4.affects_left = true;
		this.add(onomasticsAsset4);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x00091C79 File Offset: 0x0008FE79
	private void addDice()
	{
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x00091C7C File Offset: 0x0008FE7C
	private void addGroups()
	{
		this.add(new OnomasticsAsset
		{
			id = "group_1",
			short_id = '0',
			color_text = "#31EB29",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 0
		});
		this.add(new OnomasticsAsset
		{
			id = "group_2",
			short_id = '1',
			color_text = "#28ECA0",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 1
		});
		this.add(new OnomasticsAsset
		{
			id = "group_3",
			short_id = '2',
			color_text = "#3FABE9",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 2
		});
		this.add(new OnomasticsAsset
		{
			id = "group_4",
			short_id = '3',
			color_text = "#6A78FF",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 3
		});
		this.add(new OnomasticsAsset
		{
			id = "group_5",
			short_id = '4',
			color_text = "#A129EB",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 4
		});
		this.add(new OnomasticsAsset
		{
			id = "group_6",
			short_id = '5',
			color_text = "#EB29B3",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 5
		});
		this.add(new OnomasticsAsset
		{
			id = "group_7",
			short_id = '6',
			color_text = "#EB2931",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 6
		});
		this.add(new OnomasticsAsset
		{
			id = "group_8",
			short_id = '7',
			color_text = "#FF8D1A",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 7
		});
		this.add(new OnomasticsAsset
		{
			id = "group_9",
			short_id = '8',
			color_text = "#EFCB00",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 8
		});
		this.add(new OnomasticsAsset
		{
			id = "group_10",
			short_id = '9',
			color_text = "#D2DAC0",
			forced_locale_subname_id = "onomastics_group_subname",
			forced_locale_description_id = "onomastics_group_info",
			forced_locale_description_id_2 = "onomastics_group_info_2",
			type = OnomasticsAssetType.Group,
			namemaker_delegate = new OnomasticsNameMakerDelegate(this.groupAction),
			group_id = 9
		});
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x000920B8 File Offset: 0x000902B8
	private void addSpecialCharacters()
	{
		OnomasticsAsset onomasticsAsset = new OnomasticsAsset();
		onomasticsAsset.id = "space";
		onomasticsAsset.short_id = '_';
		onomasticsAsset.color_text = "#D2DAC0";
		onomasticsAsset.type = OnomasticsAssetType.Special;
		onomasticsAsset.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
		{
			if (pStringBuilder.Length == 0)
			{
				return string.Empty;
			}
			if (pStringBuilder[pStringBuilder.Length - 1] == ' ')
			{
				return string.Empty;
			}
			return " ";
		};
		onomasticsAsset.is_word_divider = true;
		this.add(onomasticsAsset);
		OnomasticsAsset onomasticsAsset2 = new OnomasticsAsset();
		onomasticsAsset2.id = "silent_space";
		onomasticsAsset2.short_id = ',';
		onomasticsAsset2.color_text = "#C93F3F";
		onomasticsAsset2.type = OnomasticsAssetType.Special;
		onomasticsAsset2.namemaker_delegate = delegate(OnomasticsAsset _, OnomasticsData _, StringBuilderPool pStringBuilder, string _, int _, ActorSex _)
		{
			if (pStringBuilder.Length == 0)
			{
				return string.Empty;
			}
			if (pStringBuilder[pStringBuilder.Length - 1] == ' ')
			{
				return string.Empty;
			}
			if (pStringBuilder[pStringBuilder.Length - 1] == ',')
			{
				return string.Empty;
			}
			return ",";
		};
		onomasticsAsset2.is_word_divider = true;
		this.add(onomasticsAsset2);
		OnomasticsAsset onomasticsAsset3 = new OnomasticsAsset();
		onomasticsAsset3.id = "hyphen";
		onomasticsAsset3.short_id = '-';
		onomasticsAsset3.color_text = "#BBA826";
		onomasticsAsset3.type = OnomasticsAssetType.Special;
		onomasticsAsset3.namemaker_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool _, string _, int _, ActorSex _) => "-");
		onomasticsAsset3.is_word_divider = true;
		this.add(onomasticsAsset3);
		OnomasticsAsset onomasticsAsset4 = new OnomasticsAsset();
		onomasticsAsset4.id = "numbers";
		onomasticsAsset4.short_id = '#';
		onomasticsAsset4.color_text = "#35B929";
		onomasticsAsset4.type = OnomasticsAssetType.Special;
		onomasticsAsset4.namemaker_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool _, string _, int _, ActorSex _) => Randy.randomInt(0, 10).ToString());
		this.add(onomasticsAsset4);
		this.add(new OnomasticsAsset
		{
			id = "divider",
			short_id = '/',
			color_text = "#D2DAC0",
			type = OnomasticsAssetType.Special,
			is_divider = true,
			is_word_divider = true,
			is_immune = true
		});
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x00092278 File Offset: 0x00090478
	private void addSexes()
	{
		OnomasticsAsset onomasticsAsset = new OnomasticsAsset();
		onomasticsAsset.id = "sex_male";
		onomasticsAsset.short_id = 'M';
		onomasticsAsset.color_text = "#86BC4E";
		onomasticsAsset.type = OnomasticsAssetType.Special;
		onomasticsAsset.check_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool _, string _, int _, ActorSex pSex) => pSex == ActorSex.Male);
		onomasticsAsset.affects_left = true;
		this.add(onomasticsAsset);
		OnomasticsAsset onomasticsAsset2 = new OnomasticsAsset();
		onomasticsAsset2.id = "sex_female";
		onomasticsAsset2.short_id = 'F';
		onomasticsAsset2.color_text = "#EB29B3";
		onomasticsAsset2.type = OnomasticsAssetType.Special;
		onomasticsAsset2.check_delegate = ((OnomasticsAsset _, OnomasticsData _, StringBuilderPool _, string _, int _, ActorSex pSex) => pSex == ActorSex.Female);
		onomasticsAsset2.affects_left = true;
		this.add(onomasticsAsset2);
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x00092340 File Offset: 0x00090540
	private string getLastWord(StringBuilderPool pStringBuilder)
	{
		ValueTuple<int, int> lastWordBounds = this.getLastWordBounds(pStringBuilder, true);
		int tStart = lastWordBounds.Item1;
		int tLength = lastWordBounds.Item2;
		return pStringBuilder.ToString(tStart, tLength);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0009236C File Offset: 0x0009056C
	[return: TupleElementNames(new string[]
	{
		"tStart",
		"tLength"
	})]
	private ValueTuple<int, int> getLastWordBounds(StringBuilderPool pStringBuilder, bool pPrevious = false)
	{
		if (pStringBuilder.Length == 0)
		{
			return new ValueTuple<int, int>(0, 0);
		}
		int tStart = pStringBuilder.LastIndexOfAny(OnomasticsLibrary._word_separators) + 1;
		int tLength = pStringBuilder.Length - tStart;
		if (pPrevious && tLength == 0)
		{
			tStart = pStringBuilder.LastIndexOfAny(OnomasticsLibrary._word_separators, tStart - 2) + 1;
			tLength = pStringBuilder.Length - tStart - (tLength + 1);
		}
		return new ValueTuple<int, int>(tStart, tLength);
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x000923CB File Offset: 0x000905CB
	private string groupAction(OnomasticsAsset pAsset, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
	{
		return pData.getRandomPartFromGroup(pAsset.id);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x000923D9 File Offset: 0x000905D9
	private bool blockGroupOnLeft(OnomasticsAsset pAsset, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
	{
		return !this.isGroupOnLeft(pAsset, pData, pStringBuilder, pLastPart, pIndex, pSex);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x000923F0 File Offset: 0x000905F0
	private bool isGroupOnLeft(OnomasticsAsset pAsset, OnomasticsData pData, StringBuilderPool pStringBuilder, string pLastPart, int pIndex, ActorSex pSex)
	{
		if (pIndex == 0)
		{
			return false;
		}
		int tNewIndex = pIndex - 1;
		List<string> tCurrentSubtemplate = pData.getCurrentSubgroup();
		OnomasticsAsset tAsset = AssetManager.onomastics_library.get(tCurrentSubtemplate[tNewIndex]);
		return tAsset.isGroupType() && !pData.isGroupEmpty(tAsset.id);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0009243C File Offset: 0x0009063C
	public override OnomasticsAsset add(OnomasticsAsset pAsset)
	{
		base.add(pAsset);
		this._dict_short_id.Add(pAsset.short_id.ToString(), pAsset);
		return pAsset;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0009245E File Offset: 0x0009065E
	public OnomasticsAsset getByShortId(string pShortID)
	{
		if (this._dict_short_id.ContainsKey(pShortID))
		{
			return this._dict_short_id[pShortID];
		}
		return null;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0009247C File Offset: 0x0009067C
	public static T GetRandom<T>(T[] list)
	{
		OnomasticsLibrary._rand.InitState(Randy.rand.state);
		return list[OnomasticsLibrary._rand.NextInt(list.Length)];
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x000924A5 File Offset: 0x000906A5
	public static T GetRandom<T>(ListPool<T> list)
	{
		OnomasticsLibrary._rand.InitState(Randy.rand.state);
		return list[OnomasticsLibrary._rand.NextInt(list.Count)];
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x000924D4 File Offset: 0x000906D4
	public override void post_init()
	{
		base.post_init();
		foreach (OnomasticsAsset tAsset in this.list)
		{
			if (string.IsNullOrEmpty(tAsset.path_icon))
			{
				tAsset.path_icon = "ui/Icons/onomastics/onomastics_" + tAsset.id;
			}
		}
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0009254C File Offset: 0x0009074C
	public override void editorDiagnostic()
	{
		foreach (OnomasticsAsset tAsset in this.list)
		{
			if (SpriteTextureLoader.getSprite(tAsset.path_icon) == null)
			{
				BaseAssetLibrary.logAssetError("Missing icon file", tAsset.path_icon);
			}
		}
		foreach (OnomasticsAsset tAsset2 in this.list)
		{
			foreach (OnomasticsAsset tAsset3 in this.list)
			{
				if (tAsset2 != tAsset3 && tAsset2.short_id == tAsset3.short_id)
				{
					Debug.LogError(string.Concat(new string[]
					{
						"OnomasticsAsset: Duplicate short_id: ",
						tAsset2.short_id.ToString(),
						" for ",
						tAsset2.id,
						" and ",
						tAsset3.id
					}));
				}
			}
		}
		foreach (KeyValuePair<string, string> pair in this._test_templates)
		{
			string tTemplate = pair.Key;
			string tExpected = pair.Value;
			string tResult = NameGenerator.generateNameFromOnomastics(null, tTemplate, null, null, ActorSex.None);
			if (tResult != tExpected)
			{
				BaseAssetLibrary.logAssetError("<e>Onomastics</e>: Template test failed: " + tResult + " != " + tExpected, tTemplate);
			}
		}
		base.editorDiagnostic();
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x00092728 File Offset: 0x00090928
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (OnomasticsAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
			this.checkLocale(tAsset, tAsset.getDescriptionID());
		}
	}

	// Token: 0x040009CA RID: 2506
	private Dictionary<string, OnomasticsAsset> _dict_short_id = new Dictionary<string, OnomasticsAsset>();

	// Token: 0x040009CB RID: 2507
	[NonSerialized]
	public List<OnomasticsAsset> list_special = new List<OnomasticsAsset>();

	// Token: 0x040009CC RID: 2508
	private static readonly char[] _word_separators = new char[]
	{
		' ',
		',',
		'-'
	};

	// Token: 0x040009CD RID: 2509
	private static Unity.Mathematics.Random _rand = default(Unity.Mathematics.Random);

	// Token: 0x040009CE RID: 2510
	private Dictionary<string, string> _test_templates = new Dictionary<string, string>
	{
		{
			"01|0:a;1:b",
			"Ab"
		},
		{
			"0_1|0:a;1:b",
			"A B"
		},
		{
			"01b|0:a;1:b",
			"A"
		},
		{
			"o|0:a;1:b;2:c;5:f",
			"Abcf"
		},
		{
			"110c|0:a;1:b",
			"Bab"
		},
		{
			"001c|0:a;1:b",
			"Aa"
		},
		{
			"001v|0:a;1:b",
			"Aba"
		},
		{
			"110v|0:a;1:b",
			"Bb"
		},
		{
			"110C|0:a;1:b",
			"Bba"
		},
		{
			"001C|0:a;1:b",
			"Aa"
		},
		{
			"001V|0:a;1:b",
			"Aab"
		},
		{
			"110V|0:a;1:b",
			"Bb"
		},
		{
			"10E|0:a;1:b",
			"A"
		},
		{
			"01E|0:a;1:b",
			"A"
		},
		{
			"01e|0:a;1:b",
			"B"
		},
		{
			"10e|0:a;1:b",
			"B"
		},
		{
			"1,10E|0:a;1:b",
			"Ba"
		},
		{
			"1-10E|0:a;1:b",
			"B-A"
		},
		{
			"0,01e|0:a;1:b",
			"Ab"
		},
		{
			"0-01e|0:a;1:b",
			"A-B"
		},
		{
			"01D|0:a;1:b",
			"Abb"
		},
		{
			"00D|0:a;1:b",
			"Aa"
		},
		{
			"01d|0:a;1:b",
			"Aab"
		},
		{
			"11d|0:a;1:b",
			"Bb"
		},
		{
			"0r|0:a",
			"Aaa"
		},
		{
			"0rr|0:a",
			"Aaaaaaa"
		},
		{
			"0rbu|0:a",
			"AA"
		}
	};
}
