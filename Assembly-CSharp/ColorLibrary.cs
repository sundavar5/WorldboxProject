using System;
using System.Collections.Generic;

// Token: 0x02000038 RID: 56
public class ColorLibrary : AssetLibrary<ColorAsset>
{
	// Token: 0x06000260 RID: 608 RVA: 0x000159A8 File Offset: 0x00013BA8
	public override void post_init()
	{
		base.post_init();
		foreach (ColorAsset colorAsset in this.list)
		{
			colorAsset.initColor();
		}
	}

	// Token: 0x06000261 RID: 609 RVA: 0x00015A00 File Offset: 0x00013C00
	public ColorAsset getColorByIndex(int pIndex)
	{
		if (pIndex >= this.list.Count)
		{
			return this.list[0];
		}
		return this.list[pIndex];
	}

	// Token: 0x06000262 RID: 610 RVA: 0x00015A2C File Offset: 0x00013C2C
	public ColorAsset getNextColor(ActorAsset pActorAsset)
	{
		this._free_colors_bonus.Clear();
		this._free_colors_main.Clear();
		this._free_colors_preferred.Clear();
		for (int i = 0; i < this.list.Count; i++)
		{
			ColorAsset tColorAsset = this.list[i];
			if (!this.isColorUsedInWorld(tColorAsset))
			{
				if (pActorAsset != null && pActorAsset.preferred_colors != null && pActorAsset.preferred_colors.Contains(tColorAsset.id))
				{
					this._free_colors_preferred.Add(tColorAsset);
				}
				if (tColorAsset.favorite)
				{
					this._free_colors_main.Add(tColorAsset);
				}
				else
				{
					this._free_colors_bonus.Add(tColorAsset);
				}
			}
		}
		if (this._free_colors_preferred.Count > 0)
		{
			return this._free_colors_preferred.GetRandom<ColorAsset>();
		}
		if (this._free_colors_main.Count > 0)
		{
			return this._free_colors_main.GetRandom<ColorAsset>();
		}
		if (this._free_colors_bonus.Count > 0)
		{
			return this._free_colors_bonus.GetRandom<ColorAsset>();
		}
		return this.list.GetRandom<ColorAsset>();
	}

	// Token: 0x06000263 RID: 611 RVA: 0x00015B2C File Offset: 0x00013D2C
	public int getNextColorIndex(ActorAsset pActorAsset)
	{
		ColorAsset tAsset = this.getNextColor(pActorAsset);
		return this.list.IndexOf(tAsset);
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00015B4D File Offset: 0x00013D4D
	public virtual bool isColorUsedInWorld(ColorAsset pAsset)
	{
		return false;
	}

	// Token: 0x06000265 RID: 613 RVA: 0x00015B50 File Offset: 0x00013D50
	protected bool checkColor(ColorAsset pAsset, int pColorIndex)
	{
		return pColorIndex == pAsset.index_id;
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00015B5E File Offset: 0x00013D5E
	public override ColorAsset add(ColorAsset pAsset)
	{
		ColorAsset colorAsset = base.add(pAsset);
		ColorAsset.saveToGlobalList(colorAsset, this.must_be_global);
		return colorAsset;
	}

	// Token: 0x06000267 RID: 615 RVA: 0x00015B73 File Offset: 0x00013D73
	public void useSameColorsFrom(ColorLibrary pSource)
	{
		this.list = pSource.list;
		this.dict = pSource.dict;
	}

	// Token: 0x0400021B RID: 539
	private readonly List<ColorAsset> _free_colors_main = new List<ColorAsset>();

	// Token: 0x0400021C RID: 540
	private readonly List<ColorAsset> _free_colors_bonus = new List<ColorAsset>();

	// Token: 0x0400021D RID: 541
	private readonly List<ColorAsset> _free_colors_preferred = new List<ColorAsset>();

	// Token: 0x0400021E RID: 542
	internal bool must_be_global;
}
