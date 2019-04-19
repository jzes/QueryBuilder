using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AtributeTest.QueryBuilder;

namespace AtributeTest
{
    public class ModelBase
    {
        private Entity entity;
        private List<Field> fields;
        private List<string> commaTypes = new List<string>{PostgreTypes.STRING, PostgreTypes.DATETIME};

        public string BuildUpdate(){
            extractFields(this);
            getTableName(this);
            var stringBuilder = new StringBuilder($"update {entity.Nome} set\n");
            foreach(var field in fields){
                if(commaTypes.Contains(field.Type)){
                    stringBuilder.Append($"\t{field.Nome} = '{field.Value}'");
                }else{
                    stringBuilder.Append($"\t{field.Nome} = {field.Value}");
                }
                if(!field.Equals(fields.Last())){
                    stringBuilder.Append(", \n");
                }    
            }
            stringBuilder.Append($"\nwhere ");
            return stringBuilder.ToString();
        }

        public string BuildDelete(){
            extractFields(this);
            getTableName(this);
            var stringBuilder = new StringBuilder("delete");
            stringBuilder.Append($"\nfrom {entity.Nome}\nwhere ");
            return stringBuilder.ToString();
        }

        public string BuildSelect(){
            extractFields(this);
            getTableName(this);
            var stringBuilder = new StringBuilder("select\n");
            foreach(var field in fields){
                stringBuilder.Append($"\t{field.Nome}");
                if(!field.Equals(fields.Last())){
                    stringBuilder.Append(", \n");
                }    
            }
            stringBuilder.Append($"\nfrom {entity.Nome}");
            return stringBuilder.ToString();
        }

        public string BuildInsert(){
            extractFields(this);
            getTableName(this);
            var stringBuilder = new StringBuilder($"insert into {entity.Nome} (\n");
            foreach(var field in fields){
                stringBuilder.Append($"\t{field.Nome}");
                if(!field.Equals(fields.Last())){
                    stringBuilder.Append(", \n");
                }
            }
            stringBuilder.Append("\n) values (\n");
            foreach(var field in fields){
                if(commaTypes.Contains(field.Type)){
                    stringBuilder.Append($"\t'{field.Value}'");
                }else{
                    stringBuilder.Append($"\t{field.Value}");
                }
                if(!field.Equals(fields.Last())){
                    stringBuilder.Append(", \n");
                }
            }
            stringBuilder.Append("\n);\n");
            return stringBuilder.ToString();
        }

        private void getTableName(Object genericObject){
            entity = (Entity)Attribute.GetCustomAttribute(genericObject.GetType(), typeof(Entity));
        }

        private void extractFields(Object genericObject){
            fields = new List<Field>();
            var propertyes = genericObject.GetType().GetProperties();
            foreach(var property in propertyes){
                var attrs = property.GetCustomAttributes(true);
                foreach(var attr in attrs){
                    var value = property.GetValue(genericObject);
                    var field = attr as Field;
                    field.Value = DataConverter.ToStringPostgreFormat(value);
                    if (field.Value.Equals(PostgreTypes.NULL)){
                        field.Type = PostgreTypes.NULL;
                    }
                    fields.Add(field);
                }
            }
        }
    }
}