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
    
    public partial class CONSULTA
    {
        public int ID_CONSULTA { get; set; }
        public Nullable<System.DateTime> DT_MARCADO { get; set; }
        public string NR_CRM { get; set; }
        public Nullable<int> ID_PACIENTE { get; set; }
    
        public virtual PACIENTE PACIENTE { get; set; }
        public virtual MEDICO MEDICO { get; set; }
    }
}
