using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000686 RID: 1670
public class EquipmentEditor : AugmentationsEditor<EquipmentAsset, EquipmentButton, EquipmentEditorButton, ItemGroupAsset, EquipmentGroupElement, IEquipmentWindow, IEquipmentEditor>, IEquipmentEditor, IAugmentationsEditor
{
	// Token: 0x170002FA RID: 762
	// (get) Token: 0x0600359E RID: 13726 RVA: 0x00189344 File Offset: 0x00187544
	protected override List<ItemGroupAsset> augmentation_groups_list
	{
		get
		{
			return AssetManager.item_groups.list;
		}
	}

	// Token: 0x170002FB RID: 763
	// (get) Token: 0x0600359F RID: 13727 RVA: 0x00189350 File Offset: 0x00187550
	protected override EquipmentAsset edited_marker_augmentation
	{
		get
		{
			return null;
		}
	}

	// Token: 0x170002FC RID: 764
	// (get) Token: 0x060035A0 RID: 13728 RVA: 0x00189353 File Offset: 0x00187553
	protected override List<EquipmentAsset> all_augmentations_list
	{
		get
		{
			return AssetManager.items.list;
		}
	}

	// Token: 0x060035A1 RID: 13729 RVA: 0x00189360 File Offset: 0x00187560
	protected override void onEnableRain()
	{
		this.rain_editor_state = PlayerConfig.instance.data.equipment_editor_state;
		this.augmentations_list_link = PlayerConfig.instance.data.equipment_editor;
		this.validateRainData();
		this.augmentations_hashset.Clear();
		this.augmentations_hashset.UnionWith(this.augmentations_list_link);
		this.loadEditorSelectedAugmentations();
		this.rain_state_toggle_action = delegate()
		{
			this.toggleRainState(ref PlayerConfig.instance.data.equipment_editor_state);
		};
		this.rain_state_switcher.toggleState(this.rain_editor_state == RainState.Remove);
	}

	// Token: 0x060035A2 RID: 13730 RVA: 0x001893E8 File Offset: 0x001875E8
	protected override void OnEnable()
	{
		if (!this.rain_editor)
		{
			Actor tActor = this.getCurrentActor();
			if (!tActor.canEditEquipment())
			{
				return;
			}
			foreach (EquipmentEditorButton equipmentEditorButton in this.all_augmentation_buttons)
			{
				EquipmentAsset tAsset = equipmentEditorButton.augmentation_button.getElementAsset();
				bool tShowButton = true;
				if (!tActor.asset.canEditItem(tAsset))
				{
					tShowButton = false;
				}
				equipmentEditorButton.gameObject.SetActive(tShowButton);
			}
		}
		base.OnEnable();
	}

	// Token: 0x060035A3 RID: 13731 RVA: 0x0018947C File Offset: 0x0018767C
	protected override void metaAugmentationClick(EquipmentEditorButton pButton)
	{
		if (!base.isAugmentationAvailable(pButton.augmentation_button))
		{
			return;
		}
		EquipmentButton tButton = pButton.augmentation_button;
		EquipmentAsset tItemAsset = pButton.augmentation_button.getElementAsset();
		if (this.canChangeSlot(tItemAsset))
		{
			bool flag = this.hasAugmentation(tButton);
			if (!this.isSlotEmpty(tButton))
			{
				this.removeAugmentation(tButton);
			}
			if (!flag)
			{
				this.addAugmentation(tButton);
			}
		}
		this.augmentation_window.checkEquipmentTabIcon();
		base.metaAugmentationClick(pButton);
	}

	// Token: 0x060035A4 RID: 13732 RVA: 0x001894E8 File Offset: 0x001876E8
	protected override void rainAugmentationClick(EquipmentEditorButton pButton)
	{
		if (!base.isAugmentationAvailable(pButton.augmentation_button))
		{
			return;
		}
		string tItemId = pButton.augmentation_button.getElementAsset().id;
		if (!this.augmentations_hashset.Contains(tItemId))
		{
			this.augmentations_hashset.Add(tItemId);
		}
		else
		{
			this.augmentations_hashset.Remove(tItemId);
		}
		base.rainAugmentationClick(pButton);
	}

	// Token: 0x060035A5 RID: 13733 RVA: 0x00189546 File Offset: 0x00187746
	protected override void showActiveButtons()
	{
		this.augmentation_window.reloadEquipment();
	}

	// Token: 0x060035A6 RID: 13734 RVA: 0x00189553 File Offset: 0x00187753
	protected override ListPool<EquipmentAsset> getOrderedAugmentationsList()
	{
		return new ListPool<EquipmentAsset>(this.all_augmentations_list);
	}

	// Token: 0x060035A7 RID: 13735 RVA: 0x00189560 File Offset: 0x00187760
	protected override void createButton(EquipmentAsset pElement, EquipmentGroupElement pGroup)
	{
		if (!pElement.show_in_meta_editor)
		{
			return;
		}
		bool tShowButton = true;
		if (!this.rain_editor && !this.getCurrentActor().asset.canEditItem(pElement))
		{
			tShowButton = false;
		}
		EquipmentEditorButton tEditorButton = Object.Instantiate<EquipmentEditorButton>(this.prefab_editor_augmentation, pGroup.augmentation_buttons_transform);
		tEditorButton.augmentation_button.is_editor_button = true;
		tEditorButton.augmentation_button.load(pElement);
		this.all_augmentation_buttons.Add(tEditorButton);
		pGroup.augmentation_buttons.Add(tEditorButton);
		tEditorButton.augmentation_button.button.onClick.RemoveAllListeners();
		tEditorButton.augmentation_button.button.onClick.AddListener(delegate()
		{
			this.editorButtonClick(tEditorButton);
		});
		tEditorButton.gameObject.SetActive(tShowButton);
	}

	// Token: 0x060035A8 RID: 13736 RVA: 0x0018964E File Offset: 0x0018784E
	protected override void startSignal()
	{
		AchievementLibrary.equipment_explorer.checkBySignal(null);
	}

	// Token: 0x060035A9 RID: 13737 RVA: 0x0018965B File Offset: 0x0018785B
	private bool canChangeSlot(EquipmentAsset pAsset)
	{
		return pAsset.can_be_given && this.getSlotFromCurrentActor(pAsset.equipment_type).canChangeSlot();
	}

	// Token: 0x060035AA RID: 13738 RVA: 0x00189678 File Offset: 0x00187878
	private bool isSlotEmpty(EquipmentButton pButton)
	{
		EquipmentAsset tItemAsset = pButton.getElementAsset();
		return this.getSlotFromCurrentActor(tItemAsset.equipment_type).isEmpty();
	}

	// Token: 0x060035AB RID: 13739 RVA: 0x001896A0 File Offset: 0x001878A0
	protected override bool hasAugmentation(EquipmentButton pButton)
	{
		EquipmentAsset tItemAsset = pButton.getElementAsset();
		ActorEquipmentSlot tSlot = this.getSlotFromCurrentActor(tItemAsset.equipment_type);
		if (tSlot.isEmpty())
		{
			return false;
		}
		Item item = tSlot.getItem();
		string tAssetId = pButton.getElementAsset().id;
		return item.getAsset().id == tAssetId;
	}

	// Token: 0x060035AC RID: 13740 RVA: 0x001896F4 File Offset: 0x001878F4
	protected override bool addAugmentation(EquipmentButton pButton)
	{
		Actor tActor = this.getCurrentActor();
		EquipmentAsset tItemAsset = pButton.getElementAsset();
		Item tItem = World.world.items.generateItem(tItemAsset, tActor.kingdom, World.world.map_stats.player_name, 1, tActor, 0, true);
		tItem.addMod("divine_rune");
		tActor.equipment.setItem(tItem, tActor);
		return true;
	}

	// Token: 0x060035AD RID: 13741 RVA: 0x00189754 File Offset: 0x00187954
	protected override bool removeAugmentation(EquipmentButton pButton)
	{
		Actor currentActor = this.getCurrentActor();
		EquipmentType tSlotType = pButton.getElementAsset().equipment_type;
		currentActor.equipment.getSlot(tSlotType).takeAwayItem();
		currentActor.setStatsDirty();
		return true;
	}

	// Token: 0x060035AE RID: 13742 RVA: 0x0018978A File Offset: 0x0018798A
	private ActorEquipmentSlot getSlotFromCurrentActor(EquipmentType pType)
	{
		return this.getCurrentActor().equipment.getSlot(pType);
	}

	// Token: 0x060035AF RID: 13743 RVA: 0x0018979D File Offset: 0x0018799D
	private Actor getCurrentActor()
	{
		return SelectedUnit.unit;
	}

	// Token: 0x060035B0 RID: 13744 RVA: 0x001897A4 File Offset: 0x001879A4
	protected override void loadEditorSelectedButton(EquipmentButton pButton, string pAugmentationId)
	{
		base.loadEditorSelectedButton(pButton, pAugmentationId);
		EquipmentAsset tAsset = AssetManager.items.get(pAugmentationId);
		pButton.load(tAsset);
	}

	// Token: 0x060035B1 RID: 13745 RVA: 0x001897CC File Offset: 0x001879CC
	protected override bool isAugmentationExists(string pId)
	{
		return AssetManager.items.has(pId);
	}

	// Token: 0x060035B2 RID: 13746 RVA: 0x001897DC File Offset: 0x001879DC
	protected override void toggleRainState(ref RainState pState)
	{
		base.toggleRainState(ref pState);
		this.art.sprite = ((pState == RainState.Add) ? this.sprite_art : this.sprite_art_void);
		if (pState == RainState.Add)
		{
			this.augmentations_hashset.Clear();
			this.augmentations_hashset.UnionWith(this.augmentations_list_link);
			this.reloadButtons();
		}
	}

	// Token: 0x040027F6 RID: 10230
	[SerializeField]
	protected Sprite sprite_art;

	// Token: 0x040027F7 RID: 10231
	[SerializeField]
	protected Sprite sprite_art_void;
}
