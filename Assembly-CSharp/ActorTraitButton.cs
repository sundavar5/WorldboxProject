using System;

// Token: 0x020006C6 RID: 1734
public class ActorTraitButton : TraitButton<ActorTrait>
{
	// Token: 0x06003794 RID: 14228 RVA: 0x0019130C File Offset: 0x0018F50C
	internal override void load(string pTraitID)
	{
		ActorTrait tTrait = AssetManager.traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x17000311 RID: 785
	// (get) Token: 0x06003795 RID: 14229 RVA: 0x0019132C File Offset: 0x0018F52C
	protected override string tooltip_type
	{
		get
		{
			return "trait";
		}
	}

	// Token: 0x06003796 RID: 14230 RVA: 0x00191333 File Offset: 0x0018F533
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			trait = this.augmentation_asset
		};
	}
}
