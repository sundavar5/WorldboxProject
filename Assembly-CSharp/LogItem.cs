using System;
using UnityEngine;

// Token: 0x0200053E RID: 1342
public readonly struct LogItem
{
	// Token: 0x06002BE4 RID: 11236 RVA: 0x0015ACE4 File Offset: 0x00158EE4
	public LogItem(string pLog, string pStackTrace, LogType pType)
	{
		this.log = pLog;
		this.stack_trace = pStackTrace;
		this.type = pType;
		this.time = DateTime.Now;
	}

	// Token: 0x06002BE5 RID: 11237 RVA: 0x0015AD06 File Offset: 0x00158F06
	public LogItem(string pLog, string pStackTrace, LogType pType, DateTime pTime)
	{
		this.log = pLog;
		this.stack_trace = pStackTrace;
		this.type = pType;
		this.time = pTime;
	}

	// Token: 0x040021B9 RID: 8633
	public readonly string log;

	// Token: 0x040021BA RID: 8634
	public readonly string stack_trace;

	// Token: 0x040021BB RID: 8635
	public readonly LogType type;

	// Token: 0x040021BC RID: 8636
	public readonly DateTime time;
}
