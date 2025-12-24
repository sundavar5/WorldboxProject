using System;
using System.Collections.Generic;

// Token: 0x020006FE RID: 1790
public class KingdomTraitsEditor : TraitsEditor<KingdomTrait, KingdomTraitButton, KingdomTraitEditorButton, KingdomTraitGroupAsset, KingdomTraitGroupElement>
{
	// Token: 0x1700032D RID: 813
	// (get) Token: 0x06003949 RID: 14665 RVA: 0x001983D2 File Offset: 0x001965D2
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Kingdom;
		}
	}

	// Token: 0x1700032E RID: 814
	// (get) Token: 0x0600394A RID: 14666 RVA: 0x001983D5 File Offset: 0x001965D5
	protected override List<KingdomTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.kingdoms_traits_groups.list;
		}
	}

	// Token: 0x1700032F RID: 815
	// (get) Token: 0x0600394B RID: 14667 RVA: 0x001983E1 File Offset: 0x001965E1
	protected override List<KingdomTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.kingdoms_traits.list;
		}
	}
}
