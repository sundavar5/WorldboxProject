using System;
using Proyecto26;
using RSG;
using Steamworks;
using UnityEngine;

// Token: 0x020005AE RID: 1454
public class SteamSDK : MonoBehaviour
{
	// Token: 0x06003021 RID: 12321 RVA: 0x001744DC File Offset: 0x001726DC
	private void Start()
	{
		if (this._initiated)
		{
			return;
		}
		this._initiated = true;
		bool tDestroy = false;
		try
		{
			SteamSDK._instance = this;
			SteamClient.Init(1206560U, true);
			RestClient.DefaultRequestHeaders["wb-stmc"] = "true";
		}
		catch (Exception message)
		{
			Debug.Log("Disabling Steam Integration");
			Debug.LogWarning(message);
			RestClient.DefaultRequestHeaders["wb-stmc"] = "na";
			tDestroy = true;
			SteamSDK._should_quit = true;
		}
		try
		{
			string tSteamID = SteamClient.SteamId.ToString();
			if (!string.IsNullOrEmpty(tSteamID))
			{
				Config.steam_id = tSteamID;
				RestClient.DefaultRequestHeaders["wb-stm"] = tSteamID;
				Debug.Log("S:" + Config.steam_id);
			}
			else
			{
				Debug.Log("S:nf");
			}
		}
		catch (Exception)
		{
		}
		try
		{
			if (Config.steam_language_allow_detect)
			{
				Debug.Log("s:Detect - Steam detecting language");
				string tSteamLanguage = SteamSDK.getSteamLanguage();
				if (!string.IsNullOrEmpty(tSteamLanguage))
				{
					string curLanguage = LocalizedTextManager.instance.language;
					if (tSteamLanguage == "en" && curLanguage != "en")
					{
						Debug.Log("s:Detect - Already have a language, not falling back to english");
					}
					else
					{
						LocalizedTextManager.instance.setLanguage(tSteamLanguage);
					}
				}
				Debug.Log("s:Detect - language " + tSteamLanguage);
			}
		}
		catch (Exception)
		{
		}
		try
		{
			string tSteamName = SteamClient.Name;
			if (!string.IsNullOrEmpty(tSteamName))
			{
				Config.steam_name = tSteamName;
			}
		}
		catch (Exception)
		{
		}
		try
		{
			if (SteamClient.RestartAppIfNecessary(1206560U))
			{
				Debug.Log("Restart App from Steam launcher");
				SteamSDK._should_quit = true;
				tDestroy = true;
			}
		}
		catch (Exception message2)
		{
			Debug.Log(message2);
		}
		if (SteamSDK._should_quit && !Config.disable_steam)
		{
			Application.Quit();
		}
		if (tDestroy)
		{
			Debug.Log("Steam is not available");
			SteamSDK.steamInitialized.Reject(new Exception("Steam is not available"));
			Object.Destroy(SteamSDK._instance);
			return;
		}
		SteamSDK.steamInitialized.Resolve();
	}

	// Token: 0x06003022 RID: 12322 RVA: 0x001746E8 File Offset: 0x001728E8
	private static string getSteamLanguage()
	{
		string gameLanguage = SteamApps.GameLanguage;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(gameLanguage);
		if (num <= 2471602315U)
		{
			if (num <= 683056061U)
			{
				if (num <= 319214730U)
				{
					if (num != 308944030U)
					{
						if (num != 316123288U)
						{
							if (num == 319214730U)
							{
								if (gameLanguage == "romanian")
								{
									return "ro";
								}
							}
						}
						else if (gameLanguage == "danish")
						{
							return "da";
						}
					}
					else if (gameLanguage == "swedish")
					{
						return "sv";
					}
				}
				else if (num <= 505713757U)
				{
					if (num != 380651494U)
					{
						if (num == 505713757U)
						{
							if (gameLanguage == "brazilian")
							{
								return "br";
							}
						}
					}
					else if (gameLanguage == "russian")
					{
						return "ru";
					}
				}
				else if (num != 599131013U)
				{
					if (num == 683056061U)
					{
						if (gameLanguage == "ukrainian")
						{
							return "ua";
						}
					}
				}
				else if (gameLanguage == "french")
				{
					return "fr";
				}
			}
			else if (num <= 1544226106U)
			{
				if (num <= 793573733U)
				{
					if (num != 693158059U)
					{
						if (num == 793573733U)
						{
							if (gameLanguage == "indonesian")
							{
								return "id";
							}
						}
					}
					else if (gameLanguage == "norwegian")
					{
						return "no";
					}
				}
				else if (num != 1262725376U)
				{
					if (num == 1544226106U)
					{
						if (gameLanguage == "hungarian")
						{
							return "hu";
						}
					}
				}
				else if (gameLanguage == "latam")
				{
					return "es";
				}
			}
			else if (num <= 1703858441U)
			{
				if (num != 1580935484U)
				{
					if (num == 1703858441U)
					{
						if (gameLanguage == "arabic")
						{
							return "ar";
						}
					}
				}
				else if (gameLanguage == "portuguese")
				{
					return "pt";
				}
			}
			else if (num != 1901528810U)
			{
				if (num == 2471602315U)
				{
					if (gameLanguage == "italian")
					{
						return "it";
					}
				}
			}
			else if (gameLanguage == "japanese")
			{
				return "ja";
			}
		}
		else if (num <= 3229236340U)
		{
			if (num > 2805355685U)
			{
				if (num <= 3210859552U)
				{
					if (num != 3180870988U)
					{
						if (num != 3210859552U)
						{
							goto IL_529;
						}
						if (!(gameLanguage == "koreana"))
						{
							goto IL_529;
						}
					}
					else
					{
						if (!(gameLanguage == "polish"))
						{
							goto IL_529;
						}
						return "pl";
					}
				}
				else if (num != 3222531841U)
				{
					if (num != 3229236340U)
					{
						goto IL_529;
					}
					if (!(gameLanguage == "finnish"))
					{
						goto IL_529;
					}
					return "fn";
				}
				else if (!(gameLanguage == "korean"))
				{
					goto IL_529;
				}
				return "ko";
			}
			if (num != 2499415067U)
			{
				if (num != 2798875500U)
				{
					if (num == 2805355685U)
					{
						if (gameLanguage == "schinese")
						{
							return "cz";
						}
					}
				}
				else if (gameLanguage == "czech")
				{
					return "cs";
				}
			}
			else if (gameLanguage == "english")
			{
				return "en";
			}
		}
		else if (num <= 3719199419U)
		{
			if (num <= 3405445907U)
			{
				if (num != 3264533134U)
				{
					if (num == 3405445907U)
					{
						if (gameLanguage == "german")
						{
							return "de";
						}
					}
				}
				else if (gameLanguage == "tchinese")
				{
					return "ch";
				}
			}
			else if (num != 3426057626U)
			{
				if (num == 3719199419U)
				{
					if (gameLanguage == "spanish")
					{
						return "es";
					}
				}
			}
			else if (gameLanguage == "vietnamese")
			{
				return "vn";
			}
		}
		else if (num <= 3759690811U)
		{
			if (num != 3739448251U)
			{
				if (num == 3759690811U)
				{
					if (gameLanguage == "thai")
					{
						return "th";
					}
				}
			}
			else if (gameLanguage == "turkish")
			{
				return "tr";
			}
		}
		else if (num != 4151292721U)
		{
			if (num == 4263372803U)
			{
				if (gameLanguage == "greek")
				{
					return "gr";
				}
			}
		}
		else if (gameLanguage == "dutch")
		{
			return "nl";
		}
		IL_529:
		return string.Empty;
	}

	// Token: 0x06003023 RID: 12323 RVA: 0x00174C24 File Offset: 0x00172E24
	private void OnDisable()
	{
		try
		{
			SteamClient.Shutdown();
		}
		catch (Exception message)
		{
			Debug.LogWarning(message);
			Object.Destroy(SteamSDK._instance);
		}
	}

	// Token: 0x06003024 RID: 12324 RVA: 0x00174C5C File Offset: 0x00172E5C
	private void OnDestroy()
	{
		SteamSDK._instance = null;
	}

	// Token: 0x04002449 RID: 9289
	public const uint STEAM_APP_ID = 1206560U;

	// Token: 0x0400244A RID: 9290
	internal static Promise steamInitialized = new Promise();

	// Token: 0x0400244B RID: 9291
	private bool _initiated;

	// Token: 0x0400244C RID: 9292
	private static SteamSDK _instance;

	// Token: 0x0400244D RID: 9293
	private static bool _should_quit = false;

	// Token: 0x0400244E RID: 9294
	private static readonly string[] _supported_steam_languages = new string[]
	{
		"ar",
		"cz",
		"ch",
		"cs",
		"da",
		"nl",
		"en",
		"fn",
		"fr",
		"de",
		"gr",
		"hu",
		"it",
		"ja",
		"ko",
		"no",
		"pl",
		"pt",
		"br",
		"ro",
		"ru",
		"es",
		"es",
		"sv",
		"th",
		"tr",
		"ua",
		"vn"
	};
}
