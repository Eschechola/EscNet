using System;
using System.Reflection;

namespace ESCHENet.ExtensionMethods
{
    public static class StringMethods
    {
        public static string GetStringValue(this Enum value)
        {
            //pega o valor do enum
            string stringValue = value.ToString();
            
            //pega o tipo do enum
            Type type = value.GetType();

            //pega a informação do campo de acordo com o valor do enum
            FieldInfo fieldInfo = type.GetField(value.ToString());
            
            //pega o vetor de valores do enum chamado
            StringValue[] attrs = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            
            //caso seja maior que 0 retorna o valor
            //senao retorna o vetor de valores
            if (attrs.Length > 0)
            {
                stringValue = attrs[0].Value;
            }

            return stringValue;
        }
    }
}
