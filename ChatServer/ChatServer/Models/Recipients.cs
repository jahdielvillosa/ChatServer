//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Recipients
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int NotificationsId { get; set; }
    
        public virtual Notifications Notification { get; set; }
    }
}
