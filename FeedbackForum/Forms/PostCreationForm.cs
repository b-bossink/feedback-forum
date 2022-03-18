using FeedbackForum.Forms;
using Logic;
using Logic.Containers;
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

            if (categoryContainer.Categories.Count > 0)
            {
                foreach (Category category in categoryContainer.Categories)
                {
                    cmbCategory.Items.Add(category.Name);
                }
            }
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            postContainer.Refresh();

            if (tbxName.TextLength <= 0 || cmbCategory.SelectedIndex < 0)
                return;

            
            Post post = new Post(tbxName.Text, DateTime.Now, new List<Comment>(), 0,
                selectedCategory, new Dictionary<Logic.Attribute, string>());
            foreach (Control[] field in allFields)
            {
                if (field[1].Text.Length <= 0)
                {
                    MessageBox.Show($"Vul de gevraagde gegevens ({field[0].Text}) field in.");
                    return;
                }    
                post.SetAttributeValue(new Logic.Attribute(field[0].Text), field[1].Text);
            }
            post.Upload();
            PostForm postForm = new PostForm(post);
            postForm.Show();
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
            foreach (Logic.Attribute attribute in selectedCategory.Attributes)
            {
                Label label = new Label()
                {
                    AutoSize = true,
                    Text = attribute.Name,
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
