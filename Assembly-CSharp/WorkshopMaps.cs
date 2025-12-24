using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RSG;
using Steamworks;
using Steamworks.Data;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x020005B0 RID: 1456
public static class WorkshopMaps
{
	// Token: 0x0600302A RID: 12330 RVA: 0x00174E27 File Offset: 0x00173027
	public static bool workshopAvailable()
	{
		return SteamSDK.steamInitialized != null && SteamSDK.steamInitialized.CurState == PromiseState.Resolved;
	}

	// Token: 0x0600302B RID: 12331 RVA: 0x00174E44 File Offset: 0x00173044
	internal static Promise uploadMap()
	{
		Promise promise = new Promise();
		WorkshopMaps.uploadProgress = 0f;
		WorkshopMapData tData = WorkshopMapData.currentMapToWorkshop();
		SaveManager.currentWorkshopMapData = tData;
		MapMetaData mapMeta = tData.meta_data_map;
		if (SaveManager.currentWorkshopMapData == null)
		{
			promise.Reject(new Exception("Missing world data"));
			return promise;
		}
		if (!MapSizeLibrary.isSizeValid(mapMeta.width))
		{
			promise.Reject(new Exception("Not a valid world size!"));
			return promise;
		}
		if (mapMeta.width != mapMeta.height)
		{
			promise.Reject(new Exception("Not a square world!"));
			return promise;
		}
		MapMetaData meta_data_map = tData.meta_data_map;
		string tMapName = meta_data_map.mapStats.name;
		string tDescription = meta_data_map.mapStats.description;
		if (string.IsNullOrWhiteSpace(tMapName))
		{
			promise.Reject(new Exception("Give your world a name!"));
			return promise;
		}
		if (string.IsNullOrWhiteSpace(tDescription))
		{
			promise.Reject(new Exception("Give your world a description!"));
			return promise;
		}
		string tMapPath = tData.main_path;
		string tPathToPreview = tData.preview_image_path;
		Editor tFile = Editor.NewCommunityFile.WithTag("World");
		if (!string.IsNullOrWhiteSpace(tMapName))
		{
			tFile = tFile.WithTitle(tMapName);
		}
		if (!string.IsNullOrWhiteSpace(tDescription))
		{
			tFile = tFile.WithDescription(tDescription);
		}
		if (!string.IsNullOrWhiteSpace(tPathToPreview))
		{
			tFile = tFile.WithPreviewFile(tPathToPreview);
		}
		if (!string.IsNullOrWhiteSpace(tMapPath))
		{
			tFile = tFile.WithContent(tMapPath);
		}
		tFile = tFile.WithFriendsOnlyVisibility();
		WorkshopMaps.uploadProgressTracker = new WorkshopUploadProgress();
		tFile.SubmitAsync(WorkshopMaps.uploadProgressTracker).ContinueWith(delegate(Task<PublishResult> taskResult)
		{
			if (taskResult.Status != TaskStatus.RanToCompletion)
			{
				promise.Reject(taskResult.Exception.GetBaseException());
				return;
			}
			PublishResult result = taskResult.Result;
			if (!result.Success)
			{
				Debug.LogError("Error when uploading Workshop world");
			}
			if (result.NeedsWorkshopAgreement)
			{
				Debug.Log("w: Needs Workshop Agreement");
				WorkshopUploadingWorldWindow.needsWorkshopAgreement = true;
				WorkshopOpenSteamWorkshop.fileID = result.FileId.ToString();
			}
			if (result.Result != Result.OK)
			{
				Debug.LogError(result.Result);
				promise.Reject(new Exception("Something went wrong: " + result.Result.ToString()));
				return;
			}
			WorkshopMaps.uploaded_file_id = result.FileId;
			World.world.game_stats.data.workshopUploads += 1L;
			WorkshopAchievements.checkAchievements();
			promise.Resolve();
		}, TaskScheduler.FromCurrentSynchronizationContext());
		return promise;
	}

	// Token: 0x0600302C RID: 12332 RVA: 0x0017500C File Offset: 0x0017320C
	internal static Task<List<Steamworks.Ugc.Item>> listWorkshopMaps(bool pOrder = false, bool pByFriends = false)
	{
		WorkshopMaps.<listWorkshopMaps>d__6 <listWorkshopMaps>d__;
		<listWorkshopMaps>d__.<>t__builder = AsyncTaskMethodBuilder<List<Steamworks.Ugc.Item>>.Create();
		<listWorkshopMaps>d__.pOrder = pOrder;
		<listWorkshopMaps>d__.pByFriends = pByFriends;
		<listWorkshopMaps>d__.<>1__state = -1;
		<listWorkshopMaps>d__.<>t__builder.Start<WorkshopMaps.<listWorkshopMaps>d__6>(ref <listWorkshopMaps>d__);
		return <listWorkshopMaps>d__.<>t__builder.Task;
	}

	// Token: 0x0600302D RID: 12333 RVA: 0x00175058 File Offset: 0x00173258
	internal static bool filesPresent(Steamworks.Ugc.Item pEntry)
	{
		if (!Directory.Exists(pEntry.Directory))
		{
			return false;
		}
		string[] tFiles = Directory.GetFiles(pEntry.Directory);
		Debug.Log(string.Concat(new string[]
		{
			"w: ",
			pEntry.Directory,
			" with ",
			tFiles.Length.ToString(),
			" Files"
		}));
		bool haveMap = false;
		bool havePreview = false;
		bool havePreviewSmall = false;
		bool haveMeta = false;
		foreach (string tFile in tFiles)
		{
			if (tFile.Contains("map.wbox"))
			{
				haveMap = true;
			}
			else if (tFile.Contains("map.meta"))
			{
				haveMeta = true;
			}
			else if (tFile.Contains("preview.png"))
			{
				havePreview = true;
			}
			else if (tFile.Contains("preview_small.png"))
			{
				havePreviewSmall = true;
			}
		}
		if (!haveMap)
		{
			Debug.Log("w: Missing Map");
		}
		if (!havePreview)
		{
			Debug.Log("w: Missing Preview");
		}
		if (!havePreviewSmall)
		{
			Debug.Log("w: Missing PreviewSmall");
		}
		if (!haveMeta)
		{
			Debug.Log("w: Missing Meta");
		}
		return haveMeta && haveMap && havePreview && havePreviewSmall;
	}

	// Token: 0x0400244F RID: 9295
	internal static WorkshopUploadProgress uploadProgressTracker = new WorkshopUploadProgress();

	// Token: 0x04002450 RID: 9296
	internal static float uploadProgress = 0f;

	// Token: 0x04002451 RID: 9297
	public static PublishedFileId uploaded_file_id;

	// Token: 0x04002452 RID: 9298
	internal static List<Steamworks.Ugc.Item> foundMaps = new List<Steamworks.Ugc.Item>();
}
