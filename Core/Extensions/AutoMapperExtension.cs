using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class AutoMapperExtension
    {



        /// <summary>
        /// 自动映射
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>映射结果</returns>
        public static TDestination AutoMap<TSource, TDestination>(this TSource source, TDestination destination) where TDestination : class
        {

            List<TSource> sourceList = new List<TSource>() { source };
            List<TDestination> destinationList = new List<TDestination>() { destination };

            return sourceList.AutoMap<TSource, TDestination>(destinationList).First();
        }

        /// <summary>
        /// 自动映射
        /// </summary>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <returns>映射结果</returns>
        public static TDestination AutoMap<TSource, TDestination>(this TSource source) where TDestination : class
        {
            List<TSource> list = new List<TSource>() { source };
            return list.AutoMap<TSource, TDestination>().First();
        }

        /// <summary>
        /// 自动映射列表
        /// </summary>
        /// <typeparam name="TSource">原始类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="sourceList">原始列表</param>
        /// <returns></returns>
        public static List<TDestination> AutoMap<TSource, TDestination>(this List<TSource> sourceList, List<TDestination> destinationList = null) where TDestination : class
        {
            Dictionary<object, TDestination> targetDictionary = new Dictionary<object, TDestination>();

            if (destinationList != null)
            {
                //如果有目标原始值时的操作

                foreach (var source in sourceList)
                {
                    //创建autoMapper映射
                    AutoMapper.Mapper.Configuration.CreateMap<TSource, TDestination>();

                    //使用autoMapper映射                
                    int index = sourceList.IndexOf(source);
                    if (destinationList.Count > index)
                    {
                        TDestination target = AutoMapper.Mapper.Map<TSource, TDestination>(source, destinationList[index]);
                        targetDictionary.Add(source, target);
                    }
                    else
                    {

                        TDestination target = AutoMapper.Mapper.Map<TSource, TDestination>(source);
                        targetDictionary.Add(source, target);
                    }
                }
            }
            else
            {

                foreach (var source in sourceList)
                {
                    //创建autoMapper映射
                    AutoMapper.Mapper.CreateMap<TSource, TDestination>();

                    //使用autoMapper映射                
                    TDestination target = AutoMapper.Mapper.Map<TSource, TDestination>(source);
                    targetDictionary.Add(source, target);
                }
            }

            return targetDictionary.Select(x => x.Value).ToList();
        }

        /// <summary>
        /// 自动映射分页列表
        /// </summary>
        /// <typeparam name="TSource">原始类型</typeparam>
        /// <typeparam name="TDestination">目标类型</typeparam>
        /// <param name="source">原始对象</param>
        /// <returns></returns>
        public static PageList<TDestination> AutoMap<TSource, TDestination>(this PageList<TSource> source) where TDestination : class
        {
            PageList<TDestination> list = new PageList<TDestination>(source.List.AutoMap<TSource, TDestination>(), source.PageIndex, source.PageSize, source.RecordCount);
            return list;
        }

    }
}
