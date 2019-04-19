using System;

namespace AtributeTest.QueryBuilder
{
    public class DataConverter
    {
        public static string ToStringPostgreFormat(Object objectToConvert){
            var typeObject = objectToConvert.GetType();
            if(objectToConvert is DateTime){
                return dateTimeConvert(objectToConvert);
            }else{
                return objectToConvert.ToString();
            }
        }

        private static string dateTimeConvert(Object objectToConvert){
            var value = (DateTime)objectToConvert;
            var nullSample = new DateTime(0);
            if(value.ToString().Equals(nullSample.ToString())){
                return "NULL";
            }
            return value.ToString("yyyy-MM-dd hh:mm:ss");
        }

        private static string doubleConvert(Object objectToConvert){
            var value = (Double)objectToConvert;
            return value.ToString().Replace(",",".");
        }
    }
}