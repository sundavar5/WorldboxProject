using System;
using UnityEngine;

// Token: 0x020002E6 RID: 742
public class SpriteGroupSystem<T> : MonoBehaviour where T : GroupSpriteObject
{
	// Token: 0x06001C01 RID: 7169 RVA: 0x000FFFAD File Offset: 0x000FE1AD
	public virtual void create()
	{
		base.transform.name = "GroupSpriteController";
		base.transform.parent = World.world.transform;
	}

	// Token: 0x06001C02 RID: 7170 RVA: 0x000FFFD4 File Offset: 0x000FE1D4
	public T[] getAll()
	{
		return this._sprites;
	}

	// Token: 0x06001C03 RID: 7171 RVA: 0x000FFFDC File Offset: 0x000FE1DC
	public void prepare()
	{
		this._used_index = 0;
	}

	// Token: 0x06001C04 RID: 7172 RVA: 0x000FFFE5 File Offset: 0x000FE1E5
	public virtual void update(float pElapsed)
	{
		this.finale();
	}

	// Token: 0x06001C05 RID: 7173 RVA: 0x000FFFED File Offset: 0x000FE1ED
	private void finale()
	{
		this.clearLast();
		this.count_active_debug = this._active_index;
		this._count_total_debug = this._total;
	}

	// Token: 0x06001C06 RID: 7174 RVA: 0x00100010 File Offset: 0x000FE210
	public void clearFull()
	{
		for (int i = 0; i < this._active_index; i++)
		{
			this.disableSprite(this._sprites[i]);
		}
		this._active_index = 0;
		this._used_index = 0;
	}

	// Token: 0x06001C07 RID: 7175 RVA: 0x00100054 File Offset: 0x000FE254
	public void clearLast()
	{
		int tDiff = this._active_index - this._used_index;
		for (int i = 0; i < tDiff; i++)
		{
			int tIndex = this._active_index - 1 - i;
			T tObject = this._sprites[tIndex];
			this.disableSprite(tObject);
			this.deactivate(tObject);
		}
		this._active_index -= tDiff;
	}

	// Token: 0x06001C08 RID: 7176 RVA: 0x001000B4 File Offset: 0x000FE2B4
	private void disableSprite(GroupSpriteObject pQ)
	{
		if (this.turn_off_renderer)
		{
			pQ.sprite_renderer.enabled = false;
			return;
		}
		if (pQ.gameObject.activeSelf)
		{
			pQ.gameObject.SetActive(false);
		}
	}

	// Token: 0x06001C09 RID: 7177 RVA: 0x001000E4 File Offset: 0x000FE2E4
	private void enableSprite(GroupSpriteObject pQ)
	{
		if (this.turn_off_renderer)
		{
			pQ.sprite_renderer.enabled = true;
			return;
		}
		if (!pQ.gameObject.activeSelf)
		{
			pQ.gameObject.SetActive(true);
		}
	}

	// Token: 0x06001C0A RID: 7178 RVA: 0x00100114 File Offset: 0x000FE314
	public virtual void deactivate(T pObject)
	{
	}

	// Token: 0x06001C0B RID: 7179 RVA: 0x00100116 File Offset: 0x000FE316
	public virtual void checkActiveAction(T pObject)
	{
	}

	// Token: 0x06001C0C RID: 7180 RVA: 0x00100118 File Offset: 0x000FE318
	internal T getNext()
	{
		T tObject;
		if (this.is_within_active_index)
		{
			tObject = this._sprites[this._used_index];
		}
		else
		{
			if (this._active_index < this._total)
			{
				tObject = this._sprites[this._active_index];
				this.enableSprite(tObject);
			}
			else
			{
				tObject = this.createNew();
			}
			this._active_index++;
		}
		this.checkActiveAction(tObject);
		this._used_index++;
		return tObject;
	}

	// Token: 0x170001BB RID: 443
	// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0010019A File Offset: 0x000FE39A
	internal bool is_within_active_index
	{
		get
		{
			return this._active_index > this._used_index;
		}
	}

	// Token: 0x06001C0E RID: 7182 RVA: 0x001001AC File Offset: 0x000FE3AC
	public T[] getFastActiveList(int pPlannedSize)
	{
		int tCurrentCount = this._active_index;
		if (tCurrentCount < pPlannedSize)
		{
			this._used_index = tCurrentCount;
			int tAdditionalNeeded = pPlannedSize - tCurrentCount;
			for (int i = 0; i < tAdditionalNeeded; i++)
			{
				this.getNext();
			}
		}
		else
		{
			this._used_index = pPlannedSize;
		}
		return this._sprites;
	}

	// Token: 0x06001C0F RID: 7183 RVA: 0x001001F4 File Offset: 0x000FE3F4
	protected virtual T createNew()
	{
		T tObject = Object.Instantiate<T>(this.prefab, base.gameObject.transform);
		if (this._total >= this._sprites.Length)
		{
			T[] tNewArray = new T[this._sprites.Length * 2];
			Array.Copy(this._sprites, tNewArray, this._sprites.Length);
			this._sprites = tNewArray;
		}
		T[] sprites = this._sprites;
		int total = this._total;
		this._total = total + 1;
		sprites[total] = tObject;
		return tObject;
	}

	// Token: 0x06001C10 RID: 7184 RVA: 0x00100271 File Offset: 0x000FE471
	public int countActive()
	{
		return this._active_index;
	}

	// Token: 0x06001C11 RID: 7185 RVA: 0x0010027C File Offset: 0x000FE47C
	public void debug(DebugTool pTool)
	{
		pTool.setSeparator();
		pTool.setText("count_active", this.count_active_debug, 0f, false, 0L, false, false, "");
		pTool.setText("count_total", this._count_total_debug, 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("active_len", this._active_index, 0f, false, 0L, false, false, "");
		pTool.setText("used_index", this._used_index, 0f, false, 0L, false, false, "");
	}

	// Token: 0x0400156C RID: 5484
	private T[] _sprites = new T[32];

	// Token: 0x0400156D RID: 5485
	private int _total;

	// Token: 0x0400156E RID: 5486
	internal T prefab;

	// Token: 0x0400156F RID: 5487
	internal int count_active_debug;

	// Token: 0x04001570 RID: 5488
	private int _count_total_debug;

	// Token: 0x04001571 RID: 5489
	private int _used_index;

	// Token: 0x04001572 RID: 5490
	private int _active_index;

	// Token: 0x04001573 RID: 5491
	public bool turn_off_renderer;
}
