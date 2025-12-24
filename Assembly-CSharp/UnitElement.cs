using System;

// Token: 0x020006DC RID: 1756
public abstract class UnitElement : WindowMetaElementBase, IRefreshElement
{
	// Token: 0x06003855 RID: 14421 RVA: 0x001950F2 File Offset: 0x001932F2
	protected override void Awake()
	{
		this.checkSetWindow();
		base.Awake();
	}

	// Token: 0x06003856 RID: 14422 RVA: 0x00195100 File Offset: 0x00193300
	protected virtual void checkSetWindow()
	{
		this.unit_window = base.GetComponentInParent<UnitWindow>();
	}

	// Token: 0x06003857 RID: 14423 RVA: 0x0019510E File Offset: 0x0019330E
	protected override void OnEnable()
	{
		this.checkSetActor();
		base.OnEnable();
	}

	// Token: 0x06003858 RID: 14424 RVA: 0x0019511C File Offset: 0x0019331C
	protected virtual void checkSetActor()
	{
		this.setActor(this.unit_window.actor);
	}

	// Token: 0x06003859 RID: 14425 RVA: 0x0019512F File Offset: 0x0019332F
	protected virtual void setActor(Actor pActor)
	{
		this.actor = pActor;
	}

	// Token: 0x0600385A RID: 14426 RVA: 0x00195138 File Offset: 0x00193338
	public override bool checkRefreshWindow()
	{
		return this.actor.isRekt() || !this.actor.hasHealth() || base.checkRefreshWindow();
	}

	// Token: 0x0600385B RID: 14427 RVA: 0x0019515E File Offset: 0x0019335E
	protected override void OnDisable()
	{
		base.OnDisable();
		this.actor = null;
	}

	// Token: 0x040029D6 RID: 10710
	protected Actor actor;

	// Token: 0x040029D7 RID: 10711
	protected UnitWindow unit_window;
}
