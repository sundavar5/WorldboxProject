using System;
using System.Collections.Generic;

// Token: 0x02000765 RID: 1893
public class SubspeciesBirthTraitsEditor : TraitsEditor<ActorTrait, ActorTraitButton, ActorTraitEditorButton, ActorTraitGroupAsset, ActorTraitGroupElement>
{
	// Token: 0x1700037E RID: 894
	// (get) Token: 0x06003C1B RID: 15387 RVA: 0x001A2EC1 File Offset: 0x001A10C1
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x1700037F RID: 895
	// (get) Token: 0x06003C1C RID: 15388 RVA: 0x001A2EC4 File Offset: 0x001A10C4
	protected override List<ActorTraitGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.trait_groups.list;
		}
	}

	// Token: 0x17000380 RID: 896
	// (get) Token: 0x06003C1D RID: 15389 RVA: 0x001A2ED0 File Offset: 0x001A10D0
	protected override ActorTrait edited_marker_augmentation
	{
		get
		{
			return AssetManager.traits.get("scar_of_divinity");
		}
	}

	// Token: 0x17000381 RID: 897
	// (get) Token: 0x06003C1E RID: 15390 RVA: 0x001A2EE1 File Offset: 0x001A10E1
	protected override List<ActorTrait> all_augmentations_list
	{
		get
		{
			return AssetManager.traits.list;
		}
	}

	// Token: 0x06003C1F RID: 15391 RVA: 0x001A2EED File Offset: 0x001A10ED
	public override ITraitsOwner<ActorTrait> getTraitsOwner()
	{
		return this.getTraitsContainer();
	}

	// Token: 0x06003C20 RID: 15392 RVA: 0x001A2EF5 File Offset: 0x001A10F5
	private SubspeciesActorBirthTraits getTraitsContainer()
	{
		return this.getSelectedSubspecies().getActorBirthTraits();
	}

	// Token: 0x06003C21 RID: 15393 RVA: 0x001A2F02 File Offset: 0x001A1102
	protected override void create()
	{
		base.create();
		this._subspecies_window = base.GetComponentInParent<SubspeciesWindow>();
		this.selected_editor_buttons = new ObjectPoolGenericMono<ActorTraitButton>(this.prefab_augmentation, this.selected_editor_augmentations_grid.transform);
	}

	// Token: 0x06003C22 RID: 15394 RVA: 0x001A2F32 File Offset: 0x001A1132
	protected override void OnEnable()
	{
		base.OnEnable();
		this.augmentations_list_link = this.getSelectedSubspecies().getActorBirthTraits().getTraitsAsStrings();
		this.augmentations_hashset.Clear();
		this.augmentations_hashset.UnionWith(this.augmentations_list_link);
		this.loadEditorSelectedAugmentations();
	}

	// Token: 0x06003C23 RID: 15395 RVA: 0x001A2F72 File Offset: 0x001A1172
	protected override void onNanoWasModified()
	{
		this.getSelectedSubspecies().eventGMO();
		base.onNanoWasModified();
	}

	// Token: 0x06003C24 RID: 15396 RVA: 0x001A2F85 File Offset: 0x001A1185
	protected override void loadEditorSelectedButton(ActorTraitButton pButton, string pAugmentationId)
	{
		base.loadEditorSelectedButton(pButton, pAugmentationId);
		pButton.load(pAugmentationId);
	}

	// Token: 0x06003C25 RID: 15397 RVA: 0x001A2F96 File Offset: 0x001A1196
	protected override bool isAugmentationExists(string pId)
	{
		return AssetManager.traits.has(pId);
	}

	// Token: 0x06003C26 RID: 15398 RVA: 0x001A2FA3 File Offset: 0x001A11A3
	protected override void metaAugmentationClick(ActorTraitEditorButton pButton)
	{
		base.metaAugmentationClick(pButton);
		this.augmentations_hashset.Clear();
		this.augmentations_hashset.UnionWith(this.getTraitsContainer().getTraitsAsStrings());
		this.loadEditorSelectedAugmentations();
	}

	// Token: 0x06003C27 RID: 15399 RVA: 0x001A2FD3 File Offset: 0x001A11D3
	protected override void refreshAugmentationWindow()
	{
		this._subspecies_window.updateStats();
		this._subspecies_window.reloadBanner();
	}

	// Token: 0x06003C28 RID: 15400 RVA: 0x001A2FEB File Offset: 0x001A11EB
	protected override void showActiveButtons()
	{
		this.loadEditorSelectedAugmentations();
	}

	// Token: 0x06003C29 RID: 15401 RVA: 0x001A2FF3 File Offset: 0x001A11F3
	private Subspecies getSelectedSubspecies()
	{
		return (Subspecies)base.meta_type_asset.get_selected();
	}

	// Token: 0x04002BD7 RID: 11223
	private SubspeciesWindow _subspecies_window;
}
