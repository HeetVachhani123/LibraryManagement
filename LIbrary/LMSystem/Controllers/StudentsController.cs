using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LMSystem.Models;

namespace LMSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _configuration;

        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public IActionResult Index(string? searchTerm, int page = 1)
        {
            var viewModel = new StudentIndexViewModel
            {
                SearchTerm = searchTerm,
                CurrentPage = page < 1 ? 1 : page
            };

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var con = new Microsoft.Data.SqlClient.SqlConnection(connectionString);
            con.Open();

            // 1. Build Dynamic Search Query Components
            string searchCondition = "";
            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchCondition = " WHERE Name LIKE @Search OR Email LIKE @Search OR Phone LIKE @Search";
            }

            // 2. Query Total Count for Pagination Bounds
            string countQuery = $"SELECT COUNT(*) FROM Students{searchCondition}";
            using (var countCmd = new Microsoft.Data.SqlClient.SqlCommand(countQuery, con))
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    countCmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%");
                }
                int totalRecords = (int)countCmd.ExecuteScalar();
                viewModel.TotalPages = (int)Math.Ceiling((double)totalRecords / viewModel.PageSize);
            }

            // Fallback adjustment if current page is out of calculated bounds
            if (viewModel.CurrentPage > viewModel.TotalPages && viewModel.TotalPages > 0)
            {
                viewModel.CurrentPage = viewModel.TotalPages;
            }

            // 3. Fetch Paginated Segment using OFFSET-FETCH
            int offset = (viewModel.CurrentPage - 1) * viewModel.PageSize;
            string dataQuery = $@"SELECT * FROM Students{searchCondition}
                                 ORDER BY StudentID 
                                 OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var dataCmd = new Microsoft.Data.SqlClient.SqlCommand(dataQuery, con))
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    dataCmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%");
                }
                dataCmd.Parameters.AddWithValue("@Offset", offset);
                dataCmd.Parameters.AddWithValue("@PageSize", viewModel.PageSize);

                using var reader = dataCmd.ExecuteReader();
                while (reader.Read())
                {
                    viewModel.Students.Add(new Student
                    {
                        StudentID = (int)reader["StudentID"],
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : ""
                    });
                }
            }

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                using (var con = GetConnection())
                {
                    string query = "INSERT INTO Students (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Phone", (object)student.Phone ?? DBNull.Value);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Student student = null;
            using (var con = GetConnection())
            {
                string query = "SELECT StudentID, Name, Email, Phone FROM Students WHERE StudentID = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                        }
                    }
                }
            }
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.StudentID) return NotFound();

            if (ModelState.IsValid)
            {
                using (var con = GetConnection())
                {
                    string query = "UPDATE Students SET Name = @Name, Email = @Email, Phone = @Phone WHERE StudentID = @Id";
                    using (var cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", student.StudentID);
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Email", student.Email);
                        cmd.Parameters.AddWithValue("@Phone", (object)student.Phone ?? DBNull.Value);
                        
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            Student student = null;
            using (var con = GetConnection())
            {
                string query = "SELECT StudentID, Name, Email, Phone FROM Students WHERE StudentID = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                        }
                    }
                }
            }
            if (student == null) return NotFound();
            return View(student);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            Student student = null;
            using (var con = GetConnection())
            {
                string query = "SELECT StudentID, Name, Email, Phone FROM Students WHERE StudentID = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                        }
                    }
                }
            }
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var con = GetConnection())
            {
                string query = "DELETE FROM Students WHERE StudentID = @Id";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
