namespace IKao.WebAnalytics.Scrape.Infrastructure;

public interface IScrape<TOutput>
{
    public delegate Task BatchCollected(List<TOutput> batch, CancellationToken token);

    public void Execute();
    public Task ExecuteAsync(CancellationToken token);
}