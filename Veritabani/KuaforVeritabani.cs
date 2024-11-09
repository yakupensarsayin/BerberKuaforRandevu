using Microsoft.EntityFrameworkCore;

namespace BerberKuaforRandevu.Veritabani
{
    public class KuaforVeritabani(DbContextOptions<KuaforVeritabani> options) : DbContext(options)
    {

    }
}
