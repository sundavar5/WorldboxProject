using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000614 RID: 1556
public class WindowMetaGeneric<TMetaObject, TData> : StatsWindow, IMetaWindow, IInterestingPeopleWindow, IMetaWithFamiliesWindow where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x170002BE RID: 702
	// (get) Token: 0x060032F8 RID: 13048 RVA: 0x00181664 File Offset: 0x0017F864
	protected virtual TMetaObject meta_object
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x060032F9 RID: 13049 RVA: 0x00181676 File Offset: 0x0017F876
	public TMetaObject getMetaObject()
	{
		return this.meta_object;
	}

	// Token: 0x060032FA RID: 13050 RVA: 0x0018167E File Offset: 0x0017F87E
	public ICoreObject getCoreObject()
	{
		return this.meta_object;
	}

	// Token: 0x060032FB RID: 13051 RVA: 0x0018168B File Offset: 0x0017F88B
	protected override void create()
	{
		this.initMonoFields();
		this.initNameInput();
		this.initStuff();
		base.create();
	}

	// Token: 0x060032FC RID: 13052 RVA: 0x001816A8 File Offset: 0x0017F8A8
	private void initMonoFields()
	{
		Transform transform = base.gameObject.transform.FindRecursive("NameInputElement");
		this._name_input = ((transform != null) ? transform.GetComponent<NameInput>() : null);
		this._main_banners = base.gameObject.transform.FindAllRecursive((Transform p) => p.name == "Main Banner");
	}

	// Token: 0x060032FD RID: 13053 RVA: 0x00181711 File Offset: 0x0017F911
	internal override bool checkCancelWindow()
	{
		return this.meta_object == null || !this.meta_object.isAlive() || base.checkCancelWindow();
	}

	// Token: 0x060032FE RID: 13054 RVA: 0x0018173C File Offset: 0x0017F93C
	protected virtual void initStuff()
	{
	}

	// Token: 0x060032FF RID: 13055 RVA: 0x0018173E File Offset: 0x0017F93E
	protected override void OnEnable()
	{
		base.OnEnable();
		this.startShowingWindow();
	}

	// Token: 0x06003300 RID: 13056 RVA: 0x0018174C File Offset: 0x0017F94C
	public virtual void startShowingWindow()
	{
		this.clear();
		this.loadBanners();
		this.loadNameInput();
		this.showTopPartInformation();
	}

	// Token: 0x06003301 RID: 13057 RVA: 0x00181766 File Offset: 0x0017F966
	protected virtual void showTopPartInformation()
	{
		if (this.species_icon != null)
		{
			this.species_icon.sprite = this.getActorIcon();
		}
		this.updateFavoriteIcon();
	}

	// Token: 0x06003302 RID: 13058 RVA: 0x00181790 File Offset: 0x0017F990
	private void loadBanners()
	{
		BannerGeneric<TMetaObject, TData>[] main_banners = this._main_banners;
		for (int i = 0; i < main_banners.Length; i++)
		{
			main_banners[i].load(this.meta_object);
		}
	}

	// Token: 0x06003303 RID: 13059 RVA: 0x001817C5 File Offset: 0x0017F9C5
	public void reloadBanner()
	{
		this.loadBanners();
	}

	// Token: 0x06003304 RID: 13060 RVA: 0x001817D0 File Offset: 0x0017F9D0
	protected virtual void loadNameInput()
	{
		this._name_input.inputField.onEndEdit.AddListener(delegate(string pString)
		{
			this.onNameChange(pString);
		});
		string tName = this.meta_object.data.name.Trim();
		this._initial_name = tName;
		this._name_input.setText(tName);
		ColorAsset tColorAsset = this.meta_object.getColor();
		if (tColorAsset != null)
		{
			this._name_input.textField.color = tColorAsset.getColorText();
		}
		else
		{
			this._name_input.textField.color = Toolbox.color_white;
		}
		if (this.meta_object.data.custom_name)
		{
			this._name_input.SetOutline();
		}
	}

	// Token: 0x06003305 RID: 13061 RVA: 0x00181899 File Offset: 0x0017FA99
	protected virtual void clear()
	{
	}

	// Token: 0x06003306 RID: 13062 RVA: 0x0018189B File Offset: 0x0017FA9B
	protected virtual void OnDisable()
	{
		this._name_input.inputField.DeactivateInputField();
	}

	// Token: 0x06003307 RID: 13063 RVA: 0x001818AD File Offset: 0x0017FAAD
	protected virtual void initNameInput()
	{
	}

	// Token: 0x06003308 RID: 13064 RVA: 0x001818B0 File Offset: 0x0017FAB0
	protected virtual bool onNameChange(string pInput)
	{
		if (string.IsNullOrWhiteSpace(pInput))
		{
			return false;
		}
		if (this.meta_object.isRekt())
		{
			return false;
		}
		string tName = pInput.Trim();
		if (this._initial_name == tName)
		{
			return false;
		}
		this.meta_object.data.custom_name = true;
		this.meta_object.setName(tName, true);
		this._initial_name = tName;
		this._name_input.SetOutline();
		return true;
	}

	// Token: 0x06003309 RID: 13065 RVA: 0x00181934 File Offset: 0x0017FB34
	public void pressFavorite()
	{
		if (this.meta_object == null)
		{
			return;
		}
		this.meta_object.setFavorite(!this.meta_object.isFavorite());
		this.updateFavoriteIcon();
		base.refreshMetaList();
		SpriteSwitcher.checkAllStates();
		if (this.meta_object.isFavorite())
		{
			WorldTip.showNowTop(this.getTipFavorite(), true);
		}
	}

	// Token: 0x0600330A RID: 13066 RVA: 0x001819A4 File Offset: 0x0017FBA4
	private void updateFavoriteIcon()
	{
		if (this._favorite_icon == null)
		{
			return;
		}
		if (this.meta_object.isFavorite())
		{
			this._favorite_icon.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this._favorite_icon.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x0600330B RID: 13067 RVA: 0x00181A00 File Offset: 0x0017FC00
	internal void tryShowPastNames()
	{
		List<NameEntry> past_names = this.meta_object.data.past_names;
		if (past_names != null && past_names.Count > 1)
		{
			string pId = "past_names";
			List<NameEntry> past_names2 = this.meta_object.data.past_names;
			base.showStatRow(pId, (past_names2 != null) ? past_names2.Count : 1, MetaType.None, -1L, "iconVillages", "past_names", new TooltipDataGetter(this.getTooltipPastNames));
		}
	}

	// Token: 0x0600330C RID: 13068 RVA: 0x00181A88 File Offset: 0x0017FC88
	internal TooltipData getTooltipPastNames()
	{
		return new TooltipData
		{
			tip_name = "past_names",
			past_names = new ListPool<NameEntry>(this.meta_object.data.past_names),
			meta_type = this.meta_type
		};
	}

	// Token: 0x0600330D RID: 13069 RVA: 0x00181AD6 File Offset: 0x0017FCD6
	protected string getTipFavorite()
	{
		return "favorited";
	}

	// Token: 0x0600330E RID: 13070 RVA: 0x00181ADD File Offset: 0x0017FCDD
	public virtual IEnumerable<Actor> getInterestingUnitsList()
	{
		return ((IMetaObject)((object)this.meta_object)).getUnits();
	}

	// Token: 0x0600330F RID: 13071 RVA: 0x00181AF4 File Offset: 0x0017FCF4
	public virtual IEnumerable<Family> getFamilies()
	{
		return ((IMetaObject)((object)this.meta_object)).getFamilies();
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x00181B0B File Offset: 0x0017FD0B
	public virtual bool hasFamilies()
	{
		return ((IMetaObject)((object)this.meta_object)).hasFamilies();
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x00181B22 File Offset: 0x0017FD22
	protected Sprite getActorIcon()
	{
		IMetaObject metaObject = this.meta_object as IMetaObject;
		if (metaObject == null)
		{
			return null;
		}
		return metaObject.getSpriteIcon();
	}

	// Token: 0x040026A7 RID: 9895
	[SerializeField]
	protected Image species_icon;

	// Token: 0x040026A8 RID: 9896
	[SerializeField]
	private Image _favorite_icon;

	// Token: 0x040026A9 RID: 9897
	protected NameInput _name_input;

	// Token: 0x040026AA RID: 9898
	private BannerGeneric<TMetaObject, TData>[] _main_banners;

	// Token: 0x040026AB RID: 9899
	internal string _initial_name;
}
