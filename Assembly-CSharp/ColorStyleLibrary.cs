using System;

// Token: 0x020000CA RID: 202
public class ColorStyleLibrary : AssetLibrary<ColorStyleAsset>
{
	// Token: 0x06000623 RID: 1571 RVA: 0x0005EA2F File Offset: 0x0005CC2F
	public override void init()
	{
		base.init();
		ColorStyleLibrary.m = this.add(new ColorStyleAsset
		{
			id = "main"
		});
	}

	// Token: 0x04000708 RID: 1800
	public static ColorStyleAsset m;
}
