using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Http;

namespace GiellyGreenApi.Controllers
{
    public class EmailController : ApiController
    {

        public void Email_Attachment(string ToEmail, string Subj, string Message)
        {
            var HostAdd = ConfigurationManager.AppSettings["Host"].ToString();
            var FromEmailid = ConfigurationManager.AppSettings["FromEmail"].ToString();
            var Pass = ConfigurationManager.AppSettings["PasswordEmail"].ToString();

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(FromEmailid);
            mailMessage.Subject = Subj;
            mailMessage.Body = Message;
            mailMessage.Body = Message;
            mailMessage.IsBodyHtml = true;            


            string file = @"C:\Users\User42\Documents\GitHub\GiellyGreen\Images\logo5.jpg";
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);

            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

            mailMessage.Attachments.Add(data);

            //var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

            //var pdfBytes = htmlToPdf.GeneratePdfFromFile("http://{siteName}/templates/PasswordResetEmail2.cshtml", null);

            //var stream = new System.IO.MemoryStream(pdfBytes);
            //email.Attachments.Add(new Attachment(stream, "invoice.pdf"));




            string[] Multi = ToEmail.Split(',');
            foreach (string Multiemailid in Multi)
            {
                mailMessage.To.Add(new MailAddress(Multiemailid));
            }
            SmtpClient smtp = new SmtpClient();
            smtp.Host = HostAdd;

            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = Pass;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mailMessage);

        }

    }
}
