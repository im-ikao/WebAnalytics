namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameCategoryRelation
{
    protected GameCategoryRelation() { }
    
    public GameCategoryRelation(int gameId, int categoryId)
    {
        GameId = gameId;
        CategoryId = categoryId;
    }
    
    public int GameId { get; set; }
    public int CategoryId { get; set; }
    public Game Game { get; set; } = null!;
    public Category Category { get; set; } = null!;
}