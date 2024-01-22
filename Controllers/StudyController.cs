using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgressPacer.Models;
using Microsoft.Data.SqlClient;
using ST10033475_PROG6212_POE_ClassLibrary_NaiyaHaribhai;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProgressPacer.Controllers
{
    [Authorize]
    public class StudyController : Controller
    {
        private readonly Prog6212PoeSt10033475Context _context;

        private readonly UserManager<IdentityUser> _userManager;


        public StudyController(Prog6212PoeSt10033475Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Study
        public async Task<IActionResult> Index()
        {
            var prog6212PoeSt10033475Context = _context.Studies.Include(s => s.MIdNavigation);
            var userName = _userManager.GetUserName(HttpContext.User);
            return View(await prog6212PoeSt10033475Context.Where(d => @d.Username.Equals(userName)).ToListAsync());
        }

        // GET: Study/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var userName = _userManager.GetUserName(HttpContext.User);
            if (id == null)
            {
                return NotFound();
            }
            //Code Attribution
            //This code has been adapted from Stack Overflow
            //https://stackoverflow.com/questions/21598365/how-do-i-determine-if-a-date-lies-between-current-week-dates
            //Atiris
            //https://stackoverflow.com/users/659223/atiris
            //The code
            //DateTime startDateOfWeek = DateTime.Now.Date; // start with actual date
            //while (startDateOfWeek.DayOfWeek != DayOfWeek.Monday) // set first day of week in your country
            //{ startDateOfWeek = startDateOfWeek.AddDays(-1d); } // after this while loop you get first day of actual week
            //DateTime endDateOfWeek = startDateOfWeek.AddDays(6d); // you just find last week day
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            DateOnly startOfWeek = date;
            while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
            { startOfWeek = startOfWeek.AddDays((int)-1d); }
            DateOnly endOfWeek = startOfWeek.AddDays((int)6d);
            var study = await _context.Studies.Where(@d => @d.Username.Equals(userName))
                .Include(s => s.MIdNavigation)
                .FirstOrDefaultAsync(m => m.StudyId == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // GET: Study/Create
        public IActionResult Create()
        {
           
            ViewData["ModuleCode"] = new SelectList(_context.Modules.Where(@a => a.Username.Equals(_userManager.GetUserName(HttpContext.User))), "ModuleCode", "ModuleCode");
            return View();
        }

        // POST: Study/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudyId,ModuleCode,ModuleName,TotalSelfStudy,SelfStudyHours,StudyDate,RemainingHrs,MId,Username")] Study study)
        {
            study.Username = _userManager.GetUserName(HttpContext.User);
            //This variable stores
            //the module code.
            string mCode = "";
            //This variable stores
            //the module name.
            string mName = "";
            //This variable stores
            //the module's total weekly self study hours.
            decimal TotalHours = 0;
            ///This variable stores
            //the start date of the module is stored in this variable
            DateOnly StartDate = DateOnly.MinValue;
            decimal unrecordedHours = 0;
            DateTime studiedDate = DateTime.Now;
            //This variable will be used for holding the ID of the module.
            int id = 0;
            ViewData["ModuleCode"] = new SelectList(_context.Modules.Where(@a => a.Username.Equals(_userManager.GetUserName(HttpContext.User))), "ModuleCode", "ModuleCode");

            try
            {
                using (SqlConnection connection = new SqlConnection(Connection.conn))
                {
                    connection.Open();
                    string sql = "Select * from MODULE where moduleCode='" + study.ModuleCode + "' AND username= '" + study.Username + "';";

                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    //If such data is found, the reader will read the data and store that data in variables.
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            study.ModuleCode = (reader["moduleCode"].ToString());
                            study.ModuleName = (reader["moduleName"].ToString());
                            study.TotalSelfStudy = decimal.Parse(reader["selfStudy"].ToString());
                            study.MId = int.Parse(reader["mId"].ToString());
                            StartDate = DateOnly.Parse(reader["startDate"].ToString());
                        }
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex) { }
            try
            {
                using (SqlConnection connection2 = new SqlConnection(Connection.conn))
                {//Code Attribution
                 //This code has been adapted from Stack Overflow
                 //https://stackoverflow.com/questions/21598365/how-do-i-determine-if-a-date-lies-between-current-week-dates
                 //Atiris
                 //https://stackoverflow.com/users/659223/atiris
                 //The code
                 //DateTime startDateOfWeek = DateTime.Now.Date; // start with actual date
                 //while (startDateOfWeek.DayOfWeek != DayOfWeek.Monday) // set first day of week in your country
                 //{ startDateOfWeek = startDateOfWeek.AddDays(-1d); } // after this while loop you get first day of actual week
                 //DateTime endDateOfWeek = startDateOfWeek.AddDays(6d); // you just find last week day
                    DateOnly date = study.StudyDate;
                    DateOnly startOfWeek = date;
                    while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
                    { startOfWeek = startOfWeek.AddDays((int)-1d); }
                    DateOnly endOfWeek = startOfWeek.AddDays((int)6d);
                    connection2.Open();

                    //If the remaining hours is equal to 0.
                    if (study.RemainingHrs == 0)
                    {
                        //If the date that the user has selected is greater than the start date of the week, and less than the end date of the week,
                        //then the following code will run.
                        if (date >= startOfWeek && date <= endOfWeek)
                        {//In this try, the system attempts to select all the information from the table study, where the module code 
                         //is equal to the user's selection, the username is the username that the user has entered and the record's study date
                         //is between the start date and the end date of the week.

                            string query = "Select * from study where moduleCode='" + study.ModuleCode + "' AND username= '" + study.Username + "' AND studyDate BETWEEN '" + startOfWeek + "' AND '" + endOfWeek + "' ORDER BY remainingHrs ASC;";

                            SqlCommand command1 = new SqlCommand(query, connection2);
                            SqlDataReader reader1 = command1.ExecuteReader();


                            //If the required data is found, it is recorded in variables and used for calculations.
                            if (reader1.HasRows)
                            {
                                while (reader1.Read())
                                {
                                    unrecordedHours = decimal.Parse(reader1["remainingHrs"].ToString());
                                    studiedDate = DateTime.Parse(reader1["studyDate"].ToString());

                                    if (study.RemainingHrs <= TotalHours && date >= StartDate)
                                    {
                                        //If the remaining hours is less than 0, it will be made equal to zero.
                                        if (study.RemainingHrs < 0)
                                        {
                                            study.RemainingHrs = 0;
                                        }
                                        //The current remaining hours minus the inputted study hours is what the remainder of hours will be.
                                        study.RemainingHrs = unrecordedHours - study.SelfStudyHours;
                                        break;


                                    }

                                }
                            }
                            //If the required data is not found then the remaining hours is equal to the total weekly study hours minus the number of study
                            //hours that the user has entered.
                            else
                            {
                                study.RemainingHrs = study.TotalSelfStudy - study.SelfStudyHours;

                            }



                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
                                
                                
                            
                        
                    
                
            
            if (!ModelState.IsValid)
            {
                _context.Add(study);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                return View(study);
            }
            ViewData["ModuleCode"] = new SelectList(_context.Modules.Where(@a => a.Username.Equals(_userManager.GetUserName(HttpContext.User))), "ModuleCode", "ModuleCode");

            return View(study);
        }

        // GET: Study/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies.FindAsync(id);
            if (study == null)
            {
                return NotFound();
            }
            ViewData["ModuleCode"] = new SelectList(_context.Modules.Where(@a => a.Username.Equals(_userManager.GetUserName(HttpContext.User))), "Module", "Module");

            return View(study);
        }

        // POST: Study/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudyId,ModuleCode,ModuleName,TotalSelfStudy,SelfStudyHours,StudyDate,RemainingHrs,MId,Username")] Study study)
        {
            if (id != study.StudyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(study);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudyExists(study.StudyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MId"] = new SelectList(_context.Modules, "MId", "MId", study.MId);
            return View(study);
        }

        // GET: Study/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var study = await _context.Studies
                .Include(s => s.MIdNavigation)
                .FirstOrDefaultAsync(m => m.StudyId == id);
            if (study == null)
            {
                return NotFound();
            }

            return View(study);
        }

        // POST: Study/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var study = await _context.Studies.FindAsync(id);
            if (study != null)
            {
                _context.Studies.Remove(study);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudyExists(int id)
        {
            return _context.Studies.Any(e => e.StudyId == id);
        }
    }
}
