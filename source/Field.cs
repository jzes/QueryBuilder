using System;

namespace AtributeTest
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)  ] 
    public class Field: Attribute
    {
        public string Nome {get; set;}
        public string Type {get; set;}
        public string Value {get; set;}
        public Field(string nome){
            this.Nome = nome;
        }
        public Field(string nome, string type){
            this.Nome = nome;
            this.Type = type;
        }
    }
}