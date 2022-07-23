public static class DapperPropertiesHelper
    {
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static DynamicParameters AutoParameterFind(object Model)
        {
            var parameters = new DynamicParameters();
            var prop = Model?.GetType().GetProperties();
            int propCount = prop.Length;
            int number;
            dynamic propValue;

            for (int i = 0; i < propCount; i++)
            {
                propValue = GetPropValue(Model, prop[i].Name);
                if (propValue is int ? (Int32.TryParse(propValue.ToString(), out number) && number > 0) : propValue is not null)
                {
                    parameters.Add(prop[i].Name, propValue/*, GetPropertyType(prop[i].PropertyType.Name)*/);
                }
            }


            return parameters;
        }
        public static DbType GetPropertyType(string name)
        {
            switch (name)
            {
                case "String": return DbType.String;
                case "Int32": return DbType.Int32;
                case "DateTime": return DbType.DateTime;
                case "Object": return DbType.Object;
                case "Boolean": return DbType.Boolean;
                default: return DbType.Object;
            }

        }
    }
