using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

// Token: 0x0200048A RID: 1162
public static class TaskExtensions
{
	// Token: 0x060027E0 RID: 10208 RVA: 0x00141684 File Offset: 0x0013F884
	public static void WaitAndUnwrapException(this Task task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		task.GetAwaiter().GetResult();
	}

	// Token: 0x060027E1 RID: 10209 RVA: 0x001416B0 File Offset: 0x0013F8B0
	public static void WaitAndUnwrapException(this Task task, CancellationToken cancellationToken)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		try
		{
			task.Wait(cancellationToken);
		}
		catch (AggregateException ex)
		{
			ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
			throw ExceptionHelpers.PrepareForRethrow(ex.InnerException);
		}
	}

	// Token: 0x060027E2 RID: 10210 RVA: 0x00141700 File Offset: 0x0013F900
	public static TResult WaitAndUnwrapException<TResult>(this Task<TResult> task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		return task.GetAwaiter().GetResult();
	}

	// Token: 0x060027E3 RID: 10211 RVA: 0x0014172C File Offset: 0x0013F92C
	public static TResult WaitAndUnwrapException<TResult>(this Task<TResult> task, CancellationToken cancellationToken)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		TResult result;
		try
		{
			task.Wait(cancellationToken);
			result = task.Result;
		}
		catch (AggregateException ex)
		{
			throw ExceptionHelpers.PrepareForRethrow(ex.InnerException);
		}
		return result;
	}

	// Token: 0x060027E4 RID: 10212 RVA: 0x00141774 File Offset: 0x0013F974
	public static void WaitWithoutException(this Task task)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		try
		{
			task.Wait();
		}
		catch (AggregateException)
		{
		}
	}

	// Token: 0x060027E5 RID: 10213 RVA: 0x001417AC File Offset: 0x0013F9AC
	public static void WaitWithoutException(this Task task, CancellationToken cancellationToken)
	{
		if (task == null)
		{
			throw new ArgumentNullException("task");
		}
		try
		{
			task.Wait(cancellationToken);
		}
		catch (AggregateException)
		{
			cancellationToken.ThrowIfCancellationRequested();
		}
	}
}
