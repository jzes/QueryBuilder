using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ModelSQLBuilder
{
    public class ModelBase
    {
        private Entity entity;
        private List<Field> fields;
        private List<string> commaTypes = new List<string>{PostgreTypes.STRING, PostgreTypes.DATETIME};
        private List<Field> KeyFields;

        public string BuildWhereByKey(){
            var stringBuilder = new StringBuilder($"\nwhere ");
            foreach(var field in KeyFields){
                if(commaTypes.Contains(field.Type)){
                    stringBuilder.Append($"{field.Nome} = '{field.Value}'");
                }else{
                    stringBuilder.Append($"{field.Nome} = {field.Value}");
                }
                if (KeyFields.Count > 1 && (!field.Equals(KeyFields.Last()))){
                    stringBuilder.Append("\nand ");
                }
            }
            stringBuilder.Append(";");
            return stringBuilder.ToString();
        }

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
            if(KeyFields.Count > 0){
                stringBuilder.Append(BuildWhereByKey());
            }
            return stringBuilder.ToString();
        }

        public string BuildDelete(){
            extractFields(this);
            getTableName(this);
            var stringBuilder = new StringBuilder("delete");
            stringBuilder.Append($"\nfrom {entity.Nome}");
            if(KeyFields.Count > 0){
                stringBuilder.Append(BuildWhereByKey());
            }
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
            KeyFields = new List<Field>();
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
                    if(field.IsKey){
                        KeyFields.Add(field);
                    }else{
                        fields.Add(field);
                    }
                }
            }
        }
    }
}