using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000494 RID: 1172
public class ThreadHelper : MonoBehaviour
{
	// Token: 0x060027FD RID: 10237 RVA: 0x001422A2 File Offset: 0x001404A2
	public static void Initialize()
	{
		if (ThreadHelper.IsActive())
		{
			return;
		}
		GameObject gameObject = new GameObject("MainThreadExecuter");
		gameObject.hideFlags = HideFlags.HideAndDontSave;
		Object.DontDestroyOnLoad(gameObject);
		ThreadHelper.instance = gameObject.AddComponent<ThreadHelper>();
	}

	// Token: 0x060027FE RID: 10238 RVA: 0x001422CE File Offset: 0x001404CE
	public static bool IsActive()
	{
		return ThreadHelper.instance != null;
	}

	// Token: 0x060027FF RID: 10239 RVA: 0x001422DB File Offset: 0x001404DB
	public void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06002800 RID: 10240 RVA: 0x001422E8 File Offset: 0x001404E8
	public static void ExecuteInUpdate(Action action)
	{
		List<Action> obj = ThreadHelper.threadEventsQueue;
		lock (obj)
		{
			ThreadHelper.threadEventsQueue.Add(action);
			ThreadHelper.threadEventsQueueEmpty = false;
		}
	}

	// Token: 0x06002801 RID: 10241 RVA: 0x00142334 File Offset: 0x00140534
	public static void InvokeInUpdate(UnityEvent eventParam)
	{
		ThreadHelper.ExecuteInUpdate(delegate
		{
			eventParam.Invoke();
		});
	}

	// Token: 0x06002802 RID: 10242 RVA: 0x00142354 File Offset: 0x00140554
	public void Update()
	{
		if (ThreadHelper.threadEventsQueueEmpty)
		{
			return;
		}
		List<Action> stagedAdEventsQueue = new List<Action>();
		List<Action> obj = ThreadHelper.threadEventsQueue;
		lock (obj)
		{
			stagedAdEventsQueue.AddRange(ThreadHelper.threadEventsQueue);
			ThreadHelper.threadEventsQueue.Clear();
			ThreadHelper.threadEventsQueueEmpty = true;
		}
		foreach (Action stagedEvent in stagedAdEventsQueue)
		{
			if (stagedEvent.Target != null)
			{
				stagedEvent();
			}
		}
	}

	// Token: 0x06002803 RID: 10243 RVA: 0x00142404 File Offset: 0x00140604
	public void OnDisable()
	{
		ThreadHelper.instance = null;
	}

	// Token: 0x06002804 RID: 10244 RVA: 0x0014240C File Offset: 0x0014060C
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
	private static void initMainThread()
	{
		ThreadHelper._main_thread_id = Thread.CurrentThread.ManagedThreadId;
	}

	// Token: 0x06002805 RID: 10245 RVA: 0x0014241D File Offset: 0x0014061D
	public static bool isMainThread()
	{
		if (ThreadHelper._is_main_thread == null)
		{
			ThreadHelper._is_main_thread = new bool?(Thread.CurrentThread.ManagedThreadId == ThreadHelper._main_thread_id);
		}
		return ThreadHelper._is_main_thread.Value;
	}

	// Token: 0x04001E1F RID: 7711
	public static ThreadHelper instance = null;

	// Token: 0x04001E20 RID: 7712
	private static List<Action> threadEventsQueue = new List<Action>();

	// Token: 0x04001E21 RID: 7713
	private static volatile bool threadEventsQueueEmpty = true;

	// Token: 0x04001E22 RID: 7714
	private static int _main_thread_id = -1;

	// Token: 0x04001E23 RID: 7715
	[ThreadStatic]
	private static bool? _is_main_thread;
}
