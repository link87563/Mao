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
    
    public partial class Comment
    {
        public int ArticleID { get; set; }
        public int CommentID { get; set; }
        public string PosterID { get; set; }
        public string ContentText { get; set; }
        public Nullable<System.DateTime> NewDateTime { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual NormalUser NormalUser { get; set; }
    }
}
