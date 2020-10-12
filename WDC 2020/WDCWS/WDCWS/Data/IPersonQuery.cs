using System.Threading.Tasks;

namespace WDCWS.Data
{
    public interface IPersonQuery
    {
        Task<Person[]> GetAll(int page, int pageSize);
        Person Store(Person person);
        Task<Person> Get(int id);
    }
}