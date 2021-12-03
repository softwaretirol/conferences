namespace DdcWorkshop.Data.Contracts;

public interface ISessionService
{
    Task<ICollection<Session>> GetAll();
    IAsyncEnumerable<Session> GetAllAsStream();
}