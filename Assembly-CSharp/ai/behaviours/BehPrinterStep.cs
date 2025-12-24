using System;

namespace ai.behaviours
{
	// Token: 0x020008EE RID: 2286
	public class BehPrinterStep : BehaviourActionActor
	{
		// Token: 0x06004553 RID: 17747 RVA: 0x001D1D7C File Offset: 0x001CFF7C
		public override BehResult execute(Actor pActor)
		{
			string template;
			pActor.data.get("template", out template, null);
			int step;
			pActor.data.get("step", out step, -1);
			int origin_x;
			pActor.data.get("origin_x", out origin_x, 0);
			int origin_y;
			pActor.data.get("origin_y", out origin_y, 0);
			PrintTemplate currentPrint = PrintLibrary.getTemplate(template);
			for (int i = 0; i < currentPrint.steps_per_tick; i++)
			{
				if (step >= currentPrint.steps.Length)
				{
					pActor.data.set("step", step);
					return BehResult.Stop;
				}
				PrintStep tStep = currentPrint.steps[step];
				WorldTile tMoveTile = BehaviourActionBase<Actor>.world.GetTile(origin_x + tStep.x, origin_y + tStep.y);
				if (tMoveTile != null)
				{
					pActor.spawnOn(tMoveTile, 0f);
					BehPrinterStep.printTile(pActor);
				}
				step++;
			}
			pActor.data.set("step", step);
			return BehResult.RestartTask;
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x001D1E70 File Offset: 0x001D0070
		private static void printTile(Actor pActor)
		{
			MusicBox.playSound("event:/SFX/UNIQUE/PrinterStep", pActor.current_tile, false, false);
			if (pActor.current_tile.top_type != null)
			{
				MapAction.decreaseTile(pActor.current_tile, false, "flash");
			}
			if (pActor.current_tile.Type.increase_to != null)
			{
				MapAction.terraformMain(pActor.current_tile, pActor.current_tile.Type.increase_to, AssetManager.terraform.get("destroy"), false);
				BehaviourActionBase<Actor>.world.setTileDirty(pActor.current_tile);
			}
			BehaviourActionBase<Actor>.world.conway_layer.remove(pActor.current_tile);
		}
	}
}
