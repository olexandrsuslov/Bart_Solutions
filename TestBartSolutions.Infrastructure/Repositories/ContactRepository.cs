using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;
using TestBartSolutions.Infrastructure.DbContext;

namespace TestBartSolutions.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly APIContext _context;

    public ContactRepository(APIContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Contact>> GetAll()
    {
        return await _context.Contacts.ToListAsync();
    }
        
    public async Task<Contact> GetById(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);

        return contact;
    }
        
    public async Task Update(int id, Contact contact)
    {
        _context.Entry(contact).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    
    public async Task Add(Contact contact)
    {
        _context.Contacts.Add(contact);
        await _context.SaveChangesAsync();
    }

    
    public async Task Delete(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        _context.Contacts.Remove(contact);
        await _context.SaveChangesAsync();

    }

    
}