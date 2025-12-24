using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000453 RID: 1107
public class Encryption
{
	// Token: 0x06002624 RID: 9764 RVA: 0x001384B4 File Offset: 0x001366B4
	public static string EncryptString(string pPlainText, string pPassPhrase)
	{
		byte[] tInitVectorBytes = Encoding.UTF8.GetBytes("sayHiIfUReadThis");
		byte[] tPlainTextBytes = Encoding.UTF8.GetBytes(pPlainText);
		string result;
		using (PasswordDeriveBytes tPassword = new PasswordDeriveBytes(pPassPhrase, null))
		{
			byte[] tKeyBytes = tPassword.GetBytes(32);
			using (RijndaelManaged tSymmetricKey = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			})
			{
				using (ICryptoTransform tEncryptor = tSymmetricKey.CreateEncryptor(tKeyBytes, tInitVectorBytes))
				{
					using (MemoryStream tMemoryStream = new MemoryStream())
					{
						using (CryptoStream tCryptoStream = new CryptoStream(tMemoryStream, tEncryptor, CryptoStreamMode.Write))
						{
							tCryptoStream.Write(tPlainTextBytes, 0, tPlainTextBytes.Length);
							tCryptoStream.FlushFinalBlock();
							result = Convert.ToBase64String(tMemoryStream.ToArray());
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x001385BC File Offset: 0x001367BC
	public static string DecryptString(string pCipherText, string pPassPhrase)
	{
		byte[] tInitVectorBytes = Encoding.UTF8.GetBytes("sayHiIfUReadThis");
		byte[] tCipherTextBytes = Convert.FromBase64String(pCipherText);
		string @string;
		using (PasswordDeriveBytes tPassword = new PasswordDeriveBytes(pPassPhrase, null))
		{
			byte[] tKeyBytes = tPassword.GetBytes(32);
			using (RijndaelManaged tSymmetricKey = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			})
			{
				using (ICryptoTransform tDecryptor = tSymmetricKey.CreateDecryptor(tKeyBytes, tInitVectorBytes))
				{
					using (MemoryStream tMemoryStream = new MemoryStream(tCipherTextBytes))
					{
						using (CryptoStream tCryptoStream = new CryptoStream(tMemoryStream, tDecryptor, CryptoStreamMode.Read))
						{
							byte[] tPlainTextBytes = new byte[tCipherTextBytes.Length];
							int tDecryptedByteCount = tCryptoStream.Read(tPlainTextBytes, 0, tPlainTextBytes.Length);
							@string = Encoding.UTF8.GetString(tPlainTextBytes, 0, tDecryptedByteCount);
						}
					}
				}
			}
		}
		return @string;
	}

	// Token: 0x04001CD9 RID: 7385
	private const string INIT_VECTOR = "sayHiIfUReadThis";

	// Token: 0x04001CDA RID: 7386
	private const int KEY_SIZE = 256;
}
