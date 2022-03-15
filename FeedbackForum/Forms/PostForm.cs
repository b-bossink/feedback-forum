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
        private bool hasUpvote;
        public PostForm(Post _post)
        {
            post = _post;
            InitializeComponent();

            lblName.Text = post.Name;
            lblDate.Text = post.CreationDate.ToString();
            lblUpvotes.Text = post.Upvotes.ToString();
            CreateFields();
            ShowComments();
        }

        private void CreateFields()
        {
            int i = 0;

            foreach (KeyValuePair<Classes.Attribute,string> attribute in post.ValuesByAttributes)
            {
                if (i == 0)
                {
                    lblHeader.Text = attribute.Key.Name;
                    lblText.Text = attribute.Value;
                }
                else
                {
                    Label attributeHeader = new Label()
                    {
                        AutoSize = true,
                        Text = attribute.Key.Name,
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

        private void ShowComments()
        {
            tbxComments.Text = "";
            for (int i = 0; i < post.Comments.Count; i++)
            {
                tbxComments.Text += $"USERNAME: {post.Comments[i].CreationDate}\r\n" + post.Comments[i].Text + "\r\n\r\n";
            }
        }

        private void btnPostComment_Click(object sender, EventArgs e)
        {
            if (tbxNewComment.Text.Length > 0)
            {
                post.Comments.Add(new Comment(tbxNewComment.Text));
                tbxNewComment.Text = "";
                ShowComments();
            }
        }

        private void btnUpvote_Click(object sender, EventArgs e)
        {
            if (!hasUpvote)
                post.Add(1);
            else
                post.Add(-1);

            hasUpvote = !hasUpvote;
            lblUpvotes.Text = post.Upvotes.ToString();
        }
    }
}
