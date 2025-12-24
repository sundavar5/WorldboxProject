using System;

// Token: 0x020003E9 RID: 1001
public class CityJobLibrary : AssetLibrary<JobCityAsset>
{
	// Token: 0x060022DD RID: 8925 RVA: 0x001232E0 File Offset: 0x001214E0
	public override void init()
	{
		base.init();
		this.add(new JobCityAsset
		{
			id = "city"
		});
		this.t.addTask("check_army");
		this.t.addTask("wait1");
		this.t.addTask("do_checks");
		this.t.addTask("wait1");
		this.t.addTask("border_shrink");
		this.t.addTask("wait1");
		this.t.addTask("produce_boat");
		this.t.addTask("wait1");
		this.t.addTask("supply_kingdom_cities");
		this.t.addTask("wait1");
		this.t.addTask("produce_resources");
		this.t.addTask("wait1");
		this.t.addTask("check_farms");
	}
}
