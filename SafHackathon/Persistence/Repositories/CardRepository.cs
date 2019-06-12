using SafHackathon.Domain.Repositories;
using SafHackathon.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafHackathon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

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

        string RandomString()
        {
            string allowedChars = "0123456789";
            //if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            //if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
        
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < 12)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < 12; ++i)
                    {
                        // Divide the byte into allowedCharSet-sized groups. If the
                        // random value falls into the last group and the last group is
                        // too small to choose from the entire allowedCharSet, ignore
                        // the value in order to avoid biasing the result.
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
            }
        }

        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public async Task<Card> InsertAsync(string entity)
        {
            int range = 5;  //expiry range
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4));
            builder.Append(random.Next(1000, 9999));
            builder.Append(RandomString(3));
            var card = new Card()
            {
                VoucherNumber = random.Next(100000000, 999999999),
                SerialNumber = builder.ToString(),
                ExpiryDate = DateTime.Now.AddMinutes(range),
                VoucherAmount = random.Next(5, 10000),
                Status = "Active",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }
        public async Task<Card> Update(string serial)
        {

            var card = await _context.Cards
               .Where(x => x.SerialNumber.Equals(serial) && x.Status.Equals("active"))
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
               .Where(x => (DateTime.Now.Subtract(x.DateCreated).Days ==0 &&
               DateTime.Now.Subtract(x.DateCreated).Hours == 0 && DateTime.Now.Subtract(x.DateCreated).Minutes <= 3))
               .ToListAsync();
        }


        public async Task<IEnumerable<Card>> GetActive()
        {
            await UpdateExpired();
            return await _context.Cards
               .Where(x => x.Status.Equals("active"))
               .ToListAsync();
        }


        public async Task<int> UpdateExpired()
        {
            string status = "active";
            _context.Cards
              .Where(x => x.Status.Equals(status) && DateTime.Compare(DateTime.Now, x.ExpiryDate) > 0)
              .ToList().ForEach(x => { x.Status = "inactive"; x.DateUpdated = DateTime.Now; });

            //card.Status = "taken";
            //_context.Cards.Update(card);
            return await _context.SaveChangesAsync();

        }


        public async Task<int> DeleteAsync()
        {
            string status = "inactive";
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
