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

    // ------------------- GET -------------------

    public async Task<IEnumerable<RentalContract>> GetAllContractsAsync()
    {
        return await _context.RentalContracts.ToListAsync();
    }

    public async Task<RentalContract?> GetContractByIdAsync(Guid id)
    {
        return await _context.RentalContracts.FindAsync(id);
    }

    // ------------------- CREATE / UPDATE / DELETE -------------------

    public async Task<RentalContract?> CreateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null)
        {
            throw new ArgumentNullException(nameof(rentalContract));
        }

        try
        {
            _context.RentalContracts.Add(rentalContract);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return null;
        }

        return rentalContract;
    }

    public async Task<RentalContract?> UpdateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null)
        {
            throw new ArgumentNullException(nameof(rentalContract));
        }

        try
        {
            _context.RentalContracts.Update(rentalContract);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return null;
        }

        return rentalContract;
    }

    public async Task<bool> DeleteContractAsync(Guid id)
    {
        if (Guid.Empty.Equals(id) || id == null)
        {
            throw new ArgumentNullException(nameof(id));
        }

        try
        {
            var contract = await _context.RentalContracts.FindAsync(id);

            if (contract != null)
            {
                _context.RentalContracts.Remove(contract);
                await _context.SaveChangesAsync();
                return true;
            }
        }
        catch (DbUpdateException)
        {
            return false;
        }

        return false;
    }
}
