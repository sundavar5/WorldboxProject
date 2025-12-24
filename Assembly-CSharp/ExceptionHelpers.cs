using System;
using System.Runtime.ExceptionServices;

// Token: 0x0200048B RID: 1163
internal static class ExceptionHelpers
{
	// Token: 0x060027E6 RID: 10214 RVA: 0x001417EC File Offset: 0x0013F9EC
	public static Exception PrepareForRethrow(Exception exception)
	{
		ExceptionDispatchInfo.Capture(exception).Throw();
		return exception;
	}
}
