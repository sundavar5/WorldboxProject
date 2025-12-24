using System;
using System.Collections.Generic;
using FMOD.Studio;

// Token: 0x02000436 RID: 1078
public class MusicBoxIdle
{
	// Token: 0x06002589 RID: 9609 RVA: 0x00135994 File Offset: 0x00133B94
	public void update(float pElapsed)
	{
		if (this._timer > 2f)
		{
			this._timer -= pElapsed;
			return;
		}
		this._timer = 2f;
		this._toRemove.Clear();
		if (World.world.quality_changer.isLowRes())
		{
			this.clearAllSounds();
		}
		this.checkDeadSounds();
		if (!World.world.quality_changer.isLowRes())
		{
			this.updateBuildings();
		}
	}

	// Token: 0x0600258A RID: 9610 RVA: 0x00135A08 File Offset: 0x00133C08
	public virtual void checkDeadSounds()
	{
		foreach (BaseSimObject tObj in this.currentAttachedSounds.Keys)
		{
			bool toRemove = false;
			if (!tObj.isAlive())
			{
				toRemove = true;
			}
			if (toRemove)
			{
				this._toRemove.Add(tObj);
			}
		}
		foreach (BaseSimObject tObj2 in this._toRemove)
		{
			this.removeSound(tObj2);
		}
	}

	// Token: 0x0600258B RID: 9611 RVA: 0x00135AB8 File Offset: 0x00133CB8
	private void updateBuildings()
	{
	}

	// Token: 0x0600258C RID: 9612 RVA: 0x00135ABC File Offset: 0x00133CBC
	private void removeSound(BaseSimObject pObj)
	{
		EventInstance tInstance;
		this.currentAttachedSounds.TryGetValue(pObj, out tInstance);
		if (tInstance.isValid())
		{
			tInstance.stop(STOP_MODE.ALLOWFADEOUT);
			tInstance.release();
			this.currentAttachedSounds.Remove(pObj);
		}
	}

	// Token: 0x0600258D RID: 9613 RVA: 0x00135B00 File Offset: 0x00133D00
	private void playAttachedSound(BaseSimObject pObject, string pSound)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		EventInstance tInstance;
		this.currentAttachedSounds.TryGetValue(pObject, out tInstance);
		if (!tInstance.isValid())
		{
			this.currentAttachedSounds.Add(pObject, tInstance);
		}
	}

	// Token: 0x0600258E RID: 9614 RVA: 0x00135B3C File Offset: 0x00133D3C
	private bool isPlaying(BaseSimObject pObject)
	{
		EventInstance tInstance;
		this.currentAttachedSounds.TryGetValue(pObject, out tInstance);
		return tInstance.isValid();
	}

	// Token: 0x0600258F RID: 9615 RVA: 0x00135B64 File Offset: 0x00133D64
	public void clearAllSounds()
	{
		foreach (EventInstance tInstance in this.currentAttachedSounds.Values)
		{
			tInstance.stop(STOP_MODE.ALLOWFADEOUT);
			tInstance.release();
		}
		this.currentAttachedSounds.Clear();
	}

	// Token: 0x06002590 RID: 9616 RVA: 0x00135BD4 File Offset: 0x00133DD4
	public int CountCurrentSounds()
	{
		return this.currentAttachedSounds.Count;
	}

	// Token: 0x04001C85 RID: 7301
	private List<BaseSimObject> _toRemove = new List<BaseSimObject>();

	// Token: 0x04001C86 RID: 7302
	public Dictionary<BaseSimObject, EventInstance> currentAttachedSounds = new Dictionary<BaseSimObject, EventInstance>();

	// Token: 0x04001C87 RID: 7303
	private float _timer;
}
