namespace CatsWebApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    [Route("mycats")]
    public class CatsController
    {
        //mycats/mycreate
        [Route("mycreate")]
        public object Create()
        {
            return "Neshto drugo";
        }

        // mycats/mydetails
        [Route("mydetails")]
        public object Details(int id)
        {
            return "Test";
        }
    }
}
