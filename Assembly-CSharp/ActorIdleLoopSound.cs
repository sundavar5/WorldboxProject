using System;
using FMOD.Studio;

// Token: 0x0200035C RID: 860
public class ActorIdleLoopSound
{
	// Token: 0x060020BE RID: 8382 RVA: 0x00118055 File Offset: 0x00116255
	public ActorIdleLoopSound(ActorAsset pAsset, Actor pActor)
	{
	}

	// Token: 0x060020BF RID: 8383 RVA: 0x0011805D File Offset: 0x0011625D
	public void stop()
	{
		this.stopLoopCallback(this._actor);
	}

	// Token: 0x060020C0 RID: 8384 RVA: 0x0011806B File Offset: 0x0011626B
	internal void stopLoopCallback(Actor pActor)
	{
		if (this.fmod_instance.isValid())
		{
			this.fmod_instance.stop(STOP_MODE.ALLOWFADEOUT);
			this.fmod_instance.release();
		}
	}

	// Token: 0x040017E4 RID: 6116
	internal EventInstance fmod_instance;

	// Token: 0x040017E5 RID: 6117
	private Actor _actor;
}
