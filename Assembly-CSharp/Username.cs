using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

// Token: 0x020005C6 RID: 1478
public class Username
{
	// Token: 0x0600307E RID: 12414 RVA: 0x00176F2C File Offset: 0x0017512C
	public static bool isValid(string strToCheck)
	{
		return false;
	}

	// Token: 0x0600307F RID: 12415 RVA: 0x00176F30 File Offset: 0x00175130
	public static Task<bool> isTaken(string pUsername)
	{
		Username.<isTaken>d__1 <isTaken>d__;
		<isTaken>d__.<>t__builder = AsyncTaskMethodBuilder<bool>.Create();
		<isTaken>d__.pUsername = pUsername;
		<isTaken>d__.<>1__state = -1;
		<isTaken>d__.<>t__builder.Start<Username.<isTaken>d__1>(ref <isTaken>d__);
		return <isTaken>d__.<>t__builder.Task;
	}
}
