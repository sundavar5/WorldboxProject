using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

// Token: 0x02000418 RID: 1048
public class ChunkObjectContainer : IDisposable
{
	// Token: 0x170001F7 RID: 503
	// (get) Token: 0x06002411 RID: 9233 RVA: 0x0012CB57 File Offset: 0x0012AD57
	public int total_units
	{
		get
		{
			return this._total_units;
		}
	}

	// Token: 0x170001F8 RID: 504
	// (get) Token: 0x06002412 RID: 9234 RVA: 0x0012CB5F File Offset: 0x0012AD5F
	public int total_buildings
	{
		get
		{
			return this._total_buildings;
		}
	}

	// Token: 0x06002413 RID: 9235 RVA: 0x0012CB68 File Offset: 0x0012AD68
	public void reset(bool pClearBuildings)
	{
		if (this._total_units == 0 && this._total_buildings == 0)
		{
			return;
		}
		if (this._total_units == 0 && !pClearBuildings)
		{
			return;
		}
		foreach (List<Actor> list in this._dict_units.Values)
		{
			list.Clear();
		}
		this.units_all.Clear();
		this._total_units = 0;
		this.kingdoms.Clear();
		this._hash_kingdoms.Clear();
		if (pClearBuildings)
		{
			this.buildings_all.Clear();
			this._total_buildings = 0;
			using (Dictionary<long, List<Building>>.ValueCollection.Enumerator enumerator2 = this._dict_buildings.Values.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					List<Building> list2 = enumerator2.Current;
					list2.Clear();
				}
				return;
			}
		}
		if (this._dict_buildings.Count > 0)
		{
			foreach (long tKingdomID in this._dict_buildings.Keys)
			{
				this.kingdoms.Add(tKingdomID);
			}
			this._hash_kingdoms.UnionWith(this.kingdoms);
		}
	}

	// Token: 0x06002414 RID: 9236 RVA: 0x0012CCC8 File Offset: 0x0012AEC8
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public List<Building> getBuildings(long pKingdom)
	{
		return this._dict_buildings[pKingdom];
	}

	// Token: 0x06002415 RID: 9237 RVA: 0x0012CCD6 File Offset: 0x0012AED6
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public List<Actor> getUnits(long pKingdom)
	{
		return this._dict_units[pKingdom];
	}

	// Token: 0x06002416 RID: 9238 RVA: 0x0012CCE4 File Offset: 0x0012AEE4
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isEmpty()
	{
		return this.kingdoms.Count == 0;
	}

	// Token: 0x06002417 RID: 9239 RVA: 0x0012CCF4 File Offset: 0x0012AEF4
	public void addActor(Actor pActor)
	{
		long tKingdomId = pActor.kingdom.id;
		if (this._hash_kingdoms.Add(tKingdomId))
		{
			List<Actor> tListActors;
			if (!this._dict_units.TryGetValue(tKingdomId, out tListActors))
			{
				tListActors = new List<Actor>();
				this._dict_units[tKingdomId] = tListActors;
				this._dict_buildings[tKingdomId] = new List<Building>();
			}
			tListActors.Add(pActor);
			this.kingdoms.Add(tKingdomId);
			this._total_units++;
		}
		else
		{
			this._dict_units[tKingdomId].Add(pActor);
			this._total_units++;
		}
		this.units_all.Add(pActor);
	}

	// Token: 0x06002418 RID: 9240 RVA: 0x0012CDA0 File Offset: 0x0012AFA0
	public void addBuilding(Building pBuilding)
	{
		long tKingdomId = pBuilding.kingdom.id;
		if (this._hash_kingdoms.Add(tKingdomId))
		{
			List<Building> tListBuildings;
			if (!this._dict_buildings.TryGetValue(tKingdomId, out tListBuildings))
			{
				tListBuildings = new List<Building>();
				this._dict_buildings[tKingdomId] = tListBuildings;
				this._dict_units[tKingdomId] = new List<Actor>();
			}
			tListBuildings.Add(pBuilding);
			this._total_buildings++;
			this.kingdoms.Add(tKingdomId);
		}
		else
		{
			this._dict_buildings[tKingdomId].Add(pBuilding);
			this._total_buildings++;
		}
		this.buildings_all.Add(pBuilding);
	}

	// Token: 0x06002419 RID: 9241 RVA: 0x0012CE4C File Offset: 0x0012B04C
	public void Dispose()
	{
		this.reset(true);
		this._dict_units.Clear();
		this._dict_buildings.Clear();
		this.units_all.Clear();
		this.buildings_all.Clear();
		this._total_units = 0;
		this._total_buildings = 0;
	}

	// Token: 0x0600241A RID: 9242 RVA: 0x0012CE9A File Offset: 0x0012B09A
	public Dictionary<long, List<Building>>.ValueCollection getDebugBuildings()
	{
		return this._dict_buildings.Values;
	}

	// Token: 0x0600241B RID: 9243 RVA: 0x0012CEA7 File Offset: 0x0012B0A7
	public Dictionary<long, List<Actor>>.ValueCollection getDebugUnits()
	{
		return this._dict_units.Values;
	}

	// Token: 0x040019FA RID: 6650
	public readonly List<long> kingdoms = new List<long>();

	// Token: 0x040019FB RID: 6651
	public readonly List<Actor> units_all = new List<Actor>();

	// Token: 0x040019FC RID: 6652
	public readonly List<Building> buildings_all = new List<Building>();

	// Token: 0x040019FD RID: 6653
	private readonly HashSet<long> _hash_kingdoms = new HashSet<long>();

	// Token: 0x040019FE RID: 6654
	private readonly Dictionary<long, List<Actor>> _dict_units = new Dictionary<long, List<Actor>>();

	// Token: 0x040019FF RID: 6655
	private readonly Dictionary<long, List<Building>> _dict_buildings = new Dictionary<long, List<Building>>();

	// Token: 0x04001A00 RID: 6656
	private int _total_units;

	// Token: 0x04001A01 RID: 6657
	private int _total_buildings;
}
