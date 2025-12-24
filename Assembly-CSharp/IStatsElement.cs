using System;
using UnityEngine;

// Token: 0x020005E0 RID: 1504
public interface IStatsElement : IRefreshElement
{
	// Token: 0x06003171 RID: 12657
	void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/');

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06003172 RID: 12658
	GameObject gameObject { get; }
}
