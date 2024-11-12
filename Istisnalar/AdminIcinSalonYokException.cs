namespace BerberKuaforRandevu.Istisnalar
{
    public class AdminIcinSalonYokException : Exception
    {
        public AdminIcinSalonYokException() : base("Adminin sorumlu olduğu salon bulunamadı.") 
        {

        }
    }

}
