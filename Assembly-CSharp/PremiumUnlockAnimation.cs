using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// Token: 0x020005F4 RID: 1524
public class PremiumUnlockAnimation : MonoBehaviour
{
	// Token: 0x060031CE RID: 12750 RVA: 0x0017C060 File Offset: 0x0017A260
	private void Awake()
	{
		this.aye.transform.localScale = new Vector3(1f, 0f, 1f);
	}

	// Token: 0x060031CF RID: 12751 RVA: 0x0017C088 File Offset: 0x0017A288
	private void Start()
	{
		this.canvasGroup = this.shineFX.GetComponent<CanvasGroup>();
		this.circleFX.SetActive(true);
		this.circleFX.transform.DOScale(Vector3.one, PremiumUnlockAnimation.scaleTime).SetLoops(-1, LoopType.Yoyo);
		this.aye.transform.DOScale(Vector3.one, PremiumUnlockAnimation.scaleTime).SetEase(Ease.OutElastic).SetDelay(PremiumUnlockAnimation.delayTime);
	}

	// Token: 0x060031D0 RID: 12752 RVA: 0x0017C100 File Offset: 0x0017A300
	private void Update()
	{
		this.canvasGroup.alpha += Time.deltaTime / this.fadeDelay;
		this.shineFX.transform.Rotate(new Vector3(0f, 0f, 1f));
	}

	// Token: 0x060031D1 RID: 12753 RVA: 0x0017C14F File Offset: 0x0017A34F
	public void clickClose()
	{
		this.circleFX.gameObject.SetActive(false);
		this.shineFX.gameObject.SetActive(false);
	}

	// Token: 0x040025BA RID: 9658
	public float time;

	// Token: 0x040025BB RID: 9659
	public GameObject circleFX;

	// Token: 0x040025BC RID: 9660
	public GameObject shineFX;

	// Token: 0x040025BD RID: 9661
	public GameObject aye;

	// Token: 0x040025BE RID: 9662
	private CanvasGroup canvasGroup;

	// Token: 0x040025BF RID: 9663
	public float fadeDelay;

	// Token: 0x040025C0 RID: 9664
	private int index;

	// Token: 0x040025C1 RID: 9665
	public Vector3 scaleAdd;

	// Token: 0x040025C2 RID: 9666
	public static float scaleTime = 1f;

	// Token: 0x040025C3 RID: 9667
	public static float delayTime = 0.5f;
}
