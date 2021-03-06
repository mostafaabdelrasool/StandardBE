﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MKMapper
{
    public static class InstanceCreator
    {
        public static object CreateEnumrableInstance(PropertyInfo source)
        {
            object Instance = null;
            if (source!=null)
            {
                Type Type = source.PropertyType.GetGenericArguments()[0];
                Type ListTpye = typeof(List<>).MakeGenericType(Type);
                Instance = Activator.CreateInstance(ListTpye);
            }
            else
            {
                Instance = Activator.CreateInstance(source.PropertyType);
            }

            return Instance;
        }
        public static object CreateObjectInstance(PropertyInfo source)
        {
            object Instance = null;
            if (source != null)
            {
                Instance = Activator.CreateInstance(source.PropertyType);

            }
            return Instance;
        }
    }
}
