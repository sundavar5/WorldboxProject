using System;
using System.IO;
using System.Security.Cryptography;
using Beebyte.Obfuscator;

// Token: 0x020005C5 RID: 1477
[ObfuscateLiterals]
public class RequestHelper
{
	// Token: 0x17000287 RID: 647
	// (get) Token: 0x0600307A RID: 12410 RVA: 0x00176E40 File Offset: 0x00175040
	public static string salt
	{
		get
		{
			if (RequestHelper._salt == "")
			{
				try
				{
					RequestHelper._salt = RequestHelper.SHA256CheckSum(typeof(RequestHelper).Assembly.Location);
				}
				catch (Exception)
				{
					RequestHelper._salt = "err";
				}
			}
			return RequestHelper._salt;
		}
	}

	// Token: 0x0600307B RID: 12411 RVA: 0x00176EA0 File Offset: 0x001750A0
	public static string SHA256CheckSum(string filePath)
	{
		string result;
		using (SHA256 SHA256 = SHA256.Create())
		{
			using (BufferedStream fileStream = new BufferedStream(File.OpenRead(filePath), 1200000))
			{
				result = BitConverter.ToString(SHA256.ComputeHash(fileStream)).Replace("-", string.Empty).ToLower();
			}
		}
		return result;
	}

	// Token: 0x040024D6 RID: 9430
	private static string _salt = "";
}
