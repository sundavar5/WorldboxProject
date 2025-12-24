using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Steamworks.Ugc;
using UnityEngine;

// Token: 0x0200080C RID: 2060
public class WorkshopMapListWindow : MonoBehaviour
{
	// Token: 0x06004088 RID: 16520 RVA: 0x001BA1A4 File Offset: 0x001B83A4
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this._timer = 0.3f;
		foreach (WorkshopMapElement workshopMapElement in this.elements)
		{
			Object.Destroy(workshopMapElement.gameObject);
		}
		this.elements.Clear();
		SteamSDK.steamInitialized.Then(delegate()
		{
			this.prepareList();
		}).Catch(delegate(Exception err)
		{
			Debug.LogError(err);
			ErrorWindow.errorMessage = "Error happened while connecting to Steam Workshop:\n" + err.Message.ToString();
			ScrollWindow.get("error_with_reason").clickShow(false, false);
		});
	}

	// Token: 0x06004089 RID: 16521 RVA: 0x001BA254 File Offset: 0x001B8454
	private void OnDisable()
	{
		this._showQueue.Clear();
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x001BA264 File Offset: 0x001B8464
	private void Update()
	{
		if (this._timer > 0f)
		{
			this._timer -= Time.deltaTime;
		}
		else
		{
			this._timer = 0.015f;
			this.showNextItemFromQueue();
		}
		if (this._no_items)
		{
			this._no_items = false;
			ScrollWindow.showWindow("steam_workshop_empty");
		}
	}

	// Token: 0x0600408B RID: 16523 RVA: 0x001BA2BC File Offset: 0x001B84BC
	private void prepareList()
	{
		WorkshopMapListWindow.<prepareList>d__10 <prepareList>d__;
		<prepareList>d__.<>t__builder = AsyncVoidMethodBuilder.Create();
		<prepareList>d__.<>4__this = this;
		<prepareList>d__.<>1__state = -1;
		<prepareList>d__.<>t__builder.Start<WorkshopMapListWindow.<prepareList>d__10>(ref <prepareList>d__);
	}

	// Token: 0x0600408C RID: 16524 RVA: 0x001BA2F4 File Offset: 0x001B84F4
	private void showNextItemFromQueue()
	{
		if (this._showQueue.Count == 0)
		{
			return;
		}
		Steamworks.Ugc.Item tSteamworksItem = this._showQueue.Dequeue();
		this.renderMapElement(tSteamworksItem);
	}

	// Token: 0x0600408D RID: 16525 RVA: 0x001BA324 File Offset: 0x001B8524
	private WorkshopMapData loadMapDataFromStorage(Steamworks.Ugc.Item pSteamworksItem)
	{
		string tPathPreviewImage = SaveManager.generatePngSmallPreviewPath(pSteamworksItem.Directory);
		WorkshopMapData tData = new WorkshopMapData();
		tData.main_path = pSteamworksItem.Directory;
		tData.workshop_item = pSteamworksItem;
		if (!string.IsNullOrEmpty(tPathPreviewImage) && File.Exists(tPathPreviewImage))
		{
			if (this.cached_sprites.ContainsKey(tPathPreviewImage))
			{
				tData.sprite_small_preview = this.cached_sprites[tPathPreviewImage];
			}
			else
			{
				try
				{
					byte[] pngBytes = File.ReadAllBytes(tPathPreviewImage);
					Texture2D tTexture = new Texture2D(32, 32);
					tTexture.anisoLevel = 0;
					tTexture.filterMode = FilterMode.Point;
					if (tTexture.LoadImage(pngBytes))
					{
						tData.sprite_small_preview = Sprite.Create(tTexture, new Rect(0f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));
						this.cached_sprites.Add(tPathPreviewImage, tData.sprite_small_preview);
					}
				}
				catch (Exception)
				{
				}
			}
		}
		MapMetaData tMetaData = SaveManager.getMetaFor(pSteamworksItem.Directory);
		bool update_meta = false;
		if (!string.IsNullOrWhiteSpace(pSteamworksItem.Title) && tMetaData.mapStats.name != pSteamworksItem.Title)
		{
			tMetaData.mapStats.name = pSteamworksItem.Title;
			update_meta = true;
		}
		if (tMetaData.mapStats.description != pSteamworksItem.Description)
		{
			tMetaData.mapStats.description = pSteamworksItem.Description;
			update_meta = true;
		}
		if (update_meta)
		{
			SaveManager.saveMetaIn(pSteamworksItem.Directory, tMetaData);
		}
		tData.meta_data_map = tMetaData;
		return tData;
	}

	// Token: 0x0600408E RID: 16526 RVA: 0x001BA4AC File Offset: 0x001B86AC
	private void renderMapElement(Steamworks.Ugc.Item pSteamworksItem)
	{
		WorkshopMapElement tElement = Object.Instantiate<WorkshopMapElement>(this.elementPrefab, this.transformContent);
		this.elements.Add(tElement);
		WorkshopMapData tWorldData = this.loadMapDataFromStorage(pSteamworksItem);
		tElement.load(tWorldData);
	}

	// Token: 0x04002EC3 RID: 11971
	public WorkshopMapElement elementPrefab;

	// Token: 0x04002EC4 RID: 11972
	private Dictionary<string, Sprite> cached_sprites = new Dictionary<string, Sprite>();

	// Token: 0x04002EC5 RID: 11973
	private List<WorkshopMapElement> elements = new List<WorkshopMapElement>();

	// Token: 0x04002EC6 RID: 11974
	public Transform transformContent;

	// Token: 0x04002EC7 RID: 11975
	private float _timer;

	// Token: 0x04002EC8 RID: 11976
	private bool _no_items;

	// Token: 0x04002EC9 RID: 11977
	private Queue<Steamworks.Ugc.Item> _showQueue = new Queue<Steamworks.Ugc.Item>();
}
