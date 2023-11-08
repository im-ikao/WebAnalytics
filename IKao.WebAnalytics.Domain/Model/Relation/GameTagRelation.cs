using IKao.WebAnalytics.Domain.ValueObjects;

namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameTagRelation
{
    protected GameTagRelation() { }
    
    public GameTagRelation(AppId gameId, int tagId)
    {
        GameId = gameId;
        TagId = tagId;
    }
    
    public AppId GameId { get; set; }
    public int TagId { get; set; }
}