using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020005E5 RID: 1509
public class LoadingScreen : MonoBehaviour
{
	// Token: 0x06003182 RID: 12674 RVA: 0x0017A83C File Offset: 0x00178A3C
	private void setupBg()
	{
		float wScreen = (float)Screen.width;
		float hScreen = (float)Screen.height;
		if (this.lastBgHeight == hScreen && this.lastBgWidth == wScreen && this.canvas.scaleFactor == this.lastCScale)
		{
			return;
		}
		this.lastBgWidth = wScreen;
		this.lastBgHeight = hScreen;
		this.lastCScale = this.canvas.scaleFactor;
		float wBg = (float)this.background.mainTexture.width * this.canvas.scaleFactor;
		float hBg = (float)this.background.mainTexture.height * this.canvas.scaleFactor;
		float tModWidth = (float)Screen.width / wBg;
		float tModHeight = (float)Screen.height / hBg;
		if (tModWidth > tModHeight)
		{
			this.background.transform.localScale = new Vector3(tModWidth, tModWidth, 1f);
			return;
		}
		this.background.transform.localScale = new Vector3(tModHeight, tModHeight, 1f);
	}

	// Token: 0x06003183 RID: 12675 RVA: 0x0017A930 File Offset: 0x00178B30
	private void Awake()
	{
		InitLibraries.initMainLibs();
		Config.enableAutoRotation(false);
		base.transform.localPosition = default(Vector3);
		if (this.inGameScreen)
		{
			this.outTimer = 0.3f;
			this.canvasGroup.alpha = 1f;
			this.appearDone = true;
			this.bar.transform.localScale = new Vector3(1f, 1f, 1f);
			return;
		}
		this.canvasGroup.alpha = 0f;
		this.bar.transform.localScale = new Vector3(0f, 1f, 1f);
	}

	// Token: 0x06003184 RID: 12676 RVA: 0x0017A9E0 File Offset: 0x00178BE0
	private void startAction()
	{
		ScrollWindow.hideAllEvent(false);
		this.modeIn = false;
		if (Config.isMobile && !Config.hasPremium)
		{
			Debug.Log("PremiumElementsChecker.goodForInterstitialAd(): " + PremiumElementsChecker.goodForInterstitialAd().ToString());
			if (PremiumElementsChecker.goodForInterstitialAd())
			{
				if (PlayInterstitialAd.instance.isReady())
				{
					PlayInterstitialAd.instance.showAd();
					PremiumElementsChecker.setInterstitialAdTimer(80);
				}
				else
				{
					PlayInterstitialAd.instance.initAds();
				}
			}
		}
		this.action();
	}

	// Token: 0x06003185 RID: 12677 RVA: 0x0017AA60 File Offset: 0x00178C60
	internal void startTransition(LoadingScreen.TransitionAction pAction)
	{
		Config.enableAutoRotation(false);
		this.action = pAction;
		this.bar.gameObject.SetActive(false);
		this.percents.gameObject.SetActive(false);
		this.topText.gameObject.SetActive(false);
		this.tipText.gameObject.SetActive(false);
		this.mask.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		this.canvasGroup.alpha = 0f;
		this.modeIn = true;
	}

	// Token: 0x06003186 RID: 12678 RVA: 0x0017AAF4 File Offset: 0x00178CF4
	private void OnEnable()
	{
		string textID = "loading_screen_" + Randy.randomInt(1, 22).ToString();
		this.topText.key = textID;
		this.tipText.key = LoadingScreen.getTipID();
		this.topText.updateText(true);
		this.tipText.updateText(true);
		this.topText.gameObject.SetActive(true);
		this.tipText.gameObject.SetActive(true);
	}

	// Token: 0x06003187 RID: 12679 RVA: 0x0017AB74 File Offset: 0x00178D74
	internal static string getTipID()
	{
		if (LoadingScreen._max_tip == 0)
		{
			int i = 0;
			while (i < 1000 && LocalizedTextManager.stringExists(LoadingScreen.getTip(i)))
			{
				LoadingScreen._max_tip = i;
				i++;
			}
		}
		int tTip = Randy.randomInt(0, LoadingScreen._max_tip + 1);
		if (tTip == LoadingScreen._last_tip)
		{
			return LoadingScreen.getTipID();
		}
		LoadingScreen._last_tip = tTip;
		return LoadingScreen.getTip(tTip);
	}

	// Token: 0x06003188 RID: 12680 RVA: 0x0017ABD4 File Offset: 0x00178DD4
	internal static string getTip(int pTip)
	{
		string tTipString = pTip.ToString();
		return "tip" + Toolbox.fillLeft(tTipString, 3, '0');
	}

	// Token: 0x06003189 RID: 12681 RVA: 0x0017ABFC File Offset: 0x00178DFC
	private void Update()
	{
		if (!string.IsNullOrEmpty(SmoothLoader.latest_called_id))
		{
			this.loadingHelperText.text = SmoothLoader.latest_called_id + ":" + SmoothLoader.latest_time;
		}
		else
		{
			this.loadingHelperText.text = "";
		}
		if (!this.inGameScreen)
		{
			if (!this.appearDone)
			{
				this.canvasGroup.alpha += Time.deltaTime;
				if (this.canvasGroup.alpha < 1f)
				{
					return;
				}
				this.appearDone = true;
				base.StartCoroutine(this.LoadGame());
			}
			float tVal = this.bar.transform.localScale.x;
			if (this.bar.transform.localScale.x < this.asyncLoad.progress)
			{
				tVal = this.bar.transform.localScale.x + Time.deltaTime * 2f;
				if (tVal > this.asyncLoad.progress)
				{
					tVal = this.asyncLoad.progress;
				}
				this.bar.transform.localScale = new Vector3(tVal, 1f, 1f);
			}
			this.percents.text = Mathf.CeilToInt(this.asyncLoad.progress * 100f).ToString() + " %";
			if (tVal >= 0.9f)
			{
				if (!this.asyncLoad.allowSceneActivation)
				{
					Analytics.LogEvent("preloading_done", true, true);
				}
				this.asyncLoad.allowSceneActivation = true;
			}
			return;
		}
		if (this.modeIn)
		{
			if (this.canvasGroup.alpha >= 1f)
			{
				this.startAction();
			}
			this.canvasGroup.alpha += Time.deltaTime * 2f;
			return;
		}
		if (this.outTimer > 0f)
		{
			this.outTimer -= Time.deltaTime;
			return;
		}
		if (this.canvasGroup.alpha <= 0f)
		{
			Config.enableAutoRotation(true);
			base.gameObject.SetActive(false);
		}
		if (!SmoothLoader.isLoading())
		{
			this.canvasGroup.alpha -= Time.fixedDeltaTime * 2f;
		}
	}

	// Token: 0x0600318A RID: 12682 RVA: 0x0017AE34 File Offset: 0x00179034
	private IEnumerator LoadGame()
	{
		this.asyncLoad = SceneManager.LoadSceneAsync("World");
		this.asyncLoad.allowSceneActivation = false;
		while (!this.asyncLoad.isDone)
		{
			yield return null;
		}
		yield break;
	}

	// Token: 0x04002554 RID: 9556
	public Image background;

	// Token: 0x04002555 RID: 9557
	public CanvasGroup canvasGroup;

	// Token: 0x04002556 RID: 9558
	public Text percents;

	// Token: 0x04002557 RID: 9559
	public LocalizedText topText;

	// Token: 0x04002558 RID: 9560
	public LocalizedText tipText;

	// Token: 0x04002559 RID: 9561
	public Image bar;

	// Token: 0x0400255A RID: 9562
	public Image mask;

	// Token: 0x0400255B RID: 9563
	private AsyncOperation asyncLoad;

	// Token: 0x0400255C RID: 9564
	private bool appearDone;

	// Token: 0x0400255D RID: 9565
	public bool inGameScreen;

	// Token: 0x0400255E RID: 9566
	internal bool modeIn;

	// Token: 0x0400255F RID: 9567
	public LoadingScreen.TransitionAction action;

	// Token: 0x04002560 RID: 9568
	private float outTimer;

	// Token: 0x04002561 RID: 9569
	public Canvas canvas;

	// Token: 0x04002562 RID: 9570
	public Text loadingHelperText;

	// Token: 0x04002563 RID: 9571
	private static int _last_tip = -1;

	// Token: 0x04002564 RID: 9572
	private static int _max_tip = 0;

	// Token: 0x04002565 RID: 9573
	private float lastBgWidth;

	// Token: 0x04002566 RID: 9574
	private float lastBgHeight;

	// Token: 0x04002567 RID: 9575
	private float lastCScale;

	// Token: 0x04002568 RID: 9576
	public bool debugg;

	// Token: 0x02000A94 RID: 2708
	// (Invoke) Token: 0x060050C2 RID: 20674
	public delegate void TransitionAction();
}
