using System;

// Token: 0x02000207 RID: 519
public abstract class ActorSimpleComponent : IDisposable
{
	// Token: 0x06001245 RID: 4677 RVA: 0x000D489B File Offset: 0x000D2A9B
	internal virtual void create(Actor pActor)
	{
		this.actor = pActor;
	}

	// Token: 0x06001246 RID: 4678 RVA: 0x000D48A4 File Offset: 0x000D2AA4
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x000D48A6 File Offset: 0x000D2AA6
	public virtual void Dispose()
	{
		this.actor = null;
	}

	// Token: 0x0400110D RID: 4365
	internal Actor actor;
}
