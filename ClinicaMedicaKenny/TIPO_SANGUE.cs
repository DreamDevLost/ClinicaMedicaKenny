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
    
    public partial class TIPO_SANGUE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TIPO_SANGUE()
        {
            this.PACIENTE = new HashSet<PACIENTE>();
        }
    
        public string DS_RH { get; set; }
        public int ID_TIPO_SANGUE { get; set; }
        public string DS_TIPO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PACIENTE> PACIENTE { get; set; }
    }
}
