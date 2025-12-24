using System;
using System.Collections.Generic;

// Token: 0x0200011D RID: 285
[Serializable]
public class KingdomTrait : BaseTrait<KingdomTrait>
{
	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0008110B File Offset: 0x0007F30B
	protected override HashSet<string> progress_elements
	{
		get
		{
			GameProgressData progress_data = base._progress_data;
			if (progress_data == null)
			{
				return null;
			}
			return progress_data.unlocked_traits_kingdom;
		}
	}

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0008111E File Offset: 0x0007F31E
	public override string typed_id
	{
		get
		{
			return "kingdom_trait";
		}
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00081125 File Offset: 0x0007F325
	protected override IEnumerable<ITraitsOwner<KingdomTrait>> getRelatedMetaList()
	{
		return World.world.kingdoms;
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00081131 File Offset: 0x0007F331
	public override BaseCategoryAsset getGroup()
	{
		return AssetManager.kingdoms_traits_groups.get(this.group_id);
	}

	// Token: 0x04000929 RID: 2345
	public bool is_local_tax_trait;

	// Token: 0x0400092A RID: 2346
	public bool is_tribute_tax_trait;

	// Token: 0x0400092B RID: 2347
	public float tax_rate;
}
