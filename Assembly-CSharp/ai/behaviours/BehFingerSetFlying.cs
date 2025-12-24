using System;

namespace ai.behaviours
{
	// Token: 0x02000960 RID: 2400
	public class BehFingerSetFlying : BehFinger
	{
		// Token: 0x0600467A RID: 18042 RVA: 0x001DE4C2 File Offset: 0x001DC6C2
		public BehFingerSetFlying(bool pFlying, float pHeightTarget = -1f)
		{
			this._flying = pFlying;
			if (pHeightTarget > -1f)
			{
				this._height_target = pHeightTarget;
			}
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x001DE4EC File Offset: 0x001DC6EC
		public override BehResult execute(Actor pActor)
		{
			pActor.setFlying(this._flying);
			if (this._flying)
			{
				if (this._height_target > -1f)
				{
					this.finger.flying_target = this._height_target;
				}
				else
				{
					this.finger.flying_target = Randy.randomFloat(5f, 13f);
				}
			}
			else
			{
				this.finger.flying_target = 0.3f;
			}
			return BehResult.Continue;
		}

		// Token: 0x040031F2 RID: 12786
		private bool _flying;

		// Token: 0x040031F3 RID: 12787
		private float _height_target = -1f;
	}
}
