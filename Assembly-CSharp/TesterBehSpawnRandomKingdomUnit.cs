using System;
using ai.behaviours;

// Token: 0x020004E7 RID: 1255
public class TesterBehSpawnRandomKingdomUnit : BehaviourActionTester
{
	// Token: 0x06002A23 RID: 10787 RVA: 0x0014B6DC File Offset: 0x001498DC
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (BehaviourActionBase<AutoTesterBot>.world.kingdoms.Count == 0)
		{
			return new TesterBehSpawnRandomCivUnit(1, "random").execute(pObject);
		}
		Kingdom tKingdom = BehaviourActionBase<AutoTesterBot>.world.kingdoms.getRandom();
		if (tKingdom == null)
		{
			return BehResult.Continue;
		}
		if (!tKingdom.hasUnits())
		{
			return BehResult.Continue;
		}
		if (!tKingdom.hasCities())
		{
			return BehResult.Continue;
		}
		City tCity = tKingdom.getCities().GetRandom<City>();
		if (tCity == null)
		{
			return BehResult.Continue;
		}
		if (!tCity.hasZones())
		{
			return BehResult.Continue;
		}
		TileZone tZone = tCity.zones.GetRandom<TileZone>();
		if (tZone == null)
		{
			return BehResult.Continue;
		}
		WorldTile tTile = tZone.tiles.GetRandom<WorldTile>();
		if (tTile == null)
		{
			return BehResult.Continue;
		}
		ActorAsset tAsset = tKingdom.getActorAsset();
		BehaviourActionBase<AutoTesterBot>.world.units.spawnNewUnit(tAsset.id, tTile, false, true, 6f, null, false, false);
		return BehResult.Continue;
	}
}
