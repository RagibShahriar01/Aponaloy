namespace APONALOY.AllClass
{
    partial class AdminPannel
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
            this.BtnApprove = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dgvRequests = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnSendMessage = new System.Windows.Forms.Button();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBoxsingout = new System.Windows.Forms.PictureBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.BtnDeleteUser = new System.Windows.Forms.Button();
            this.textBoxUserSearch = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsingout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnApprove
            // 
            this.BtnApprove.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnApprove.Location = new System.Drawing.Point(1053, 126);
            this.BtnApprove.Name = "BtnApprove";
            this.BtnApprove.Size = new System.Drawing.Size(93, 48);
            this.BtnApprove.TabIndex = 1;
            this.BtnApprove.Text = "Approve";
            this.BtnApprove.UseVisualStyleBackColor = true;
            this.BtnApprove.Click += new System.EventHandler(this.BtnApprove_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1053, 285);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(206, 67);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // dgvRequests
            // 
            this.dgvRequests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRequests.Location = new System.Drawing.Point(15, 126);
            this.dgvRequests.Name = "dgvRequests";
            this.dgvRequests.RowHeadersWidth = 51;
            this.dgvRequests.RowTemplate.Height = 24;
            this.dgvRequests.Size = new System.Drawing.Size(1018, 321);
            this.dgvRequests.TabIndex = 3;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(429, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(206, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(261, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search Appointment";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1049, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(231, 19);
            this.label2.TabIndex = 6;
            this.label2.Text = "Send Appointment Date Message";
            // 
            // BtnSendMessage
            // 
            this.BtnSendMessage.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSendMessage.Location = new System.Drawing.Point(1052, 368);
            this.BtnSendMessage.Name = "BtnSendMessage";
            this.BtnSendMessage.Size = new System.Drawing.Size(78, 42);
            this.BtnSendMessage.TabIndex = 7;
            this.BtnSendMessage.Text = "Send";
            this.BtnSendMessage.UseVisualStyleBackColor = true;
            this.BtnSendMessage.Click += new System.EventHandler(this.BtnSendMessage_Click);
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = global::APONALOY.Properties.Resources.ICON;
            this.pictureBox8.Location = new System.Drawing.Point(12, 12);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(60, 61);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox8.TabIndex = 54;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBoxsingout
            // 
            this.pictureBoxsingout.Image = global::APONALOY.Properties.Resources.SignOut;
            this.pictureBoxsingout.Location = new System.Drawing.Point(1079, 12);
            this.pictureBoxsingout.Name = "pictureBoxsingout";
            this.pictureBoxsingout.Size = new System.Drawing.Size(90, 47);
            this.pictureBoxsingout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxsingout.TabIndex = 8;
            this.pictureBoxsingout.TabStop = false;
            this.pictureBoxsingout.Click += new System.EventHandler(this.pictureBoxsingout_Click);
            // 
            // dgvUsers
            // 
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Location = new System.Drawing.Point(95, 483);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.Size = new System.Drawing.Size(867, 236);
            this.dgvUsers.TabIndex = 55;
            // 
            // BtnDeleteUser
            // 
            this.BtnDeleteUser.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteUser.Location = new System.Drawing.Point(1052, 483);
            this.BtnDeleteUser.Name = "BtnDeleteUser";
            this.BtnDeleteUser.Size = new System.Drawing.Size(93, 60);
            this.BtnDeleteUser.TabIndex = 56;
            this.BtnDeleteUser.Text = "Delete User";
            this.BtnDeleteUser.UseVisualStyleBackColor = true;
            this.BtnDeleteUser.Click += new System.EventHandler(this.BtnDeleteUser_Click);
            // 
            // textBoxUserSearch
            // 
            this.textBoxUserSearch.Location = new System.Drawing.Point(1052, 607);
            this.textBoxUserSearch.Name = "textBoxUserSearch";
            this.textBoxUserSearch.Size = new System.Drawing.Size(215, 22);
            this.textBoxUserSearch.TabIndex = 57;
            this.textBoxUserSearch.TextChanged += new System.EventHandler(this.textBoxUserSearch_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1048, 576);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 19);
            this.label3.TabIndex = 58;
            this.label3.Text = "Search User";
            // 
            // AdminPannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(1289, 758);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUserSearch);
            this.Controls.Add(this.BtnDeleteUser);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBoxsingout);
            this.Controls.Add(this.BtnSendMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgvRequests);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.BtnApprove);
            this.Name = "AdminPannel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminPannel";
            this.Load += new System.EventHandler(this.AdminPannel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequests)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxsingout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BtnApprove;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataGridView dgvRequests;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnSendMessage;
        private System.Windows.Forms.PictureBox pictureBoxsingout;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Button BtnDeleteUser;
        private System.Windows.Forms.TextBox textBoxUserSearch;
        private System.Windows.Forms.Label label3;
    }
}