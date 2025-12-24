using System;
using RSG;

// Token: 0x020005BA RID: 1466
public static class Auth
{
	// Token: 0x06003062 RID: 12386 RVA: 0x00176C4E File Offset: 0x00174E4E
	public static void initializeAuth()
	{
		if (Auth.initialized)
		{
			return;
		}
		Auth.initialized = true;
	}

	// Token: 0x06003063 RID: 12387 RVA: 0x00176C5E File Offset: 0x00174E5E
	public static void AuthStateChanged(object sender, EventArgs eventArgs)
	{
	}

	// Token: 0x06003064 RID: 12388 RVA: 0x00176C60 File Offset: 0x00174E60
	public static void signOut()
	{
	}

	// Token: 0x06003065 RID: 12389 RVA: 0x00176C62 File Offset: 0x00174E62
	public static bool isValidUsername(string username)
	{
		return false;
	}

	// Token: 0x06003066 RID: 12390 RVA: 0x00176C65 File Offset: 0x00174E65
	public static bool isValidEmail(string email)
	{
		return false;
	}

	// Token: 0x04002490 RID: 9360
	public static UserLoginWindow userLoginWindow;

	// Token: 0x04002491 RID: 9361
	public static bool isLoggedIn = false;

	// Token: 0x04002492 RID: 9362
	public static string userId;

	// Token: 0x04002493 RID: 9363
	public static string userName;

	// Token: 0x04002494 RID: 9364
	public static string displayName;

	// Token: 0x04002495 RID: 9365
	public static string emailAddress;

	// Token: 0x04002496 RID: 9366
	private static bool initialized = false;

	// Token: 0x04002497 RID: 9367
	public static bool authLoaded = false;

	// Token: 0x04002498 RID: 9368
	public static Promise authLoadedPromise = new Promise();
}
