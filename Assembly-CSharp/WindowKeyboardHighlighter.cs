using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200049E RID: 1182
internal class WindowKeyboardHighlighter : MonoBehaviour
{
	// Token: 0x060028A7 RID: 10407 RVA: 0x00146077 File Offset: 0x00144277
	private void OnEnable()
	{
		if (!TouchScreenKeyboard.isSupported)
		{
			Object.Destroy(base.GetComponent<WindowKeyboardHighlighter>());
			return;
		}
		this.findInputFields();
	}

	// Token: 0x060028A8 RID: 10408 RVA: 0x00146094 File Offset: 0x00144294
	private void findInputFields()
	{
		this.noInputs = 0;
		this.rescan = false;
		WindowKeyboardHighlighter.inputFields.Clear();
		foreach (Transform tTransform in base.transform.GetComponentsInChildren<Transform>())
		{
			if (tTransform.HasComponent<InputField>())
			{
				WindowKeyboardHighlighter.inputFields.Add(tTransform.GetComponent<InputField>());
			}
		}
	}

	// Token: 0x060028A9 RID: 10409 RVA: 0x001460EF File Offset: 0x001442EF
	private void OnDisable()
	{
		WindowKeyboardHighlighter.inputFields.Clear();
	}

	// Token: 0x060028AA RID: 10410 RVA: 0x001460FC File Offset: 0x001442FC
	private void up(InputField pInput)
	{
		int desiredHeight = Screen.height / 4 * 3;
		if (pInput.transform.position.y >= (float)desiredHeight)
		{
			return;
		}
		Vector3 tPos = base.gameObject.transform.localPosition;
		tPos.y += 10f;
		base.transform.localPosition = tPos;
	}

	// Token: 0x060028AB RID: 10411 RVA: 0x00146158 File Offset: 0x00144358
	private void down()
	{
		if (base.gameObject.transform.localPosition.y <= 10f)
		{
			return;
		}
		Vector3 tPos = base.gameObject.transform.localPosition;
		tPos.y -= 5f;
		base.transform.localPosition = tPos;
	}

	// Token: 0x060028AC RID: 10412 RVA: 0x001461B0 File Offset: 0x001443B0
	private void Update()
	{
		this.anyFocused = false;
		if (WindowKeyboardHighlighter.inputFields.Count == 0)
		{
			this.noInputs++;
		}
		foreach (InputField tInput in WindowKeyboardHighlighter.inputFields)
		{
			if (!tInput.gameObject.activeInHierarchy)
			{
				this.rescan = true;
			}
			if (tInput.isFocused)
			{
				this.up(tInput);
				this.anyFocused = true;
			}
		}
		if (!this.anyFocused)
		{
			this.down();
		}
		if (this.rescan || this.noInputs > 60)
		{
			this.findInputFields();
		}
	}

	// Token: 0x04001E8B RID: 7819
	private static List<InputField> inputFields = new List<InputField>();

	// Token: 0x04001E8C RID: 7820
	private bool rescan;

	// Token: 0x04001E8D RID: 7821
	private int noInputs;

	// Token: 0x04001E8E RID: 7822
	private bool anyFocused;
}
