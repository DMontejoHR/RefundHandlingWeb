using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RefundHandlingWeb.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RefundHandlingWeb.Controllers
{
    public class HomeController : Controller
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}
        
        public ActionResult Index(Guid id)
        {
            Guid RefundId = id;
            var model = new RealTimeInfo();
            model = RealTimeInfo.getRealTimeInfo(id);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult RefreshIndex(Guid id)
        {
            Guid RACToken = Guid.Parse("A845F26F-1481-4E2B-B716-A2B0CFE8E2BD");
            Guid RefundId = id;
            ARSReference.ARSClient cs = new ARSReference.ARSClient();
            cs.RAC_RARealTimeCheck(RACToken, id);

            return Index(id);
            //var model = new RealTimeInfo();
            //model = RealTimeInfo.getRealTimeInfo(id);

            //return View(model);
        }

        //public ActionResult TicketsinCRM(Guid id)
        //{
        //    var model = new ExtSea();
        //    //model = ExtSea.respuesta();
        //    model.fillRespuestas("7895262886");
        //    return View(model);
        
        //}

        //public ActionResult TicketsinCRM()
        //{
        //    var model = new ExtSea();
        //    //model = ExtSea.respuesta();
        //    model.fillRespuestas("7895262886");
        //    return View(model);

        //}

        public ActionResult TicketsinCRM(string s, Guid id)
        {
            var model = new ExtSea();
            //model = ExtSea.respuesta();
            model.fillRespuestas(s, id);
            return View(model);

        }
        public void SomeTaskAsync(int id)
        {
            AsyncManager.OutstandingOperations.Increment();
            Task.Factory.StartNew(taskId =>
            {
                for (int i = 0; i < 100; i++)
                {
                    Thread.Sleep(200);
                    HttpContext.Application["task" + taskId] = i;
                }
                var result = "result";
                AsyncManager.OutstandingOperations.Decrement();
                AsyncManager.Parameters["result"] = result;
                return result;
            }, id);
        }

        public ActionResult SomeTaskCompleted(string result)
        {
            //Guid RefundId = id;
            //var model = new RealTimeInfo();
            //model = RealTimeInfo.getRealTimeInfo(id);
            //return View(model);
            
            return Content(result, "text/plain");
        }

        public ActionResult SomeTaskProgress(int id)
        {
            
            return Json(new
            {
                Progress = HttpContext.Application["task" + id]
            }, JsonRequestBehavior.AllowGet);
        }
    }
}