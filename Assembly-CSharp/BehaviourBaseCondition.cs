using System;

// Token: 0x02000384 RID: 900
public abstract class BehaviourBaseCondition<T>
{
	// Token: 0x06002188 RID: 8584 RVA: 0x0011CCB5 File Offset: 0x0011AEB5
	public virtual bool check(T pObject)
	{
		return true;
	}
}
