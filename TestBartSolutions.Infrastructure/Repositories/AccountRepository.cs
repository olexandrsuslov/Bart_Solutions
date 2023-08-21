using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;
using TestBartSolutions.Infrastructure.DbContext;

namespace TestBartSolutions.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly APIContext _context;

    public AccountRepository(APIContext context)
    {
        _context = context;
    }
    
        public async Task<IEnumerable<Account>> GetAll()
        {
            return await _context.Accounts.ToListAsync();
        }
        
        public async Task<Account> GetById(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            return account;
        }
        
        public async Task Update(int id, Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    
        public async Task Add(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
        }

    
        public async Task Delete(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

        }

        public bool Exists(int id)
        {
            return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    
}