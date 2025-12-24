using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005E1 RID: 1505
public class IconClickRotator : MonoBehaviour
{
	// Token: 0x06003173 RID: 12659 RVA: 0x0017A468 File Offset: 0x00178668
	private void Awake()
	{
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.click));
		base.gameObject.AddOrGetComponent<ScrollableButton>();
		this._startRotation = base.transform.rotation;
	}

	// Token: 0x06003174 RID: 12660 RVA: 0x0017A4A8 File Offset: 0x001786A8
	private void click()
	{
		this.startRandomRotation();
	}

	// Token: 0x06003175 RID: 12661 RVA: 0x0017A4B0 File Offset: 0x001786B0
	private void startRandomRotation()
	{
		if (this._rotationRoutine != null)
		{
			base.StopCoroutine(this._rotationRoutine);
		}
		float tRandomAngle = Random.Range(-180f, 180f);
		Quaternion targetRotation = Quaternion.Euler(0f, 0f, tRandomAngle);
		this._rotationRoutine = base.StartCoroutine(this.RotateTo(targetRotation, 0.2f));
	}

	// Token: 0x06003176 RID: 12662 RVA: 0x0017A50A File Offset: 0x0017870A
	private IEnumerator RotateTo(Quaternion targetRotation, float duration)
	{
		float time = 0f;
		Quaternion initialRotation = base.transform.rotation;
		while (time < duration)
		{
			base.transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, time / duration);
			time += Time.deltaTime;
			yield return null;
		}
		base.transform.rotation = targetRotation;
		yield break;
	}

	// Token: 0x0400254E RID: 9550
	private Quaternion _startRotation;

	// Token: 0x0400254F RID: 9551
	private Coroutine _rotationRoutine;
}
