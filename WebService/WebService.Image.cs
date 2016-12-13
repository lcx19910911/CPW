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
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Image Find_Image(string id)
        {
            if (!id.IsNotNullOrEmpty())
                return null;
            using (DbRepository entities = new DbRepository())
            {
                return entities.Image.Find(id);
            }
            
        }

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public WebResult<PageList<Image>> Get_ImagePageList(
            int pageIndex,
            int pageSize,
            string title,
            DateTime? pushTimeStart,
            DateTime? pushTimeEnd
            )
        {
            using (DbRepository entities = new DbRepository())
            {
                var query = entities.Image.Where(x=>x.IsDelete==false).AsQueryable().AsNoTracking();
                if (title.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.Title.Contains(title));
                }
                if (pushTimeStart.HasValue)
                {
                    query = query.Where(x => x.PushTime.Date > pushTimeStart);
                }
                if (pushTimeEnd.HasValue)
                {
                    var endTime = pushTimeEnd.Value.AddDays(1).Date;
                    query = query.Where(x => x.PushTime.Date < endTime);
                }
                var count = query.Count();
                var list = query.OrderBy(x => x.CreatedTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return ResultPageList(list, pageIndex, pageSize, count);
            }
        }


        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> Add_Image(Image model)
        {
            using (DbRepository entities = new DbRepository())
            {
                model.ID = Guid.NewGuid().ToString("N");
                entities.Image.Add(model);

                model.CreatedTime = DateTime.Now;
                model.UpdatedTime = DateTime.Now;

                if (entities.SaveChanges() > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }

        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public WebResult<bool> Update_Image(Image model)
        {
            using (DbRepository entities = new DbRepository())
            {

                var oldEntity = entities.Image.Find(model.ID);
                if (oldEntity != null)
                {
                    oldEntity.Content = model.Content;
                    oldEntity.Path = model.Path;
                    oldEntity.Title = model.Title;
                    oldEntity.UpdatedTime = DateTime.Now;
                }
                else
                    return Result(false, ErrorCode.sys_param_format_error);

                if (entities.SaveChanges() > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }

        }


        /// <summary>
        /// 删除考试记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public WebResult<bool> Delete_Image(string ids)
        {
            if (!ids.IsNotNullOrEmpty())
            {
                return Result(false, ErrorCode.sys_param_format_error);
            }
            using (DbRepository entities = new DbRepository())
            {
                //找到实体
                entities.Image.Where(x => ids.Contains(x.ID)).ToList().ForEach(x =>
                {
                    x.IsDelete = true;
                });
                if (entities.SaveChanges() > 0)
                {
                    return Result(true);
                }
                else
                {
                    return Result(false, ErrorCode.sys_fail);
                }
            }
        }
    }
}
