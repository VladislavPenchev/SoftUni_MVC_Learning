namespace CatsWebApp.Services
{
    using System.Collections.Generic;

    public class CatService : ICatService
    {

        public CatService()
        {
            this.Cats = new[] { "Gosho", "Pesho", "Ivan" };
        }

       public IEnumerable<string> Cats { get; set; }
        
    }
}
