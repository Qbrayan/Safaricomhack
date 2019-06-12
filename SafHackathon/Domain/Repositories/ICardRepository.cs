using SafHackathon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Domain.Repositories
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> ListAsync();
        Task<Card> InsertAsync(string entity);
        Task<IEnumerable<Card>> GetBySerialAsync(string serial);
        Task<Card> Update(string status);
        Task<int> DeleteAsync(string status);
        Task<IEnumerable<Card>> GetByDate();
    }
}
