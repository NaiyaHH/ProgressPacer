using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProgressPacer.Models;
using ST10033475_PROG6212_POE_ClassLibrary_NaiyaHaribhai;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace ProgressPacer.Controllers
{
    [Authorize]
    public class ModuleController : Controller
    {
        private readonly Prog6212PoeSt10033475Context _context;

        private readonly UserManager<IdentityUser> _userManager;
        
        Methods method = new Methods();
        public ModuleController(Prog6212PoeSt10033475Context context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        // GET: Module
        public async Task<IActionResult> Index()
        {
            var userName = _userManager.GetUserName(HttpContext.User);
            return View(await _context.Modules.Where(d => @d.Username.Equals(userName)).ToListAsync());
        }

        // GET: Module/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.MId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // GET: Module/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Module/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MId,ModuleCode,ModuleName,NumCredits,NumWeeks,ClassHours,SelfStudy,StartDate,Username")] Module @module)
        {
            module.SelfStudy = (decimal)method.CalcHrs(module.NumCredits, (double)module.NumWeeks, (double)module.ClassHours);
            module.Username = _userManager.GetUserName(HttpContext.User);
			int id = 0;
			try
			{

				using (SqlConnection connection = new SqlConnection(Connection.conn))
				{
					connection.Open();
					//This sql statement checks if the module already exists in the database.
					string sql = "Select mID from MODULE where moduleCode='" + module.ModuleCode + "' AND username= '" + module.Username + "';";

					SqlCommand command = new SqlCommand(sql, connection);
					SqlDataReader reader = command.ExecuteReader();
					//If the data does exist in the database then a message will be displayed to the user telling the user of this.
					if (reader.HasRows)
					{
						id = int.Parse(reader["mId"].ToString());
						reader.Close();
					}

					//If the data is not already found in the database
					else
					{
						id = 0;
						reader.Close();
					}



				}



			}
			catch (SqlException ex)
			{
				ex.ToString();
			}
			if (!(ModelState.IsValid) &&  id == 0)
            {   
                _context.Add(@module);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@module);
        }

		

		// GET: Module/Edit/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            return View(@module);
        }

        // POST: Module/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MId,ModuleCode,ModuleName,NumCredits,NumWeeks,ClassHours,SelfStudy,StartDate,Username")] Module @module)
        {
            if (id != @module.MId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(@module.MId))
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
            return View(@module);
        }

        // GET: Module/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .FirstOrDefaultAsync(m => m.MId == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }

        // POST: Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @module = await _context.Modules.FindAsync(id);
            if (@module != null)
            {
                _context.Modules.Remove(@module);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.MId == id);
        }


		
	}
}                        
    

