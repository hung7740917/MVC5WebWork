using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5WebWork.ActionFilter
{
    public class 紀錄執行時間Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ActionStartTime = DateTime.Now;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.ActionEndTime = DateTime.Now;
            filterContext.Controller.ViewBag.ActionDuration = filterContext.Controller.ViewBag.ActionEndTime - filterContext.Controller.ViewBag.ActionEndTime;
            Debug.WriteLine("Action執行時間：" + $"{ Convert.ToString(filterContext.Controller.ViewBag.ActionDuration)}");
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.ResultStartTime = DateTime.Now;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.ViewBag.ResultEndTime = DateTime.Now;
            filterContext.Controller.ViewBag.ResultDuration = filterContext.Controller.ViewBag.ResultEndTime - filterContext.Controller.ViewBag.ResultEndTime;
            Debug.WriteLine("Result執行時間：" + $"{ Convert.ToString(filterContext.Controller.ViewBag.ActionDuration)}");
        }
    }
}