﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MKMapper
{
    public class MapProperties
    {
        public MapProperties()
        {
            this.MapCollection = true;
        }
        /// <summary>
        /// set all source properties to destination properties which name is equal
        /// </summary>
        /// <param name="SourceObject"></param>
        /// <param name="DesitnationObject"></param>
        /// <returns></returns>
        public T2 Map<T1, T2>(T1 SourceObject, T2 DesitnationObject, List<string> excludes = null)
        {
            if (SourceObject == null)
            {
                return DesitnationObject;
            }
            this.excludes = excludes ?? new List<string>();
            Type destType = DesitnationObject.GetType();
            MappingPath.Clear();
            if (SourceObject is IEnumerable && DesitnationObject is IEnumerable)
            {
                var destItemType = destType.Name.Contains("List") ? destType.GetProperties()[2] :
                          destType.GetProperties()[1];
                foreach (var item in (IEnumerable)SourceObject)
                {
                    destType.GetMethod("Add").Invoke(DesitnationObject,
                        new[] { GetProperties(item, InstanceCreator.CreateObjectInstance(destItemType)) });
                    MappingPath.Clear();
                }
            }
            else
            {
                GetProperties(SourceObject, DesitnationObject);
            }
            return DesitnationObject;
        }

        private object GetProperties(object SourceObject, object DesitnationObject)
        {
            object data = null; Type sourceType = SourceObject.GetType();
            DesitnationObject.GetType().GetProperties().Where(x => !excludes.Contains(x.Name.ToLower()))
                .ToList().ForEach(destProp =>
            {
                if (sourceType.GetProperties().Any(a => a.Name.ToLower() ==
                destProp.Name.ToLower()))
                {
                    var sourceProp = sourceType.GetProperties().
                    Where(a => a.Name.ToLower() == destProp.Name.ToLower()).FirstOrDefault();

                    var SourceValues = sourceProp.GetValue(SourceObject);

                    //Check if source value is of type enumrable
                    if (sourceProp.PropertyType.IsInterface ||
                    (sourceProp.PropertyType.IsGenericType &&
                     sourceProp.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                     && destProp.PropertyType.IsInterface ||
                    (destProp.PropertyType.IsGenericType &&
                     destProp.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    && SourceValues != null && MapCollection)
                    {
                        data = EnumerableAssign(DesitnationObject, destProp, data, sourceType, sourceProp, SourceValues);
                    }
                    //check if source property is object
                    else if (sourceProp.PropertyType.IsClass &&
                        !sourceProp.PropertyType.Name.Contains("String") && SourceValues != null
                        && destProp.PropertyType.IsClass && !destProp.PropertyType.Name.Contains("String") && MapCollection)
                    {
                        data = ObjectAssing(DesitnationObject, destProp, data, sourceType, sourceProp, SourceValues);
                    }
                    //check if source property is value type
                    else if (!sourceProp.PropertyType.IsClass && !sourceProp.PropertyType.IsInterface ||
                        sourceProp.PropertyType.Name.Contains("String"))
                    {

                        var val = OnAssigning != null ? OnAssigning(new AssignedProperty
                        {
                            SourceName = sourceProp.Name,
                            PropertyName = destProp.Name,
                            PropertyType = destProp.PropertyType,
                            SourceValue = SourceValues,
                            sourceObject = SourceObject
                        }) : null;
                        if (val != null)
                        {
                            destProp.SetValue(DesitnationObject, Convert.ChangeType(val, sourceProp.PropertyType));
                        }
                        else
                        {
                            destProp.SetValue(DesitnationObject, SourceValues);
                        }

                    }
                    else if (!MapCollection)
                    {
                        destProp.SetValue(DesitnationObject, null);
                    }
                }

            });

            return DesitnationObject;
        }

        private object ObjectAssing(object DesitnationObject, PropertyInfo destProp, object data,
            Type sourceType, PropertyInfo sourceProp, object SourceValues)
        {
            if (SourceValues != null)
            {
                if (!MappingPath.Keys.Contains(sourceType))
                {
                    MappingPath.Add(sourceType, new List<string>());
                }

                if (!MappingPath[sourceType].Contains(sourceProp.Name) && !MappingPath.Keys.Contains(sourceProp.PropertyType))
                {
                    MappingPath[sourceType].Add(sourceProp.Name);
                    data = InstanceCreator.CreateObjectInstance(destProp);
                    GetProperties(SourceValues, data);
                    MappingPath.Remove(sourceProp.PropertyType);
                    destProp.SetValue(DesitnationObject, data);
                }

            }

            return data;
        }

        private object EnumerableAssign(object DesitnationObject, PropertyInfo destProp, object data, Type sourceType, PropertyInfo sourceProp, object SourceValues)
        {
            if (((IEnumerable)SourceValues).Cast<object>().Count() > 0)
            {
                if (!MappingPath.Keys.Contains(sourceType))
                {
                    MappingPath.Add(sourceType, new List<string>());
                }
                if (!MappingPath[sourceType].Contains(sourceProp.PropertyType.GetGenericArguments()[0].Name)
                && !MappingPath.Keys.Contains(sourceProp.PropertyType.GetGenericArguments()[0]))
                {
                    MappingPath[sourceType].Add(sourceProp.PropertyType.GetGenericArguments()[0].Name);
                    data = InstanceCreator.CreateEnumrableInstance(destProp);

                    foreach (var item in (IEnumerable)SourceValues)
                    {
                        data.GetType().GetMethod("Add").Invoke(data,
                         new[] { GetProperties(item,
                                 Activator.CreateInstance(destProp.PropertyType.GetGenericArguments()[0])) });
                        MappingPath.Remove(sourceProp.PropertyType.GetGenericArguments()[0]);
                    }
                    destProp.SetValue(DesitnationObject, data);
                }

            }

            return data;
        }

        public bool MapCollection { get; set; }
        private Dictionary<Type, List<string>> MappingPath = new Dictionary<Type, List<string>>();
        private List<string> excludes;

        public Func<AssignedProperty, object> OnAssigning { get; set; }
    }

}

