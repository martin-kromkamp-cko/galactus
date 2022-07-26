namespace Processing.Configuration.Schemes;

public interface ICardSchemeRepository
{
    IQueryable<CardScheme> All();
    
    Task<CardScheme> AddAsync(CardScheme cardScheme, CancellationToken cancellationToken);

    Task<CardScheme> UpdateAsync(CardScheme cardScheme, CancellationToken cancellationToken);
}