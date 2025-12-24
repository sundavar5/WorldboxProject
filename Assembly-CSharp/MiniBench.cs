using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public readonly struct MiniBench : IDisposable
{
	// Token: 0x06002AA4 RID: 10916 RVA: 0x00153F31 File Offset: 0x00152131
	public MiniBench(string pID)
	{
		this._id = pID;
		this._sw = new Stopwatch();
		this._sw.Start();
		this._dont_show_below_ms = 0L;
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x00153F58 File Offset: 0x00152158
	public MiniBench(string pID, long pDontShowBelowMs)
	{
		this._id = pID;
		this._sw = new Stopwatch();
		this._sw.Start();
		this._dont_show_below_ms = pDontShowBelowMs;
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x00153F80 File Offset: 0x00152180
	public void Dispose()
	{
		this._sw.Stop();
		long tElapsed = this._sw.ElapsedMilliseconds;
		if (tElapsed < this._dont_show_below_ms)
		{
			return;
		}
		string text;
		if (tElapsed <= 999L)
		{
			if (tElapsed <= 499L)
			{
				text = "";
			}
			else
			{
				text = "<color=yellow>";
			}
		}
		else
		{
			text = "<color=red>";
		}
		string tColor = text;
		string tColorEnd = (tElapsed > 499L) ? "</color>" : "";
		double tElapsedMs = this._sw.Elapsed.TotalSeconds;
		if (this._id.Length + 2 > MiniBench.MAX_LOG_LENGTH)
		{
			MiniBench.MAX_LOG_LENGTH = this._id.Length + 2;
		}
		Debug.Log(string.Concat(new string[]
		{
			Toolbox.fillRight("[" + this._id + "]", MiniBench.MAX_LOG_LENGTH, ' '),
			" = ",
			tColor,
			tElapsedMs.ToString("F6"),
			tColorEnd
		}));
	}

	// Token: 0x04001FF4 RID: 8180
	private readonly string _id;

	// Token: 0x04001FF5 RID: 8181
	private readonly Stopwatch _sw;

	// Token: 0x04001FF6 RID: 8182
	private readonly long _dont_show_below_ms;

	// Token: 0x04001FF7 RID: 8183
	private const string _COLOR_WARN = "<color=yellow>";

	// Token: 0x04001FF8 RID: 8184
	private const string _COLOR_SLOW = "<color=red>";

	// Token: 0x04001FF9 RID: 8185
	private static int MAX_LOG_LENGTH = 38;
}
