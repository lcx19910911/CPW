using Core;
using Model;
using DOL.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public partial class WebService
    {

        /// <summary>
        /// 更新实例
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool SetConfig(Config item)
        {
            using (CBDEntities entities = new CBDEntities())
            {
                if (item == null || string.IsNullOrEmpty(item.Name))
                    return false;
                var source = entities.Company.FirstOrDefault(x => x.Id == 1);
                if (source != null)
                {
                    source.Name = item.Name;
                    source.QQ = item.QQ;
                    source.Email = item.Email;
                    source.Phone = item.Phone;
                    source.TelePhone = item.TelePhone;
                    source.Fax = item.Fax;
                    source.ZipCode = item.ZipCode;
                    source.Address = item.Address;
                    source.ConnectPeople = item.ConnectPeople;
                    source.AbouyUs = item.AbouyUs;
                    source.History = item.History;

                    if (HttpContext.Current.Request.Files["AbouyUsImage"] != null)
                    {
                        source.AbouyUsImage = OSSHelper.Upload(Enum.System.OSSFileCode.Image, Guid.NewGuid().ToString() + ".jpg", HttpContext.Current.Request.Files["AbouyUsImage"].InputStream, "image/jpeg");
                    }
                    if (HttpContext.Current.Request.Files["CarouselImage1"] != null)
                    {
                        source.CarouselImage1 = OSSHelper.Upload(Enum.System.OSSFileCode.Image, Guid.NewGuid().ToString() + ".jpg", HttpContext.Current.Request.Files["CarouselImage1"].InputStream, "image/jpeg");
                    }
                    if (HttpContext.Current.Request.Files["CarouselImage2"] != null)
                    {
                        source.CarouselImage2 = OSSHelper.Upload(Enum.System.OSSFileCode.Image, Guid.NewGuid().ToString() + ".jpg", HttpContext.Current.Request.Files["CarouselImage2"].InputStream, "image/jpeg");
                    }
                    if (HttpContext.Current.Request.Files["CarouselImage3"] != null)
                    {
                        source.CarouselImage3 = OSSHelper.Upload(Enum.System.OSSFileCode.Image, Guid.NewGuid().ToString() + ".jpg", HttpContext.Current.Request.Files["CarouselImage3"].InputStream, "image/jpeg");
                    }
                    if (source.CreateTime == null)
                        source.CreateTime = DateTime.Now;

                    return entities.SaveChanges() > 0 ? true : false;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
