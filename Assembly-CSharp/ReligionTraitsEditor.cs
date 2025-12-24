using System;
using System.Collections.Generic;

// Token: 0x02000745 RID: 1861
public class ReligionTraitsEditor : TraitsEditor<ReligionTrait, ReligionTraitButton, ReligionTraitEditorButton, ReligionTraitGroupAsset, ReligionTraitGroupElement>
{
	// Token: 0x17000356 RID: 854
	// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x0019F9CF File Offset: 0x0019DBCF
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Religion;
		}
	}

	// Token: 0x17000357 RID: 855
	// (get) Token: 0x06003AFA RID: 15098 RVA: 0x0019F9D2 File Offset: 0x0019DBD2
	protected override List<ReligionTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.religion_trait_groups.list;
		}
	}

	// Token: 0x17000358 RID: 856
	// (get) Token: 0x06003AFB RID: 15099 RVA: 0x0019F9DE File Offset: 0x0019DBDE
	protected override List<ReligionTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.religion_traits.list;
		}
	}

	// Token: 0x17000359 RID: 857
	// (get) Token: 0x06003AFC RID: 15100 RVA: 0x0019F9EA File Offset: 0x0019DBEA
	protected override ReligionTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.religion_traits.get("divine_insight");
		}
	}

	// Token: 0x06003AFD RID: 15101 RVA: 0x0019F9FB File Offset: 0x0019DBFB
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_religion.checkBySignal(null);
	}
}
