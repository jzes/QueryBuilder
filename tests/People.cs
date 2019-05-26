using System;
using ModelSQLBuilder;

namespace tests
{
    [Entity("cbpessoa")]
    public class People : ModelBase<People>
    {
        [Field("id_cbpessoa", PostgreTypes.INT, true)]
        public int Id {get; set;}
        
        [Field("nome_str", PostgreTypes.STRING)]
        public string FirstName{get; set;}

        [Field("cargo_str", PostgreTypes.STRING)]
        public string Cargo {get; set;}

        [Field("cpf_str", PostgreTypes.STRING)]
        public string CPF {get; set;}

        [Field("rg_str", PostgreTypes.STRING)]
        public string Document {get; set;}

        [Field("data_nascimento",PostgreTypes.DATETIME)]
        public DateTime BirdDate{get; set;}

        [Field("value", PostgreTypes.DOUBLE)]
        public Double Value{get; set;}


    }
}