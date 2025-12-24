using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.UI;

// Token: 0x0200084B RID: 2123
public class WindowPremiumRestoreHelper : MonoBehaviour
{
	// Token: 0x06004272 RID: 17010 RVA: 0x001C1F1C File Offset: 0x001C011C
	private void OnEnable()
	{
		this.updateConsoleText();
	}

	// Token: 0x06004273 RID: 17011 RVA: 0x001C1F24 File Offset: 0x001C0124
	public void startRestoreTimeout()
	{
		this._restore_timeout = 6f;
		this._restore_index = 0;
		this._restore_phrases.Shuffle<string>();
		this.updateConsoleText();
	}

	// Token: 0x06004274 RID: 17012 RVA: 0x001C1F4C File Offset: 0x001C014C
	private void Update()
	{
		if (this._restore_timeout > 0f)
		{
			this._restore_timeout -= Time.deltaTime;
			if (Time.frameCount % Randy.randomInt(15, 40) != 0)
			{
				return;
			}
		}
		else if (Time.frameCount % 30 != 0)
		{
			return;
		}
		this.updateConsoleText();
	}

	// Token: 0x06004275 RID: 17013 RVA: 0x001C1F9B File Offset: 0x001C019B
	private void updateConsoleText()
	{
		this._show_caret = !this._show_caret;
		if (InAppManager.restore_ui_buffering || this._restore_timeout > 0f)
		{
			this.showTerminalLoading();
			return;
		}
		this.showTerminalInfo();
	}

	// Token: 0x06004276 RID: 17014 RVA: 0x001C1FD0 File Offset: 0x001C01D0
	private void showTerminalLoading()
	{
		if (this._restore_index < 14 && this._restore_index < this._restore_phrases.Length)
		{
			this._restore_index++;
		}
		using (StringBuilderPool tSB = new StringBuilderPool())
		{
			for (int i = 0; i < this._restore_index; i++)
			{
				float tProgress = Mathf.Clamp01((float)(i + 1) / (float)this._restore_phrases.Length);
				int tPercent = Mathf.RoundToInt(tProgress * 100f);
				int tFilledBlocks = Mathf.RoundToInt(tProgress * 4f);
				int tEmptyBlocks = 4 - tFilledBlocks;
				string tBarColor = this.getBarColor(tProgress);
				string pString = ("[" + new string('█', tFilledBlocks) + new string('░', tEmptyBlocks) + "]").ColorHex(tBarColor, false);
				string pString2 = this._restore_phrases[i].ToLower().PadRight(27);
				string tColoredPercent = string.Format("{0,2}%", tPercent).ColorHex("#FFFF66", false);
				string tColor = (i < this._restore_index - 3) ? "#558855" : WindowPremiumRestoreHelper.COLOR_LEFT_DARK;
				string tLineUser = this.user.ColorHex(tColor, false);
				string tLinePhrase = pString2.ColorHex(tColor, false);
				string tLineBar = pString.ColorHex(tColor, false);
				tSB.AppendLine(string.Concat(new string[]
				{
					tLineUser,
					" ",
					tLinePhrase,
					" ",
					tLineBar,
					" ",
					tColoredPercent
				}));
			}
			tSB.AppendLine(">".ColorHex(WindowPremiumRestoreHelper.COLOR_LEFT_DARK, false));
			this._text_console.text = tSB.ToString();
		}
	}

	// Token: 0x06004277 RID: 17015 RVA: 0x001C218C File Offset: 0x001C038C
	private void showTerminalInfo()
	{
		using (StringBuilderPool tSB = new StringBuilderPool())
		{
			PlayerConfig instance = PlayerConfig.instance;
			PlayerConfigData tData = (instance != null) ? instance.data : null;
			if (!string.IsNullOrEmpty(InAppManager.restore_message))
			{
				tSB.AppendLine((this.user + " " + InAppManager.restore_message).blue());
			}
			if (tData != null && tData.premiumDisabled)
			{
				tSB.AppendLine((this.user + " Premium disabled in debug menu!").red());
			}
			tSB.AppendLine(string.Concat(new string[]
			{
				this.user,
				" Premium active: ",
				(tData != null) ? tData.premium.blue() : null,
				" / ",
				Config.hasPremium.blue()
			}));
			tSB.AppendLine(this.user + " web_status: " + Application.internetReachability.ToString().blue());
			if (InAppManager.instance == null || InAppManager.instance.controller == null)
			{
				tSB.AppendLine((this.user + " InAppManager not initialized").red());
			}
			else
			{
				tSB.AppendLine((this.user + " InAppManager initialized").blue());
				if (!InAppManager.googleAccount)
				{
					tSB.AppendLine((this.user + " Google account missing? Not logged in?").red());
				}
				if (InAppManager.validator == null)
				{
					if (!string.IsNullOrEmpty(InAppManager.validator_message))
					{
						tSB.AppendLine((this.user + " Validator error " + InAppManager.validator_message).red());
					}
					else
					{
						tSB.AppendLine((this.user + " Validator not initialized").red());
					}
				}
				else
				{
					tSB.AppendLine((this.user + " Validator initialized").blue());
				}
				Product tProduct = InAppManager.instance.controller.products.WithID("premium");
				if (tProduct != null)
				{
					tSB.AppendLine(string.Concat(new string[]
					{
						this.user,
						" available: ",
						tProduct.availableToPurchase.blue(),
						" has_receipt: ",
						tProduct.hasReceipt.yellow()
					}));
					if (!tProduct.hasReceipt)
					{
						tSB.AppendLine((this.user + " current user doesn't have a receipt - product not owned").red());
					}
					else
					{
						tSB.AppendLine((this.user + " current user has a receipt!").blue());
					}
					StringBuilderPool stringBuilderPool = tSB;
					string user = this.user;
					string str = " tx: ";
					string transactionID = tProduct.transactionID;
					stringBuilderPool.AppendLine(user + str + ((transactionID != null) ? transactionID.Truncate(26) : null).yellow());
					tSB.AppendLine(string.Concat(new string[]
					{
						this.user,
						" valid: ",
						InAppManager.last_tValidPurchase.blue(),
						" pending: ",
						InAppManager.last_tPurchasePending.blue()
					}));
					if (!tProduct.hasReceipt || InAppManager.validator == null)
					{
						goto IL_551;
					}
					try
					{
						IPurchaseReceipt[] array = InAppManager.validator.Validate(tProduct.receipt);
						int i = 0;
						foreach (IPurchaseReceipt tReceipt in array)
						{
							i++;
							if (tReceipt != null)
							{
								tSB.AppendLine(string.Format("{0} {1} re: {2} {3}", new object[]
								{
									this.user,
									i,
									tReceipt.productID.yellow(),
									tReceipt.purchaseDate.ToString("yyyy-MM-dd HH:mmzzz").blue()
								}));
								tSB.AppendLine(string.Format("{0} {1} tx: {2}", this.user, i, tReceipt.transactionID.yellow()));
								GooglePlayReceipt tGoogle = tReceipt as GooglePlayReceipt;
								if (tGoogle != null)
								{
									tSB.AppendLine(string.Format("{0} {1} re: {2} {3}", new object[]
									{
										this.user,
										i,
										tGoogle.orderID.yellow(),
										tGoogle.purchaseState.blue()
									}));
								}
								AppleInAppPurchaseReceipt tApple = tReceipt as AppleInAppPurchaseReceipt;
								if (tApple != null)
								{
									tSB.AppendLine(string.Format("{0} {1} re: {2} {3}", new object[]
									{
										this.user,
										i,
										tApple.originalTransactionIdentifier.yellow(),
										tApple.originalPurchaseDate.ToString("yyyy-MM-dd HH:mmzzz").blue()
									}));
									tSB.AppendLine(string.Format("{0} {1} re: {2}", this.user, i, tApple.cancellationDate.ToString("yyyy-MM-dd HH:mmzzz").blue()));
									tSB.AppendLine(string.Format("{0} {1} re: {2}", this.user, i, tApple.productType.ToString().yellow()));
								}
							}
						}
						goto IL_551;
					}
					catch (Exception e)
					{
						tSB.AppendLine(string.Format("{0} Exception: {1}", this.user, e));
						goto IL_551;
					}
				}
				tSB.AppendLine((this.user + " Product not found").red());
			}
			IL_551:
			tSB.AppendLine(string.Concat(new string[]
			{
				this.user,
				" op: ",
				ButtonEvent.premium_restore_opened.blue(),
				" res: ",
				ButtonEvent.premium_restore_action_pressed.blue(),
				" more: ",
				string.Format("{0}", ButtonEvent.premium_more_help_pressed).blue()
			}));
			using (ListPool<string> tRandoms = new ListPool<string>(6))
			{
				if (!string.IsNullOrEmpty(Config.gs))
				{
					ListPool<string> listPool = tRandoms;
					string gs = Config.gs;
					listPool.Add(((gs != null) ? gs.Truncate(11) : null) ?? this.generateFakeMD5('G', 4));
				}
				if (tData != null && !tData.pPossible0507)
				{
					tRandoms.Add(this.generateFakeMD5('P', 4));
				}
				while (tRandoms.Count < 6)
				{
					tRandoms.Add(this.generateRandomMD5(4));
				}
				tRandoms.Shuffle<string>();
				for (int j = 0; j < tRandoms.Count; j += 2)
				{
					tSB.AppendLine(string.Concat(new string[]
					{
						this.user,
						" ",
						tRandoms[j].blue(),
						" ",
						tRandoms[j + 1].blue()
					}));
				}
				tSB.AppendLine(this.user + " OS: " + SystemInfo.operatingSystem.blue());
				tSB.AppendLine(this.user + " device: " + SystemInfo.deviceModel.blue());
				tSB.AppendLine(string.Concat(new string[]
				{
					this.user,
					" type: ",
					SystemInfo.deviceType.ToString().ToUpper().Truncate(4).blue(),
					" imode: ",
					Application.installMode.ToString().ToUpper().Truncate(4).blue(),
					" sand: ",
					(Application.sandboxType.ToString().ToUpper().Truncate(4).blue() ?? "").blue()
				}));
				tSB.AppendLine(string.Concat(new string[]
				{
					this.user,
					" v: ",
					Config.versionCodeText.blue(),
					" (",
					Config.gitCodeText.blue(),
					")"
				}));
				if (!Config.hasPremium)
				{
					tSB.AppendLine(this.user + " " + "IF YOU HAVE ISSUES SHOW THIS TO DEVS".red());
				}
				else
				{
					tSB.AppendLine(this.user + " " + "ALL GOOD! Enjoy WorldBox".yellow());
				}
				if (this._show_caret)
				{
					tSB.AppendLine("> █".ColorHex(WindowPremiumRestoreHelper.COLOR_LEFT_DARK, false));
				}
				else
				{
					tSB.AppendLine(">".ColorHex(WindowPremiumRestoreHelper.COLOR_LEFT_DARK, false));
				}
				this._text_console.text = tSB.ToString();
			}
		}
	}

	// Token: 0x06004278 RID: 17016 RVA: 0x001C2A6C File Offset: 0x001C0C6C
	private string generateRandomMD5(int pLength = 4)
	{
		if (pLength <= 0)
		{
			return string.Empty;
		}
		string result;
		using (StringBuilderPool sb = new StringBuilderPool())
		{
			for (int i = 0; i < pLength; i++)
			{
				string tHex = Randy.randomInt(0, 256).ToString("X2");
				sb.Append(tHex);
				sb.Append(':');
			}
			result = sb.ToString().TrimEnd(':');
		}
		return result;
	}

	// Token: 0x06004279 RID: 17017 RVA: 0x001C2AEC File Offset: 0x001C0CEC
	private string generateFakeMD5(char pLetter, int pLength = 4)
	{
		if (pLength <= 0)
		{
			return string.Empty;
		}
		string result;
		using (StringBuilderPool sb = new StringBuilderPool())
		{
			for (int i = 0; i < pLength; i++)
			{
				sb.Append(pLetter);
				sb.Append(pLetter);
				sb.Append(':');
			}
			result = sb.ToString().TrimEnd(':');
		}
		return result;
	}

	// Token: 0x0600427A RID: 17018 RVA: 0x001C2B5C File Offset: 0x001C0D5C
	private string getBarColor(float progress)
	{
		if (progress < 0.3f)
		{
			return "#FF5555";
		}
		if (progress < 0.7f)
		{
			return "#FFFF55";
		}
		return "#55FF55";
	}

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x0600427B RID: 17019 RVA: 0x001C2B7F File Offset: 0x001C0D7F
	private string user
	{
		get
		{
			return "w:/box:".ColorHex(WindowPremiumRestoreHelper.COLOR_LEFT_DARK, false);
		}
	}

	// Token: 0x040030B1 RID: 12465
	[SerializeField]
	private Text _text_console;

	// Token: 0x040030B2 RID: 12466
	private float _restore_timeout;

	// Token: 0x040030B3 RID: 12467
	private static string COLOR_LEFT_DARK = "#45B714";

	// Token: 0x040030B4 RID: 12468
	private const float RESTORE_TIMEOUT = 6f;

	// Token: 0x040030B5 RID: 12469
	private bool _show_caret = true;

	// Token: 0x040030B6 RID: 12470
	private readonly string[] _restore_phrases = new string[]
	{
		"Restoring",
		"Verifying integrity",
		"Authenticating deities",
		"Decrypting receipts",
		"Syncing time",
		"Validating purpose",
		"Loading configs",
		"Rebuilding indexes",
		"Rechecking derps",
		"Clearing temps",
		"Allocating memory",
		"Running diagnostics",
		"Parsing metadata",
		"Linking modules",
		"Thinking",
		"Untangling",
		"Melting",
		"Cooking",
		"Resurrecting skeletons",
		"Negotiating with entropy",
		"Reattaching soul bindings",
		"Refreshing mythos",
		"Loading universal constants",
		"Aligning timelines",
		"Resetting divine counters",
		"Auditing reality logs",
		"Reinitializing worldframe",
		"Binding laws of physics",
		"Sealing causality breaches",
		"Decoding fate instructions",
		"Sanitizing memory cache"
	};

	// Token: 0x040030B7 RID: 12471
	private int _restore_index;
}
