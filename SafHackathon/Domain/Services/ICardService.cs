using SafHackathon.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Domain.Services
{
    public interface ICardService
    {
        Task<IEnumerable<Card>> ListAsync();
        Task<Card> InsertAsync(string entity);
        Task<IEnumerable<Card>> GetBySerialAsync(string serial);
        Task<IEnumerable<Card>> GetByDate();

        Task<IEnumerable<Card>> GetActive();

        Task<Card> Update(string serial);

        Task<int> DeleteAsync();

    }

   
}
