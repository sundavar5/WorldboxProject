using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x020005AF RID: 1455
internal class WorkshopAchievements
{
	// Token: 0x06003027 RID: 12327 RVA: 0x00174D88 File Offset: 0x00172F88
	internal static void checkAchievements()
	{
		SteamSDK.steamInitialized.Then(delegate()
		{
			WorkshopAchievements.countUsersWorkshopMaps();
		}).Catch(delegate(Exception err)
		{
			Debug.Log("Error happened while getting users maps");
			Debug.Log(err);
		});
	}

	// Token: 0x06003028 RID: 12328 RVA: 0x00174DE4 File Offset: 0x00172FE4
	internal static Task countUsersWorkshopMaps()
	{
		WorkshopAchievements.<countUsersWorkshopMaps>d__1 <countUsersWorkshopMaps>d__;
		<countUsersWorkshopMaps>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<countUsersWorkshopMaps>d__.<>1__state = -1;
		<countUsersWorkshopMaps>d__.<>t__builder.Start<WorkshopAchievements.<countUsersWorkshopMaps>d__1>(ref <countUsersWorkshopMaps>d__);
		return <countUsersWorkshopMaps>d__.<>t__builder.Task;
	}
}
