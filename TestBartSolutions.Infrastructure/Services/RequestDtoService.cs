using Microsoft.EntityFrameworkCore;
using TestBartSolutions.Application.Interfaces;
using TestBartSolutions.Application.Repositories;
using TestBartSolutions.Core.Models;
using TestBartSolutions.Infrastructure.DbContext;
using TestBartSolutions.Models;

namespace TestBartSolutions.Infrastructure.Services;

public class RequestDtoService : IRequestDto
{
    
    private readonly IAccountRepository _accountrepository;
    private readonly IContactRepository _contactrepository;
    private readonly IIncidentRepository _incidentrepository;

    public RequestDtoService(IAccountRepository accountrepository, IContactRepository contactrepository,
        IIncidentRepository incidentrepository)
    {
        _accountrepository = accountrepository;
        _contactrepository = contactrepository;
        _incidentrepository = incidentrepository;
    }
    public async Task PostDto(RequestDto requestDto)
    {
        // var accounts = await _accountrepository.GetAll();
        // Console.WriteLine(accounts);
        // if (!accounts.Any(a => a.Name == requestDto.AccountName))
        // {
        //     return NotFound();
        // }
        var accounts =  await _accountrepository.GetAll();
        var account = accounts.First(a => a.Name == requestDto.AccountName);

        var contacts = await _contactrepository.GetAll();
        if (!contacts.Any(c => c.Email == requestDto.Email))
        {
            await _contactrepository.Add(new Contact()
            {
                FirstName = requestDto.FirstName,
                SecondName = requestDto.SecondName,
                Email = requestDto.Email,
                AccountId = account.Id,
                Account = account
            });
            string IncName = StringGenerator.RandomStringGenerator();
            account.IncidentName = IncName;
            account.Incident = new Incident()
            {
                IncidentName = IncName,
                Description = requestDto.Description
            };
            await _accountrepository.Update(account.Id, account);
                
        }
        else
        {
            var contact = contacts.First(c => c.Email == requestDto.Email);
            contact.FirstName = requestDto.FirstName;
            contact.SecondName = requestDto.SecondName;
            if (contact.AccountId != account.Id)
            {
                contact.AccountId = account.Id;
                contact.Account = account;
            }

            await _contactrepository.Update(contact.Id, contact);
        }
    }
}