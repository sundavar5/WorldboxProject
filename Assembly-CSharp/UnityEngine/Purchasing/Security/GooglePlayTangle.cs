using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000986 RID: 2438
	public class GooglePlayTangle
	{
		// Token: 0x06004705 RID: 18181 RVA: 0x001E21A8 File Offset: 0x001E03A8
		public static byte[] Data()
		{
			if (!GooglePlayTangle.IsPopulated)
			{
				return null;
			}
			return Obfuscator.DeObfuscate(GooglePlayTangle.data, GooglePlayTangle.order, GooglePlayTangle.key);
		}

		// Token: 0x04003202 RID: 12802
		private static byte[] data = Convert.FromBase64String("dMZFZnRJQk1uwgzCs0lFRUVBREdEmfRYkbtqgRbn1uonvTwN/R8KFU6dxJq3OX0iF3cfyyJx8SV7RVGGxkVLRHTGRU5GxkVFRP5Y/5bFSR/VDy6kDuoXivfvEGyjKBQT+9rZmVyqpLrINBBIHwrgnBmw5oYPgCTQFfAkFYtqkxQ7rApQgWXHUO1Yazc8LKUBLugsGw9EnuaTejink6/77p3l5I8yLPPXoWzpZiaKULv3J2oWZA/rW+FQV6LIhhRwCV/P+f9WmvYdd6ZmDDpkn3tjgD+VREl/ATD2fsHxO5PI/rKsCQMWxPlMIo8bIhQj7mVfsjIQHaUvOw4F+T5WKYoyovWQkU1OkQpSqXwrq6UIHOp9hHP01aTEwqMk21zFcUZHRURF");

		// Token: 0x04003203 RID: 12803
		private static int[] order = new int[]
		{
			0,
			6,
			8,
			6,
			11,
			8,
			7,
			9,
			12,
			12,
			10,
			11,
			13,
			13,
			14
		};

		// Token: 0x04003204 RID: 12804
		private static int key = 68;

		// Token: 0x04003205 RID: 12805
		public static readonly bool IsPopulated = true;
	}
}
