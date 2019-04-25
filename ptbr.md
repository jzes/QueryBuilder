# ModelSQLBuilder

Em alguns cenários onde enfrentamos um banco de daos legado, ou onde o banco de dados em que trabalhamos é cheio de regras de negócio confusas e tem funções e entidades mal documentadas, implementar um ORM pode trazer inumeros debitos tecnicos e pode ser uma solução que acarrete em mais problemas que soluções.
Para evitar isso acabamos por escrever código SQL dentro de nossas aplicações, e isso pode ser uma solução um pouco feia, para evitar isso e ainda sim manter o legado como ele é atualmente o ModelSQLBuilder foi iniciado

## Como Usar

O uso é simples basta baixar o pacote do [nuget](https://www.nuget.org/packages/ModelSQLBuilder/) e adicionar a dependência, após isso basta criar os models que precisar e herdar de `ModelBase`, depois é só utilizar as anotations disponiveis para campos `Field` e para tabelas `Entity` com os respectivos nomes. No final seu model vai ficar mais ou menos assim

```c#
using System;
using ModelSQLBuilder;

namespace tests
{
    [Entity("people_tbl")]
    public class People : ModelBase
    {
        [Field("people_f_name", PostgreTypes.STRING)]
        public string FirstName{get; set;}

        [Field("people_age", PostgreTypes.INT)]
        public int Age {get; set;}

        [Field("people_l_name", PostgreTypes.STRING)]
        public string LastName {get; set;}

        [Field("people_bdate",PostgreTypes.DATETIME)]
        public DateTime BirdDate{get; set;}

        [Field("people_value")]
        public Double Value{get; set;}
    }
}
```

Após os models terem sido criados com a herança de `ModelBase` ganhamos os métodos de build 

```
BuildInsert
BuildUpdate
BuildSelect
BuildDelete
```

E podemos utilizanos livremente, assim

```c#
static void Main(string[] args)
{
    var johnDoe = new People();
    johnDoe.FirstName = "John";
    johnDoe.LastName = "Doe";
    johnDoe.Age = 28;
    johnDoe.BirdDate = DateTime.Now;
    johnDoe.Value = 2300.50;

    System.Console.WriteLine(johnDoe.BuildInsert());
    System.Console.WriteLine("\n---\n");
    System.Console.WriteLine(johnDoe.BuildUpdate());
    System.Console.WriteLine("\n---\n");
    System.Console.WriteLine(johnDoe.BuildSelect());
    System.Console.WriteLine("\n---\n");
    System.Console.WriteLine(johnDoe.BuildDelete());
}

```

A saida para esse método fica assim

```
insert into people_tbl (
        people_f_name,
        people_age,
        people_l_name,
        people_bdate,
        people_value
) values (
        'John',
        28,
        'Doe',
        '2019-04-24 10:44:59',
        2300.5
);


---

update people_tbl set
        people_f_name = 'John',
        people_age = 28,
        people_l_name = 'Doe',
        people_bdate = '2019-04-24 10:44:59',
        people_value = 2300.5
where

---

select
        people_f_name,
        people_age,
        people_l_name,
        people_bdate,
        people_value
from people_tbl

---

delete
from people_tbl
where
```

## Contribute

Just fork it and send your PR

