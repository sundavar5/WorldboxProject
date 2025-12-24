using System;

// Token: 0x0200066B RID: 1643
public class ClanTraitButton : TraitButton<ClanTrait>
{
	// Token: 0x06003521 RID: 13601 RVA: 0x00187ED0 File Offset: 0x001860D0
	internal override void load(string pTraitID)
	{
		ClanTrait tTrait = AssetManager.clan_traits.get(pTraitID);
		this.load(tTrait);
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x00187EF0 File Offset: 0x001860F0
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_clan.checkBySignal(null);
	}

	// Token: 0x170002E2 RID: 738
	// (get) Token: 0x06003523 RID: 13603 RVA: 0x00187EFD File Offset: 0x001860FD
	protected override string tooltip_type
	{
		get
		{
			return "clan_trait";
		}
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x00187F04 File Offset: 0x00186104
	protected override TooltipData tooltipDataBuilder()
	{
		return new TooltipData
		{
			clan_trait = this.augmentation_asset
		};
	}
}
