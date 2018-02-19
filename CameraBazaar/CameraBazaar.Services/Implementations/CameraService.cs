namespace CameraBazaar.Services.Implementations
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Data;
    using System.Collections.Generic;
    using System.Linq;

    public class CameraService : ICameraService
    {
        private readonly CameraBazaarDbContext _db;

        public CameraService(CameraBazaarDbContext db)
        {
            this._db = db;
        }

        public void Create(
            CameraMake make,
            string model,
            decimal price,
            int quantity,
            int minShutterSpeed,
            int maxShutterSpeed,
            MinISo minIso,
            int maxISO,
            bool isFullFrame,
            string videoResolution,
            IEnumerable<LightMetering> lightMeterings,
            string description,
            string imageUrl,
            string userId)
        {

            LightMetering lightMetering = lightMeterings.First();
            foreach (var lightMeteringvalue in lightMeterings.Skip(1))
            {
                lightMetering |= lightMeteringvalue; 
            }

            var camera = new Camera
            {
                Make = make,
                Model = model,
                Price = price,
                Quantity = quantity,
                MinShutterSpeed = minShutterSpeed,
                MAxShutterSpeed = maxShutterSpeed,
                MinIso = minIso,
                IsFullFrame = isFullFrame,
                VideoResolution = videoResolution,
                LightMetering = (LightMetering)lightMeterings.Cast<int>().Sum(),
                Description = description,
                ImageUrl = imageUrl,
                UserId = userId
            };


            this._db.Add(camera);
            this._db.SaveChanges();
        }
    }
}
