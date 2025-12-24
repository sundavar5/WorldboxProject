using System;

// Token: 0x0200016B RID: 363
public class SimGlobals : AssetLibrary<SimGlobalAsset>
{
	// Token: 0x06000B0B RID: 2827 RVA: 0x000A1769 File Offset: 0x0009F969
	public override void init()
	{
		base.init();
		SimGlobals.m = this.add(new SimGlobalAsset
		{
			id = "main"
		});
	}

	// Token: 0x04000AB5 RID: 2741
	public static SimGlobalAsset m;
}
