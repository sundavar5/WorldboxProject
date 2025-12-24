using System;
using UnityEngine;

// Token: 0x02000208 RID: 520
public class BaseActorComponent : MonoBehaviour, IDisposable
{
	// Token: 0x06001249 RID: 4681 RVA: 0x000D48B7 File Offset: 0x000D2AB7
	internal virtual void create(Actor pActor)
	{
		this.actor = pActor;
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x000D48C0 File Offset: 0x000D2AC0
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x000D48C2 File Offset: 0x000D2AC2
	public virtual void Dispose()
	{
		this.actor = null;
	}

	// Token: 0x0400110E RID: 4366
	internal Actor actor;
}
