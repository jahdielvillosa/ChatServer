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
    
    public partial class Logs
    {
        public int Id { get; set; }
        public string SendDT { get; set; }
        public string Status { get; set; }
        public int NotificationsId { get; set; }
    
        public virtual Notifications Notification { get; set; }
    }
}
