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
    
    public partial class FundEquity
    {
        public string FundSymbol { get; set; }
        public string EquitySymbol { get; set; }
        public decimal Weighting { get; set; }
        public decimal Shares { get; set; }
    
        public virtual Equity Equity { get; set; }
        public virtual Fund Fund { get; set; }
    }
}
