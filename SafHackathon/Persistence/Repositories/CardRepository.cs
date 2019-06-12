using SafHackathon.Domain.Repositories;
using SafHackathon.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafHackathon.Domain.Repositories;
using SafHackathon.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SafHackathon.Persistence.Repositories
{
    
    public class CardRepository : BaseRepository, ICardRepository
    {
        public CardRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<Card>> ListAsync()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> InsertAsync(string entity)
        {
            entity.ToString();
            Random random = new Random();
            var card = new Card()
            {
                VoucherNumber = random.Next(1, 100000000),
                SerialNumber = new string(Enumerable.Repeat(entity, 18)
      .Select(s => s[random.Next(s.Length)]).ToArray()),
                ExpiryDate = new DateTime(2018, 9, 1, 12, 0, 0, 0, DateTimeKind.Local),
                VoucherAmount = random.Next(5, 100000000),
                Status = "Active",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }
        public async Task<Card> Update(string status)
        {

            var card = await _context.Cards
               .Where(x => x.SerialNumber.Equals(status))
               .FirstOrDefaultAsync();

            if (card == null)
            {
                return null;
            }

            card.Status = "taken";
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();

            return card;
        }


        public async Task<IEnumerable<Card>> GetBySerialAsync(string serial)
        {
            return await _context.Cards
               .Where(x => x.SerialNumber.Equals(serial))
               .ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetByDate()
        {
            return await _context.Cards
               .Where(x => (DateTime.Now.Subtract(x.DateCreated).Minutes <=3))
               .ToListAsync();
        }


        public async Task<int> DeleteAsync(string status)
        {
            var existinginative = await _context.Cards.Where(x => x.Status.Equals(status)).ToListAsync();
            if (existinginative == null)
            {
                return 0;
            }
            foreach (Card c in existinginative) {
                _context.Cards.Remove(c);
            }
            return await _context.SaveChangesAsync();

        }
    }
}
