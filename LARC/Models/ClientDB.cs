//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LARC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientDB
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientDB()
        {
            this.PortfolioDBs = new HashSet<PortfolioDB>();
        }
    
        public int ID { get; set; }
        public int AccountID { get; set; }
        public Nullable<int> PortfolioID { get; set; }
    
        public virtual AccountDB AccountDB { get; set; }
        public virtual PortfolioDB PortfolioDB { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PortfolioDB> PortfolioDBs { get; set; }
    }
}
