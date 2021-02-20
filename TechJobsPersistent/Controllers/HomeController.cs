using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TechJobsPersistent.Models;
using TechJobsPersistent.ViewModels;
using TechJobsPersistent.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace TechJobsPersistent.Controllers
{
    public class HomeController : Controller
    {
        private JobDbContext context;

        public HomeController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet]
        public IActionResult AddJob()
        {
            List<Employer> employers = context.Employers.ToList();
            List<Skill> skills = context.Skills.ToList();

            AddJobViewModel addJobViewModel = new AddJobViewModel(employers, skills);
            return View(addJobViewModel);
        }

    [HttpPost]    
        public IActionResult ProcessAddJobForm(AddJobViewModel addJobViewModel, string[] selectedSkills )    
        {
            if (ModelState.IsValid)
            {
                Job newJob = new Job
                {
                    EmployerId = addJobViewModel.EmployerId,
                    Employer = context.Employers.Find(addJobViewModel.EmployerId),
                    Name = addJobViewModel.Name
                };

                //loop to go through each item in selectedSkills 	

                for (int i = 0; i < selectedSkills.Count(); i++)
                { 
                    JobSkill jobSkill = new JobSkill
                    {
                        Job = newJob,
                        SkillId = int.Parse(selectedSkills[i]) 
                    };
                    context.JobSkills.Add(jobSkill);
                }

                context.Jobs.Add(newJob);
                context.SaveChanges();

                return Redirect("Index");
            }
            //update adjobviewmodel by updating the skills property

            addJobViewModel.Skills = context.Skills.ToList();

            return View("AddJob", addJobViewModel);  
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs
                .Include(j => j.Employer)
                .Single(j => j.Id == id);

            List<JobSkill> jobSkills = context.JobSkills
                .Where(js => js.JobId == id)
                .Include(js => js.Skill)
                .ToList();

            JobDetailViewModel viewModel = new JobDetailViewModel(theJob, jobSkills);
            return View(viewModel);
        }
    }
}
