using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LMSystem.Models;

namespace LMSystem.Controllers
{
    public class LibrariansController : Controller
    {
        private readonly IConfiguration _config;

        public LibrariansController(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index(string? searchTerm, int page = 1)
        {
            if (page < 1) page = 1;
            int pageSize = 5; // Change this number to control items per page
            int offset = (page - 1) * pageSize;

            var librarians = new List<Librarian>();
            int totalRecords = 0;

            using var con = GetConnection();
            con.Open();

            // 1. Get Total Count for Pagination Links
            string countQuery = "SELECT COUNT(*) FROM Librarians WHERE (@SearchTerm IS NULL OR Name LIKE '%' + @SearchTerm + '%')";
            using (var countCmd = new SqlCommand(countQuery, con))
            {
                countCmd.Parameters.AddWithValue("@SearchTerm", (object?)searchTerm ?? DBNull.Value);
                totalRecords = (int)countCmd.ExecuteScalar();
            }

            // 2. Fetch Filtered and Paginated Records (ORDER BY is required for OFFSET)
            string dataQuery = @"SELECT * FROM Librarians 
                                 WHERE (@SearchTerm IS NULL OR Name LIKE '%' + @SearchTerm + '%')
                                 ORDER BY LibrarianID 
                                 OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var cmd = new SqlCommand(dataQuery, con))
            {
                cmd.Parameters.AddWithValue("@SearchTerm", (object?)searchTerm ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Offset", offset);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    librarians.Add(new Librarian
                    {
                        LibrarianID = (int)reader["LibrarianID"],
                        Name = reader["Name"].ToString(),
                        Age = (int)reader["Age"],
                        Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : ""
                    });
                }
            }

            // 3. Populate and return View Model
            var viewModel = new LibrarianIndexViewModel
            {
                Librarians = librarians,
                SearchTerm = searchTerm,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Librarian model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var con = GetConnection();
            var cmd = new SqlCommand("INSERT INTO Librarians (Name, Age, Phone) VALUES (@Name, @Age, @Phone)", con);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Age", model.Age);
            cmd.Parameters.AddWithValue("@Phone", (object)model.Phone ?? DBNull.Value);
            
            con.Open();
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Librarian librarian = new();
            using var con = GetConnection();
            var cmd = new SqlCommand("SELECT * FROM Librarians WHERE LibrarianID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                librarian.LibrarianID = (int)reader["LibrarianID"];
                librarian.Name = reader["Name"].ToString();
                librarian.Age = (int)reader["Age"];
                librarian.Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "";
            }
            else
            {
                return NotFound();
            }
            return View(librarian);
        }

        [HttpPost]
        public IActionResult Edit(Librarian model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var con = GetConnection();
            var cmd = new SqlCommand("UPDATE Librarians SET Name=@Name, Age=@Age, Phone=@Phone WHERE LibrarianID=@id", con);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Age", model.Age);
            cmd.Parameters.AddWithValue("@Phone", (object)model.Phone ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@id", model.LibrarianID);
            
            con.Open();
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
        
        public IActionResult Details(int id)
        {
            Librarian librarian = new();
            using var con = GetConnection();
            var cmd = new SqlCommand("SELECT * FROM Librarians WHERE LibrarianID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                librarian.LibrarianID = (int)reader["LibrarianID"];
                librarian.Name = reader["Name"].ToString();
                librarian.Age = (int)reader["Age"];
                librarian.Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "";
            }
            else
            {
                return NotFound();
            }
            return View(librarian);
        }

        public IActionResult Delete(int id)
        {
            Librarian librarian = new();
            using var con = GetConnection();
            var cmd = new SqlCommand("SELECT * FROM Librarians WHERE LibrarianID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                librarian.LibrarianID = (int)reader["LibrarianID"];
                librarian.Name = reader["Name"].ToString();
                librarian.Age = (int)reader["Age"];
                librarian.Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : "";
            }
            else
            {
                return NotFound();
            }
            return View(librarian);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using var con = GetConnection();
            var cmd = new SqlCommand("DELETE FROM Librarians WHERE LibrarianID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            
            con.Open();
            cmd.ExecuteNonQuery();
            return RedirectToAction("Index");
        }
    }
}
