using System;

// Token: 0x0200047C RID: 1148
[Serializable]
public class RewardedPower
{
	// Token: 0x06002767 RID: 10087 RVA: 0x0013F1BB File Offset: 0x0013D3BB
	public RewardedPower(string pName, double pTimeStamp)
	{
		this.name = pName;
		this.timeStamp = pTimeStamp;
	}

	// Token: 0x04001D92 RID: 7570
	public string name;

	// Token: 0x04001D93 RID: 7571
	public double timeStamp;
}
