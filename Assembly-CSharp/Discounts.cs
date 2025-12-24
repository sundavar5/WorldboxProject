using System;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;
using UnityEngine.Purchasing;

// Token: 0x02000451 RID: 1105
internal static class Discounts
{
	// Token: 0x0600261F RID: 9759 RVA: 0x00138240 File Offset: 0x00136440
	internal static void checkDiscounts()
	{
		try
		{
			Discounts.checkPlatform();
			if (InAppManager.instance != null)
			{
				InAppManager instance = InAppManager.instance;
				bool flag;
				if (instance == null)
				{
					flag = (null != null);
				}
				else
				{
					IStoreController controller = instance.controller;
					flag = (((controller != null) ? controller.products : null) != null);
				}
				if (flag)
				{
					Product product = InAppManager.instance.controller.products.WithID("premium");
					if (product != null)
					{
						Discounts.discountRequest(product.metadata);
						goto IL_71;
					}
					Debug.Log("DC:no req/prod");
					goto IL_71;
				}
			}
			Debug.Log("DC:np");
			IL_71:;
		}
		catch (Exception message)
		{
			Debug.Log("DC:err");
			Debug.Log(message);
		}
	}

	// Token: 0x06002620 RID: 9760 RVA: 0x001382E4 File Offset: 0x001364E4
	private static void discountRequest(ProductMetadata pProductMeta)
	{
		if (Discounts.platform.Length < 2)
		{
			return;
		}
		if (pProductMeta == null)
		{
			return;
		}
		string vURL = "https://currency.superworldbox.com/discounts/" + Discounts.platform + ".json?" + Toolbox.cacheBuster();
		string tPostData = JsonConvert.SerializeObject(pProductMeta, new JsonSerializerSettings
		{
			DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
		});
		if (string.IsNullOrEmpty(tPostData))
		{
			return;
		}
		if (tPostData == "{}")
		{
			return;
		}
		RestClient.Post(vURL, tPostData).Then(delegate(ResponseHelper response)
		{
			string tResponse = response.Text;
			if (string.IsNullOrEmpty(tResponse))
			{
				return;
			}
			if (tResponse == "{}")
			{
				return;
			}
			if (tResponse.Substring(0, 1) != "{")
			{
				return;
			}
			Debug.Log(tResponse);
			DiscountData currencyData = JsonConvert.DeserializeObject<DiscountData>(tResponse);
			Debug.Log("DS:Setting");
			if (!string.IsNullOrEmpty(currencyData.discount) && !string.IsNullOrEmpty(currencyData.price_current) && !string.IsNullOrEmpty(currencyData.price_old))
			{
				LocalizedTextPrice.discount = currencyData.discount;
				LocalizedTextPrice.price_current = currencyData.price_current;
				LocalizedTextPrice.price_old = currencyData.price_old;
				Debug.Log("DS:Set");
				return;
			}
			Debug.Log("DS:NSet");
		}).Catch(delegate(Exception err)
		{
			Debug.Log("DS:err");
			Debug.Log(err.Message);
		});
	}

	// Token: 0x06002621 RID: 9761 RVA: 0x00138398 File Offset: 0x00136598
	private static void checkPlatform()
	{
		RuntimePlatform runtimePlatform = Application.platform;
		if (runtimePlatform <= RuntimePlatform.Android)
		{
			switch (runtimePlatform)
			{
			case RuntimePlatform.OSXEditor:
				Discounts.platform = "mac";
				return;
			case RuntimePlatform.OSXPlayer:
				Discounts.platform = "mac";
				return;
			case RuntimePlatform.WindowsPlayer:
				Discounts.platform = "pc";
				return;
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.OSXDashboardPlayer:
			case RuntimePlatform.WindowsWebPlayer:
			case (RuntimePlatform)6:
				break;
			case RuntimePlatform.WindowsEditor:
				Discounts.platform = "pc";
				return;
			case RuntimePlatform.IPhonePlayer:
				Discounts.platform = "ios";
				return;
			default:
				if (runtimePlatform == RuntimePlatform.Android)
				{
					Discounts.platform = "android";
					return;
				}
				break;
			}
		}
		else
		{
			if (runtimePlatform == RuntimePlatform.LinuxPlayer)
			{
				Discounts.platform = "linux";
				return;
			}
			if (runtimePlatform == RuntimePlatform.LinuxEditor)
			{
				Discounts.platform = "linux";
				return;
			}
		}
		Discounts.platform = "unknown";
	}

	// Token: 0x04001CD7 RID: 7383
	private static ProductMetadata localPriceData;

	// Token: 0x04001CD8 RID: 7384
	private static string platform;
}
