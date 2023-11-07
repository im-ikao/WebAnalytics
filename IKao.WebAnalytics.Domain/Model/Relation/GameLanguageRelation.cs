namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameLanguageRelation
{
    protected GameLanguageRelation() { }
    
    public GameLanguageRelation(int gameId, int languageId)
    {
        GameId = gameId;
        LanguageId = languageId;
    }


    public int GameId { get; set; }
    public int LanguageId { get; set; }
    public Game Game { get; set; } = null!;
    public Language Language { get; set; } = null!;
}