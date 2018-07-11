using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using ChatServer.Models;
using System.Data;
using System.Data.Entity;

namespace ChatServer
{   
    /// <summary>
    /// Summary description for LocalService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LocalService : System.Web.Services.WebService
    {
        //database
        private ChatServerModelContainer db = new ChatServerModelContainer();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //getlist

        //insert record
        [WebMethod]
        public string InsertNotification(int Id, int jobID, int appId,
             string message, string status)
        {
            var notify = db.Notifications.Where(n => n.ServiceId.Equals(Id)).FirstOrDefault();

            if (notify != null)
            {
                //do nothing
                return "not successful. record duplicate";
            }
            else
            {
                //insert notifications
                db.Notifications.Add(new Notifications
                {
                    ServiceId = Id,
                    JobId = jobID,
                    AppId = appId,
                    //Recipient = recipient,
                    Message = message,
                    //SendDT = sendDt,
                    Status = status

                });

                db.SaveChanges();
                
                return "successful";
            }
        }

        [WebMethod]
        public void insertRecipients(int notificationid, string recipient)
        {
            //insert notification recipients
           
                //insert recipients
                db.Recipients.Add(new Recipients
                {
                    Number = recipient,
                    NotificationsId = notificationid

                });
                db.SaveChanges();
        
        }


        [WebMethod]
        public void getRecord(int id)
        {
            bool value = false;
            var notify = db.Notifications.Where(n => n.ServiceId.Equals(id)).FirstOrDefault();
            if (notify != null)
            {
                value = true;
            }
            else
            {
                value = false;
            }

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented));
        }

        [WebMethod]
        public void getRecordId(int serviceid)
        {
            var notify = db.Notifications.Where(n => n.ServiceId.Equals(serviceid)).FirstOrDefault();

            int referenceId = notify.Id;

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(referenceId, Newtonsoft.Json.Formatting.Indented));
        }
        
        [WebMethod]
        public void getStatus(int serviceid)
        {
            var notify = db.Notifications.Where(n => n.ServiceId.Equals(serviceid)).FirstOrDefault();

            string referenceId = notify.Status;

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(referenceId, Newtonsoft.Json.Formatting.Indented));
        }

        //update record
        //delete record

        //read record by id
        [WebMethod]
        public void readNotification(int id)
        {

           // Notifications notification = new Notifications();

            var notify= db.Notifications.Where(n => n.Id.Equals(id)).FirstOrDefault();

            //create table for the message
            DataTable Dt = new DataTable("Table");
            if (notify != null)
            {

                //columns for the table
                Dt.Columns.Add("Id", typeof(int));
                Dt.Columns.Add("ServiceId", typeof(string));
                Dt.Columns.Add("JobId", typeof(string));
                Dt.Columns.Add("AppId", typeof(string));
                Dt.Columns.Add("Recipient", typeof(string));
                Dt.Columns.Add("Message", typeof(string));
                Dt.Columns.Add("DtSchedule", typeof(string));
                Dt.Columns.Add("Status", typeof(string));

                //add for the client, driver, admin
                int Id = notify.Id;
                int ServiceId = notify.ServiceId;
                int JobId = notify.JobId;
                int AppId = notify.AppId;
                //string dtSchedule = notify.SendDT;
                string msg = notify.Message;
                //string recipients = notify.Recipient;

               // Dt.Rows.Add(Id, ServiceId, JobId, AppId, dtSchedule, msg, recipients);

                DataSet ds = new DataSet();
                ds.Tables.Add(Dt);
                ds.DataSetName = "Table";

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));
            }
        }


        [WebMethod]
        public string updateRecord(int serviceId)
        {
            using (ChatServerModelContainer context = new ChatServerModelContainer())
            {
                // "id" is the id in your table (parameter passed)

                Notifications jnr = context.Notifications.Where(n => n.ServiceId.Equals(serviceId)).FirstOrDefault();
                jnr.Status = "Sent";
                context.Entry(jnr).State = EntityState.Modified;
                context.SaveChanges();
                return "Sent";
            }
        }

    }
}
