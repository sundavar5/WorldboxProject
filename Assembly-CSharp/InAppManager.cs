using System;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

// Token: 0x02000464 RID: 1124
[ObfuscateLiterals]
public class InAppManager : MonoBehaviour, IDetailedStoreListener, IStoreListener
{
	// Token: 0x1700020F RID: 527
	// (get) Token: 0x0600265A RID: 9818 RVA: 0x00139342 File Offset: 0x00137542
	private IAppleExtensions apple
	{
		get
		{
			if (this._apple == null)
			{
				this._apple = this.extensions.GetExtension<IAppleExtensions>();
			}
			return this._apple;
		}
	}

	// Token: 0x17000210 RID: 528
	// (get) Token: 0x0600265B RID: 9819 RVA: 0x00139363 File Offset: 0x00137563
	private IGooglePlayStoreExtensions googleplay
	{
		get
		{
			if (this._googleplay == null)
			{
				this._googleplay = this.extensions.GetExtension<IGooglePlayStoreExtensions>();
			}
			return this._googleplay;
		}
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x00139384 File Offset: 0x00137584
	private void Start()
	{
		Debug.Log("InAppManager::Start");
		if (InAppManager.instance != null)
		{
			Debug.LogError("Multiple in-app managers have been instantiated.");
			return;
		}
		InAppManager.instance = this;
		if (PlayerConfig.instance != null && !PlayerConfig.instance.data.pPossible0507)
		{
			return;
		}
		InAppManager.activatePrem(false);
		Debug.Log("InAppManager::End");
	}

	// Token: 0x0600265D RID: 9821 RVA: 0x001393E2 File Offset: 0x001375E2
	private static bool checkGoogleAccount()
	{
		if (!InAppManager.googleAccount)
		{
			Debug.Log("google account missing");
			ErrorWindow.errorMessage = "A Google Account is missing or you're not logged in with one.";
			ScrollWindow.get("error_with_reason").clickShow(false, false);
		}
		return InAppManager.googleAccount;
	}

	// Token: 0x0600265E RID: 9822 RVA: 0x00139415 File Offset: 0x00137615
	private static void debugPremium()
	{
	}

	// Token: 0x0600265F RID: 9823 RVA: 0x00139418 File Offset: 0x00137618
	private void InitializePurchasing(int pWhere = -1)
	{
		Debug.Log(string.Format("InitializePurchasing {0}", pWhere));
		if (this.IsInitialized())
		{
			return;
		}
		if (InAppManager._initialized)
		{
			return;
		}
		InAppManager._initialized = true;
		InAppManager.instance = this;
		InAppManager._builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), Array.Empty<IPurchasingModule>());
		InAppManager._builder.logUnavailableProducts = true;
		InAppManager._builder.AddProduct("premium", ProductType.NonConsumable, new IDs
		{
			{
				"premium",
				new string[]
				{
					"GooglePlay"
				}
			},
			{
				"premium",
				new string[]
				{
					"AppleAppStore"
				}
			}
		});
		try
		{
			InAppManager.validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
			Debug.Log("validator assigned");
			InAppManager.validator_message = null;
		}
		catch (NotImplementedException exception)
		{
			Debug.Log("validator not assigned");
			Debug.LogError("Cross Platform Validator Not Implemented: " + exception.Message);
			InAppManager.validator_message = "Validator not implemented";
		}
		catch (Exception exception2)
		{
			Debug.Log("validator not assigned");
			Debug.LogError("Validator Exception: " + exception2.Message);
			InAppManager.validator_message = exception2.Message;
		}
		UnityPurchasing.Initialize(this, InAppManager._builder);
	}

	// Token: 0x06002660 RID: 9824 RVA: 0x0013956C File Offset: 0x0013776C
	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
		this.apple.RegisterPurchaseDeferredListener(new Action<Product>(this.OnAskToBuy));
		this.checkPremium();
		this.checkWorldnet();
		Discounts.checkDiscounts();
	}

	// Token: 0x06002661 RID: 9825 RVA: 0x001395A4 File Offset: 0x001377A4
	public void checkPremium()
	{
		bool tValidPurchase = true;
		bool tPurchasePending = false;
		Product tProduct = this.controller.products.WithID("premium");
		Debug.Log("[cp]");
		Debug.Log("[avl] " + tProduct.availableToPurchase.ToString());
		Debug.Log("[txi] " + tProduct.transactionID);
		Debug.Log("[hrc] " + tProduct.hasReceipt.ToString());
		InAppManager.last_tValidPurchase = tValidPurchase;
		InAppManager.last_tPurchasePending = tPurchasePending;
		Config.lockGameControls = false;
		if (!tValidPurchase && tPurchasePending)
		{
			Debug.Log("[nvp] pp");
			return;
		}
		Debug.Log("[vp] 3 " + tValidPurchase.ToString());
		Debug.Log("[hr] " + tProduct.hasReceipt.ToString());
		if ((tValidPurchase && tProduct.hasReceipt) || PlayerConfig.instance.data.premium)
		{
			Debug.Log("[phr]");
			InAppManager.activatePrem(false);
		}
		else
		{
			Debug.Log("[vp] 4 " + tValidPurchase.ToString());
			Debug.Log("[hr] " + tProduct.hasReceipt.ToString());
			Debug.Log("[hp] " + PlayerConfig.instance.data.premium.ToString());
		}
		Debug.Log("[cpd]");
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x0013970A File Offset: 0x0013790A
	public void checkWorldnet()
	{
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x0013970C File Offset: 0x0013790C
	public static void consumePremium()
	{
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x0013970E File Offset: 0x0013790E
	private void OnAskToBuy(Product item)
	{
		Debug.Log("Purchase deferred: " + item.definition.id);
		Config.lockGameControls = false;
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x00139730 File Offset: 0x00137930
	public string getDebugInfo()
	{
		string str = "";
		Product product = this.controller.products.WithID("premium");
		return str + "hasReceipt: " + product.hasReceipt.ToString() + "\nreceipt: " + product.receipt + "\nprem? " + PlayerConfig.instance.data.premium.ToString();
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x001397A0 File Offset: 0x001379A0
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	internal static void activatePrem(bool pShowWindowUnlocked = false)
	{
		Debug.Log("[ap]");
		PlayerConfig.instance.data.premium = true;
		PlayerConfig.saveData();
		Config.hasPremium = true;
		PremiumElementsChecker.checkElements();
		PlayerConfig.setFirebaseProp("have_premium", "yes");
		if (pShowWindowUnlocked)
		{
			if (!PlayerConfig.instance.data.tutorialFinished)
			{
				ScrollWindow.queueWindow("premium_unlocked");
			}
			else
			{
				ScrollWindow.hideAllEvent(false);
				ScrollWindow.showWindow("premium_unlocked");
			}
		}
		InAppManager.debugPremium();
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x0013981B File Offset: 0x00137A1B
	private void activateSub(bool pShowWindowUnlocked = false)
	{
		if (pShowWindowUnlocked && PlayerConfig.instance.data.tutorialFinished)
		{
			ScrollWindow.hideAllEvent(false);
			ScrollWindow.showWindow("worldnet_sub");
		}
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x00139841 File Offset: 0x00137A41
	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Cannot initialize IAP system: " + error.ToString());
		Config.lockGameControls = false;
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x00139865 File Offset: 0x00137A65
	public void OnInitializeFailed(InitializationFailureReason error, string message)
	{
		Debug.Log("Cannot initialize IAP system: " + error.ToString());
		Debug.Log(message);
		Config.lockGameControls = false;
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x00139890 File Offset: 0x00137A90
	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
	{
		Config.lockGameControls = true;
		string str = "Purchase OK: ";
		string str2;
		if (args == null)
		{
			str2 = null;
		}
		else
		{
			Product purchasedProduct = args.purchasedProduct;
			if (purchasedProduct == null)
			{
				str2 = null;
			}
			else
			{
				ProductDefinition definition = purchasedProduct.definition;
				str2 = ((definition != null) ? definition.id : null);
			}
		}
		Debug.Log(str + str2);
		string str3 = "Args: ";
		Product product = (args != null) ? args.purchasedProduct : null;
		Debug.Log(str3 + ((product != null) ? product.ToString() : null));
		string a;
		if (args == null)
		{
			a = null;
		}
		else
		{
			Product purchasedProduct2 = args.purchasedProduct;
			if (purchasedProduct2 == null)
			{
				a = null;
			}
			else
			{
				ProductDefinition definition2 = purchasedProduct2.definition;
				a = ((definition2 != null) ? definition2.id : null);
			}
		}
		if (a == "premium")
		{
			Debug.Log("[pp] process premium");
			return this.ProcessPremium(args);
		}
		string a2;
		if (args == null)
		{
			a2 = null;
		}
		else
		{
			Product purchasedProduct3 = args.purchasedProduct;
			if (purchasedProduct3 == null)
			{
				a2 = null;
			}
			else
			{
				ProductDefinition definition3 = purchasedProduct3.definition;
				a2 = ((definition3 != null) ? definition3.id : null);
			}
		}
		if (a2 == "worldnet")
		{
			Debug.Log("[pw] process worldnet");
			return this.ProcessWorldnet(args);
		}
		Debug.Log("[pn] process nothing to be done");
		return PurchaseProcessingResult.Complete;
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x0013998C File Offset: 0x00137B8C
	public PurchaseProcessingResult ProcessPremium(PurchaseEventArgs args)
	{
		bool validPurchase = true;
		bool purchasePending = false;
		Debug.Log("[prp]");
		if (!validPurchase && purchasePending)
		{
			Debug.Log("[np] 3");
			Config.lockGameControls = false;
			return PurchaseProcessingResult.Pending;
		}
		string text;
		if (args == null)
		{
			text = null;
		}
		else
		{
			Product purchasedProduct = args.purchasedProduct;
			if (purchasedProduct == null)
			{
				text = null;
			}
			else
			{
				ProductDefinition definition = purchasedProduct.definition;
				text = ((definition != null) ? definition.id : null);
			}
		}
		string tPurchasedProduct = text;
		Debug.Log("[vp] 2 " + validPurchase.ToString());
		Debug.Log("[lc] " + tPurchasedProduct);
		if (validPurchase && string.Equals(tPurchasedProduct, "premium", StringComparison.OrdinalIgnoreCase))
		{
			InAppManager.activatePrem(true);
			Debug.Log(string.Format("[ppp] '{0}'", tPurchasedProduct));
		}
		else
		{
			Debug.Log("[np] 4");
		}
		Config.lockGameControls = false;
		InAppManager.debugPremium();
		return PurchaseProcessingResult.Complete;
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x00139A4C File Offset: 0x00137C4C
	public PurchaseProcessingResult ProcessWorldnet(PurchaseEventArgs args)
	{
		bool validPurchase = true;
		bool purchasePending = false;
		PlayerConfig.instance.data.worldnet = "";
		if (!validPurchase && purchasePending)
		{
			Debug.Log("purchase pending");
			Config.lockGameControls = false;
			return PurchaseProcessingResult.Pending;
		}
		Debug.Log("check if valid");
		Config.lockGameControls = false;
		if (validPurchase && string.Equals(args.purchasedProduct.definition.id, "worldnet", StringComparison.OrdinalIgnoreCase))
		{
			Debug.Log("valid!");
			this.setWorldnetSubscription(args.purchasedProduct.transactionID);
			this.activateSub(true);
			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
		}
		Debug.Log("we are here");
		if (Config.lockGameControls)
		{
			Debug.Log("lockgamecontrosl locked");
		}
		else
		{
			Debug.Log("lockgamecontrosl not locked");
		}
		return PurchaseProcessingResult.Complete;
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x00139B24 File Offset: 0x00137D24
	public void setWorldnetSubscription(string pTransactionID)
	{
		PlayerConfig.instance.data.worldnet = pTransactionID;
		PlayerConfig.saveData();
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x00139B3B File Offset: 0x00137D3B
	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		Debug.Log("[D] WORLDBOX PURCHASE FAILED  " + p.ToString());
		Config.lockGameControls = false;
		ScrollWindow.showWindow("premium_purchase_error");
		this.InitializePurchasing(50);
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x00139B75 File Offset: 0x00137D75
	public void OnPurchaseFailed(Product i, PurchaseFailureDescription p)
	{
		Debug.Log("[X] WORLDBOX PURCHASE FAILED  " + p.message);
		this.OnPurchaseFailed(i, p.reason);
	}

	// Token: 0x06002670 RID: 9840 RVA: 0x00139B99 File Offset: 0x00137D99
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void checkPremiumReceipt(string pReceipt, ref bool validPurchase, ref bool purchasePending)
	{
	}

	// Token: 0x06002671 RID: 9841 RVA: 0x00139B9B File Offset: 0x00137D9B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void checkWorldnetReceipt(string pReceipt, ref bool validPurchase, ref bool purchasePending)
	{
	}

	// Token: 0x06002672 RID: 9842 RVA: 0x00139B9D File Offset: 0x00137D9D
	public bool buyPremium()
	{
		InAppManager.activatePrem(true);
		Config.lockGameControls = false;
		return true;
	}

	// Token: 0x06002673 RID: 9843 RVA: 0x00139BAC File Offset: 0x00137DAC
	public bool buyWorldNet()
	{
		return false;
	}

	// Token: 0x06002674 RID: 9844 RVA: 0x00139BB0 File Offset: 0x00137DB0
	private bool BuyProductID(string productId)
	{
		if (!this.IsInitialized())
		{
			ScrollWindow.showWindow(productId + "_purchase_error");
			Debug.Log("BuyProductID FAIL. Not initialized.");
			Config.lockGameControls = false;
			return false;
		}
		Product product = this.controller.products.WithID(productId);
		if (product == null)
		{
			return false;
		}
		if (product.availableToPurchase)
		{
			Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
			Config.lockGameControls = true;
			this.controller.InitiatePurchase(product);
			if (ScrollWindow.windowLoaded("premium_menu") && ScrollWindow.isCurrentWindow("premium_menu"))
			{
				ScrollWindow.get("premium_menu").clickHide("right");
			}
			return true;
		}
		ScrollWindow.showWindow(productId + "_purchase_error");
		Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
		Config.lockGameControls = false;
		return false;
	}

	// Token: 0x06002675 RID: 9845 RVA: 0x00139C84 File Offset: 0x00137E84
	public void RestorePurchases()
	{
		InAppManager.restore_message = "Restoring...";
		InAppManager.restore_ui_buffering = true;
		if (Config.isEditor)
		{
			InAppManager.restore_ui_buffering = false;
		}
		if (!this.IsInitialized())
		{
			this.InitializePurchasing(66);
			Debug.Log("RestorePurchases FAIL. Not initialized.");
			InAppManager.restore_message = "IAP not initialized, failed to restore purchases.";
			return;
		}
	}

	// Token: 0x06002676 RID: 9846 RVA: 0x00139CD3 File Offset: 0x00137ED3
	private bool IsInitialized()
	{
		return this.controller != null && this.extensions != null;
	}

	// Token: 0x04001CEF RID: 7407
	public static InAppManager instance;

	// Token: 0x04001CF0 RID: 7408
	private static bool _initialized = false;

	// Token: 0x04001CF1 RID: 7409
	private IAppleExtensions _apple;

	// Token: 0x04001CF2 RID: 7410
	private IGooglePlayStoreExtensions _googleplay;

	// Token: 0x04001CF3 RID: 7411
	internal IStoreController controller;

	// Token: 0x04001CF4 RID: 7412
	private IExtensionProvider extensions;

	// Token: 0x04001CF5 RID: 7413
	internal static CrossPlatformValidator validator;

	// Token: 0x04001CF6 RID: 7414
	public static bool last_availableToPurchase;

	// Token: 0x04001CF7 RID: 7415
	public static string last_transactionID;

	// Token: 0x04001CF8 RID: 7416
	public static bool last_hasReceipt;

	// Token: 0x04001CF9 RID: 7417
	public static bool last_tValidPurchase;

	// Token: 0x04001CFA RID: 7418
	public static bool last_tPurchasePending;

	// Token: 0x04001CFB RID: 7419
	internal static bool googleAccount = true;

	// Token: 0x04001CFC RID: 7420
	private static ConfigurationBuilder _builder;

	// Token: 0x04001CFD RID: 7421
	public static bool restore_ui_buffering;

	// Token: 0x04001CFE RID: 7422
	public static string restore_message;

	// Token: 0x04001CFF RID: 7423
	public static string validator_message;
}
