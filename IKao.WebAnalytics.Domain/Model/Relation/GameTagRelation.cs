namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameTagRelation
{
    protected GameTagRelation() { }
    
    public GameTagRelation(int gameId, int tagId)
    {
        GameId = gameId;
        TagId = tagId;
    }
    
    public int GameId { get; set; }
    public int TagId { get; set; }
    public Game Game { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}