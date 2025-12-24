using System;

// Token: 0x020002FE RID: 766
public static class WarWinnerExtensions
{
	// Token: 0x06001D1B RID: 7451 RVA: 0x00104FAC File Offset: 0x001031AC
	public static string getLocaleID(this WarWinner pWinner)
	{
		string result;
		switch (pWinner)
		{
		case WarWinner.Attackers:
			result = "attackers";
			break;
		case WarWinner.Defenders:
			result = "defenders";
			break;
		case WarWinner.Peace:
			result = "peace";
			break;
		case WarWinner.Merged:
			result = "war_winner_merged";
			break;
		default:
			result = "war_winner_nobody";
			break;
		}
		return result;
	}
}
