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
    
    public partial class UserPet
    {
        public int PetID { get; set; }
        public string PetName { get; set; }
        public string UserID { get; set; }
        public int CategoryID { get; set; }
        public Nullable<bool> PetSex { get; set; }
        public byte[] PetPhoto { get; set; }
        public Nullable<System.DateTime> PetBirthday { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual NormalUser NormalUser { get; set; }
    }
}
