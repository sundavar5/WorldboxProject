using System;

namespace ai.behaviours
{
	// Token: 0x020008ED RID: 2285
	public class BehPrinterSetup : BehaviourActionActor
	{
		// Token: 0x06004551 RID: 17745 RVA: 0x001D1C9C File Offset: 0x001CFE9C
		public override BehResult execute(Actor pActor)
		{
			int step;
			pActor.data.get("step", out step, -1);
			if (step < 0)
			{
				pActor.data.set("origin_x", pActor.current_tile.pos.x);
				pActor.data.set("origin_y", pActor.current_tile.pos.y);
				string tTemplate;
				pActor.data.get("template", out tTemplate, null);
				PrintTemplate currentPrint = PrintLibrary.getTemplate(tTemplate);
				pActor.data.set("steps", currentPrint.steps.Length);
				pActor.data.set("step", 0);
			}
			int steps;
			pActor.data.get("steps", out steps, -1);
			if (step >= steps)
			{
				pActor.dieSimpleNone();
				return BehResult.Stop;
			}
			return BehResult.Continue;
		}
	}
}
