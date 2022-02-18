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
            if (tbxName.TextLength <= 0 || tbxDescription.TextLength <= 0 || cmbCategory.SelectedIndex < 0 || tbxMoreText.TextLength <= 0)
                return;

            Post post = new Post(tbxName.Text, DateTime.Now, new List<Comment>(), selectedCategory);
            post.SetAttributeValue("Description", tbxDescription.Text);
            post.SetAttributeValue("More Text", tbxMoreText.Text);
            MessageBox.Show("Created new Post! This post has: \nTitle: " + post.Name + "\nUpload Date: " + post.CreationDate + "\nCategory: " + post.Category.Name + "\nAttributes" + post.Category.Attributes.Keys);
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
