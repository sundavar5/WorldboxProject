using System;
using System.Collections.Generic;

// Token: 0x02000771 RID: 1905
public class SubspeciesTraitsEditor : TraitsEditor<SubspeciesTrait, SubspeciesTraitButton, SubspeciesTraitEditorButton, SubspeciesTraitGroupAsset, SubspeciesTraitGroupElement>
{
	// Token: 0x17000389 RID: 905
	// (get) Token: 0x06003C56 RID: 15446 RVA: 0x001A350F File Offset: 0x001A170F
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x1700038A RID: 906
	// (get) Token: 0x06003C57 RID: 15447 RVA: 0x001A3512 File Offset: 0x001A1712
	protected override List<SubspeciesTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.subspecies_trait_groups.list;
		}
	}

	// Token: 0x1700038B RID: 907
	// (get) Token: 0x06003C58 RID: 15448 RVA: 0x001A351E File Offset: 0x001A171E
	protected override List<SubspeciesTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.subspecies_traits.list;
		}
	}

	// Token: 0x1700038C RID: 908
	// (get) Token: 0x06003C59 RID: 15449 RVA: 0x001A352A File Offset: 0x001A172A
	protected override SubspeciesTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.subspecies_traits.get("gmo");
		}
	}

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x06003C5A RID: 15450 RVA: 0x001A353B File Offset: 0x001A173B
	protected override List<string> filter_traits
	{
		get
		{
			return base.getActorAsset().trait_filter_subspecies;
		}
	}

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06003C5B RID: 15451 RVA: 0x001A3548 File Offset: 0x001A1748
	protected override List<string> filter_trait_groups
	{
		get
		{
			return base.getActorAsset().trait_group_filter_subspecies;
		}
	}

	// Token: 0x06003C5C RID: 15452 RVA: 0x001A3555 File Offset: 0x001A1755
	protected override void onNanoWasModified()
	{
		((Subspecies)this.getTraitsOwner()).eventGMO();
		base.onNanoWasModified();
	}

	// Token: 0x06003C5D RID: 15453 RVA: 0x001A356D File Offset: 0x001A176D
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_subspecies.checkBySignal(null);
		AchievementLibrary.swarm.checkBySignal(this.getTraitsOwner());
	}
}
