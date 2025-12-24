using System;

// Token: 0x02000483 RID: 1155
internal sealed class RewardTester
{
	// Token: 0x06002798 RID: 10136 RVA: 0x0014082C File Offset: 0x0013EA2C
	internal bool haveRew(string pPowID)
	{
		RewardedPower tResultPow = null;
		foreach (RewardedPower tPow in PlayerConfig.instance.data.rewardedPowers)
		{
			if (tPow.name == pPowID)
			{
				tResultPow = tPow;
				break;
			}
		}
		return tResultPow != null;
	}

	// Token: 0x06002799 RID: 10137 RVA: 0x0014089C File Offset: 0x0013EA9C
	internal bool checkRew()
	{
		if (PlayerConfig.instance.data.rewardedPowers.Count == 0)
		{
			return false;
		}
		double tCur = Epoch.Current();
		int tDur = 1860;
		bool tSav = false;
		int i = 0;
		while (i < PlayerConfig.instance.data.rewardedPowers.Count)
		{
			RewardedPower tPow = PlayerConfig.instance.data.rewardedPowers[i];
			bool expired = false;
			if (tPow.timeStamp > tCur)
			{
				expired = true;
			}
			if (tCur - tPow.timeStamp > (double)tDur)
			{
				expired = true;
			}
			if (expired)
			{
				PlayerConfig.instance.data.rewardedPowers.RemoveAt(i);
				tSav = true;
			}
			else
			{
				i++;
			}
		}
		if (tSav)
		{
			PlayerConfig.saveData();
			return true;
		}
		return false;
	}
}
