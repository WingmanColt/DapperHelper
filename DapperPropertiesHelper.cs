// Helper class is getting all properties from your given model, iteriate throgh them on runtime.
// We are using this method to set all parameters of DynamicParameters() without declaring every prop.
// It`s not the best optimized case for performance because the action happens runtime but it will save us a lot of time.

public static class DapperPropertiesHelper
    {
       public static DynamicParameters AutoParameterFind(object Model, string? SkipByAttributeName)
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
                    if(SkipByAttributeName is not null && !HasAttribute(prop[i], SkipByAttributeName))
                    {
                        parameters.Add(prop[i].Name, propValue/*, GetPropertyType(prop[i].PropertyType.Name)*/);
                    }
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
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static bool HasAttribute(this PropertyInfo target, string atrName)
        {
            var attribs = target.GetCustomAttributesData().Any(x => x.AttributeType.Name == atrName + "Attribute");
            return attribs;
        }
    }
