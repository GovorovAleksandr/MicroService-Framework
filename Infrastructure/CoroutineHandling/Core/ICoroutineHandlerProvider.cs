namespace CoroutineHandling.Core
{
	internal interface ICoroutineHandlerProvider
	{
		CoroutineHandler Get();
	}
}