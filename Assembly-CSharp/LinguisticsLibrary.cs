using System;

// Token: 0x0200028B RID: 651
public class LinguisticsLibrary : AssetLibrary<LinguisticsAsset>
{
	// Token: 0x06001906 RID: 6406 RVA: 0x000ECFE0 File Offset: 0x000EB1E0
	public override void init()
	{
		this.addPronounce();
		this.addWordGroups();
		this.addUnique();
		this.addMain();
	}

	// Token: 0x06001907 RID: 6407 RVA: 0x000ECFFC File Offset: 0x000EB1FC
	private void addPronounce()
	{
		this.add(new LinguisticsAsset
		{
			id = "pron_subj",
			word_group = true,
			add_space = true,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"pron_subj_i",
				"pron_subj_we",
				"pron_subj_you",
				"pron_subj_it",
				"pron_subj_they"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "pron_obj",
			word_group = true,
			add_space = true,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"pron_obj_me",
				"pron_obj_us",
				"pron_obj_you",
				"pron_obj_it",
				"pron_obj_them"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "pron_poss_adj",
			word_group = true,
			add_space = true,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"pron_poss_my",
				"pron_poss_our",
				"pron_poss_your",
				"pron_poss_its",
				"pron_poss_their"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "pron_posspr",
			word_group = true,
			add_space = true,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"pron_poss_mine",
				"pron_poss_ours",
				"pron_poss_yours",
				"pron_poss_theirs"
			})
		});
	}

	// Token: 0x06001908 RID: 6408 RVA: 0x000ED184 File Offset: 0x000EB384
	private void addWordGroups()
	{
		this.add(new LinguisticsAsset
		{
			id = "word_concept",
			word_group = true,
			add_space = true,
			word_type = WordType.Concept,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"concept_love",
				"concept_death",
				"concept_nature"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "word_action",
			word_group = true,
			add_space = true,
			word_type = WordType.Action,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"action_run",
				"action_walk",
				"action_fight"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "word_object",
			word_group = true,
			add_space = true,
			word_type = WordType.Object,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"object_sword",
				"object_shield",
				"object_bow"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "word_place",
			word_group = true,
			add_space = true,
			word_type = WordType.Place,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"place_forest",
				"place_mountain",
				"place_hill"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "word_creature",
			word_group = true,
			add_space = true,
			word_type = WordType.Creature,
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"creature_king",
				"creature_queen",
				"creature_prince"
			})
		});
	}

	// Token: 0x06001909 RID: 6409 RVA: 0x000ED344 File Offset: 0x000EB544
	private void addMain()
	{
		this.add(new LinguisticsAsset
		{
			id = "vowel",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "diphthongs",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"ai",
				"ei",
				"oi",
				"au",
				"ou",
				"ia",
				"io",
				"ua",
				"ue"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "consonant",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"p",
				"b",
				"t",
				"d",
				"k",
				"g",
				"f",
				"v",
				"s",
				"z",
				"h",
				"m",
				"n",
				"l",
				"r",
				"w",
				"y",
				"j"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "onset1",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"p",
				"b",
				"t",
				"d",
				"k",
				"g",
				"f",
				"v",
				"s",
				"z",
				"sh",
				"zh",
				"m",
				"n",
				"l",
				"r",
				"w",
				"y",
				"ch",
				"j"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "onset2",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"pr",
				"br",
				"tr",
				"dr",
				"kr",
				"gr",
				"fr",
				"vr",
				"shr",
				"thr",
				"pl",
				"bl",
				"kl",
				"gl",
				"fl",
				"vl",
				"tw",
				"dw",
				"kw",
				"gw",
				"sw",
				"sk",
				"st",
				"sp"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "coda1",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"n",
				"m",
				"l",
				"r",
				"s",
				"sh",
				"z"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "coda2",
			array = AssetLibrary<LinguisticsAsset>.a<string>(new string[]
			{
				"nd",
				"nt",
				"nk",
				"mp",
				"lt",
				"ld",
				"lp",
				"lf",
				"rk",
				"rt",
				"rs",
				"rz",
				"st",
				"sk"
			})
		});
		this.add(new LinguisticsAsset
		{
			id = "syllable_starts"
		});
		this.t.addPattern(50, new string[]
		{
			"C",
			"V"
		});
		this.t.addPattern(25, new string[]
		{
			"C",
			"C",
			"V"
		});
		this.t.addPattern(20, new string[]
		{
			"C",
			"V",
			"C"
		});
		this.t.addPattern(5, new string[]
		{
			"V"
		});
		this.add(new LinguisticsAsset
		{
			id = "syllable_mids"
		});
		this.t.addPattern(40, new string[]
		{
			"C",
			"V"
		});
		this.t.addPattern(30, new string[]
		{
			"C",
			"V",
			"C"
		});
		this.t.addPattern(10, new string[]
		{
			"C",
			"C",
			"V"
		});
		this.t.addPattern(20, new string[]
		{
			"V"
		});
		this.add(new LinguisticsAsset
		{
			id = "syllable_ends"
		});
		this.t.addPattern(40, new string[]
		{
			"C",
			"V",
			"C"
		});
		this.t.addPattern(30, new string[]
		{
			"V",
			"C"
		});
		this.t.addPattern(20, new string[]
		{
			"C",
			"V"
		});
		this.t.addPattern(10, new string[]
		{
			"V"
		});
	}

	// Token: 0x0600190A RID: 6410 RVA: 0x000ED9AC File Offset: 0x000EBBAC
	private void addUnique()
	{
		this.add(new LinguisticsAsset
		{
			id = "comma",
			simple_text = ","
		});
		this.add(new LinguisticsAsset
		{
			id = "period",
			simple_text = ".",
			next_uppercase = true
		});
		this.add(new LinguisticsAsset
		{
			id = "semicolon",
			simple_text = ";",
			next_uppercase = true
		});
		this.add(new LinguisticsAsset
		{
			id = "colon",
			simple_text = ":"
		});
		this.add(new LinguisticsAsset
		{
			id = "dash",
			add_space = true,
			simple_text = "—"
		});
		this.add(new LinguisticsAsset
		{
			id = "hyphen",
			simple_text = "-"
		});
		this.add(new LinguisticsAsset
		{
			id = "ellipsis",
			simple_text = "...",
			next_uppercase = true
		});
		this.add(new LinguisticsAsset
		{
			id = "question_mark",
			simple_text = "?",
			next_uppercase = true
		});
		this.add(new LinguisticsAsset
		{
			id = "exclamation_mark",
			simple_text = "!",
			next_uppercase = true
		});
		this.add(new LinguisticsAsset
		{
			id = "space",
			simple_text = " "
		});
		this.add(new LinguisticsAsset
		{
			id = "quotation_marks",
			symbols_around = true,
			add_space = true,
			symbols_around_left = "“",
			symbols_around_right = "”"
		});
		this.add(new LinguisticsAsset
		{
			id = "parentheses",
			symbols_around = true,
			add_space = true,
			symbols_around_left = "(",
			symbols_around_right = ")"
		});
		this.add(new LinguisticsAsset
		{
			id = "brackets",
			symbols_around = true,
			add_space = true,
			symbols_around_left = "[",
			symbols_around_right = "]"
		});
		this.add(new LinguisticsAsset
		{
			id = "braces",
			symbols_around = true,
			add_space = true,
			symbols_around_left = "{",
			symbols_around_right = "}"
		});
		this.add(new LinguisticsAsset
		{
			id = "apostrophe",
			add_space = true,
			simple_text = "'"
		});
	}

	// Token: 0x040013AC RID: 5036
	private const string C = "C";

	// Token: 0x040013AD RID: 5037
	private const string V = "V";
}
