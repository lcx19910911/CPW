﻿
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service;
using Model;
using System.Text;

namespace Web.Controllers
{
    public class BaseController : Controller
    {

        public ActionResult _404()
        {
            return View();
        }


        public ActionResult _500()
        {
            return View();
        }

        public ActionResult Forbidden()
        {
            return View("Forbidden");
        }
        


        /// <summary>
        /// 返回部分视图的错误页
        /// </summary>
        /// <returns></returns>
        public PartialViewResult PartialError()
        {
            return null;
        }



        private WebClient client = null;

        protected internal WebClient Client
        {
            get
            {
                if (client == null)
                {
                    client = new WebClient(this.HttpContext);
                }
                return client;
            }
        }

        private WebService webService = null;

        protected internal WebService WebService
        {
            get
            {
                if (webService == null)
                {
                    webService = new WebService(this.Client);
                }
                return webService;
            }
        }

        #region Json返回

        /// <summary>
        /// 返回异常编号
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        protected internal JsonResult JResult(ErrorCode code)
        {
            return Json(new
            {
                Code = code,
                ErrorDesc = code.GetDescription()
            }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回异常编号附带自定义消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="appendMsg"></param>
        /// <returns></returns>
        protected internal JsonResult JResult(ErrorCode code, string appendMsg)
        {
            return Json(new
            {
                Code = code,
                ErrorDesc = code.GetDescription(),
                Append = appendMsg
            }, JsonRequestBehavior.AllowGet);
        }


        protected internal JsonResult JResult<T>(T model)
        {
            return Json(new
            {
                Code = ErrorCode.sys_success,
                Result = model
            }, JsonRequestBehavior.AllowGet);
        }


        protected internal JsonResult ParamsErrorJResult(ModelStateDictionary type)
        {
            return Json(new
            {
                Code = ErrorCode.sys_param_format_error,
                ErrorDesc = type.Where(x => x.Value.Errors.Count != 0).FirstOrDefault().Value.Errors.FirstOrDefault()?.ErrorMessage
            }, JsonRequestBehavior.AllowGet);
        }


        protected internal JsonResult JResult<T>(WebResult<T> model)
        {
            if (model.OccurError)
            {
                return JResult(model.Code, model.Append);
            }
            return JResult(model.Result);
        }

        protected internal JsonResult JResult(string model)
        {
            return Json(new
            {
                Code = ErrorCode.sys_success,
                Result = model
            }, JsonRequestBehavior.AllowGet);
        }


        protected internal JsonResult JResult<T>(WebResult<PageList<T>> model, Func<T, object> selector)
        {
            if (model.OccurError)
            {
                return JResult(model.Code);
            }
            return Json(new
            {
                Code = model.Code,
                Result = new
                {
                    RecordCount = model.Result.RecordCount,
                    PageCount = model.Result.PageCount,
                    IsLastPage = model.Result.IsLastPage,
                    List = model.Result.List.Select(selector).ToList()
                }
            }, JsonRequestBehavior.AllowGet);
        }

        protected internal JsonResult JResult<T>(WebResult<List<T>> model, Func<T, object> selector)
        {
            if (model.OccurError)
            {
                return JResult(model.Code);
            }
            return Json(new
            {
                Code = model.Code,
                Result = model.Result != null ? model.Result.Select(selector).ToList() : null
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        protected internal ViewResult View<T>(WebResult<T> model)
        {
            if (model.OccurError)
            {
                return View("Error");
            }
            return View(model.Result);
        }

        protected internal ActionResult ReLogin()
        {
            return RedirectToAction("Index", "Login");
        }


        private User _loginUser = null;

        public User LoginUser
        {
            get
            {
                return _loginUser != null ? _loginUser : CookieHelper.GetCurrentUser();
            }
        }
    }
}