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
    
    public partial class ESPECIALIDADE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ESPECIALIDADE()
        {
            this.MEDICO_ESPECIALIDADE = new HashSet<MEDICO_ESPECIALIDADE>();
        }
    
        public string NM_ESPECIALIDADE { get; set; }
        public int ID_ESPECIALIDADE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MEDICO_ESPECIALIDADE> MEDICO_ESPECIALIDADE { get; set; }
    }
}
