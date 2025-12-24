using System;

// Token: 0x02000076 RID: 118
internal class MapLoaderContainer
{
	// Token: 0x06000446 RID: 1094 RVA: 0x0002D727 File Offset: 0x0002B927
	public MapLoaderContainer(MapLoaderAction pAction, string pID, bool pDebugLog = true, float pNewWaitTimerValue = 0.001f)
	{
		this.action = pAction;
		this.id = pID;
		this.debug_log = pDebugLog;
		this.new_timer_value = pNewWaitTimerValue;
	}

	// Token: 0x04000384 RID: 900
	public MapLoaderAction action;

	// Token: 0x04000385 RID: 901
	public string id;

	// Token: 0x04000386 RID: 902
	public bool debug_log = true;

	// Token: 0x04000387 RID: 903
	public float new_timer_value = 0.001f;
}
