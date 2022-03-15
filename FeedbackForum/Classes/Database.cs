using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace FeedbackForum.Classes
{
    class Database
    {
        private string connectionString = "Server=mssqlstud.fhict.local;Database=dbi426602;User Id=dbi426602;Password=db80551Nk!;";

        public void Update(Post post)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "insert into Post (category_id, user_id, title, creation_date) values" +
                $"(1, 1, '{post.Name}', '{post.CreationDate.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}');";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine(query);

            try
            {
                cmd.ExecuteNonQuery();
            } catch
            {
                MessageBox.Show("Something went wrong.");
            }

            conn.Close();
        }

        public void Update(Category category)
        {

        }

        public List<Post> LoadPosts()
        {
            return null;
        }

        public List<Category> LoadCategories()
        {
            return null;
        }
    }
}
