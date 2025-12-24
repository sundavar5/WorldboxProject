using System;

// Token: 0x020004F9 RID: 1273
public class BehaviourActionTester : BehaviourActionBase<AutoTesterBot>
{
	// Token: 0x06002A50 RID: 10832 RVA: 0x0014C2F8 File Offset: 0x0014A4F8
	public override bool errorsFound(AutoTesterBot pObject)
	{
		return (this.null_check_tile_target && pObject.beh_tile_target == null) || base.errorsFound(pObject);
	}

	// Token: 0x04001F81 RID: 8065
	public bool null_check_tile_target;
}
