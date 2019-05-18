# ModelSQLBuilder

ModelSQLBuilder facilita o uso de banco de dados sem o uso de um ORM.

## Principais caracteristicas

- Baseado em comandos SQL
- Transparente, qualquer comando gerado pode ser acessado como `string`
- Modular, as features podem ser usadas independentemente uma da outra

## Porque Usar

O ModelSQLBuilder foi idealizado em um cenário de banco de dados caótico onde o uso de um ORM traria mais problemas do que soluções, esse cenário pode ser caracterizado por bancos de dados de sistemas que já se tornaram legados bem como por falta de documentação e manuais que expliquem as regras implementadas.

Nestes casos o mais comum é fazer o acesso escrevendo as querys manualmente e as utilizando no código da aplicação. Este é o problema que o ModelSQLBuilder resolve.

## Como Utilizar

Um pacote do [nuget](https://www.nuget.org/packages/ModelSQLBuilder/)]   esta disponível para download e uso em qualquer projeto C#, podendo ser encontrado através da ferramenta do visual studio, bem como através do comando do CLI do .net core.

Para iniciar o uso é necessário a criação de uma classe que sirva como modelo de dados para a implementação como por exemplo.

```c#
    public class People
    {
    	public int Id {get; set;}
    	public string FirstName {get; set;}
    	public string Role {get; set;}
    	public string CPF {get; set;}
    	public string Document {get; set;}
    	public DateTime BirdDate {get; set;}
    	public Double Value {get; set;}
    }
```

É uma classe simples que guarda algumas informações sobre uma pessoa, agora devemos preparar esta classe simples para o uso da nossa biblioteca.

```c#
    [Entity("cbpessoa")]
    public class People : ModelBase<People>
    {
        [Field("id_cbpessoa", PostgreTypes.INT, true)]
        public int Id {get; set;}
        
        [Field("nome_str", PostgreTypes.STRING)]
        public string FirstName{get; set;}
    
        [Field("cargo_str", PostgreTypes.STRING, true)]
        public string Role{get; set;}
    
        [Field("cpf_str", PostgreTypes.STRING)]
        public string CPF {get; set;}
    
        [Field("rg_str", PostgreTypes.STRING)]
        public string Document{get; set;}
    
        [Field("data_nascimento",PostgreTypes.DATETIME)]
        public DateTime BirdDate{get; set;}
    
        [Field("value", PostgreTypes.DOUBLE)]
        public Double Value{get; set;}
    }
```

O que fizemos aqui foi adicionar alguns atributos e adicionar uma herança de `ModelBase<T> onde.

`T` é a nossa própria classe, dessa forma estamos dizendo a biblioteca qual o formato de dados estamos esperando na saida.

Os dois atributos adicionadas foram `Entity` para a classe, sinalizando o que vem a ser a nossa tabela, e `Field` em cada propriedade que representa um campo da tabela, vamos ver melhor os parâmetros que cada uma dos atributos recebe.

### Entity

Este atributo serve apenas para classes e representa uma tabela do banco de dados ele recebe apenas um parâmetro string que é o nome da tabela a qual ele fará referência.

### Field

Este atributo é mais complexo, ele pode receber até três parâmetros, em ordem

- Nome.: O nome do campo assim como ele é no banco de dados na tabela em questão
- Tipo.: O tipo do campo é uma sinalização onde o tipo do objeto em C# é sinalizado para a biblioteca através das constantes que a mesma disponibiliza, realizando assim uma "tradução" de tipos para os tipos do banco de dados
- Chave.: Caso queira usar o campo como chave para esta tabela, independentemente de ele ser ou não uma `Primary Key` no banco de dados esse parâmetro deve ser `true`, caso contrário, pode ser omitido

Com a nossa classe preparada para representar uma tabela nós podemos começar a usufruir dos métodos que temos disponíveis no nosso `ModelBase`. 

Podemos dividir a biblioteca em dois grupos, um grupo para abstrair o código SQL e outro para lidar com conexões, tratando-as automaticamente dentro de transações com `using` .

### Abstraindo o SQL

Através dos métodos implementados no `ModelBase` podemos abstrair o código SQL conforme o exemplo abaixo. 

```c#
    static void Main(string[] args)
    {
        var johnDoe = new People();
        johnDoe.Id = 156;
        johnDoe.FirstName = "John";
        johnDoe.Document = "12345678";
        johnDoe.CPF = "123456";
        johnDoe.BirdDate = DateTime.Now;
        johnDoe.Value = 2300.50;
    
        System.Console.WriteLine(johnDoe.BuildSelect());
        System.Console.WriteLine(johnDoe.BuildUpdate());
        System.Console.WriteLine(johnDoe.BuildWhereByKey());
        System.Console.WriteLine(johnDoe.BuildInsert());
        System.Console.WriteLine(johnDoe.BuildDelete());
    }
```

Através dos métodos apresentados acima temos acesso a `string` que representa o comando SQL desta forma podemos executa-lo utilizando qualquer biblioteca que queiramos.

Vamos analisar cada método independentemente.

### BuildSelect

Esse método retorna a `string` do comando `select` sem `where`, caso seja preciso selecionar dados utilizando a chave como valor comparativo pode-se usar o método `BuildWhereByKey` que será explicado em detalhes mais adiante, caso seja preciso um `where` mais complexo o mesmo pode ser concatenado de dentro de uma string à saída do método.

Saída do método

```sql
    select
    	nome_str, 
    	cpf_str, 
    	rg_str, 
    	data_nascimento, 
    	value, 
    	id_cbpessoa, 
    	cargo_str
    from cbpessoa
```