namespace CameraBazaar.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public User()
        {
            LightMetering some = LightMetering.Spot | LightMetering.evaluative;
        }

        public List<Camera> Cameras { get; set; } = new List<Camera>();

    }
}
