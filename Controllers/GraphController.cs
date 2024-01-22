using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProgressPacer.Models;
using ST10033475_PROG6212_POE_ClassLibrary_NaiyaHaribhai;
using Microsoft.Data.SqlClient;

namespace ProgressPacer.Controllers
{
	public class GraphController : Controller
	{
		private readonly ILogger<GraphController> _logger;
		private readonly UserManager<IdentityUser> _userManager;

		public GraphController(ILogger<GraphController> logger, UserManager<IdentityUser> usermanager)
		{
			_logger = logger;
			_userManager = usermanager;
		}

		public IActionResult Index()
		{
			try
			{
				List<DataPoint> dataPoints1 = new List<DataPoint>();

				List<DataPoint> dataPoints2 = new List<DataPoint>();

				DateOnly dateOnly;

				DateOnly date = DateOnly.MaxValue;

				decimal studyHrs = 0;
				string name = string.Empty;
				string code = string.Empty;
				double hrs = 0;
				int weeks = 0;

				using (SqlConnection connection = new SqlConnection(Connection.conn))
				{

					connection.Open();

					//This sql statement find the module in the database
					string sql = "SELECT TOP 1 * FROM MODULE WHERE USERNAME = '" + _userManager.GetUserName(HttpContext.User) + "';";

					SqlCommand command = new SqlCommand(sql, connection);
					SqlDataReader reader = command.ExecuteReader();
					//If the data does exist in the database then the data will be retrieved
					if (reader.HasRows)
					{
						while (reader.Read())
						{
							code = (reader["moduleCode"].ToString());
							hrs = double.Parse(reader["selfStudy"].ToString());
							weeks = int.Parse(reader["numWeeks"].ToString());
							name = (reader["moduleName"].ToString());
							date = DateOnly.Parse(reader["StartDate"].ToString().TrimEnd('0', ':'));

						}
					}
					reader.Close();
					connection.Close();

				}
				ViewBag.Name = JsonConvert.SerializeObject("Hours Studied Per Week - " + name + " (" + code + ")");

				ViewBag.Code = JsonConvert.SerializeObject("Actual Hours Studied");
				ViewBag.Hours = JsonConvert.SerializeObject("Ideal Weekly Study Hours");

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
				DateOnly startOfWeek = date;
				while (startOfWeek.DayOfWeek != DayOfWeek.Monday)
				{ startOfWeek = startOfWeek.AddDays((int)-1d); }
				DateOnly endOfWeek = startOfWeek.AddDays((int)6d);
				using (SqlConnection connection = new SqlConnection(Connection.conn))
				{
					connection.Open();

					for (int i = 0; i < weeks; i++)
					{
						//This sql statement checks if the module already exists in the database.
						string sql1 = "SELECT SelfStudyHours FROM STUDY WHERE USERNAME = '" + _userManager.GetUserName(HttpContext.User) + "' AND moduleCode = '" + code + "' AND studyDate BETWEEN '" + startOfWeek + "'AND '" + endOfWeek + "';";

						Thread.Sleep(100);
						SqlCommand command1 = new SqlCommand(sql1, connection);
						SqlDataReader reader1 = command1.ExecuteReader();
						//If the data does exist in the database then a message will be displayed to the user telling the user of this.
						if (reader1.HasRows)
						{
							try
							{
								while (reader1.Read())
								{
									studyHrs = studyHrs + decimal.Parse(reader1["SelfStudyHours"].ToString());
								}
							}

							catch (Exception ex)
							{
								_logger.LogError(ex.Message);
							}
						}
						startOfWeek = startOfWeek.AddDays((int)7d);
						endOfWeek = endOfWeek.AddDays((int)7d);
						dataPoints1.Add(new DataPoint(("Week " + (i + 1)).ToString(), ((double)studyHrs)));
						dataPoints2.Add(new DataPoint(("Week " + (i + 1)).ToString(), hrs));
						reader1.Close();
						studyHrs = 0;
					}
				}

				ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);

				ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
			}

			catch (Exception ex) {
				return View();
			}


			return View();
		}
	}
}
