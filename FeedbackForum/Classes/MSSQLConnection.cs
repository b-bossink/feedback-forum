using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace FeedbackForum.Classes
{
    class MSSQLConnection
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
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM Post";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);

            List<Post> result = new List<Post>();
            using(SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    Post post = new Post(
                        reader["title"].ToString(),
                        LoadCategory(Convert.ToInt32(reader["category_id"])),
                        id
                        );
                    foreach (Attribute attribute in post.Category.Attributes)
                    {
                        post.SetAttributeValue(attribute, GetAttributeValue(id, attribute.ID));
                    }
                    result.Add(post);
                }
            }
            Console.WriteLine("succesfully added.");
            conn.Close();
            return result;
        }

        public List<Category> LoadCategories()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT * FROM Category";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Category category = new Category(reader["name"].ToString(), LoadAttributes(Convert.ToInt32(reader["id"])));
                }
            }
            return null;
        }

        private List<Attribute> LoadAttributes(int categoryID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = $"SELECT * FROM Attribute WHERE category_id = {categoryID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);
            List<Attribute> result = new List<Attribute>();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(new Attribute(reader["name"].ToString(), Convert.ToInt32(reader["id"])));
                }
            }

            conn.Close();
            return result;
        }

        private Category LoadCategory(int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = $"SELECT * FROM Category WHERE id = {id}";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);
            Category result = null;

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = new Category(reader["name"].ToString(), LoadAttributes(id));
                }
            }

            conn.Close();

            if (result == null)
                MessageBox.Show($"Couldnt find category with ID {id}");
            return result;
        }

        private string GetAttributeValue(int postID, int attributeID)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = $"SELECT * FROM PostAttribute WHERE post_id = {postID} AND attribute_id = {attributeID}";
            SqlCommand cmd = new SqlCommand(query, conn);
            Console.WriteLine("EXECUTING: " + query);
            string result = "";

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    result = reader["value"].ToString();
                }
            }

            conn.Close();

            return result;
        }
    }
}
