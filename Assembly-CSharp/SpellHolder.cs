using System;
using System.Collections.Generic;

// Token: 0x0200016F RID: 367
public class SpellHolder
{
	// Token: 0x06000B10 RID: 2832 RVA: 0x000A17DA File Offset: 0x0009F9DA
	public bool hasAny()
	{
		return this._spells.Count > 0;
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x000A17EA File Offset: 0x0009F9EA
	public SpellAsset getRandomSpell()
	{
		return this._spells.GetRandom<SpellAsset>();
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x000A17F7 File Offset: 0x0009F9F7
	public bool hasSpell(SpellAsset pSpell)
	{
		return this._spells_hashset.Contains(pSpell);
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x000A1805 File Offset: 0x0009FA05
	public void reset()
	{
		this._spells.Clear();
		this._spells_hashset.Clear();
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000A181D File Offset: 0x0009FA1D
	public void mergeWith(SpellHolder pListSpells)
	{
		this.mergeWith(pListSpells.spells);
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x000A182B File Offset: 0x0009FA2B
	public void mergeWith(IReadOnlyList<SpellAsset> pSpells)
	{
		this._spells.AddRange(pSpells);
		this._spells_hashset.UnionWith(pSpells);
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x000A1848 File Offset: 0x0009FA48
	public void mergeWith(List<string> pSpellIDs)
	{
		foreach (string tId in pSpellIDs)
		{
			SpellAsset tAsset = AssetManager.spells.get(tId);
			if (tAsset != null)
			{
				this.addSpell(tAsset);
			}
		}
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x000A18A8 File Offset: 0x0009FAA8
	public void addSpell(SpellAsset pSpell)
	{
		this._spells.Add(pSpell);
		this._spells_hashset.Add(pSpell);
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x000A18C3 File Offset: 0x0009FAC3
	public IReadOnlyCollection<SpellAsset> getSpellsHashset()
	{
		return this._spells_hashset;
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000B19 RID: 2841 RVA: 0x000A18CB File Offset: 0x0009FACB
	public IReadOnlyList<SpellAsset> spells
	{
		get
		{
			return this._spells;
		}
	}

	// Token: 0x04000ACA RID: 2762
	private List<SpellAsset> _spells = new List<SpellAsset>();

	// Token: 0x04000ACB RID: 2763
	private HashSet<SpellAsset> _spells_hashset = new HashSet<SpellAsset>();
}
