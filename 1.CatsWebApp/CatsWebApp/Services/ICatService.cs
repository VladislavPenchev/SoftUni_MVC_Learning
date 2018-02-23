namespace CatsWebApp.Services
{
    using System.Collections.Generic;
    public interface ICatService
    {
        IEnumerable<string> Cats { get; set; }


    }
}
