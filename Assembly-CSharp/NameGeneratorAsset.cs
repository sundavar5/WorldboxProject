using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000135 RID: 309
[Serializable]
public class NameGeneratorAsset : Asset
{
	// Token: 0x0600093A RID: 2362 RVA: 0x000855FF File Offset: 0x000837FF
	public bool hasOnomastics()
	{
		return this.onomastics_templates.Count > 0;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0008560F File Offset: 0x0008380F
	public void addOnomastic(string pOnomastic)
	{
		if (this.onomastics_templates == null)
		{
			this.onomastics_templates = new List<string>();
		}
		this.onomastics_templates.Add(pOnomastic);
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00085630 File Offset: 0x00083830
	public void addTemplate(string pTemplateString)
	{
		if (this.templates == null)
		{
			this.templates = new List<string[]>();
		}
		string[] tSteps = pTemplateString.Split(',', StringSplitOptions.None);
		this.templates.Add(tSteps);
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x00085666 File Offset: 0x00083866
	public void addPartGroup(string pGroupString)
	{
		if (this.part_groups == null)
		{
			this.part_groups = new List<string>();
		}
		this.part_groups.Add(pGroupString);
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00085687 File Offset: 0x00083887
	public void addPartGroup2(string pGroupString)
	{
		if (this.part_groups2 == null)
		{
			this.part_groups2 = new List<string>();
		}
		this.part_groups2.Add(pGroupString);
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x000856A8 File Offset: 0x000838A8
	public void addPartGroup3(string pGroupString)
	{
		if (this.part_groups3 == null)
		{
			this.part_groups3 = new List<string>();
		}
		this.part_groups3.Add(pGroupString);
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000856C9 File Offset: 0x000838C9
	public void addDictPart(string pID, string pListString)
	{
		if (this.dict_parts == null)
		{
			this.dict_parts = new Dictionary<string, string>();
		}
		this.dict_parts.Add(pID, pListString);
	}

	// Token: 0x04000965 RID: 2405
	public static string[] vowels_all = new string[]
	{
		"a",
		"e",
		"i",
		"o",
		"u",
		"y"
	};

	// Token: 0x04000966 RID: 2406
	public static string[] consonants_all = new string[]
	{
		"b",
		"c",
		"d",
		"f",
		"g",
		"h",
		"j",
		"k",
		"l",
		"m",
		"n",
		"p",
		"q",
		"r",
		"s",
		"t",
		"v",
		"w",
		"x",
		"z"
	};

	// Token: 0x04000967 RID: 2407
	public static string[] consonants_sounds = new string[]
	{
		"b",
		"c",
		"d",
		"f",
		"g",
		"h",
		"ph",
		"ch",
		"k",
		"l",
		"m",
		"n",
		"p",
		"r",
		"s",
		"t",
		"v",
		"w",
		"sh"
	};

	// Token: 0x04000968 RID: 2408
	public string[] special1;

	// Token: 0x04000969 RID: 2409
	public string[] special2;

	// Token: 0x0400096A RID: 2410
	public string[] vowels;

	// Token: 0x0400096B RID: 2411
	public string[] consonants;

	// Token: 0x0400096C RID: 2412
	public string[] parts;

	// Token: 0x0400096D RID: 2413
	public string[] addition_start;

	// Token: 0x0400096E RID: 2414
	public string[] addition_ending;

	// Token: 0x0400096F RID: 2415
	public List<string> onomastics_templates = new List<string>();

	// Token: 0x04000970 RID: 2416
	public List<string> part_groups;

	// Token: 0x04000971 RID: 2417
	public List<string> part_groups2;

	// Token: 0x04000972 RID: 2418
	public List<string> part_groups3;

	// Token: 0x04000973 RID: 2419
	public Dictionary<string, string> dict_parts;

	// Token: 0x04000974 RID: 2420
	public bool use_dictionary;

	// Token: 0x04000975 RID: 2421
	public List<string[]> templates;

	// Token: 0x04000976 RID: 2422
	[DefaultValue(1)]
	public int max_vowels_in_row = 1;

	// Token: 0x04000977 RID: 2423
	[DefaultValue(1)]
	public int max_consonants_in_row = 1;

	// Token: 0x04000978 RID: 2424
	[DefaultValue(0.5f)]
	public float add_addition_chance = 0.5f;

	// Token: 0x04000979 RID: 2425
	public NameGeneratorCheck check;

	// Token: 0x0400097A RID: 2426
	public NameGeneratorReplacer replacer;

	// Token: 0x0400097B RID: 2427
	public NameGeneratorReplacerKingdom replacer_kingdom;

	// Token: 0x0400097C RID: 2428
	public NameGeneratorFinalizer finalizer;
}
