using System;

// Token: 0x020005A9 RID: 1449
[Serializable]
public class SignalAsset : Asset
{
	// Token: 0x0600300C RID: 12300 RVA: 0x00173FDB File Offset: 0x001721DB
	public bool isBanned()
	{
		return this._banned;
	}

	// Token: 0x0600300D RID: 12301 RVA: 0x00173FE4 File Offset: 0x001721E4
	public bool ban()
	{
		return this._banned = true;
	}

	// Token: 0x0600300E RID: 12302 RVA: 0x00173FFC File Offset: 0x001721FC
	public bool unban()
	{
		return this._banned = false;
	}

	// Token: 0x0400243E RID: 9278
	public SignalAction action;

	// Token: 0x0400243F RID: 9279
	public bool has_action;

	// Token: 0x04002440 RID: 9280
	public AchievementCheck action_achievement;

	// Token: 0x04002441 RID: 9281
	public bool has_action_achievement;

	// Token: 0x04002442 RID: 9282
	public SignalBanCheckAction ban_check_action;

	// Token: 0x04002443 RID: 9283
	public bool has_ban_check_action;

	// Token: 0x04002444 RID: 9284
	private bool _banned;
}
