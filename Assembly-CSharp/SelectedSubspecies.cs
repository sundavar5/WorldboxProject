using System;
using UnityEngine;

// Token: 0x02000756 RID: 1878
public class SelectedSubspecies : SelectedMeta<Subspecies, SubspeciesData>
{
	// Token: 0x1700036B RID: 875
	// (get) Token: 0x06003B65 RID: 15205 RVA: 0x001A0B4B File Offset: 0x0019ED4B
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Subspecies;
		}
	}

	// Token: 0x06003B66 RID: 15206 RVA: 0x001A0B4E File Offset: 0x0019ED4E
	protected override string getPowerTabAssetID()
	{
		return "selected_subspecies";
	}

	// Token: 0x06003B67 RID: 15207 RVA: 0x001A0B55 File Offset: 0x0019ED55
	protected override void Awake()
	{
		base.Awake();
		this._container_traits_birth = base.GetComponentInChildren<SubspeciesSelectedContainerBirthTraits>();
	}

	// Token: 0x06003B68 RID: 15208 RVA: 0x001A0B69 File Offset: 0x0019ED69
	protected override void updateTraits()
	{
		base.updateTraits();
		if (this._container_traits_birth == null)
		{
			return;
		}
		this._container_traits_birth.update(this.nano_object);
	}

	// Token: 0x06003B69 RID: 15209 RVA: 0x001A0B94 File Offset: 0x0019ED94
	protected override void showStatsGeneral(Subspecies pSubspecies)
	{
		base.showStatsGeneral(pSubspecies);
		base.setIconValue("i_kingdoms", (float)pSubspecies.countMainKingdoms(), null, "", false, "", '/');
		base.setIconValue("i_villages", (float)pSubspecies.countMainCities(), null, "", false, "", '/');
	}

	// Token: 0x06003B6A RID: 15210 RVA: 0x001A0BF8 File Offset: 0x0019EDF8
	public void openBirthTraitsTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("BirthTraitsEditor");
	}

	// Token: 0x06003B6B RID: 15211 RVA: 0x001A0C19 File Offset: 0x0019EE19
	public void openGeneticsTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("Genetics");
	}

	// Token: 0x06003B6C RID: 15212 RVA: 0x001A0C3A File Offset: 0x0019EE3A
	protected override void updateElementsOnChange(Subspecies pNano)
	{
		base.updateElementsOnChange(pNano);
		this._banners_cities_kingdoms.update(pNano);
	}

	// Token: 0x06003B6D RID: 15213 RVA: 0x001A0C4F File Offset: 0x0019EE4F
	protected override void checkAchievements(Subspecies pNano)
	{
		AchievementLibrary.checkSubspeciesAchievements(pNano);
	}

	// Token: 0x04002BA8 RID: 11176
	[SerializeField]
	private CitiesKingdomsContainersController _banners_cities_kingdoms;

	// Token: 0x04002BA9 RID: 11177
	private SubspeciesSelectedContainerBirthTraits _container_traits_birth;
}
