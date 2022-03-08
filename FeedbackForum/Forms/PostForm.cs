using FeedbackForum.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FeedbackForum.Forms
{
    public partial class PostForm : Form
    {
        private Post post;
        public PostForm(Post _post)
        {
            post = _post;
            InitializeComponent();

            lblName.Text = post.Name;
            lblDate.Text = post.CreationDate.ToString();
            CreateFields();
        }

        private void CreateFields()
        {
            int i = 0;

            foreach (KeyValuePair<string,string> attribute in post.Attributes)
            {
                if (i == 0)
                {
                    lblHeader.Text = attribute.Key;
                    lblText.Text = attribute.Value;
                }
                else
                {
                    Label attributeHeader = new Label()
                    {
                        AutoSize = true,
                        Text = attribute.Key,
                        Size = lblHeader.Size,
                        Font = lblHeader.Font,
                        Location = new Point(lblText.Location.X, lblText.Location.Y + i)
                    };

                    Label attributeText = new Label()
                    {
                        AutoEllipsis = true,
                        Text = attribute.Value,
                        Size = lblText.Size,
                        MaximumSize = new Size(200000,20000),
                        Font = lblText.Font,
                        Location = new Point(lblText.Location.X, attributeHeader.Location.Y + 45)
                    };

                    Controls.Add(attributeHeader);
                    Controls.Add(attributeText);
                }
                i += 90;
            }
        }
    }
}
