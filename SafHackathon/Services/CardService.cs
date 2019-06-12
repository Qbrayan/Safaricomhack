using SafHackathon.Domain.Models;
using SafHackathon.Domain.Repositories;
using SafHackathon.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafHackathon.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;

        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }
        public async Task<IEnumerable<Card>> ListAsync()
        {
            return await _cardRepository.ListAsync();
        }

        public async Task<Card> InsertAsync(string entity)
        {
            return await _cardRepository.InsertAsync(entity);
        }

        public async Task<Card> Update(string status)
        {
            return await _cardRepository.Update(status);
        }

        public async Task<IEnumerable<Card>> GetBySerialAsync(string serial)
        {
            return await _cardRepository.GetBySerialAsync(serial);
        }

        public async Task<IEnumerable<Card>> GetByDate()
        {
            return await _cardRepository.GetByDate();
        }

        public async Task<int> DeleteAsync(string status)
        {
            return await _cardRepository.DeleteAsync(status);
        }

    }

   
}
