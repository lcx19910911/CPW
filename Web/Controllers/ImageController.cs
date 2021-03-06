﻿
using Core;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace Web.Controllers
{
    /// <summary>
    /// 科目薪资
    /// </summary>
    public class ImageController : BaseController
    {

        [LoginFilter]
        public ViewResult Index()
        {
            return View();
        }




        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [LoginFilter]
        public JsonResult Add(Image entity)
        {
            ModelState.Remove("ID");
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("CreatedTime");
            if (ModelState.IsValid)
            {
                var result = WebService.Add_Image(entity);
                return JResult(result);
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [LoginFilter]
        public JsonResult Update(Image entity)
        {
            ModelState.Remove("UpdatedTime");
            ModelState.Remove("CreatedTime");
            if (ModelState.IsValid)
            {
                var result = WebService.Update_Image(entity);
                return JResult(result);
            }
            else
            {
                return ParamsErrorJResult(ModelState);
            }
        }



        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult GetPageList(int pageIndex,
            int pageSize,
            string title,
            DateTime? pushTimeStart,
            DateTime? pushTimeEnd)
        {
            return JResult(WebService.Get_ImagePageList(pageIndex, pageSize, title, pushTimeStart, pushTimeEnd));
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Find(string id)
        {
            return JResult(WebService.Find_Image(id));
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Delete(string ids)
        {
            return JResult(WebService.Delete_Image(ids));
        }
        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Detial(string id)
        {
            string nonceStr = new CheckCode().CreateVerifyCode();
            string timestamp = GetUnixTimeStamp(DateTime.Now);
            ReturnStr objec = SignatureHelper.GetSignature(timestamp, nonceStr);
            ViewBag.AppId = System.Configuration.ConfigurationManager.AppSettings["appid"];
            ViewBag.NonceStr = objec.nonceStr;
            ViewBag.TimeStamp = objec.timestamp;
            ViewBag.Signature = objec.signature;
            return View(WebService.Find_Image(id));
        }

        /// <summary>
        /// 获得时间戳字符串
        /// </summary>
        /// <param name="dt">当前时间</param>
        /// <returns></returns>
        public string GetUnixTimeStamp(DateTime dt)
        {
            DateTime unixStartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan timeSpan = dt.Subtract(unixStartTime);
            string timeStamp = timeSpan.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }
        
    }
}