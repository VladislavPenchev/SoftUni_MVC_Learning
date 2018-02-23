namespace CameraBazaar.Web.Controllers
{
    using CameraBazaar.Data.Models;
    using CameraBazaar.Services;
    using CameraBazaar.Web.Models.Cameras;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CamerasController : Controller
    {
        private readonly ICameraService _cameras;
        private readonly UserManager<User> _userManager;
        public CamerasController(UserManager<User> userManager, ICameraService cameras)
        {
            this._cameras = cameras;
            this._userManager = userManager;
        }


        [Authorize]
        //[Route(nameof(Add))]
        public IActionResult Add() => this.View();


        [Authorize]
        [HttpPost]
        public IActionResult Add(AddCameraViewModel cameraModel)
        {
            if (!ModelState.IsValid)
            {
                return View(cameraModel);
            }

            this._cameras.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MAxShutterSpeed,
                cameraModel.MinIso,
                cameraModel.MaxIso,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this._userManager.GetUserId(User)
                );

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
