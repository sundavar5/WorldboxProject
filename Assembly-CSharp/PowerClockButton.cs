using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F2 RID: 1522
public class PowerClockButton : MonoBehaviour
{
	// Token: 0x060031BD RID: 12733 RVA: 0x0017BC8C File Offset: 0x00179E8C
	private void Update()
	{
		if (Config.time_scale_asset == null)
		{
			return;
		}
		if (Config.time_scale_asset.id != this._latest_used)
		{
			this._latest_used = Config.time_scale_asset.id;
			this.currentSpeedIcon.sprite = SpriteTextureLoader.getSprite(Config.time_scale_asset.path_icon);
		}
	}

	// Token: 0x0400259C RID: 9628
	public Image currentSpeedIcon;

	// Token: 0x0400259D RID: 9629
	private string _latest_used = string.Empty;
}
