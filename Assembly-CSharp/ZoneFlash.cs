using System;
using UnityEngine;

// Token: 0x02000356 RID: 854
public class ZoneFlash : BaseEffect
{
	// Token: 0x060020AC RID: 8364 RVA: 0x0011761F File Offset: 0x0011581F
	public void start(Color pColor, float pAlpha = 0.2f)
	{
		this.sprite_renderer.color = pColor;
		base.setAlpha(pAlpha);
	}

	// Token: 0x060020AD RID: 8365 RVA: 0x00117634 File Offset: 0x00115834
	public override void update(float pElapsed)
	{
		base.setAlpha(this.alpha - pElapsed * 0.1f);
		if (this.alpha <= 0f)
		{
			this.kill();
		}
	}
}
