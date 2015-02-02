using MVCInputMask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCInputMask.Controllers
{
    public class MyViewModelController : Controller
    {
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
