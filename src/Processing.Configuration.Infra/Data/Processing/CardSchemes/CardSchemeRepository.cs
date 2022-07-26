using Processing.Configuration.Schemes;

namespace Processing.Configuration.Infra.Data.Processing;

public class CardSchemeRepository : ICardSchemeRepository
{
    private readonly ProcessingContext _processingContext;

    public CardSchemeRepository(ProcessingContext processingContext)
    {
        _processingContext = processingContext;
    }

    public IQueryable<CardScheme> All()
    {
        return _processingContext.CardSchemes;
    }

    public async Task<CardScheme> AddAsync(CardScheme cardScheme, CancellationToken cancellationToken)
    {
        var newCardScheme = await _processingContext.CardSchemes.AddAsync(cardScheme, cancellationToken);
        await _processingContext.SaveChangesAsync(cancellationToken);

        return newCardScheme.Entity;
    }

    public async Task<CardScheme> UpdateAsync(CardScheme cardScheme, CancellationToken cancellationToken)
    {
        _processingContext.Update(cardScheme);
        await _processingContext.SaveChangesAsync(cancellationToken);

        return cardScheme;
    }
}