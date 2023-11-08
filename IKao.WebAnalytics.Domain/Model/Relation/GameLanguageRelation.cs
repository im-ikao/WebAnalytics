using IKao.WebAnalytics.Domain.ValueObjects;

namespace IKao.WebAnalytics.Domain.Model.Relation;

public class GameLanguageRelation
{
    protected GameLanguageRelation() { }
    
    public GameLanguageRelation(AppId gameId, int languageId)
    {
        GameId = gameId;
        LanguageId = languageId;
    }


    public AppId GameId { get; set; }
    public int LanguageId { get; set; }

}