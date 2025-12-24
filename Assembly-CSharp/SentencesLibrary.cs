using System;

// Token: 0x0200028D RID: 653
public class SentencesLibrary : AssetLibrary<SentenceAsset>
{
	// Token: 0x0600190F RID: 6415 RVA: 0x000EDC94 File Offset: 0x000EBE94
	public override void init()
	{
		this.add(new SentenceAsset
		{
			id = "declarative"
		});
		this.t.addTemplate(new string[]
		{
			"pron_obj",
			"word_concept",
			"comma",
			"word_concept",
			"word_action",
			"word_concept",
			"word_creature"
		});
		this.t.addTemplate(new string[]
		{
			"pron_obj",
			"word_concept",
			"pron_poss_adj",
			"word_place"
		});
		this.t.addTemplate(new string[]
		{
			"pron_obj",
			"word_concept",
			"pron_poss_adj",
			"word_place"
		});
		this.add(new SentenceAsset
		{
			id = "interrogative"
		});
		this.add(new SentenceAsset
		{
			id = "imperative"
		});
		this.add(new SentenceAsset
		{
			id = "exclamatory"
		});
	}
}
