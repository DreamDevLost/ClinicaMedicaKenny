//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClinicaMedicaKenny
{
    using System;
    using System.Collections.Generic;
    
    public partial class MEDICO_ESPECIALIDADE
    {
        public int ID_MEDICO_ESPECIALIDADE { get; set; }
        public Nullable<int> ID_ESPECIALIDADE { get; set; }
        public string NR_CRM { get; set; }
    
        public virtual ESPECIALIDADE ESPECIALIDADE { get; set; }
        public virtual MEDICO MEDICO { get; set; }
    }
}
