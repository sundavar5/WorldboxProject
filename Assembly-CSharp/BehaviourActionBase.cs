using System;
using ai.behaviours;

// Token: 0x02000383 RID: 899
public class BehaviourActionBase<T> : BehaviourElementAI
{
	// Token: 0x170001EE RID: 494
	// (get) Token: 0x0600217E RID: 8574 RVA: 0x0011CB4F File Offset: 0x0011AD4F
	protected static MapBox world
	{
		get
		{
			return MapBox.instance;
		}
	}

	// Token: 0x0600217F RID: 8575 RVA: 0x0011CB56 File Offset: 0x0011AD56
	public override void create()
	{
		base.create();
		this.setupErrorChecks();
	}

	// Token: 0x06002180 RID: 8576 RVA: 0x0011CB64 File Offset: 0x0011AD64
	public virtual void prepare(T pObject)
	{
	}

	// Token: 0x06002181 RID: 8577 RVA: 0x0011CB66 File Offset: 0x0011AD66
	public virtual BehResult startExecute(T pObject)
	{
		if (this._has_error_check)
		{
			if (this.shouldRetry(pObject))
			{
				return BehResult.RepeatStep;
			}
			if (this.errorsFound(pObject))
			{
				return BehResult.Stop;
			}
		}
		this.prepare(pObject);
		return this.execute(pObject);
	}

	// Token: 0x06002182 RID: 8578 RVA: 0x0011CB94 File Offset: 0x0011AD94
	public virtual BehResult execute(T pObject)
	{
		return BehResult.Continue;
	}

	// Token: 0x06002183 RID: 8579 RVA: 0x0011CB97 File Offset: 0x0011AD97
	protected virtual void setupErrorChecks()
	{
		this.setHasErrorCheck();
	}

	// Token: 0x06002184 RID: 8580 RVA: 0x0011CB9F File Offset: 0x0011AD9F
	private void setHasErrorCheck()
	{
		this._has_error_check = true;
	}

	// Token: 0x06002185 RID: 8581 RVA: 0x0011CBA8 File Offset: 0x0011ADA8
	public virtual bool errorsFound(T pObject)
	{
		return false;
	}

	// Token: 0x06002186 RID: 8582 RVA: 0x0011CBAC File Offset: 0x0011ADAC
	public virtual bool shouldRetry(T pObject)
	{
		return (this.uses_cities && BehaviourActionBase<T>.world.cities.isLocked()) || (this.uses_kingdoms && BehaviourActionBase<T>.world.kingdoms.isLocked()) || (this.uses_books && BehaviourActionBase<T>.world.books.isLocked()) || (this.uses_religions && BehaviourActionBase<T>.world.religions.isLocked()) || (this.uses_languages && BehaviourActionBase<T>.world.languages.isLocked()) || (this.uses_cultures && BehaviourActionBase<T>.world.cultures.isLocked()) || (this.uses_clans && BehaviourActionBase<T>.world.clans.isLocked()) || (this.uses_plots && BehaviourActionBase<T>.world.plots.isLocked()) || (this.uses_families && BehaviourActionBase<T>.world.families.isLocked());
	}

	// Token: 0x040018C7 RID: 6343
	public bool uses_kingdoms;

	// Token: 0x040018C8 RID: 6344
	public bool uses_cities;

	// Token: 0x040018C9 RID: 6345
	public bool uses_books;

	// Token: 0x040018CA RID: 6346
	public bool uses_religions;

	// Token: 0x040018CB RID: 6347
	public bool uses_languages;

	// Token: 0x040018CC RID: 6348
	public bool uses_cultures;

	// Token: 0x040018CD RID: 6349
	public bool uses_clans;

	// Token: 0x040018CE RID: 6350
	public bool uses_plots;

	// Token: 0x040018CF RID: 6351
	public bool uses_families;

	// Token: 0x040018D0 RID: 6352
	protected bool _has_error_check;
}
