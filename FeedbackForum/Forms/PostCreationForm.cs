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
        private PostContainer postContainer;
        private CategoryContainer categoryContainer;
        private Category selectedCategory;
        private List<Control[]> allFields = new List<Control[]>();
        public PostCreationForm()
        {
            InitializeComponent();
            postContainer = new PostContainer();
            categoryContainer = new CategoryContainer();

            foreach (Category category in categoryContainer.Categories)
            {
                cmbCategory.Items.Add(category.Name);
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            if (tbxName.TextLength <= 0 || cmbCategory.SelectedIndex < 0)
                return;

            
            Post post = new Post(tbxName.Text, selectedCategory);
            foreach (Control[] field in allFields)
            {
                //if (field[1].Text.Length <= 0)
                //    return;


                post.SetAttributeValue(field[0].Text, field[1].Text);
            }
            lblName.Text = "lol";

            postContainer.Add(post);
            MessageBox.Show("Created new Post! This post has: \nTitle: " + post.Name + "\nUpload Date: " + post.CreationDate + "\nCategory: " + post.Category.Name + "\nAttributes" + post.Category.Attributes.Keys);
        }

        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (Category category in categoryContainer.Categories)
            {
                if (cmbCategory.SelectedItem.ToString() == category.Name)
                {
                    selectedCategory = category;
                    DeleteFields();
                    ShowFields();
                    return;
                }
            }
        }
        private void DeleteFields()
        {
            foreach(Control[] field in allFields)
            {
                Controls.Remove(field[0]);
                Controls.Remove(field[1]);
            }
            allFields.Clear();
        }
        private void ShowFields()
        {
            int i = 90;
            foreach (KeyValuePair<string,string> attribute in selectedCategory.Attributes)
            {
                Label label = new Label()
                {
                    AutoSize = true,
                    Text = attribute.Key,
                    Size = lblName.Size,
                    Font = lblName.Font,
                    Location = new Point(lblName.Location.X, lblName.Location.Y + i)
                };

                TextBox textbox = new TextBox()
                {
                    Size = tbxName.Size,
                    Font = tbxName.Font,
                    Location = new Point(tbxName.Location.X, tbxName.Location.Y + i)
                };

                Console.WriteLine(label.Text);

                this.Controls.Add(label);
                this.Controls.Add(textbox);

                allFields.Add(new Control[2]
                {
                    label,
                    textbox
                });
                i += 90;
            }
        }
    }
}
