using System;
using UnityEngine;

// Token: 0x0200064C RID: 1612
public class BrushSizesButton : MonoBehaviour
{
	// Token: 0x0600346B RID: 13419 RVA: 0x00185BD9 File Offset: 0x00183DD9
	private void Awake()
	{
		this._power_button = base.GetComponent<PowerButton>();
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x00185BE8 File Offset: 0x00183DE8
	private void Update()
	{
		if (Config.current_brush != this._latest_used)
		{
			this._latest_used = Config.current_brush;
			BrushData tNewBrush = Brush.get(Config.current_brush);
			if (tNewBrush == null)
			{
				Debug.LogError(Config.current_brush + " is not a valid brush");
				return;
			}
			tNewBrush.setupImage(this._power_button.icon);
		}
	}

	// Token: 0x04002784 RID: 10116
	private PowerButton _power_button;

	// Token: 0x04002785 RID: 10117
	private string _latest_used = string.Empty;
}
