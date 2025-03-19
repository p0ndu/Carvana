namespace Carvana.Services;

public interface IRentalService
{
    Task<IEnumerable<RentalContract>> GetAllContractsAsync();
    Task<RentalContract?>GetContractByIdAsync(Guid id);
    Task<bool> DeleteContractAsync(Guid id);
    Task<RentalContract> CreateContractAsync(RentalContract contract);
    Task<RentalContract> UpdateContractAsync(RentalContract contract);
}