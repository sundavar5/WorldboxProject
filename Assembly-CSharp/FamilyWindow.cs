using System;
using UnityEngine.UI;

// Token: 0x02000696 RID: 1686
public class FamilyWindow : WindowMetaGeneric<Family, FamilyData>
{
	// Token: 0x17000304 RID: 772
	// (get) Token: 0x060035EE RID: 13806 RVA: 0x00189F3F File Offset: 0x0018813F
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x17000305 RID: 773
	// (get) Token: 0x060035EF RID: 13807 RVA: 0x00189F42 File Offset: 0x00188142
	protected override Family meta_object
	{
		get
		{
			return SelectedMetas.selected_family;
		}
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x00189F4C File Offset: 0x0018814C
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Family tFamily = this.meta_object;
		if (tFamily == null)
		{
			return;
		}
		ActorAsset tAsset = tFamily.getActorAsset();
		this.title_family.text = LocalizedTextManager.getText(tAsset.getCollectiveTermID(), null, false);
	}

	// Token: 0x060035F1 RID: 13809 RVA: 0x00189F8C File Offset: 0x0018818C
	internal override void showStatsRows()
	{
		Family tFamily = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tFamily.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("founder", tFamily.data.main_founder_id_1, tFamily.data.founder_actor_name_1, null, "actor_traits/iconStupid");
		if (tFamily.data.main_founder_id_2 != -1L)
		{
			base.tryToShowActor("founder", tFamily.data.main_founder_id_2, tFamily.data.founder_actor_name_2, null, "actor_traits/iconStupid");
		}
		base.tryToShowMetaKingdom("origin", tFamily.data.founder_kingdom_id, tFamily.data.founder_kingdom_name, null);
		base.tryToShowMetaCity("birthplace", tFamily.data.founder_city_id, tFamily.data.founder_city_name, null, "iconCity");
		base.tryToShowMetaSubspecies("founder_subspecies", tFamily.data.subspecies_id, tFamily.data.subspecies_name, null);
		foreach (Family tOriginFamily in tFamily.getOriginFamilies())
		{
			base.tryToShowMetaFamily("origin_family", -1L, null, tOriginFamily);
		}
		base.tryToShowMetaSpecies("founder_species", tFamily.data.species_id);
	}

	// Token: 0x0400280E RID: 10254
	public Text title_family;
}
