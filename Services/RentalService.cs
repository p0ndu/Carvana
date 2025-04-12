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

    // Retrieves all rental contracts from the database
    public async Task<IEnumerable<RentalContract>> GetAllContractsAsync()
    {
        return await _context.RentalContracts.ToListAsync(); // Fetch and return all rental contracts
    }

    // Retrieves a rental contract by its unique ID
    public async Task<RentalContract?> GetContractByIdAsync(Guid id)
    {
        return await _context.RentalContracts.FindAsync(id); // Return the contract with the specified ID
    }

    // ------------------- CREATE -------------------

    // Creates a new rental contract and adds it to the database
    public async Task<RentalContract?> CreateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null)
        {
            throw new ArgumentNullException(nameof(rentalContract)); // Ensure the contract is not null
        }

        try
        {
            _context.RentalContracts.Add(rentalContract); // Add the contract to the context
            await _context.SaveChangesAsync(); // Save changes to the database
        }
        catch (DbUpdateException)
        {
            return null; // Return null if there was an error during DB update
        }

        return rentalContract; // Return the newly created contract
    }

    // ------------------- UPDATE -------------------

    // Updates an existing rental contract in the database
    public async Task<RentalContract?> UpdateContractAsync(RentalContract rentalContract)
    {
        if (rentalContract == null)
        {
            throw new ArgumentNullException(nameof(rentalContract)); // Ensure the contract is not null
        }

        try
        {
            _context.RentalContracts.Update(rentalContract); // Mark the contract as modified in the context
            await _context.SaveChangesAsync(); // Save changes to the database
        }
        catch (DbUpdateException)
        {
            return null; // Return null if there was an error during DB update
        }

        return rentalContract; // Return the updated contract
    }

    // ------------------- DELETE -------------------

    // Deletes a rental contract by its unique ID
    public async Task<bool> DeleteContractAsync(Guid id)
    {
        if (Guid.Empty.Equals(id) || id == null)
        {
            throw new ArgumentNullException(nameof(id)); // Ensure the ID is not null or empty
        }

        try
        {
            var contract = await _context.RentalContracts.FindAsync(id);

            if (contract != null)
            {
                _context.RentalContracts.Remove(contract); // Remove the contract from the context
                await _context.SaveChangesAsync(); // Save changes to the database
                return true; // Return true if the contract was successfully deleted
            }
        }
        catch (DbUpdateException)
        {
            return false; // Return false if there was an error during DB update
        }

        return false; // Return false if the contract was not found
    }
}
