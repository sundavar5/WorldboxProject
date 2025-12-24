using System;
using UnityEngine;

// Token: 0x020007A8 RID: 1960
public class FadeOutDelayed : MonoBehaviour
{
	// Token: 0x06003E08 RID: 15880 RVA: 0x001B093A File Offset: 0x001AEB3A
	private void OnEnable()
	{
		this.reset();
	}

	// Token: 0x06003E09 RID: 15881 RVA: 0x001B0944 File Offset: 0x001AEB44
	private void Update()
	{
		this._delay_time_left -= Time.deltaTime;
		if (this._delay_time_left > 0f)
		{
			return;
		}
		if (this._time_left <= 0f)
		{
			return;
		}
		this._time_left -= Time.deltaTime;
		this._group.alpha = Mathf.Lerp(this._min_alpha, this._max_alpha, this._time_left / this._duration);
	}

	// Token: 0x06003E0A RID: 15882 RVA: 0x001B09BA File Offset: 0x001AEBBA
	private void reset()
	{
		this._delay_time_left = this._delay;
		this._time_left = this._duration;
		this._group.alpha = this._max_alpha;
	}

	// Token: 0x04002D0A RID: 11530
	[SerializeField]
	private CanvasGroup _group;

	// Token: 0x04002D0B RID: 11531
	[SerializeField]
	private float _duration;

	// Token: 0x04002D0C RID: 11532
	[SerializeField]
	private float _delay;

	// Token: 0x04002D0D RID: 11533
	[SerializeField]
	[Range(0f, 1f)]
	private float _max_alpha = 1f;

	// Token: 0x04002D0E RID: 11534
	[SerializeField]
	[Range(0f, 1f)]
	private float _min_alpha;

	// Token: 0x04002D0F RID: 11535
	private float _time_left;

	// Token: 0x04002D10 RID: 11536
	private float _delay_time_left;
}
