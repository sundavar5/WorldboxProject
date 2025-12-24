using System;
using System.ComponentModel;

// Token: 0x0200009A RID: 154
[Serializable]
public class WorldTimeScaleAsset : Asset, ILocalizedAsset
{
	// Token: 0x060004CE RID: 1230 RVA: 0x0003311A File Offset: 0x0003131A
	public string getLocaleID()
	{
		return this.locale_key;
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x00033124 File Offset: 0x00031324
	public WorldTimeScaleAsset getNext(bool pCycle = false)
	{
		int tMaxIndex = AssetManager.time_scales.list.Count - 2;
		if (DebugConfig.debug_enabled)
		{
			tMaxIndex = AssetManager.time_scales.list.Count - 1;
		}
		int tIndex = AssetManager.time_scales.list.IndexOf(this);
		if (++tIndex > tMaxIndex)
		{
			if (!pCycle)
			{
				return this;
			}
			tIndex = 0;
		}
		return AssetManager.time_scales.list[tIndex];
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0003318C File Offset: 0x0003138C
	public WorldTimeScaleAsset getPrevious(bool pCycle = false)
	{
		int tIndex = AssetManager.time_scales.list.IndexOf(this);
		if (--tIndex < 0)
		{
			if (!pCycle)
			{
				return this;
			}
			tIndex = AssetManager.time_scales.list.Count - 1;
		}
		return AssetManager.time_scales.list[tIndex];
	}

	// Token: 0x04000509 RID: 1289
	public float multiplier;

	// Token: 0x0400050A RID: 1290
	[DefaultValue(1)]
	public int ticks = 1;

	// Token: 0x0400050B RID: 1291
	public int conway_ticks;

	// Token: 0x0400050C RID: 1292
	public bool sonic;

	// Token: 0x0400050D RID: 1293
	public bool render_skip;

	// Token: 0x0400050E RID: 1294
	public string path_icon;

	// Token: 0x0400050F RID: 1295
	public string locale_key;
}
