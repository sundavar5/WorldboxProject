using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004EB RID: 1259
public class TesterBehSpawnRandomUnit : BehaviourActionTester
{
	// Token: 0x06002A2C RID: 10796 RVA: 0x0014B8C0 File Offset: 0x00149AC0
	public TesterBehSpawnRandomUnit(int pAmount = 1, string pLocation = "random")
	{
		this._amount = pAmount;
		this._location = pLocation;
		this.filter_delegate = ((ActorAsset pActorAsset) => !pActorAsset.isTemplateAsset() && pActorAsset.has_ai_system && !pActorAsset.is_boat && !pActorAsset.unit_other && !pActorAsset.special && !pActorAsset.id.Contains("zombie"));
	}

	// Token: 0x06002A2D RID: 10797 RVA: 0x0014B8FC File Offset: 0x00149AFC
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (this.assets == null)
		{
			using (ListPool<string> tAssets = new ListPool<string>())
			{
				foreach (ActorAsset tActorAsset in AssetManager.actor_library.list)
				{
					if (this.filter_delegate(tActorAsset))
					{
						tAssets.Add(tActorAsset.id);
					}
				}
				tAssets.Shuffle<string>();
				this.assets = tAssets.ToArray<string>();
			}
		}
		string tAsset = this.assets.GetRandom<string>();
		TileZone tZone = BehaviourActionBase<AutoTesterBot>.world.zone_calculator.zones.GetRandom<TileZone>();
		for (int i = 0; i < this._amount; i++)
		{
			WorldTile tTile;
			if (this._location == "tile_target" && pObject.beh_tile_target != null)
			{
				tTile = pObject.beh_tile_target;
			}
			else
			{
				tTile = tZone.tiles.GetRandom<WorldTile>();
			}
			Actor tActor = BehaviourActionBase<AutoTesterBot>.world.units.spawnNewUnit(tAsset, tTile, false, true, 6f, null, false, false);
			if (tActor == null)
			{
				Debug.LogError("could not spawn " + tAsset);
			}
			else if (tAsset == "printer")
			{
				tActor.data.set("template", TesterBehSpawnRandomUnit.printers.GetRandom<string>());
			}
		}
		return base.execute(pObject);
	}

	// Token: 0x04001F6F RID: 8047
	private string[] assets;

	// Token: 0x04001F70 RID: 8048
	private int _amount;

	// Token: 0x04001F71 RID: 8049
	private string _location;

	// Token: 0x04001F72 RID: 8050
	public ActorAssetFilter filter_delegate;

	// Token: 0x04001F73 RID: 8051
	private static readonly List<string> printers = new List<string>
	{
		"hexagon",
		"skull",
		"squares",
		"yinyang",
		"island1",
		"star",
		"heart",
		"diamond",
		"aliendrawing",
		"crater",
		"labyrinth",
		"spiral",
		"starfort",
		"code"
	};
}
