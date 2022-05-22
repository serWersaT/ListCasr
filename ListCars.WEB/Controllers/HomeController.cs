using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListCars.DAL.Models;
using ListCars.BLL.Service;

namespace ListCars.WEB.Controllers
{
    public class HomeController : Controller
    {
        Service service = new Service();
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult _GetAllCars()
        {
            return PartialView("Views/PartialViews/_GetAllCars.cshtml");
        }

        public IActionResult _GetAddCars()
        {
            return PartialView("Views/PartialViews/_GetAddCars.cshtml");
        }

        public IActionResult _GetStat()
        {
            return PartialView("Views/PartialViews/_GetStat.cshtml");
        }


        [HttpPost]
        public JsonResult AddCars([FromBody] CarModel model)
        {
            return Json(service.InsertCar(model));
        }


        [HttpPost]
        public JsonResult SelectAllCars([FromBody] NavigateModel model)
        {
            return Json(service.SelectCar(model));
        }

        [HttpPost]
        public JsonResult DeleteCars([FromBody] int id)
        {
            return Json(service.DeleteCar(id));
        }

        [HttpPost]
        public JsonResult UpdateCars([FromBody] CarModel model)
        {
            return Json(service.UpdateCar(model));
        }

        [HttpPost]
        public JsonResult SelectGeneralStatReports([FromBody] NavigateModel model)
        {
            return Json(service.SelectGeneralReports(model));
        }

        [HttpPost]
        public JsonResult SelectCarReport([FromBody] NavigateModel model)
        {
            return Json(service.SelectCarReports(model));
        }
    }
}
