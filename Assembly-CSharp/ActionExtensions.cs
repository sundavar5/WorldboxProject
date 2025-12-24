using System;

// Token: 0x02000444 RID: 1092
public static class ActionExtensions
{
	// Token: 0x060025D5 RID: 9685 RVA: 0x001375CC File Offset: 0x001357CC
	public static bool[] Run(this WorldAction pAction, BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		Delegate[] invocationList = pAction.GetInvocationList();
		bool[] tResults = new bool[invocationList.Length];
		int tIndex = 0;
		foreach (WorldAction tAction in invocationList)
		{
			tResults[tIndex++] = tAction(pTarget, pTile);
		}
		return tResults;
	}

	// Token: 0x060025D6 RID: 9686 RVA: 0x00137614 File Offset: 0x00135814
	public static bool RunAnyTrue(this WorldAction pAction, BaseSimObject pTarget = null, WorldTile pTile = null)
	{
		Delegate[] invocationList = pAction.GetInvocationList();
		bool tSuccess = false;
		Delegate[] array = invocationList;
		for (int i = 0; i < array.Length; i++)
		{
			if (((WorldAction)array[i])(pTarget, pTile))
			{
				tSuccess = true;
			}
		}
		return tSuccess;
	}

	// Token: 0x060025D7 RID: 9687 RVA: 0x0013764C File Offset: 0x0013584C
	public static bool[] Run(this AttackAction pAction, BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		Delegate[] invocationList = pAction.GetInvocationList();
		bool[] tResults = new bool[invocationList.Length];
		int tIndex = 0;
		foreach (AttackAction tAction in invocationList)
		{
			tResults[tIndex++] = tAction(pSelf, pTarget, pTile);
		}
		return tResults;
	}

	// Token: 0x060025D8 RID: 9688 RVA: 0x00137698 File Offset: 0x00135898
	public static bool RunAnyTrue(this AttackAction pAction, BaseSimObject pSelf, BaseSimObject pTarget, WorldTile pTile = null)
	{
		Delegate[] invocationList = pAction.GetInvocationList();
		bool tSuccess = false;
		Delegate[] array = invocationList;
		for (int i = 0; i < array.Length; i++)
		{
			if (((AttackAction)array[i])(pSelf, pTarget, pTile))
			{
				tSuccess = true;
			}
		}
		return tSuccess;
	}
}
