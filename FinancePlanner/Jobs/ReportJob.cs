using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using FinancePlanner.Database;
using FinancePlanner.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Quartz;

namespace FinancePlanner.Jobs
{
    public class ReportJob : IJob
    {
        private PlannerContext _context;

        public ReportJob(PlannerContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            List<IdentityUser> users = await GetUsers();

            foreach (var user in users)
            {
                var userId = user.Id;
                var email = user.Email;
                
                using (var msg = new MailMessage("testprotocollsender@gmail.com", email))
                {
                    var reports = await GetReports(userId);
                    var currentDateTime = DateTime.Now;
                    var reportsToSend = reports.Where(r => (r.NextScheduledDate - currentDateTime).TotalSeconds < 60).ToList();

                    if (reportsToSend.Any())
                    {
                        foreach (var report in reportsToSend)
                        {
                            var title = report.Title;
                            var plan = await GetPlan(report.PlantId);
                            var goals = await GetGoals(plan.Id);

                            msg.Subject = title + " notification";
                            msg.Body = "";
                            if (plan != null)
                            {
                                msg.Body += "Hi!\n Here's a status update about your plan " + plan.Title + "\n";
                                msg.Body += "Goals:\n";
                                foreach (var goal in goals)
                                {
                                    msg.Body += "Title: " + goal.Title + "\n";
                                    msg.Body += "Target: " + goal.NumericalTarget + "\n";
                                    msg.Body += "Progress: " + goal.NumericalProgress + "\n";
                                }
                            }
                            using (SmtpClient sc = new SmtpClient())
                            {
                                sc.EnableSsl = true;
                                sc.Host = "smtp.gmail.com";
                                sc.Port = 587;
                                sc.Credentials = new NetworkCredential("testprotocollsender@gmail.com", "jE6hxxcN5RApxscWqmhv");
                                sc.Send(msg);
                            }
                            
                            if (report.SendingSchedule == "hourly")
                            {
                                report.NextScheduledDate = DateTime.Now.AddHours(1);
                            } else if (report.SendingSchedule == "daily")
                            {
                                report.NextScheduledDate = DateTime.Now.AddDays(1);
                            } else if (report.SendingSchedule == "weekly")
                            {
                                report.NextScheduledDate = DateTime.Now.AddDays(7);
                            } 

                            _context.Update(report);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }


            
        }

        private async Task<List<Goal>> GetGoals(int id)
        {
            return await _context.Goals.Where(x => x.PlanId == id).ToListAsync();
        }

        private async Task<Plan> GetPlan(int id)
        {
            return await _context.Plans.FindAsync(id);
        }
        
        private async Task<Event> GetEvent(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        private async Task<List<Report>> GetReports(string userId)
        {
            return await _context.Reports.ToListAsync();
        }

        private async Task<List<IdentityUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}