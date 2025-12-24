using System;

// Token: 0x020007FC RID: 2044
public class WindowMetaElement<TMetaObject, TData> : WindowMetaElementBase where TMetaObject : CoreSystemObject<TData> where TData : BaseSystemData
{
	// Token: 0x0600402B RID: 16427 RVA: 0x001B78B8 File Offset: 0x001B5AB8
	protected override void Awake()
	{
		this.window = base.GetComponentInParent<WindowMetaGeneric<TMetaObject, TData>>();
		base.Awake();
	}

	// Token: 0x0600402C RID: 16428 RVA: 0x001B78CC File Offset: 0x001B5ACC
	protected override void OnEnable()
	{
		this.meta_object = this.window.getMetaObject();
		base.OnEnable();
	}

	// Token: 0x0600402D RID: 16429 RVA: 0x001B78E5 File Offset: 0x001B5AE5
	protected override void OnDisable()
	{
		base.OnDisable();
		this.meta_object = default(TMetaObject);
	}

	// Token: 0x0600402E RID: 16430 RVA: 0x001B78F9 File Offset: 0x001B5AF9
	public override bool checkRefreshWindow()
	{
		return this.meta_object.isRekt() || base.checkRefreshWindow();
	}

	// Token: 0x04002E8B RID: 11915
	protected TMetaObject meta_object;

	// Token: 0x04002E8C RID: 11916
	protected WindowMetaGeneric<TMetaObject, TData> window;
}
