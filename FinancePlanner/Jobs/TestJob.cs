using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Quartz;

namespace FinancePlanner.Jobs
{
    public class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            using (var msg = new MailMessage("testprotocollsender@gmail.com", "simon.blnt93@gmail.com"))
            {
                msg.Subject = "Sample Test Mail";
                msg.Body = "Hello world From Stopbyte.com, Sent at: " + DateTime.Now;
                using (SmtpClient sc = new SmtpClient())
                {
                    sc.EnableSsl = true;
                    sc.Host = "smtp.gmail.com";
                    sc.Port = 587;
                    sc.Credentials = new NetworkCredential("testprotocollsender@gmail.com", "jE6hxxcN5RApxscWqmhv");
                    sc.Send(msg);
                }
            }
        }
    }
}