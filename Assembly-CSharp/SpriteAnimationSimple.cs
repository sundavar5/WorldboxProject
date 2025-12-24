using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000777 RID: 1911
public class SpriteAnimationSimple : MonoBehaviour
{
	// Token: 0x06003C82 RID: 15490 RVA: 0x001A3F47 File Offset: 0x001A2147
	private void Awake()
	{
		this._renderer = base.GetComponent<Image>();
	}

	// Token: 0x06003C83 RID: 15491 RVA: 0x001A3F55 File Offset: 0x001A2155
	public void setActionFinish(EffectParticlesCursorDelegate pAction)
	{
		this._action_finish = pAction;
	}

	// Token: 0x06003C84 RID: 15492 RVA: 0x001A3F5E File Offset: 0x001A215E
	public void resetAnim()
	{
		this._frame_index_current = 0;
		this._next_frame_time = this._time_between_frames;
		this.updateFrame();
	}

	// Token: 0x06003C85 RID: 15493 RVA: 0x001A3F7C File Offset: 0x001A217C
	internal virtual void update(float pElapsed)
	{
		if (this._next_frame_time > 0f)
		{
			this._next_frame_time -= pElapsed;
			if (this._next_frame_time > 0f)
			{
				return;
			}
		}
		this._next_frame_time = this._time_between_frames;
		this._frame_index_current++;
		if (this._frame_index_current >= this._frames.Length)
		{
			if (this._action_finish != null)
			{
				this._action_finish(this);
				return;
			}
		}
		else
		{
			this.updateFrame();
		}
	}

	// Token: 0x06003C86 RID: 15494 RVA: 0x001A3FF8 File Offset: 0x001A21F8
	private void updateFrame()
	{
		Sprite tSprite = this._frames[this._frame_index_current];
		this._renderer.sprite = tSprite;
	}

	// Token: 0x06003C87 RID: 15495 RVA: 0x001A401F File Offset: 0x001A221F
	public void setFrames(Sprite[] pFrames)
	{
		this._frames = pFrames;
	}

	// Token: 0x04002BF3 RID: 11251
	[SerializeField]
	public float _time_between_frames = 0.1f;

	// Token: 0x04002BF4 RID: 11252
	private Image _renderer;

	// Token: 0x04002BF5 RID: 11253
	[SerializeField]
	public Sprite[] _frames;

	// Token: 0x04002BF6 RID: 11254
	private EffectParticlesCursorDelegate _action_finish;

	// Token: 0x04002BF7 RID: 11255
	private int _frame_index_current;

	// Token: 0x04002BF8 RID: 11256
	private float _next_frame_time;
}
