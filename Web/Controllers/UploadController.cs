using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOL.Web.Controllers
{
    [AllowAnonymous]
    public class UploadController : Controller
    {
        // GET: Upload
        public JsonResult UploadImage(string mark)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file != null)
            {
                if (file.ContentLength > 4 * 1024 * 1024)
                {
                    return Json(new { Code=2});
                }
                else
                {
                    string path = UploadHelper.Save(file, mark);
                    return Json(new { Code = 0,Data= path });
                }
            }
            else
                return Json(new { Code = 1 });
        }
    }
}