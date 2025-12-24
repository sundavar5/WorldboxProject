using System;

// Token: 0x0200065B RID: 1627
public class CitySelectedResources : UICityResources
{
	// Token: 0x060034C0 RID: 13504 RVA: 0x00186A12 File Offset: 0x00184C12
	public void update(City pCity)
	{
		this.meta_object = pCity;
		base.showResources();
	}

	// Token: 0x060034C1 RID: 13505 RVA: 0x00186A21 File Offset: 0x00184C21
	protected override void OnEnable()
	{
	}

	// Token: 0x060034C2 RID: 13506 RVA: 0x00186A23 File Offset: 0x00184C23
	protected override void onListChange()
	{
		if (base.city == null)
		{
			return;
		}
		base.onListChange();
	}
}
