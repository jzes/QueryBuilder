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
        public string Name{get; set;}

        [Field("cargo_str", PostgreTypes.STRING)]
        public string Role {get; set;}

        [Field("cpf_str", PostgreTypes.STRING)]
        public string CPF {get; set;}

        [Field("rg_str",PostgreTypes.DATETIME)]
        public string Document{get; set;}

        [Field("data_nascimento", PostgreTypes.DATETIME)]
        public DateTime BirthDate{get; set;}

    }
}