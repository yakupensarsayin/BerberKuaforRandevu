using BerberKuaforRandevu.Models;
using BerberKuaforRandevu.Veritabani;
using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Repository
{
    public class RandevuRepository(KuaforVeritabani context)
    {
        private readonly KuaforVeritabani _context = context;

        public async Task<List<Randevu>?> GetRandevuAsync(Func<IQueryable<Randevu>, IQueryable<Randevu>> query)
        {
                IQueryable<Randevu> dbQuery = _context.Set<Randevu>();
                IQueryable<Randevu> linqQuery = query(dbQuery);
                return await linqQuery.ToListAsync();
        }

    }
}
