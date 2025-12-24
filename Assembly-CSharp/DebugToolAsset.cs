using System;

// Token: 0x020000DC RID: 220
[Serializable]
public class DebugToolAsset : Asset
{
	// Token: 0x06000676 RID: 1654 RVA: 0x000605BB File Offset: 0x0005E7BB
	public void showForMaxim()
	{
		if (!Config.editor_maxim)
		{
			return;
		}
		this.show_on_start = Config.editor_maxim;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x000605D0 File Offset: 0x0005E7D0
	public void showForMastef()
	{
		if (!Config.editor_mastef)
		{
			return;
		}
		this.show_on_start = Config.editor_mastef;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x000605E5 File Offset: 0x0005E7E5
	public void showForNikon()
	{
		if (!Config.editor_nikon)
		{
			return;
		}
		this.show_on_start = Config.editor_nikon;
	}

	// Token: 0x04000738 RID: 1848
	public string name;

	// Token: 0x04000739 RID: 1849
	public string benchmark_group_id = string.Empty;

	// Token: 0x0400073A RID: 1850
	public string benchmark_total = string.Empty;

	// Token: 0x0400073B RID: 1851
	public string benchmark_total_group = string.Empty;

	// Token: 0x0400073C RID: 1852
	public string path_icon;

	// Token: 0x0400073D RID: 1853
	public int priority;

	// Token: 0x0400073E RID: 1854
	public DebugToolType type;

	// Token: 0x0400073F RID: 1855
	public DebugToolAssetAction action_1;

	// Token: 0x04000740 RID: 1856
	public DebugToolAssetAction action_2;

	// Token: 0x04000741 RID: 1857
	public DebugToolAssetAction action_start;

	// Token: 0x04000742 RID: 1858
	public DebugToolUpdateDelegate action_update;

	// Token: 0x04000743 RID: 1859
	public float update_timeout = 0.1f;

	// Token: 0x04000744 RID: 1860
	public bool show_on_start;

	// Token: 0x04000745 RID: 1861
	public bool show_benchmark_buttons;

	// Token: 0x04000746 RID: 1862
	public bool split_benchmark;

	// Token: 0x04000747 RID: 1863
	public bool show_last_count;
}
