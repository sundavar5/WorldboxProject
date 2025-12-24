using System;
using UnityEngine;

// Token: 0x0200074A RID: 1866
public class KingdomSelectedContainerCities : SelectedElementBase<CityBanner>
{
	// Token: 0x06003B0E RID: 15118 RVA: 0x0019FD1A File Offset: 0x0019DF1A
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<CityBanner>(this._banner_prefab, this._grid);
		this._grid.gameObject.AddOrGetComponent<TraitsGrid>();
	}

	// Token: 0x06003B0F RID: 15119 RVA: 0x0019FD44 File Offset: 0x0019DF44
	public void update(NanoObject pNano)
	{
		this.refresh(pNano);
	}

	// Token: 0x06003B10 RID: 15120 RVA: 0x0019FD50 File Offset: 0x0019DF50
	protected override void refresh(NanoObject pNano)
	{
		base.clear();
		foreach (City tCity in ((Kingdom)pNano).getCities())
		{
			this.addBanner(tCity);
		}
	}

	// Token: 0x06003B11 RID: 15121 RVA: 0x0019FDA8 File Offset: 0x0019DFA8
	private void addBanner(City pCity)
	{
		this._pool.getNext().load(pCity);
	}

	// Token: 0x04002B8F RID: 11151
	[SerializeField]
	private CityBanner _banner_prefab;
}
