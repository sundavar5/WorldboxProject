using System;

// Token: 0x0200021C RID: 540
public abstract class BaseSystemManager
{
	// Token: 0x06001381 RID: 4993 RVA: 0x000D8DA8 File Offset: 0x000D6FA8
	public virtual void ClearAllDisposed()
	{
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x000D8DAA File Offset: 0x000D6FAA
	public virtual void parallelDirtyUnitsCheck()
	{
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x000D8DAC File Offset: 0x000D6FAC
	public virtual void checkLists()
	{
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x000D8DAE File Offset: 0x000D6FAE
	public virtual void clear()
	{
		this.ClearAllDisposed();
		BaseSystemManager.anything_changed = false;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x000D8DBC File Offset: 0x000D6FBC
	public virtual void checkDeadObjects()
	{
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x000D8DBE File Offset: 0x000D6FBE
	public virtual bool isUnitsDirty()
	{
		return false;
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x000D8DC1 File Offset: 0x000D6FC1
	public virtual bool isLocked()
	{
		return this.isUnitsDirty();
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x000D8DC9 File Offset: 0x000D6FC9
	public virtual void startCollectHistoryData()
	{
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x000D8DCB File Offset: 0x000D6FCB
	public virtual void clearLastYearStats()
	{
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x000D8DCD File Offset: 0x000D6FCD
	public virtual void showDebugTool(DebugTool pTool)
	{
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x000D8DCF File Offset: 0x000D6FCF
	public virtual bool hasAny()
	{
		return this.Count > 0;
	}

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x0600138C RID: 5004 RVA: 0x000D8DDA File Offset: 0x000D6FDA
	public virtual int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x000D8DE1 File Offset: 0x000D6FE1
	public virtual string debugShort()
	{
		return string.Format("[c:{0}]", this.Count);
	}

	// Token: 0x0400118A RID: 4490
	protected static int _latest_hash = 1;

	// Token: 0x0400118B RID: 4491
	internal static bool anything_changed = false;
}
