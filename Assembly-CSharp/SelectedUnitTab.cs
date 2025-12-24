using System;
using ai.behaviours;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006D7 RID: 1751
public class SelectedUnitTab : SelectedNano<Actor>
{
	// Token: 0x06003836 RID: 14390 RVA: 0x00193D58 File Offset: 0x00191F58
	private void Start()
	{
		SelectedUnit.subscribeClearEvent(new SelectedUnitClearEvent(this.clearLastObject));
	}

	// Token: 0x06003837 RID: 14391 RVA: 0x00193D6C File Offset: 0x00191F6C
	private void OnEnable()
	{
		if (this._drag_snap_avatar == null)
		{
			return;
		}
		if (InputHelpers.mouseSupported)
		{
			this._drag_snap_avatar.enabled = true;
			return;
		}
		this._drag_snap_avatar.enabled = false;
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x00193D9D File Offset: 0x00191F9D
	private void LateUpdate()
	{
		if (!this._requested_buttons_update)
		{
			return;
		}
		this._requested_buttons_update = false;
		PowerTabController.instance.tab_selected_unit.findNeighbours(true);
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x00193DBF File Offset: 0x00191FBF
	protected override void updateElementsOnChange(Actor pActor)
	{
		base.updateElementsOnChange(pActor);
		pActor.asset.unlock(true);
		this.updateButtons(pActor);
		this.showStatsGeneral(pActor);
		this.updateStatuses(pActor);
		this.updateEquipment(pActor);
	}

	// Token: 0x0600383A RID: 14394 RVA: 0x00193DF4 File Offset: 0x00191FF4
	protected override void updateElementsAlways(Actor pActor)
	{
		this.showTask(pActor);
		this.showStatBars(pActor);
		this.checkAvatar(pActor);
		base.setIconValue("i_age", (float)pActor.getAge(), null, "", false, "", '/');
		base.updateElementsAlways(pActor);
	}

	// Token: 0x0600383B RID: 14395 RVA: 0x00193E45 File Offset: 0x00192045
	protected override void checkAchievements(Actor pActor)
	{
		AchievementLibrary.checkUnitAchievements(pActor);
	}

	// Token: 0x0600383C RID: 14396 RVA: 0x00193E50 File Offset: 0x00192050
	private void updateButtons(Actor pActor)
	{
		this._button_equipment_editor.gameObject.SetActive(pActor.asset.can_edit_equipment);
		this._button_mind.gameObject.SetActive(pActor.asset.inspect_mind);
		this._button_traits_editor.gameObject.SetActive(pActor.asset.can_edit_traits);
		this._button_genealogy.gameObject.SetActive(pActor.asset.inspect_genealogy);
		this._button_plots_tab.gameObject.SetActive(pActor.isKing() || pActor.isCityLeader() || pActor.hasClan());
		this._button_equipment_editor_icon.sprite = (pActor.isWeaponFirearm() ? this._button_equipment_editor_sprite_firearm : this._button_equipment_editor_sprite_normal);
		this._requested_buttons_update = true;
	}

	// Token: 0x0600383D RID: 14397 RVA: 0x00193F1A File Offset: 0x0019211A
	private void updateStatuses(Actor pActor)
	{
		this._container_status.update(pActor);
	}

	// Token: 0x0600383E RID: 14398 RVA: 0x00193F28 File Offset: 0x00192128
	private void updateEquipment(Actor pActor)
	{
		this._container_equipment.update(pActor);
	}

	// Token: 0x0600383F RID: 14399 RVA: 0x00193F38 File Offset: 0x00192138
	protected override void showStatsGeneral(Actor pActor)
	{
		base.setIconValue("i_renown", (float)pActor.renown, null, "", false, "", '/');
		base.setIconValue("i_level", (float)pActor.level, null, "", false, "", '/');
		base.setIconValue("i_experience", (float)pActor.data.experience, new float?((float)pActor.getExpToLevelup()), "", false, "", '/');
		base.setIconValue("i_money", (float)pActor.money, null, "", false, "", '/');
		base.setIconValue("i_loot", (float)pActor.loot, null, "", false, "", '/');
		base.setIconValue("i_kills", (float)pActor.data.kills, null, "", false, "", '/');
		base.setIconValue("i_children", (float)pActor.current_children_count, null, "", false, "", '/');
		int tMaxDamage = (int)pActor.stats["damage"];
		int tMinDamage = (int)((float)tMaxDamage * pActor.stats["damage_range"]);
		base.setIconValue("i_damage", (float)tMinDamage, new float?((float)tMaxDamage), "", false, "", '-');
		this.name_field.text = pActor.getName();
		this.name_field.color = pActor.kingdom.getColor().getColorText();
		if (pActor.asset.is_boat)
		{
			this.icon_right.sprite = pActor.asset.getSpriteIcon();
		}
		else if (pActor.isSexMale())
		{
			this.icon_right.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
		}
		else
		{
			this.icon_right.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
		}
		Sprite tIcon;
		if (pActor.asset.is_boat && pActor.hasCity())
		{
			tIcon = pActor.city.getSpriteIcon();
		}
		else
		{
			tIcon = pActor.asset.getSpriteIcon();
		}
		this.icon_left.sprite = tIcon;
	}

	// Token: 0x06003840 RID: 14400 RVA: 0x00194172 File Offset: 0x00192372
	private void checkAvatar(Actor pActor)
	{
		if (base.isNanoChanged(pActor) || this._avatar_element.avatarLoader.actorStateChanged())
		{
			this._avatar_element.show(pActor);
			return;
		}
		this._avatar_element.updateTileSprite();
	}

	// Token: 0x06003841 RID: 14401 RVA: 0x001941A7 File Offset: 0x001923A7
	private void showStatAvatar(Actor pActor)
	{
		this._avatar_element.show(pActor);
	}

	// Token: 0x06003842 RID: 14402 RVA: 0x001941B8 File Offset: 0x001923B8
	private void showTask(Actor pActor)
	{
		BehaviourTaskActor tTask = pActor.ai.task;
		string tTaskTitle = pActor.getTaskText();
		Sprite tIcon;
		if (tTask == null)
		{
			tIcon = this._no_task_icon;
		}
		else
		{
			tIcon = tTask.getSprite();
		}
		this._task_icon_left.sprite = tIcon;
		this._task_icon_right.sprite = tIcon;
		this._task_title.text = tTaskTitle;
	}

	// Token: 0x06003843 RID: 14403 RVA: 0x00194210 File Offset: 0x00192410
	private void showStatBars(Actor pActor)
	{
		float tHealthBarCur = (float)pActor.getHealth();
		float tHealthMax = (float)pActor.getMaxHealth();
		this._bar_health.setBar(tHealthBarCur, tHealthMax, "/" + ((int)tHealthMax).ToText(4), false, false, true, 0.25f);
		bool tShowHappiness = pActor.hasEmotions();
		this._bar_happiness_icon.load(pActor);
		this.checkShowProgressBar(this._bar_happiness, "%", (float)pActor.getHappinessPercent(), 100f, tShowHappiness);
		bool tShowHunger = pActor.needsFood();
		float tHungerCurrent = (float)pActor.getNutrition() / (float)pActor.getMaxNutrition() * 100f;
		this.checkShowProgressBar(this._bar_hunger, "%", tHungerCurrent, 100f, tShowHunger);
		bool tShowStamina = !pActor.asset.force_hide_stamina;
		int tStaminaMax = pActor.getMaxStamina();
		float tStaminaCurrent = (float)Mathf.Clamp(pActor.getStamina(), 0, tStaminaMax);
		this.checkShowProgressBar(this._bar_stamina, "/" + tStaminaMax.ToText(), tStaminaCurrent, (float)tStaminaMax, tShowStamina);
		bool tShowMana = !pActor.asset.force_hide_mana;
		int tManaMax = pActor.getMaxMana();
		float tManaCurrent = (float)Mathf.Clamp(pActor.getMana(), 0, tManaMax);
		this.checkShowProgressBar(this._bar_mana, "/" + tManaMax.ToText(), tManaCurrent, (float)tManaMax, tShowMana);
	}

	// Token: 0x06003844 RID: 14404 RVA: 0x00194356 File Offset: 0x00192556
	protected override string getPowerTabAssetID()
	{
		return "selected_unit";
	}

	// Token: 0x06003845 RID: 14405 RVA: 0x0019435D File Offset: 0x0019255D
	private void checkShowProgressBar(StatBar pBar, string pEnding, float pCurrentValue, float pMax, bool pShow)
	{
		pBar.gameObject.SetActive(pShow);
		if (!pShow)
		{
			return;
		}
		pBar.setBar(pCurrentValue, pMax, pEnding, false, false, true, 0.25f);
	}

	// Token: 0x06003846 RID: 14406 RVA: 0x00194384 File Offset: 0x00192584
	public void avatarTouchScream()
	{
		if (SelectedUnit.isSet())
		{
			this.nano_object.pokeFromAvatarUI();
			World.world.locatePosition(this.nano_object.current_position);
		}
		this._avatar_text.spawnAvatarText(null);
		((IShakable)ToolbarButtons.instance).shake();
	}

	// Token: 0x040029B6 RID: 10678
	[SerializeField]
	private Image _task_icon_left;

	// Token: 0x040029B7 RID: 10679
	[SerializeField]
	private Image _task_icon_right;

	// Token: 0x040029B8 RID: 10680
	[SerializeField]
	private Text _task_title;

	// Token: 0x040029B9 RID: 10681
	[SerializeField]
	private Sprite _no_task_icon;

	// Token: 0x040029BA RID: 10682
	[SerializeField]
	private StatBar _bar_health;

	// Token: 0x040029BB RID: 10683
	[SerializeField]
	private StatBar _bar_mana;

	// Token: 0x040029BC RID: 10684
	[SerializeField]
	private StatBar _bar_stamina;

	// Token: 0x040029BD RID: 10685
	[SerializeField]
	private StatBar _bar_hunger;

	// Token: 0x040029BE RID: 10686
	[SerializeField]
	private StatBar _bar_happiness;

	// Token: 0x040029BF RID: 10687
	[SerializeField]
	private HappinessBarIcon _bar_happiness_icon;

	// Token: 0x040029C0 RID: 10688
	[SerializeField]
	private UiUnitAvatarElement _avatar_element;

	// Token: 0x040029C1 RID: 10689
	[SerializeField]
	private UnitTextManager _avatar_text;

	// Token: 0x040029C2 RID: 10690
	[SerializeField]
	private SwitchStateButton _button_traits_editor;

	// Token: 0x040029C3 RID: 10691
	[SerializeField]
	private SwitchStateButton _button_equipment_editor;

	// Token: 0x040029C4 RID: 10692
	[SerializeField]
	private SwitchStateButton _button_mind;

	// Token: 0x040029C5 RID: 10693
	[SerializeField]
	private SwitchStateButton _button_genealogy;

	// Token: 0x040029C6 RID: 10694
	[SerializeField]
	private SwitchStateButton _button_plots_tab;

	// Token: 0x040029C7 RID: 10695
	[SerializeField]
	private Image _button_equipment_editor_icon;

	// Token: 0x040029C8 RID: 10696
	[SerializeField]
	private Sprite _button_equipment_editor_sprite_normal;

	// Token: 0x040029C9 RID: 10697
	[SerializeField]
	private Sprite _button_equipment_editor_sprite_firearm;

	// Token: 0x040029CA RID: 10698
	[SerializeField]
	private ActorSelectedContainerStatus _container_status;

	// Token: 0x040029CB RID: 10699
	[SerializeField]
	private ActorSelectedContainerEquipment _container_equipment;

	// Token: 0x040029CC RID: 10700
	[SerializeField]
	private DragSnapElement _drag_snap_avatar;

	// Token: 0x040029CD RID: 10701
	private bool _requested_buttons_update;
}
