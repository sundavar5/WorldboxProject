using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000542 RID: 1346
public class UiDebugButtonBatchSize : MonoBehaviour
{
	// Token: 0x06002BF8 RID: 11256 RVA: 0x0015B1E5 File Offset: 0x001593E5
	private void Awake()
	{
		this._button.onClick.AddListener(new UnityAction(this.click));
	}

	// Token: 0x06002BF9 RID: 11257 RVA: 0x0015B203 File Offset: 0x00159403
	public void click()
	{
		ParallelHelper.moveDebugBatchSize();
		this.updateText();
	}

	// Token: 0x06002BFA RID: 11258 RVA: 0x0015B210 File Offset: 0x00159410
	private void OnEnable()
	{
		this.updateText();
	}

	// Token: 0x06002BFB RID: 11259 RVA: 0x0015B218 File Offset: 0x00159418
	private void updateText()
	{
		this._text.text = ParallelHelper.DEBUG_BATCH_SIZE.ToString();
	}

	// Token: 0x040021CB RID: 8651
	[SerializeField]
	private Text _text;

	// Token: 0x040021CC RID: 8652
	[SerializeField]
	private Button _button;
}
