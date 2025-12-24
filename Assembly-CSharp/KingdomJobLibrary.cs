using System;

// Token: 0x020003ED RID: 1005
public class KingdomJobLibrary : AssetLibrary<KingdomJob>
{
	// Token: 0x060022E6 RID: 8934 RVA: 0x00123444 File Offset: 0x00121644
	public override void init()
	{
		base.init();
		this.add(new KingdomJob
		{
			id = "kingdom"
		});
		this.t.addTask("do_checks");
		this.t.addTask("wait1");
		this.t.addTask("wait1");
	}
}
