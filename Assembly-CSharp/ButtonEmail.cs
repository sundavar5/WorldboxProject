using System;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000827 RID: 2087
public class ButtonEmail : MonoBehaviour
{
	// Token: 0x06004124 RID: 16676 RVA: 0x001BC420 File Offset: 0x001BA620
	public void SendEmail()
	{
		string email = "supworldbox@gmail.com";
		string subject = this.convert("WorldBox Feedback ( " + Application.version + " )");
		string body = this.convert("Yo!\r\n");
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			email,
			"?subject=",
			subject,
			"&body=",
			body
		}));
		Analytics.LogEvent("clicked_send_email", true, true);
	}

	// Token: 0x06004125 RID: 16677 RVA: 0x001BC49C File Offset: 0x001BA69C
	public void SendEmailLogs()
	{
		string email = "supworldbox+errors@gmail.com";
		string subject = this.convert("WorldBox Error Logs ( " + Application.version + " )");
		string body = this.convert("Please take a look at this error :\r\n" + LogHandler.log.Substring(Math.Max(0, LogHandler.log.Length - 4000)));
		Application.OpenURL(string.Concat(new string[]
		{
			"mailto:",
			email,
			"?subject=",
			subject,
			"&body=",
			body
		}));
		Analytics.LogEvent("clicked_send_error_email", true, true);
	}

	// Token: 0x06004126 RID: 16678 RVA: 0x001BC53B File Offset: 0x001BA73B
	private string convert(string url)
	{
		return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
	}
}
