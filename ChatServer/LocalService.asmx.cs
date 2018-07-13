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
        public void insertLog(int notificationid, string status)
        {
            //insert notification recipients

            //insert recipients
            db.Logs.Add(new Logs
            {
                SendDT = DateTime.Now.ToString(),
                Status = status,
                NotificationsId = notificationid

            });
            db.SaveChanges();
            string value = "insert log success.";
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented));
        }

        [WebMethod]
        public void getRecord(int id)
        {
            bool value = false;
            var notify = db.Notifications.Where(n => n.ServiceId.Equals(id)).FirstOrDefault();
            if (notify != null)
            {
                value = true;   //has no record
            }
            else
            {
                value = false;  //has record
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
        public void updateRecord(string serviceId)
        {
                // "id" is the id in your table (parameter passed)
            int svcID = Int32.Parse(serviceId);
            string status = "Sent";
            Notifications jnr = db.Notifications.Where(n => n.ServiceId.Equals(svcID)).FirstOrDefault();
            jnr.Status = status;
            db.Entry(jnr).State = EntityState.Modified;
            db.SaveChanges();
                
            
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(status, Newtonsoft.Json.Formatting.Indented));

        }



        [WebMethod]
        public void getRecipients(int refID)
        {
            // "id" is the id in your table (parameter passed)
            var Recipient = db.Recipients.Where(r => r.NotificationsId.Equals(refID)).ToList();
            //var notify = db.Notifications.Where(n => n.Id.Equals(refId)).FirstOrDefault(); 
            //create table for the message
            DataTable Dt = new DataTable("Table");
            //columns for the table
            Dt.Columns.Add("Recipient", typeof(string));

            foreach (var contact in Recipient)
            {
                string RecipientNum = contact.Number;

                Dt.Rows.Add(RecipientNum);

            }
            
            DataSet ds = new DataSet();
            ds.Tables.Add(Dt);
            ds.DataSetName = "Table";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));

        }

        [WebMethod]
        public void getNotifcationList()
        {
            //get top 20 items
            var notify = db.Notifications.Where(n => n.Status != null).ToList().OrderByDescending(n => n.Id).Take(20);
            
            //create table for the message
            DataTable Dt = new DataTable("Table");

            //columns for the table
            Dt.Columns.Add("Id", typeof(string));
            Dt.Columns.Add("NotficationId", typeof(string));
            Dt.Columns.Add("ServiceId", typeof(string));
            Dt.Columns.Add("AppId", typeof(string));
            Dt.Columns.Add("Message", typeof(string));
            Dt.Columns.Add("ClientName", typeof(string));
            Dt.Columns.Add("DTlog", typeof(string));
            Dt.Columns.Add("Status", typeof(string));

            foreach (var data in notify)
            {
                string ID = data.Id.ToString();
                string notifyID = data.ServiceId.ToString();
                string serviceID = data.JobId.ToString();
                string AppId = data.AppId.ToString();
                string message = data.Message.ToString();
                string clientName = db.Recipients.Where(r => r.NotificationsId.Equals(data.Id)).FirstOrDefault().Number;
                string Status = data.Status.ToString();
                string DTLog =  "none" ;
                try
                {
                    DTLog = db.Logs.Where(l => l.NotificationsId.Equals(data.Id)).FirstOrDefault().SendDT;
                }catch(Exception ex)
                {

                }

                Dt.Rows.Add(ID, notifyID, serviceID, AppId, message, clientName, DTLog, Status);

            }

            DataSet ds = new DataSet();
            ds.Tables.Add(Dt);
            ds.DataSetName = "Table";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));


        }


        [WebMethod]
        public void getLogs()
        {
           
            var logs = db.Logs.Where(n => n.SendDT != null).ToList().OrderByDescending(n => n.Id).Take(10);
            
            //create table for the message
            DataTable Dt = new DataTable("Table");

            //columns for the table
            Dt.Columns.Add("Id", typeof(string));
            Dt.Columns.Add("NotficationId", typeof(string));
            Dt.Columns.Add("ServiceId", typeof(string));
            Dt.Columns.Add("AppId", typeof(string));
            Dt.Columns.Add("Message", typeof(string));
            Dt.Columns.Add("ClientName", typeof(string));
            Dt.Columns.Add("DTlog", typeof(string));
            Dt.Columns.Add("Status", typeof(string));

            foreach (var data in logs)
            {
                string ID = data.Id.ToString();
                string notifyID = data.NotificationsId.ToString();
                string serviceID = data.Notification.ServiceId.ToString();
                string AppId = data.Notification.AppId.ToString();
                string message = "none";
                string clientName = db.Recipients.Where(r => r.NotificationsId.Equals(data.NotificationsId)).FirstOrDefault().Number;
                string Status = data.Status.ToString();
                string DTLog = data.SendDT;
                try
                {
                    DTLog = db.Logs.Where(l => l.NotificationsId.Equals(data.Id)).FirstOrDefault().SendDT;
                }
                catch (Exception ex)
                {

                }

                Dt.Rows.Add(ID, notifyID, serviceID, AppId, message, clientName, DTLog, Status);

            }

            DataSet ds = new DataSet();
            ds.Tables.Add(Dt);
            ds.DataSetName = "Table";

            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented));


        }

    }
}
