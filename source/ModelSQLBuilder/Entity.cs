using System;

namespace ModelSQLBuilder
{

    [AttributeUsage(AttributeTargets.Class)  ] 
    public class Entity : Attribute
    {
        public string Nome {get; set;}
        public Entity(string nome){
            this.Nome = nome;
        }
    }
}