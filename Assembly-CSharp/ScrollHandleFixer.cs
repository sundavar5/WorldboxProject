using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200083B RID: 2107
public class ScrollHandleFixer : MonoBehaviour, ILayoutSelfController, ILayoutController
{
	// Token: 0x060041CD RID: 16845 RVA: 0x001BF2B4 File Offset: 0x001BD4B4
	private void Awake()
	{
		this._bar.onValueChanged.AddListener(delegate(float pValue)
		{
			if (this._bar_updating)
			{
				return;
			}
			if (this._bar.size > 0.05f)
			{
				return;
			}
			this._bar_updating = true;
			this._bar.size = 0.05f;
			this._bar_updating = false;
		});
	}

	// Token: 0x060041CE RID: 16846 RVA: 0x001BF2D2 File Offset: 0x001BD4D2
	private void Update()
	{
		this.checkBarSize();
	}

	// Token: 0x060041CF RID: 16847 RVA: 0x001BF2DA File Offset: 0x001BD4DA
	private void LateUpdate()
	{
		this.checkBarSize();
	}

	// Token: 0x060041D0 RID: 16848 RVA: 0x001BF2E2 File Offset: 0x001BD4E2
	public void SetLayoutHorizontal()
	{
		this.checkBarSize();
	}

	// Token: 0x060041D1 RID: 16849 RVA: 0x001BF2EA File Offset: 0x001BD4EA
	public void SetLayoutVertical()
	{
		this.checkBarSize();
	}

	// Token: 0x060041D2 RID: 16850 RVA: 0x001BF2F2 File Offset: 0x001BD4F2
	private void checkBarSize()
	{
		if (this._bar.size > 0.05f)
		{
			return;
		}
		this._bar.size = 0.05f;
	}

	// Token: 0x04003014 RID: 12308
	private const float MIN_SIZE = 0.05f;

	// Token: 0x04003015 RID: 12309
	[SerializeField]
	private Scrollbar _bar;

	// Token: 0x04003016 RID: 12310
	private bool _bar_updating;
}
