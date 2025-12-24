using System;
using System.Collections.Generic;
using db;
using db.tables;
using UnityEngine;

// Token: 0x0200022D RID: 557
public class WorldObject : NanoObject, IMetaObject, ICoreObject
{
	// Token: 0x17000129 RID: 297
	// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000DC756 File Offset: 0x000DA956
	private HistoryMetaDataAsset _history_meta_data_asset
	{
		get
		{
			return AssetManager.history_meta_data_library.get("world");
		}
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000DC767 File Offset: 0x000DA967
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.World;
		}
	}

	// Token: 0x060014F4 RID: 5364 RVA: 0x000DC76B File Offset: 0x000DA96B
	public override long getID()
	{
		return 1L;
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000DC76F File Offset: 0x000DA96F
	// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000DC780 File Offset: 0x000DA980
	public override string name
	{
		get
		{
			return World.world.map_stats.name;
		}
		protected set
		{
			World.world.map_stats.name = value;
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000DC792 File Offset: 0x000DA992
	public List<Actor> units
	{
		get
		{
			return World.world.units.getSimpleList();
		}
	}

	// Token: 0x060014F8 RID: 5368 RVA: 0x000DC7A3 File Offset: 0x000DA9A3
	public int countUnits()
	{
		return World.world.units.Count;
	}

	// Token: 0x060014F9 RID: 5369 RVA: 0x000DC7B4 File Offset: 0x000DA9B4
	public IEnumerable<Actor> getUnits()
	{
		return World.world.units;
	}

	// Token: 0x060014FA RID: 5370 RVA: 0x000DC7C0 File Offset: 0x000DA9C0
	public bool hasUnits()
	{
		return World.world.units.Count > 0;
	}

	// Token: 0x060014FB RID: 5371 RVA: 0x000DC7D4 File Offset: 0x000DA9D4
	public Actor getRandomUnit()
	{
		return World.world.units.GetRandom();
	}

	// Token: 0x060014FC RID: 5372 RVA: 0x000DC7E5 File Offset: 0x000DA9E5
	public Actor getRandomActorForReaper()
	{
		return null;
	}

	// Token: 0x060014FD RID: 5373 RVA: 0x000DC7E8 File Offset: 0x000DA9E8
	public IEnumerable<Family> getFamilies()
	{
		return World.world.families;
	}

	// Token: 0x060014FE RID: 5374 RVA: 0x000DC7F4 File Offset: 0x000DA9F4
	public int countFamilies()
	{
		return World.world.families.Count;
	}

	// Token: 0x060014FF RID: 5375 RVA: 0x000DC805 File Offset: 0x000DAA05
	public bool hasFamilies()
	{
		return World.world.families.Count > 0;
	}

	// Token: 0x06001500 RID: 5376 RVA: 0x000DC819 File Offset: 0x000DAA19
	public override ColorAsset getColor()
	{
		return AssetManager.kingdom_colors_library.list.GetRandom<ColorAsset>();
	}

	// Token: 0x06001501 RID: 5377 RVA: 0x000DC82A File Offset: 0x000DAA2A
	public MetaObjectData getMetaData()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001502 RID: 5378 RVA: 0x000DC831 File Offset: 0x000DAA31
	public int getRenown()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001503 RID: 5379 RVA: 0x000DC838 File Offset: 0x000DAA38
	public int getPopulationPeople()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001504 RID: 5380 RVA: 0x000DC83F File Offset: 0x000DAA3F
	public long getTotalKills()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001505 RID: 5381 RVA: 0x000DC846 File Offset: 0x000DAA46
	public long getTotalDeaths()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001506 RID: 5382 RVA: 0x000DC84D File Offset: 0x000DAA4D
	public bool isSelected()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001507 RID: 5383 RVA: 0x000DC854 File Offset: 0x000DAA54
	public Actor getOldestVisibleUnit()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001508 RID: 5384 RVA: 0x000DC85B File Offset: 0x000DAA5B
	public Actor getOldestVisibleUnitForNameplatesCached()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001509 RID: 5385 RVA: 0x000DC864 File Offset: 0x000DAA64
	public void startCollectHistoryData()
	{
		Delegate[] invocationList = this._history_meta_data_asset.collector.GetInvocationList();
		for (int i = 0; i < invocationList.Length; i++)
		{
			HistoryTable historyTable = ((HistoryDataCollector)invocationList[i])(this);
			historyTable.timestamp = (long)World.world.map_stats.history_current_year;
			DBInserter.insertData(historyTable, "world");
		}
	}

	// Token: 0x0600150A RID: 5386 RVA: 0x000DC8BE File Offset: 0x000DAABE
	public void clearLastYearStats()
	{
	}

	// Token: 0x0600150B RID: 5387 RVA: 0x000DC8C0 File Offset: 0x000DAAC0
	public ActorAsset getActorAsset()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600150C RID: 5388 RVA: 0x000DC8C7 File Offset: 0x000DAAC7
	public Sprite getSpriteIcon()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600150D RID: 5389 RVA: 0x000DC8CE File Offset: 0x000DAACE
	public bool isCursorOver()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600150E RID: 5390 RVA: 0x000DC8D5 File Offset: 0x000DAAD5
	public void setCursorOver()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600150F RID: 5391 RVA: 0x000DC8DC File Offset: 0x000DAADC
	public int getAge()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001510 RID: 5392 RVA: 0x000DC8E3 File Offset: 0x000DAAE3
	public bool isFavorite()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001511 RID: 5393 RVA: 0x000DC8EA File Offset: 0x000DAAEA
	public void switchFavorite()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001512 RID: 5394 RVA: 0x000DC8F1 File Offset: 0x000DAAF1
	public bool hasCities()
	{
		return World.world.cities.Count > 0;
	}

	// Token: 0x06001513 RID: 5395 RVA: 0x000DC905 File Offset: 0x000DAB05
	public IEnumerable<City> getCities()
	{
		return World.world.cities;
	}

	// Token: 0x06001514 RID: 5396 RVA: 0x000DC911 File Offset: 0x000DAB11
	public bool hasKingdoms()
	{
		return World.world.kingdoms.Count > 0;
	}

	// Token: 0x06001515 RID: 5397 RVA: 0x000DC925 File Offset: 0x000DAB25
	public IEnumerable<Kingdom> getKingdoms()
	{
		return World.world.kingdoms;
	}

	// Token: 0x040011DC RID: 4572
	protected static readonly HashSet<Family> _family_counter = new HashSet<Family>();
}
