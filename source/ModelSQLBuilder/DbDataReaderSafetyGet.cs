using System;
using System.Data.Common;

namespace ModelSQLBuilder
{
    public static class DbDataReaderSafetyGet
    {
        public static int GetSafetyInt(this DbDataReader dataReader, string fieldName){
            try{
                return Convert.ToInt32(dataReader[fieldName]);
            }catch(Exception){
                return 0;
            }
        }

        public static string GetSafetyString(this DbDataReader dataReader, string fieldName){
            try{
                return dataReader[fieldName].ToString();
            }catch(Exception){
               return ""; 
            }
        }


    }
}