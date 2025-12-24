using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005CE RID: 1486
[RequireComponent(typeof(Button))]
public class ButtonUtil : MonoBehaviour
{
	// Token: 0x060030E5 RID: 12517 RVA: 0x00177D8C File Offset: 0x00175F8C
	public void ResetState()
	{
		if (this._button == null)
		{
			this._button = base.GetComponent<Button>();
			this._button.onClick.AddListener(new UnityAction(this.playSound));
		}
		this._button.enabled = false;
		this._button.enabled = true;
	}

	// Token: 0x060030E6 RID: 12518 RVA: 0x00177DE7 File Offset: 0x00175FE7
	private void playSound()
	{
		SoundBox.click();
	}

	// Token: 0x040024E9 RID: 9449
	private Button _button;
}
