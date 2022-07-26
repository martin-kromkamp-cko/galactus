namespace Processing.Configuration.MerchantCategoryCodes;

public interface IMerchantCategoryCodeRepository
{
    IQueryable<MerchantCategoryCode> All();

    Task<MerchantCategoryCode> AddAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken);

    Task<MerchantCategoryCode> UpdateAsync(MerchantCategoryCode merchantCategoryCode, CancellationToken cancellationToken);
}