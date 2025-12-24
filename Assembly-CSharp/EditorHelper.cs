using System;

// Token: 0x02000452 RID: 1106
public static class EditorHelper
{
	// Token: 0x06002622 RID: 9762 RVA: 0x00138450 File Offset: 0x00136650
	public static bool HasArgument(string pName)
	{
		string[] tArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < tArgs.Length; i++)
		{
			if (tArgs[i].Contains(pName))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06002623 RID: 9763 RVA: 0x00138480 File Offset: 0x00136680
	public static string GetArgument(string pName)
	{
		string[] tArgs = Environment.GetCommandLineArgs();
		for (int i = 0; i < tArgs.Length; i++)
		{
			if (tArgs[i].Contains(pName))
			{
				return tArgs[i + 1];
			}
		}
		return null;
	}
}
