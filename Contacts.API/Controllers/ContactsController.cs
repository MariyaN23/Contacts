using Contacts.API.Contracts;
using Contacts.Core.Abstractions;
using Contacts.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactsService _contactsService;
        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactsResponse>>> GetContacts()
        {
            var contacts = await _contactsService.GetAllContacts();
            var response = contacts.Select(c => new ContactsResponse(c.Id, c.Name, c.MobilePhone, c.JobTitle, c.BirthDate));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateContact([FromBody] ContactsRequest request)
        {
            var (contact, error) = Contact.Create(
                Guid.NewGuid(),
                request.name,
                request.mobilePhone,
                request.jobTitle,
                request.birthDate);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }
            var id = await _contactsService.CreateContact(contact);

            return Ok(id);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateContact(Guid id, [FromBody] ContactsRequest request)
        {
            var contactId = await _contactsService.UpdateContact(id, request.name, request.mobilePhone, request.jobTitle, request.birthDate);

            return Ok(contactId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteContact(Guid id)
        {
            var contactId = await _contactsService.DeleteContact(id);

            return Ok(contactId);
        }
    }
}
