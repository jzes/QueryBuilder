using System;
using ModelSQLBuilder;

namespace tests
{
    [Entity("people_tbl")]
    public class People : ModelBase
    {
        [Field("cnh_str", PostgreTypes.STRING, true)]
        public string Document2 {get; set;}
        [Field("cpf_str", PostgreTypes.STRING, true)]
        public string Document {get; set;}
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