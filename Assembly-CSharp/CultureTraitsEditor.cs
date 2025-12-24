using System;
using System.Collections.Generic;

// Token: 0x0200067D RID: 1661
public class CultureTraitsEditor : TraitsEditor<CultureTrait, CultureTraitButton, CultureTraitEditorButton, CultureTraitGroupAsset, CultureTraitGroupElement>
{
	// Token: 0x170002F2 RID: 754
	// (get) Token: 0x06003569 RID: 13673 RVA: 0x0018889B File Offset: 0x00186A9B
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Culture;
		}
	}

	// Token: 0x170002F3 RID: 755
	// (get) Token: 0x0600356A RID: 13674 RVA: 0x0018889E File Offset: 0x00186A9E
	protected override List<CultureTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.culture_trait_groups.list;
		}
	}

	// Token: 0x170002F4 RID: 756
	// (get) Token: 0x0600356B RID: 13675 RVA: 0x001888AA File Offset: 0x00186AAA
	protected override List<CultureTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.culture_traits.list;
		}
	}

	// Token: 0x170002F5 RID: 757
	// (get) Token: 0x0600356C RID: 13676 RVA: 0x001888B6 File Offset: 0x00186AB6
	protected override CultureTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.culture_traits.get("ethno_sculpted");
		}
	}

	// Token: 0x0600356D RID: 13677 RVA: 0x001888C7 File Offset: 0x00186AC7
	protected override void startSignal()
	{
		AchievementLibrary.trait_explorer_culture.checkBySignal(null);
	}

	// Token: 0x0600356E RID: 13678 RVA: 0x001888D4 File Offset: 0x00186AD4
	protected override void metaAugmentationClick(CultureTraitEditorButton pButton)
	{
		base.metaAugmentationClick(pButton);
		if (pButton.augmentation_button.getElementAsset().group_id != "succession")
		{
			return;
		}
		AchievementLibrary.succession.check(null);
	}
}
