//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CaptainMao.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hospital
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hospital()
        {
            this.Scorce = new HashSet<Scorce>();
        }
    
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string HospitalAddress { get; set; }
        public int AddressArea { get; set; }
        public string HospitalPhone { get; set; }
        public int CategoryID { get; set; }
        public string BusinessHours { get; set; }
        public string Emergency { get; set; }
        public string OutpatientProject { get; set; }
        public string Equipment { get; set; }
        public string WebAddress { get; set; }
        public string OnlineConsultation { get; set; }
        public string OnView { get; set; }
        public string Map { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Citys Citys { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Scorce> Scorce { get; set; }
    }
}
