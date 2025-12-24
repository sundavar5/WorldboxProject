using System;
using System.Collections.Generic;

// Token: 0x0200066F RID: 1647
public class ClanTraitsEditor : TraitsEditor<ClanTrait, ClanTraitButton, ClanTraitEditorButton, ClanTraitGroupAsset, ClanTraitGroupElement>
{
	// Token: 0x170002E3 RID: 739
	// (get) Token: 0x06003529 RID: 13609 RVA: 0x00187F37 File Offset: 0x00186137
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Clan;
		}
	}

	// Token: 0x170002E4 RID: 740
	// (get) Token: 0x0600352A RID: 13610 RVA: 0x00187F3A File Offset: 0x0018613A
	protected override List<ClanTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.clan_trait_groups.list;
		}
	}

	// Token: 0x170002E5 RID: 741
	// (get) Token: 0x0600352B RID: 13611 RVA: 0x00187F46 File Offset: 0x00186146
	protected override List<ClanTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.clan_traits.list;
		}
	}

	// Token: 0x170002E6 RID: 742
	// (get) Token: 0x0600352C RID: 13612 RVA: 0x00187F52 File Offset: 0x00186152
	protected override ClanTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.clan_traits.get("geb");
		}
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x00187F63 File Offset: 0x00186163
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_clan.checkBySignal(null);
	}
}
