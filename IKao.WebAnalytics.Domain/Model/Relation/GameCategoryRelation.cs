using IKao.WebAnalytics.Domain.ValueObjects;

namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameCategoryRelation
{
    protected GameCategoryRelation() { }
    
    public GameCategoryRelation(AppId gameId, int categoryId)
    {
        GameId = gameId;
        CategoryId = categoryId;
    }
    
    public AppId GameId { get; set; }
    public int CategoryId { get; set; }

}