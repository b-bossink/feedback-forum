using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Presentation.Forms
{
    public partial class CategoryCreationForm : Form
    {
        public CategoryCreationForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Logic.Attribute> attributes = new List<Logic.Attribute>();
            TextBox[] textboxes = new TextBox[6]
            {
                textBox1,
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6
            };

            foreach (TextBox tbx in textboxes)
            {
                if (tbx.Text != "")
                {
                    attributes.Add(new Logic.Attribute(tbx.Text));
                }
            }
            Category category = new Category(tbxName.Text, attributes);
            category.Upload();
        }
    }
}
