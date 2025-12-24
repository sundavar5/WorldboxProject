using System;

// Token: 0x02000145 RID: 325
public class OnomasticsEvolutionLibrary : AssetLibrary<OnomasticsEvolutionAsset>
{
	// Token: 0x060009CB RID: 2507 RVA: 0x00090260 File Offset: 0x0008E460
	public override void init()
	{
		base.init();
		this.add(new OnomasticsEvolutionAsset
		{
			from = "k",
			to = "c",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "g",
			to = "k",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "f",
			to = "v",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "g",
			to = "gh",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "f",
			to = "gh",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "v",
			to = "b",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ci",
			to = "z",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_not_in_start)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "s",
			to = "z",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "t",
			to = "d",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ks",
			to = "x",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_not_in_start)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "th",
			to = "f",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "th",
			to = "d",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "thi",
			to = "ti",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "er",
			to = "ar",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "sh",
			to = "sch",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "sh",
			to = "sz",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ch",
			to = "cz",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "mm",
			to = "m",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "mn",
			to = "m",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "gg",
			to = "g",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_not_in_start)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "nn",
			to = "n",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "mm",
			to = "hm",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ph",
			to = "f",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ph",
			to = "pp",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "p",
			to = "pp",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ck",
			to = "k",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_not_in_start)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ck",
			to = "gg",
			not_surrounded_by = OnomasticsEvolutionLibrary._consonants,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_not_in_start)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "e",
			to = "ai",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "e",
			to = "a",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "o",
			to = "a",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "u",
			to = "y",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ie",
			to = "y",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace_in_end)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "i",
			to = "y",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "y",
			to = "oe",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "e",
			to = "ae",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "a",
			to = "au",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "o",
			to = "au",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oo",
			to = "ou",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oo",
			to = "ue",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oo",
			to = "oa",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oo",
			to = "u",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ee",
			to = "i",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ie",
			to = "e",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ei",
			to = "ee",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ea",
			to = "ee",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ea",
			to = "ei",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ear",
			to = "ere",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "re",
			to = "ru",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "sp",
			to = "shp",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "sht",
			to = "st",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "u",
			to = "oe",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "o",
			to = "oe",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oh",
			to = "oe",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ah",
			to = "ae",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oe",
			to = "u",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oe",
			to = "oh",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ae",
			to = "ah",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ah",
			to = "oh",
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "a",
			to = "ah",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "o",
			to = "oh",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "u",
			to = "uh",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels_h,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "ah",
			to = "a",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "oh",
			to = "o",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
		this.add(new OnomasticsEvolutionAsset
		{
			from = "uh",
			to = "u",
			not_surrounded_by = OnomasticsEvolutionLibrary._vowels,
			reverse = false,
			replacer = new OnomasticsReplacerDelegate(OnomasticsEvolutionLibrary.replace)
		});
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x00091284 File Offset: 0x0008F484
	public static bool replace(OnomasticsEvolutionAsset pAsset, ref string pReplace)
	{
		if (!pReplace.Contains(pAsset.from))
		{
			return false;
		}
		int tIndex = pReplace.AllIndexesOf(pAsset.from).GetRandom<int>();
		char tPrevChar = (tIndex > 0) ? pReplace[tIndex - 1] : ' ';
		char tNextChar = (tIndex + pAsset.from.Length < pReplace.Length) ? pReplace[tIndex + pAsset.from.Length] : ' ';
		char tToFirst = pAsset.to.First();
		char tToLast = pAsset.to.Last();
		if (tPrevChar == tToLast)
		{
			return false;
		}
		if (tPrevChar == tToFirst)
		{
			return false;
		}
		if (tNextChar == tToFirst)
		{
			return false;
		}
		if (tNextChar == tToLast)
		{
			return false;
		}
		char[] not_surrounded_by = pAsset.not_surrounded_by;
		if (not_surrounded_by != null && not_surrounded_by.Contains(tPrevChar))
		{
			return false;
		}
		char[] not_surrounded_by2 = pAsset.not_surrounded_by;
		if (not_surrounded_by2 != null && not_surrounded_by2.Contains(tNextChar))
		{
			return false;
		}
		pReplace = pReplace.Remove(tIndex, pAsset.from.Length).Insert(tIndex, pAsset.to);
		return true;
	}

	// Token: 0x060009CD RID: 2509 RVA: 0x0009137C File Offset: 0x0008F57C
	public static bool replace_in_end(OnomasticsEvolutionAsset pAsset, ref string pReplace)
	{
		if (!pReplace.Contains(pAsset.from))
		{
			return false;
		}
		int tIndex = pReplace.LastIndexOf(pAsset.from);
		if (tIndex + pAsset.from.Length != pReplace.Length)
		{
			return false;
		}
		char tPrevChar = (tIndex > 0) ? pReplace[tIndex - 1] : ' ';
		char tNextChar = (tIndex + pAsset.from.Length < pReplace.Length) ? pReplace[tIndex + pAsset.from.Length] : ' ';
		char tToFirst = pAsset.to.First();
		char tToLast = pAsset.to.Last();
		if (tPrevChar == tToLast)
		{
			return false;
		}
		if (tPrevChar == tToFirst)
		{
			return false;
		}
		if (tNextChar == tToFirst)
		{
			return false;
		}
		if (tNextChar == tToLast)
		{
			return false;
		}
		char[] not_surrounded_by = pAsset.not_surrounded_by;
		if (not_surrounded_by != null && not_surrounded_by.Contains(tPrevChar))
		{
			return false;
		}
		char[] not_surrounded_by2 = pAsset.not_surrounded_by;
		if (not_surrounded_by2 != null && not_surrounded_by2.Contains(tNextChar))
		{
			return false;
		}
		pReplace = pReplace.Remove(tIndex, pAsset.from.Length).Insert(tIndex, pAsset.to);
		return true;
	}

	// Token: 0x060009CE RID: 2510 RVA: 0x00091485 File Offset: 0x0008F685
	public static bool replace_not_in_start(OnomasticsEvolutionAsset pAsset, ref string pReplace)
	{
		return pReplace.Contains(pAsset.from) && pReplace.IndexOf(pAsset.from) != 0 && OnomasticsEvolutionLibrary.replace(pAsset, ref pReplace);
	}

	// Token: 0x060009CF RID: 2511 RVA: 0x000914B0 File Offset: 0x0008F6B0
	public override OnomasticsEvolutionAsset add(OnomasticsEvolutionAsset pAsset)
	{
		pAsset.id = pAsset.from + "_" + pAsset.to;
		this.t = base.add(pAsset);
		if (this.t.reverse)
		{
			return this.add(new OnomasticsEvolutionAsset
			{
				id = this.t.to + "_" + this.t.from,
				from = this.t.to,
				to = this.t.from,
				not_surrounded_by = this.t.not_surrounded_by,
				replacer = this.t.replacer,
				reverse = false
			});
		}
		return this.t;
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x00091578 File Offset: 0x0008F778
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (OnomasticsEvolutionAsset tAsset in this.list)
		{
			OnomasticsEvolver.add(tAsset);
			if (tAsset.from.Length >= tAsset.to.Length)
			{
				OnomasticsEvolver.add(tAsset);
				OnomasticsEvolver.add(tAsset);
			}
			if (tAsset.from.Length > tAsset.to.Length)
			{
				OnomasticsEvolver.add(tAsset);
				OnomasticsEvolver.add(tAsset);
			}
		}
		OnomasticsEvolver.shuffle();
	}

	// Token: 0x040009C7 RID: 2503
	private static readonly char[] _vowels = AssetLibrary<OnomasticsEvolutionAsset>.a<char>(new char[]
	{
		'a',
		'e',
		'i',
		'o',
		'u',
		'y'
	});

	// Token: 0x040009C8 RID: 2504
	private static readonly char[] _vowels_h = AssetLibrary<OnomasticsEvolutionAsset>.a<char>(new char[]
	{
		'a',
		'e',
		'i',
		'o',
		'u',
		'y',
		'h'
	});

	// Token: 0x040009C9 RID: 2505
	private static readonly char[] _consonants = AssetLibrary<OnomasticsEvolutionAsset>.a<char>(new char[]
	{
		'b',
		'c',
		'd',
		'f',
		'g',
		'h',
		'j',
		'k',
		'l',
		'm',
		'n',
		'p',
		'q',
		'r',
		's',
		't',
		'v',
		'w',
		'x',
		'z'
	});
}
