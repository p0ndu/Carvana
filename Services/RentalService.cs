using Carvana.Data;
using Microsoft.EntityFrameworkCore;

namespace Carvana.Services;

public class RentalService : IRentalService
{
    private readonly ApplicationDbContext _context;

    public RentalService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    
    public async Task<IEnumerable<RentalContract>> GetAllContractsAsync()
    {
        return await _context.RentalContracts.ToListAsync();
    }

    public async Task<RentalContract?> GetContractByIdAsync(Guid id)
    {
        return await _context.RentalContracts.FindAsync(id);
    }

    public async Task<RentalContract>CreateContractAsync(RentalContract rentalContract)
    {
        _context.RentalContracts.Add(rentalContract);
        await _context.SaveChangesAsync();
        
        return rentalContract;
    }

    public async Task<RentalContract> UpdateContractAsync(RentalContract rentalContract)
    {
        _context.RentalContracts.Update(rentalContract);
        await _context.SaveChangesAsync();
        
        return rentalContract;
    }

    public async Task<bool> DeleteContractAsync(Guid id)
    {
        var contract = await _context.RentalContracts.FindAsync(id);
        
        if (contract != null)
        {
            _context.RentalContracts.Remove(contract);
            await _context.SaveChangesAsync();

            return true;
        } 
        return false;
    }
    
}