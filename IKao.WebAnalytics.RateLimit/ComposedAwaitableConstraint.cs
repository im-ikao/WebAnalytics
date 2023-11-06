namespace IKao.WebAnalytics.RateLimit;

internal class ComposedAwaitableConstraint : IAwaitableConstraint
{
    private readonly IAwaitableConstraint _awaitableConstraint1;
    private readonly IAwaitableConstraint _awaitableConstraint2;
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    internal ComposedAwaitableConstraint(IAwaitableConstraint awaitableConstraint1, IAwaitableConstraint awaitableConstraint2)
    {
        _awaitableConstraint1 = awaitableConstraint1;
        _awaitableConstraint2 = awaitableConstraint2;
    }

    public IAwaitableConstraint Clone()
    {
        return new ComposedAwaitableConstraint(_awaitableConstraint1.Clone(), _awaitableConstraint2.Clone());
    }

    public async Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
    {
        await _semaphore.WaitAsync(cancellationToken);
        
        IDisposable[] disposables;
        
        try 
        {
            disposables = await Task.WhenAll(_awaitableConstraint1.WaitForReadiness(cancellationToken), _awaitableConstraint2.WaitForReadiness(cancellationToken));
        }
        catch (Exception) 
        {
            _semaphore.Release();
            throw;
        } 
        
        return new DisposeAction(() => 
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
            _semaphore.Release();
        });
    }
}