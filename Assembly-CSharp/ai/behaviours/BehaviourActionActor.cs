using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x0200093A RID: 2362
	public class BehaviourActionActor : BehaviourActionBase<Actor>
	{
		// Token: 0x06004612 RID: 17938 RVA: 0x001D48EB File Offset: 0x001D2AEB
		public BehResult forceTask(Actor pActor, string pTask, bool pClean = true, bool pForceAction = false)
		{
			pActor.setTask(pTask, pClean, false, pForceAction);
			return BehResult.Skip;
		}

		// Token: 0x06004613 RID: 17939 RVA: 0x001D48F9 File Offset: 0x001D2AF9
		public BehResult forceTaskImmediate(Actor pActor, string pTask, bool pClean = true, bool pForceAction = false)
		{
			pActor.setTask(pTask, pClean, false, pForceAction);
			return BehResult.ImmediateRun;
		}

		// Token: 0x06004614 RID: 17940 RVA: 0x001D4908 File Offset: 0x001D2B08
		public override bool errorsFound(Actor pObject)
		{
			if (pObject.current_tile.region == null)
			{
				return true;
			}
			if (pObject.current_tile.region.island == null)
			{
				return true;
			}
			if (this.null_check_city)
			{
				if (pObject.city == null)
				{
					return true;
				}
				if (!pObject.city.isAlive())
				{
					return true;
				}
			}
			if (this.null_check_actor_target)
			{
				if (pObject.beh_actor_target == null)
				{
					return true;
				}
				if (!pObject.beh_actor_target.isAlive())
				{
					return true;
				}
			}
			if (this.null_check_tile_target && pObject.beh_tile_target == null)
			{
				return true;
			}
			if (this.check_building_target_non_usable)
			{
				if (pObject.beh_building_target == null)
				{
					return true;
				}
				if (!pObject.beh_building_target.isUsable())
				{
					return true;
				}
			}
			else if (this.null_check_building_target)
			{
				if (pObject.beh_building_target == null)
				{
					return true;
				}
				if (!pObject.beh_building_target.isAlive())
				{
					return true;
				}
			}
			return base.errorsFound(pObject);
		}

		// Token: 0x06004615 RID: 17941 RVA: 0x001D49D5 File Offset: 0x001D2BD5
		public static void clear()
		{
			BehaviourActionActor.temp_actors.Clear();
			BehaviourActionActor.possible_moves.Clear();
		}

		// Token: 0x040031C2 RID: 12738
		public bool null_check_city;

		// Token: 0x040031C3 RID: 12739
		public bool null_check_kingdom;

		// Token: 0x040031C4 RID: 12740
		public bool null_check_tile_target;

		// Token: 0x040031C5 RID: 12741
		public bool null_check_building_target;

		// Token: 0x040031C6 RID: 12742
		public bool null_check_actor_target;

		// Token: 0x040031C7 RID: 12743
		public bool check_building_target_non_usable;

		// Token: 0x040031C8 RID: 12744
		public bool land_if_hovering;

		// Token: 0x040031C9 RID: 12745
		internal bool special_prevent_can_be_attacked;

		// Token: 0x040031CA RID: 12746
		internal string force_animation_id = string.Empty;

		// Token: 0x040031CB RID: 12747
		internal bool force_animation;

		// Token: 0x040031CC RID: 12748
		internal bool socialize;

		// Token: 0x040031CD RID: 12749
		protected static List<Actor> temp_actors = new List<Actor>();

		// Token: 0x040031CE RID: 12750
		protected static List<WorldTile> possible_moves = new List<WorldTile>();

		// Token: 0x040031CF RID: 12751
		public bool calibrate_target_position;

		// Token: 0x040031D0 RID: 12752
		public float check_actor_target_position_distance;
	}
}
