using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Grupo.Infrastructure.Repositories.Base
{
    public class DapperUtils
    {
        [AttributeUsage(AttributeTargets.Property)]
        public class DapperKeyAttribute : Attribute { }

        [AttributeUsage(AttributeTargets.Property)]
        public class DapperIgnoreAttribute : Attribute { }

        /// <summary>
        /// Este método oi criado especificamente para a montagem dos parâmetros quando for insert 
        /// </summary>
        public static PropertyContainer ParsePropertiesInsert<T>(T obj)
        {

            //Caso não tenhamos nenhum impacto remover esse método e usar apenas o ParseProperties
            var propertyContainer = new PropertyContainer();

            var typeName = typeof(T).Name;
            var validKeyNames = new[] { "ID", string.Format("{0}Id", typeName) };

            Type t = obj.GetType();
            var properties = t.GetProperties();

            foreach (var property in properties)
            {
                // Skip reference types (but still include string!)
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    continue;

                // Skip methods specifically ignored
                if (property.IsDefined(typeof(DapperIgnoreAttribute), false))
                    continue;

                var name = property.Name;
                var value = property.GetValue(obj);


                if (property.IsDefined(typeof(DapperKeyAttribute), false) || validKeyNames.Contains(name))
                {
                    propertyContainer.AddId(name, value);
                }
                else
                {
                    propertyContainer.AddValue(name, value);
                }
            }

            return propertyContainer;
        }

        public class PropertyContainer
        {
            private readonly Dictionary<string, object> _ids;
            private readonly Dictionary<string, object> _values;

            #region Properties

            internal IEnumerable<string> IdNames
            {
                get { return _ids.Keys; }
            }

            internal IEnumerable<string> ValueNames
            {
                get { return _values.Keys; }
            }

            internal IEnumerable<string> AllNames
            {
                get { return _ids.Keys.Union(_values.Keys); }
            }

            internal IDictionary<string, object> IdPairs
            {
                get { return _ids; }
            }

            internal IDictionary<string, object> ValuePairs
            {
                get { return _values; }
            }

            internal IEnumerable<KeyValuePair<string, object>> AllPairs
            {
                get { return _ids.Concat(_values); }
            }

            #endregion

            #region Constructor
            internal PropertyContainer()
            {
                _ids = new Dictionary<string, object>();
                _values = new Dictionary<string, object>();
            }
            #endregion

            #region Methods
            internal void AddId(string name, object value)
            {
                _ids.Add(name, value);
            }
            internal void AddValue(string name, object value)
            {
                _values.Add(name, value);
            }
            #endregion
        }
    }
}
