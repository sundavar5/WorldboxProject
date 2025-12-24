using System;
using System.Collections.Generic;

// Token: 0x02000363 RID: 867
internal static class Ant
{
	// Token: 0x060020D4 RID: 8404 RVA: 0x00118D6C File Offset: 0x00116F6C
	public static WorldTile getNextTile(WorldTile pTile, ActorDirection pDirection)
	{
		switch (pDirection)
		{
		case ActorDirection.Up:
			return pTile.tile_up;
		case ActorDirection.UpRight:
		{
			if (pTile == null)
			{
				return null;
			}
			WorldTile tile_up = pTile.tile_up;
			if (tile_up == null)
			{
				return null;
			}
			return tile_up.tile_right;
		}
		case ActorDirection.Right:
			return pTile.tile_right;
		case ActorDirection.UpLeft:
		{
			if (pTile == null)
			{
				return null;
			}
			WorldTile tile_up2 = pTile.tile_up;
			if (tile_up2 == null)
			{
				return null;
			}
			return tile_up2.tile_left;
		}
		case ActorDirection.Down:
			return pTile.tile_down;
		case ActorDirection.DownRight:
		{
			if (pTile == null)
			{
				return null;
			}
			WorldTile tile_down = pTile.tile_down;
			if (tile_down == null)
			{
				return null;
			}
			return tile_down.tile_right;
		}
		case ActorDirection.DownLeft:
		{
			if (pTile == null)
			{
				return null;
			}
			WorldTile tile_down2 = pTile.tile_down;
			if (tile_down2 == null)
			{
				return null;
			}
			return tile_down2.tile_left;
		}
		case ActorDirection.Left:
			return pTile.tile_left;
		default:
			return null;
		}
	}

	// Token: 0x060020D5 RID: 8405 RVA: 0x00118E1C File Offset: 0x0011701C
	public static WorldTile randomNeighbour(WorldTile pTile)
	{
		WorldTile result;
		try
		{
			Ant._axis_neighbours.Add(pTile.tile_up);
			Ant._axis_neighbours.Add(pTile.tile_right);
			Ant._axis_neighbours.Add(pTile.tile_left);
			Ant._axis_neighbours.Add(pTile.tile_down);
			foreach (WorldTile tTile in Ant._axis_neighbours.LoopRandom<WorldTile>())
			{
				if (tTile != null)
				{
					return tTile;
				}
			}
			result = null;
		}
		finally
		{
			Ant._axis_neighbours.Clear();
		}
		return result;
	}

	// Token: 0x060020D6 RID: 8406 RVA: 0x00118ECC File Offset: 0x001170CC
	internal static void antUseOnTile(WorldTile pTile, string pType)
	{
		MapAction.terraformMain(pTile, AssetManager.tiles.get(pType), TerraformLibrary.destroy, false);
		MusicBox.playSound("event:/SFX/UNIQUE/langton/ant_step", pTile, false, false);
	}

	// Token: 0x04001842 RID: 6210
	private static List<WorldTile> _axis_neighbours = new List<WorldTile>(4);
}
