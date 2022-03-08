
namespace FeedbackForum.Forms
{
    partial class PostForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.tbxNewComment = new System.Windows.Forms.TextBox();
            this.btnPostComment = new System.Windows.Forms.Button();
            this.tbxComments = new System.Windows.Forms.TextBox();
            this.lblUpvotes = new System.Windows.Forms.Label();
            this.btnUpvote = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblName.Location = new System.Drawing.Point(117, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(406, 128);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "<TITEL>";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.Location = new System.Drawing.Point(55, 158);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(183, 45);
            this.lblHeader.TabIndex = 1;
            this.lblHeader.Text = "<koptitel>";
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblText.Location = new System.Drawing.Point(67, 203);
            this.lblText.MaximumSize = new System.Drawing.Size(1325, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(1305, 80);
            this.lblText.TabIndex = 2;
            this.lblText.Text = "text text text text text text text text text text text text text text text text t" +
    "ext text text text text text text text text text text text text text text text t" +
    "ext text text text text text ";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDate.Location = new System.Drawing.Point(1190, 53);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(169, 40);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "01-01-2022";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxNewComment
            // 
            this.tbxNewComment.Location = new System.Drawing.Point(67, 802);
            this.tbxNewComment.Multiline = true;
            this.tbxNewComment.Name = "tbxNewComment";
            this.tbxNewComment.Size = new System.Drawing.Size(1320, 146);
            this.tbxNewComment.TabIndex = 4;
            // 
            // btnPostComment
            // 
            this.btnPostComment.Location = new System.Drawing.Point(1237, 902);
            this.btnPostComment.Name = "btnPostComment";
            this.btnPostComment.Size = new System.Drawing.Size(150, 46);
            this.btnPostComment.TabIndex = 5;
            this.btnPostComment.Text = "Plaats";
            this.btnPostComment.UseVisualStyleBackColor = true;
            this.btnPostComment.Click += new System.EventHandler(this.btnPostComment_Click);
            // 
            // tbxComments
            // 
            this.tbxComments.Location = new System.Drawing.Point(2, 979);
            this.tbxComments.MaxLength = 50000;
            this.tbxComments.Multiline = true;
            this.tbxComments.Name = "tbxComments";
            this.tbxComments.ReadOnly = true;
            this.tbxComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxComments.Size = new System.Drawing.Size(1450, 306);
            this.tbxComments.TabIndex = 6;
            // 
            // lblUpvotes
            // 
            this.lblUpvotes.AutoSize = true;
            this.lblUpvotes.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblUpvotes.Location = new System.Drawing.Point(73, 52);
            this.lblUpvotes.Name = "lblUpvotes";
            this.lblUpvotes.Size = new System.Drawing.Size(38, 45);
            this.lblUpvotes.TabIndex = 7;
            this.lblUpvotes.Text = "0";
            // 
            // btnUpvote
            // 
            this.btnUpvote.Location = new System.Drawing.Point(67, 96);
            this.btnUpvote.Name = "btnUpvote";
            this.btnUpvote.Size = new System.Drawing.Size(48, 46);
            this.btnUpvote.TabIndex = 8;
            this.btnUpvote.Text = "+";
            this.btnUpvote.UseVisualStyleBackColor = true;
            this.btnUpvote.Click += new System.EventHandler(this.btnUpvote_Click);
            // 
            // PostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1454, 1288);
            this.Controls.Add(this.btnUpvote);
            this.Controls.Add(this.lblUpvotes);
            this.Controls.Add(this.tbxComments);
            this.Controls.Add(this.btnPostComment);
            this.Controls.Add(this.tbxNewComment);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.lblName);
            this.Name = "PostForm";
            this.Text = "PostForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox tbxNewComment;
        private System.Windows.Forms.Button btnPostComment;
        private System.Windows.Forms.TextBox tbxComments;
        private System.Windows.Forms.Label lblUpvotes;
        private System.Windows.Forms.Button btnUpvote;
    }
}