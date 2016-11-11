using Core;
using DOL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EntityCache<T>
    {
        /// <summary>
        /// 站内通知全局缓存
        /// </summary>
        /// <returns></returns>
        private List<T> Cache_GetList(T entity)
        {
            var key = CacheHelper.RenderKey(Params.Cache_Prefix_Key, typeof(T).Name);
            return CacheHelper.Get<List<T>>(key, () =>
            {
                using (var db = new DbRepository())
                {
                    var list = new List<T>();
                    switch (typeof(T).Name)
                    {
                        case "User":
                            list = db.User.ToList();
                            break;
                    }
                    return list;
                }
                return null;
            });
        }
    }
}
