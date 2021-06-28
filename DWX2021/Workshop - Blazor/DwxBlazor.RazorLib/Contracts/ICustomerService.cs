using System.Collections.Generic;
using System.Threading.Tasks;
using DwxBlazor.RazorLib.Models;

namespace DwxBlazor.RazorLib.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> Get(int id);
    }
}