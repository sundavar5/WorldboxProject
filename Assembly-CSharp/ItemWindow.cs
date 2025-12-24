using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000699 RID: 1689
public class ItemWindow : WindowMetaGeneric<Item, ItemData>
{
	// Token: 0x17000307 RID: 775
	// (get) Token: 0x0600360D RID: 13837 RVA: 0x0018A7AF File Offset: 0x001889AF
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Item;
		}
	}

	// Token: 0x17000308 RID: 776
	// (get) Token: 0x0600360E RID: 13838 RVA: 0x0018A7B3 File Offset: 0x001889B3
	protected override Item meta_object
	{
		get
		{
			return SelectedMetas.selected_item;
		}
	}

	// Token: 0x0600360F RID: 13839 RVA: 0x0018A7BA File Offset: 0x001889BA
	public void clickReforge()
	{
		this.meta_object.reforge(1);
		this.meta_object.addMod("divine_rune");
		this.updateStates();
	}

	// Token: 0x06003610 RID: 13840 RVA: 0x0018A7DF File Offset: 0x001889DF
	public void clickReforgeDivine()
	{
		this.meta_object.reforge(30);
		this.meta_object.addMod("divine_rune");
		this.updateStates();
	}

	// Token: 0x06003611 RID: 13841 RVA: 0x0018A808 File Offset: 0x00188A08
	public void clickCursed()
	{
		if (this.meta_object.hasMod("cursed"))
		{
			this.meta_object.removeMod("cursed");
		}
		else
		{
			this.meta_object.addMod("cursed");
		}
		this.meta_object.addMod("divine_rune");
		this.updateStates();
	}

	// Token: 0x06003612 RID: 13842 RVA: 0x0018A864 File Offset: 0x00188A64
	public void clickEternal()
	{
		if (this.meta_object.hasMod("eternal"))
		{
			this.meta_object.removeMod("eternal");
		}
		else
		{
			this.meta_object.addMod("eternal");
		}
		this.meta_object.addMod("divine_rune");
		this.updateStates();
	}

	// Token: 0x06003613 RID: 13843 RVA: 0x0018A8BE File Offset: 0x00188ABE
	public void clickTransmutation()
	{
		this.meta_object.transmute();
		this.meta_object.addMod("divine_rune");
		this.updateStates();
	}

	// Token: 0x06003614 RID: 13844 RVA: 0x0018A8E4 File Offset: 0x00188AE4
	private void updateStates()
	{
		this.showTopPartInformation();
		this.loadNameInput();
		base.updateStatsRows();
		AchievementLibrary.godly_smithing.check(null);
		Actor tActor = this.meta_object.getActor();
		if (!tActor.isRekt())
		{
			tActor.setStatsDirty();
		}
	}

	// Token: 0x06003615 RID: 13845 RVA: 0x0018A92C File Offset: 0x00188B2C
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		this.clear();
		Item tItem = this.meta_object;
		this._item_banner.load(tItem);
		EquipmentAsset tAsset = this.meta_object.getAsset();
		string tMat = "";
		if (tAsset.material != "basic")
		{
			tMat = tMat + "(" + LocalizedTextManager.getText(tAsset.getMaterialID(), null, false) + ") ";
		}
		this._text_item_type.GetComponent<LocalizedText>().setKeyAndUpdate(tItem.getItemKeyType());
		this._text_item_type.text = tMat + this._text_item_type.text;
		this._text_item_type.color = Toolbox.makeColor(this.meta_object.getQualityColor());
		this._text_item_description.text = tItem.getItemDescription();
		this._button_cursed.setEnabled(this.meta_object.hasMod("cursed"));
		this._button_eternal.setEnabled(this.meta_object.hasMod("eternal"));
	}

	// Token: 0x06003616 RID: 13846 RVA: 0x0018AA30 File Offset: 0x00188C30
	internal override void showStatsRows()
	{
		Item tItem = this.meta_object;
		BaseStatsHelper.showItemModsRows(new BaseStatsHelper.KeyValueFieldGetter(base.getStatRow), tItem);
		BaseStatsHelper.showBaseStatsRows(new BaseStatsHelper.KeyValueFieldGetter(base.getStatRow), tItem.getFullStats(), true);
		base.showStatRow("durability", tItem.getDurabilityString(), MetaType.None, -1L, null, null, null);
		if (tItem.data.kills > 0)
		{
			base.showStatRow("creature_statistics_kills", tItem.data.kills, MetaType.None, -1L, null, null, null);
		}
	}

	// Token: 0x06003617 RID: 13847 RVA: 0x0018AAB6 File Offset: 0x00188CB6
	private void showOutline()
	{
		this._outline.show(RarityLibrary.legendary.color_container);
	}

	// Token: 0x06003618 RID: 13848 RVA: 0x0018AAD0 File Offset: 0x00188CD0
	protected override void loadNameInput()
	{
		this._name_input.inputField.onEndEdit.AddListener(delegate(string pString)
		{
			this.onNameChange(pString);
		});
		string tName = this.meta_object.getName(false).Trim();
		this._initial_name = tName;
		this._name_input.setText(tName);
		this._name_input.textField.color = Toolbox.makeColor(this.meta_object.getQualityColor());
		if (this.meta_object.data.custom_name)
		{
			this._name_input.SetOutline();
		}
	}

	// Token: 0x0400281B RID: 10267
	[SerializeField]
	private EquipmentBanner _item_banner;

	// Token: 0x0400281C RID: 10268
	[SerializeField]
	private Text _text_item_type;

	// Token: 0x0400281D RID: 10269
	[SerializeField]
	private Text _text_item_description;

	// Token: 0x0400281E RID: 10270
	[SerializeField]
	private SwitchButton _button_cursed;

	// Token: 0x0400281F RID: 10271
	[SerializeField]
	private SwitchButton _button_eternal;

	// Token: 0x04002820 RID: 10272
	[SerializeField]
	private Sprite _frame_sprite_legendary;

	// Token: 0x04002821 RID: 10273
	[SerializeField]
	private Sprite _frame_sprite_epic;

	// Token: 0x04002822 RID: 10274
	private IconOutline _outline;
}
