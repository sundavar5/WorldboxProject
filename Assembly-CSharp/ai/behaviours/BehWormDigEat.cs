using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200090F RID: 2319
	public class BehWormDigEat : BehaviourActionActor
	{
		// Token: 0x060045A0 RID: 17824 RVA: 0x001D2EFC File Offset: 0x001D10FC
		public override BehResult execute(Actor pActor)
		{
			int wormSize;
			pActor.data.get("worm_size", out wormSize, 1);
			if (pActor.current_tile.Height < 220)
			{
				this.loopWithBrush(pActor.current_tile, wormSize, new PowerActionWithID(BehWormDigEat.tileDrawWorm), null);
			}
			BehWormDigEat.checkForWorms(pActor.current_tile, wormSize, pActor);
			return BehResult.Continue;
		}

		// Token: 0x060045A1 RID: 17825 RVA: 0x001D2F58 File Offset: 0x001D1158
		public static void checkForWorms(WorldTile pCenterTile, int pBrushSize, Actor pActor)
		{
			BrushData pBrush = Brush.get(pBrushSize, "hcirc_");
			for (int i = 0; i < pBrush.pos.Length; i++)
			{
				int tX = pCenterTile.x + pBrush.pos[i].x;
				int tY = pCenterTile.y + pBrush.pos[i].y;
				if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
				{
					WorldTile tTile = BehaviourActionBase<Actor>.world.GetTileSimple(tX, tY);
					BehWormDigEat.checkWorms(tTile, pActor);
					BehaviourActionBase<Actor>.world.flash_effects.flashPixel(tTile, 10, ColorType.Purple);
				}
			}
		}

		// Token: 0x060045A2 RID: 17826 RVA: 0x001D3004 File Offset: 0x001D1204
		public void loopWithBrush(WorldTile pCenterTile, int pBrushSize, PowerActionWithID pAction, string pPowerID = null)
		{
			BrushData pBrush = Brush.get(pBrushSize, "hcirc_");
			for (int i = 0; i < pBrush.pos.Length; i++)
			{
				int tX = pCenterTile.x + pBrush.pos[i].x;
				int tY = pCenterTile.y + pBrush.pos[i].y;
				if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
				{
					WorldTile tTile = BehaviourActionBase<Actor>.world.GetTileSimple(tX, tY);
					pAction(tTile, pPowerID);
				}
			}
		}

		// Token: 0x060045A3 RID: 17827 RVA: 0x001D3098 File Offset: 0x001D1298
		public static void checkWorms(WorldTile pTile, Actor pActor)
		{
			pTile.doUnits(delegate(Actor tActor)
			{
				if (pActor.data.id == tActor.data.id)
				{
					return;
				}
				if (tActor.asset.id == "worm")
				{
					int wormSize;
					pActor.data.get("worm_size", out wormSize, 1);
					int tWormSize;
					tActor.data.get("worm_size", out tWormSize, 1);
					tActor.dieSimpleNone();
					wormSize += tWormSize;
					pActor.data.set("worm_size", wormSize);
				}
			});
		}

		// Token: 0x060045A4 RID: 17828 RVA: 0x001D30C4 File Offset: 0x001D12C4
		public static bool tileDrawWorm(WorldTile pTile, string pPowerID)
		{
			if (pTile == null)
			{
				return false;
			}
			BehWormDig.wormTile(pTile);
			if (pTile.Type.ocean && pTile.Type.liquid && Randy.randomChance(0.25f))
			{
				BehWormDigEat.spawnBurst(pTile, "rain", false);
			}
			if (pTile.Type.lava)
			{
				LavaHelper.removeLava(pTile);
				if (Randy.randomChance(0.25f))
				{
					BehWormDigEat.spawnBurst(pTile, "lava", true);
				}
			}
			if (pTile.isOnFire())
			{
				pTile.stopFire();
			}
			if (Randy.randomChance(0.25f))
			{
				if (pTile.Type.IsType("sand"))
				{
					BehWormDigEat.spawnBurst(pTile, "pixel", false);
				}
				else if (pTile.Type.can_be_farm)
				{
					BehWormDigEat.spawnBurst(pTile, "pixel", false);
				}
			}
			return true;
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x001D3190 File Offset: 0x001D1390
		private static void spawnBurst(WorldTile pTile, string pType, bool pCreateGround = true)
		{
			if (BehaviourActionBase<Actor>.world.drop_manager.getActiveIndex() > 300)
			{
				return;
			}
			BehaviourActionBase<Actor>.world.drop_manager.spawnParabolicDrop(pTile, pType, 0f, 0.62f, 104f, 0.7f, 23.5f, -1f);
		}

		// Token: 0x040031BC RID: 12732
		private static List<BrushPixelData> myRange = new List<BrushPixelData>();
	}
}
