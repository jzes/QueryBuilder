using System;

namespace ModelSQLBuilder
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)  ] 
    public class Field: Attribute
    {
        public string Nome {get; set;}
        public string Type {get; set;}
        public string Value {get; set;}
        public bool IsKey {get; set;}
        public Field(string nome){
            this.Nome = nome;
        }
        public Field(string nome, string type){
            this.Nome = nome;
            this.Type = type;
        }
        public Field(string nome, string type, bool IsKey){
            this.Nome = nome;
            this.Type = type;
            this.IsKey = IsKey;
        }
    }
}