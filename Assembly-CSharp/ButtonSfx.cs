using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000441 RID: 1089
[RequireComponent(typeof(Button))]
public class ButtonSfx : MonoBehaviour
{
	// Token: 0x060025C8 RID: 9672 RVA: 0x0013725A File Offset: 0x0013545A
	private void Start()
	{
		this._button = base.GetComponent<Button>();
		this._button.onClick.AddListener(new UnityAction(this.playSound));
	}

	// Token: 0x060025C9 RID: 9673 RVA: 0x00137284 File Offset: 0x00135484
	private void playSound()
	{
		SoundBox.click();
		this._button.enabled = false;
		this._button.enabled = true;
	}

	// Token: 0x04001CB6 RID: 7350
	private Button _button;
}
