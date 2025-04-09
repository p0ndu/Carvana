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

    public async Task<RentalContract?>CreateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null) // if no rental contract passed in
        {
            throw new ArgumentNullException(nameof(rentalContract));
        }

        try
        {
            _context.RentalContracts.Add(rentalContract);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException e) // if error adding or saving changes to DB
        {
            return null;
        }

        return rentalContract;
    }

    public async Task<RentalContract?> UpdateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null) // if argument passed in is missing
        {
            throw new ArgumentNullException(nameof(rentalContract));
        }

        try
        {
            _context.RentalContracts.Update(rentalContract); 
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException) // if error updating or saving changes
        {
            return null;
        }
        
        return rentalContract;
    }

    public async Task<bool> DeleteContractAsync(Guid id)
    {
        if (Guid.Empty.Equals(id) || id == null) // if ID s missing or empty
        {
            throw new ArgumentNullException(nameof(id));
        }

        try
        {
            var contract = await _context.RentalContracts.FindAsync(id); // try find the contract

            if (contract != null) // if contract is found
            {
                _context.RentalContracts.Remove(contract); // remove the contract
                await _context.SaveChangesAsync(); // save db

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