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
            _post = post;
            InitializeComponent();
        }
    }
}
