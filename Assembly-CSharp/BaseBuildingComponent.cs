using System;

// Token: 0x02000231 RID: 561
public class BaseBuildingComponent : IDisposable
{
	// Token: 0x06001538 RID: 5432 RVA: 0x000DD284 File Offset: 0x000DB484
	internal virtual void create(Building pBuilding)
	{
		this.building = pBuilding;
	}

	// Token: 0x06001539 RID: 5433 RVA: 0x000DD28D File Offset: 0x000DB48D
	public virtual void update(float pElapsed)
	{
	}

	// Token: 0x0600153A RID: 5434 RVA: 0x000DD28F File Offset: 0x000DB48F
	public virtual void Dispose()
	{
		this.building = null;
	}

	// Token: 0x040011F9 RID: 4601
	internal Building building;
}
