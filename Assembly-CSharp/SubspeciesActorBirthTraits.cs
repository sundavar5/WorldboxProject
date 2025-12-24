using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x020002E0 RID: 736
public class SubspeciesActorBirthTraits : ITraitsOwner<ActorTrait>
{
	// Token: 0x06001BBB RID: 7099 RVA: 0x000FD954 File Offset: 0x000FBB54
	public void init(ActorAsset pActorAsset, Subspecies pSubspecies)
	{
		this._asset = pActorAsset;
		this.setSubspecies(pSubspecies);
		if (this._asset.traits != null)
		{
			foreach (string tTraitId in this._asset.traits)
			{
				this.addTrait(tTraitId, false);
			}
		}
		if (WorldLawLibrary.world_law_mutant_box.isEnabled())
		{
			int tAmount = Randy.randomInt(1, 4);
			for (int i = 0; i < tAmount; i++)
			{
				ActorTrait tTrait = AssetManager.traits.pot_traits_mutation_box.GetRandom<ActorTrait>();
				if (tTrait.isAvailable())
				{
					this.addTrait(tTrait, true);
				}
			}
		}
	}

	// Token: 0x06001BBC RID: 7100 RVA: 0x000FDA10 File Offset: 0x000FBC10
	public void reset()
	{
		this._traits.Clear();
	}

	// Token: 0x06001BBD RID: 7101 RVA: 0x000FDA1D File Offset: 0x000FBC1D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public IReadOnlyCollection<ActorTrait> getTraits()
	{
		return this._traits;
	}

	// Token: 0x06001BBE RID: 7102 RVA: 0x000FDA25 File Offset: 0x000FBC25
	public bool hasTraits()
	{
		return this._traits.Count > 0;
	}

	// Token: 0x06001BBF RID: 7103 RVA: 0x000FDA35 File Offset: 0x000FBC35
	public List<string> getTraitsAsStrings()
	{
		return Toolbox.getListForSave<ActorTrait>(this._traits);
	}

	// Token: 0x06001BC0 RID: 7104 RVA: 0x000FDA42 File Offset: 0x000FBC42
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool hasTrait(ActorTrait pTrait)
	{
		return this._traits.Contains(pTrait);
	}

	// Token: 0x06001BC1 RID: 7105 RVA: 0x000FDA50 File Offset: 0x000FBC50
	internal bool hasOppositeTrait(ActorTrait pTrait)
	{
		return pTrait.hasOppositeTrait(this._traits);
	}

	// Token: 0x06001BC2 RID: 7106 RVA: 0x000FDA60 File Offset: 0x000FBC60
	public bool addTrait(string pTraitID, bool pRemoveOpposites = false)
	{
		ActorTrait tTrait = AssetManager.traits.get(pTraitID);
		return tTrait != null && this.addTrait(tTrait, pRemoveOpposites);
	}

	// Token: 0x06001BC3 RID: 7107 RVA: 0x000FDA88 File Offset: 0x000FBC88
	public bool addTrait(ActorTrait pTrait, bool pRemoveOpposites = false)
	{
		if (this.hasTrait(pTrait))
		{
			return false;
		}
		if (pTrait.traits_to_remove != null)
		{
			this.removeTraits(pTrait.traits_to_remove);
		}
		if (pRemoveOpposites)
		{
			this.removeOppositeTraits(pTrait);
		}
		else if (this.hasOppositeTrait(pTrait))
		{
			return false;
		}
		this._traits.Add(pTrait);
		return true;
	}

	// Token: 0x06001BC4 RID: 7108 RVA: 0x000FDAD9 File Offset: 0x000FBCD9
	public bool removeTrait(ActorTrait pTrait)
	{
		return this._traits.Remove(pTrait);
	}

	// Token: 0x06001BC5 RID: 7109 RVA: 0x000FDAE8 File Offset: 0x000FBCE8
	public void removeTraits(ICollection<ActorTrait> pTraits)
	{
		foreach (ActorTrait tTrait in pTraits)
		{
			this._traits.Remove(tTrait);
		}
	}

	// Token: 0x06001BC6 RID: 7110 RVA: 0x000FDB38 File Offset: 0x000FBD38
	private void removeOppositeTraits(ActorTrait pTrait)
	{
		if (!pTrait.hasOppositeTraits<ActorTrait>())
		{
			return;
		}
		this.removeTraits(pTrait.opposite_traits);
	}

	// Token: 0x06001BC7 RID: 7111 RVA: 0x000FDB50 File Offset: 0x000FBD50
	public void sortTraits(IReadOnlyCollection<ActorTrait> pTraits)
	{
		if (!this._traits.SetEquals(pTraits))
		{
			return;
		}
		this._traits.Clear();
		foreach (ActorTrait tTrait in pTraits)
		{
			this._traits.Add(tTrait);
		}
	}

	// Token: 0x06001BC8 RID: 7112 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
	public void traitModifiedEvent()
	{
	}

	// Token: 0x06001BC9 RID: 7113 RVA: 0x000FDBBC File Offset: 0x000FBDBC
	public void fillTraitAssetsFromStringList(IEnumerable<string> pList)
	{
		this._traits.Clear();
		if (pList == null)
		{
			return;
		}
		foreach (string tID in pList)
		{
			ActorTrait tTrait = AssetManager.traits.get(tID);
			if (tTrait != null)
			{
				this._traits.Add(tTrait);
			}
		}
	}

	// Token: 0x06001BCA RID: 7114 RVA: 0x000FDC28 File Offset: 0x000FBE28
	public ActorAsset getActorAsset()
	{
		return this._asset;
	}

	// Token: 0x06001BCB RID: 7115 RVA: 0x000FDC30 File Offset: 0x000FBE30
	public void setSubspecies(Subspecies pSubspecies)
	{
		this._subspecies = pSubspecies;
	}

	// Token: 0x0400154D RID: 5453
	private ActorAsset _asset;

	// Token: 0x0400154E RID: 5454
	private readonly HashSet<ActorTrait> _traits = new HashSet<ActorTrait>();

	// Token: 0x0400154F RID: 5455
	private Subspecies _subspecies;
}
