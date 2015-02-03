using MVCInputMask.Attributes;
using MVCInputMask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInputMask.Controllers
{
    [NgController(typeof(MyViewModel), InheritanceFriendly = true, Name = "MyViewModelController")]
    public class MyViewModelController : Controller
    {
        public ActionResult AngularIndex()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new MyViewModel()
            {
                Date = DateTime.Now,
                PhoneNumber = ""
            });
        }

        [HttpPost]
        public ActionResult Create(MyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                return RedirectToAction("Create");
            }
        }
    }
}
