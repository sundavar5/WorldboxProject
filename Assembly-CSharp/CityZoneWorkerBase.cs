using System;
using System.Collections.Generic;

// Token: 0x02000325 RID: 805
public abstract class CityZoneWorkerBase
{
	// Token: 0x06001F0B RID: 7947 RVA: 0x0010EAD1 File Offset: 0x0010CCD1
	protected void prepareWave()
	{
		this._wave.Clear();
		this._next_wave.Clear();
	}

	// Token: 0x06001F0C RID: 7948 RVA: 0x0010EAE9 File Offset: 0x0010CCE9
	internal virtual void clearAll()
	{
		this._zones_checked.Clear();
		this._wave.Clear();
		this._next_wave.Clear();
		this.checkZoneDebug();
	}

	// Token: 0x06001F0D RID: 7949 RVA: 0x0010EB12 File Offset: 0x0010CD12
	protected virtual void queueConnection(ZoneConnection pConnection, Queue<ZoneConnection> pWave, bool pSetChecked = false)
	{
		pWave.Enqueue(pConnection);
		if (pSetChecked)
		{
			this._zones_checked.Add(pConnection);
		}
	}

	// Token: 0x06001F0E RID: 7950 RVA: 0x0010EB2B File Offset: 0x0010CD2B
	protected void checkZoneDebug()
	{
		if (this.debug)
		{
			World.world.zone_calculator.clearDebug();
		}
	}

	// Token: 0x040016B5 RID: 5813
	protected bool debug;

	// Token: 0x040016B6 RID: 5814
	protected Queue<ZoneConnection> _wave = new Queue<ZoneConnection>();

	// Token: 0x040016B7 RID: 5815
	protected Queue<ZoneConnection> _next_wave = new Queue<ZoneConnection>();

	// Token: 0x040016B8 RID: 5816
	protected HashSet<ZoneConnection> _zones_checked = new HashSet<ZoneConnection>();
}
