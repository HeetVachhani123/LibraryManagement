using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using LMSystem.Models;

namespace LMSystem.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IConfiguration _configuration;

        public DashboardController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                model.TotalStudents = ExecuteScalarQuery(con, "SELECT COUNT(1) FROM Students");
                model.TotalBooks = ExecuteScalarQuery(con, "SELECT COUNT(1) FROM Books13");
                model.TotalLibrarians = ExecuteScalarQuery(con, "SELECT COUNT(1) FROM Librarians");
                model.TotalBorrowings = ExecuteScalarQuery(con, "SELECT COUNT(1) FROM BorrowRecords13");
                model.TotalPublications = ExecuteScalarQuery(con, "SELECT COUNT(1) FROM Publications");
            }

            return View(model);
        }

        private int ExecuteScalarQuery(SqlConnection con, string query)
        {
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                object result = cmd.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToInt32(result) : 0;
            }
        }
    }
}
