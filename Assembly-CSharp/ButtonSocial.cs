using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200082A RID: 2090
public class ButtonSocial : MonoBehaviour
{
	// Token: 0x0600412A RID: 16682 RVA: 0x001BC59C File Offset: 0x001BA79C
	private void Awake()
	{
		switch (this._social_type)
		{
		case SocialType.Facebook:
			this._text.text = 82.ToString() + "k+";
			return;
		case SocialType.Twitter:
			this._text.text = 56.ToString() + "k+";
			return;
		case SocialType.Discord:
			this._text.text = 560.ToString() + "k+";
			return;
		case SocialType.Reddit:
			this._text.text = 140.ToString() + "k+";
			return;
		default:
			return;
		}
	}

	// Token: 0x0600412B RID: 16683 RVA: 0x001BC64E File Offset: 0x001BA84E
	public void openFacebook()
	{
		Analytics.LogEvent("open_link_facebook", true, true);
		Application.OpenURL("https://www.facebook.com/superworldbox");
	}

	// Token: 0x0600412C RID: 16684 RVA: 0x001BC666 File Offset: 0x001BA866
	public void openTwitter()
	{
		Analytics.LogEvent("open_link_twitter", true, true);
		Application.OpenURL("http://twitter.com/mixamko");
	}

	// Token: 0x0600412D RID: 16685 RVA: 0x001BC67E File Offset: 0x001BA87E
	public void openDiscord()
	{
		Analytics.LogEvent("open_link_discord", true, true);
		Application.OpenURL("https://discordapp.com/invite/worldbox");
		AchievementLibrary.social_network.check(null);
	}

	// Token: 0x0600412E RID: 16686 RVA: 0x001BC6A2 File Offset: 0x001BA8A2
	public void openLinkReddit()
	{
		Analytics.LogEvent("open_link_reddit", true, true);
		Application.OpenURL("https://www.reddit.com/r/worldbox");
	}

	// Token: 0x0600412F RID: 16687 RVA: 0x001BC6BA File Offset: 0x001BA8BA
	public void openLinkMoonBox()
	{
		Analytics.LogEvent("open_link_moonbox", true, true);
		if (Config.isIos)
		{
			Application.OpenURL("https://bit.ly/moonbox_wb_ap");
			return;
		}
		Application.OpenURL("https://bit.ly/moonbox_wb");
	}

	// Token: 0x06004130 RID: 16688 RVA: 0x001BC6E4 File Offset: 0x001BA8E4
	public void openLinkSteam()
	{
		Analytics.LogEvent("open_link_steam", true, true);
		Application.OpenURL(string.Format("https://store.steampowered.com/app/{0}/", 1206560U) + "?utm_source=game_bar" + "&utm_campaign=get_wishlists" + "&utm_medium=" + Application.platform.ToString());
	}

	// Token: 0x04002F9F RID: 12191
	[SerializeField]
	private SocialType _social_type;

	// Token: 0x04002FA0 RID: 12192
	[SerializeField]
	private Text _text;
}
