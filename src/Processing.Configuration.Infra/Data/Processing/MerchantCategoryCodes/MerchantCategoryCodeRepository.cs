using System.Diagnostics;
using Processing.Configuration.MerchantCategoryCodes;

namespace Processing.Configuration.Infra.Data.Processing.MerchantCategoryCodes;

public class MerchantCategoryCodeRepository : IMerchantCategoryCodeRepository
{
    private readonly ProcessingContext _dbContext;
    
    public MerchantCategoryCodeRepository(ProcessingContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<MerchantCategoryCode> All()
    {
        return _dbContext.MerchantCategoryCodes;
    }

    public async Task<MerchantCategoryCode> AddAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken)
    {
        var newMcc = await _dbContext.MerchantCategoryCodes.AddAsync(merchantCategoryCode, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return newMcc.Entity;
    }

    public async Task<MerchantCategoryCode> UpdateAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken)
    {
        _dbContext.Update(merchantCategoryCode);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return merchantCategoryCode;
    }
}