using System;

namespace QueryBuilder
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