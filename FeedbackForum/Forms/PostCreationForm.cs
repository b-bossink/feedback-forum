using FeedbackForum.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedbackForum
{
    public partial class PostCreationForm : Form
    {
        private Category selectedCategory;
        public PostCreationForm()
        {
            InitializeComponent();

            foreach (Category category in Category.GetAll())
            {
                cmbCategory.Items.Add(category.Name);
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            Post post = new Post(tbxName.Text, DateTime.Now, new List<Comment>(), new Category("", new Dictionary<string, string>()));
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Category category in Category.GetAll())
            {
                if (cmbCategory.SelectedItem.ToString() == category.Name)
                {
                    selectedCategory = category;
                    return;
                }
            }
        }
    }
}
