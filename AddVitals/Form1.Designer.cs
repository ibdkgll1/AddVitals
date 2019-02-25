namespace AddVitals
{
    partial class Form1
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btOpenFileDialog = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbDirectoryPath = new System.Windows.Forms.Label();
            this.lbFileName = new System.Windows.Forms.Label();
            this.btExit = new System.Windows.Forms.Button();
            this.btUpload = new System.Windows.Forms.Button();
            this.lbStatus = new System.Windows.Forms.Label();
            this.lbFullPath = new System.Windows.Forms.Label();
            this.dgvDetail = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btLoadFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // btOpenFileDialog
            // 
            this.btOpenFileDialog.Location = new System.Drawing.Point(6, 46);
            this.btOpenFileDialog.Name = "btOpenFileDialog";
            this.btOpenFileDialog.Size = new System.Drawing.Size(30, 23);
            this.btOpenFileDialog.TabIndex = 0;
            this.btOpenFileDialog.Text = "...";
            this.btOpenFileDialog.UseVisualStyleBackColor = true;
            this.btOpenFileDialog.Click += new System.EventHandler(this.btOpenFileDialog_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(42, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "File:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lbDirectoryPath);
            this.groupBox1.Controls.Add(this.lbFileName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btOpenFileDialog);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select File:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Path:";
            // 
            // lbDirectoryPath
            // 
            this.lbDirectoryPath.AutoSize = true;
            this.lbDirectoryPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDirectoryPath.Location = new System.Drawing.Point(44, 26);
            this.lbDirectoryPath.Name = "lbDirectoryPath";
            this.lbDirectoryPath.Size = new System.Drawing.Size(41, 13);
            this.lbDirectoryPath.TabIndex = 3;
            this.lbDirectoryPath.Text = "label2";
            // 
            // lbFileName
            // 
            this.lbFileName.AutoSize = true;
            this.lbFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFileName.Location = new System.Drawing.Point(86, 51);
            this.lbFileName.Name = "lbFileName";
            this.lbFileName.Size = new System.Drawing.Size(41, 13);
            this.lbFileName.TabIndex = 2;
            this.lbFileName.Text = "label2";
            // 
            // btExit
            // 
            this.btExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btExit.Location = new System.Drawing.Point(888, 12);
            this.btExit.Name = "btExit";
            this.btExit.Size = new System.Drawing.Size(99, 75);
            this.btExit.TabIndex = 3;
            this.btExit.Text = "Application Exit";
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // btUpload
            // 
            this.btUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUpload.Location = new System.Drawing.Point(783, 12);
            this.btUpload.Name = "btUpload";
            this.btUpload.Size = new System.Drawing.Size(99, 75);
            this.btUpload.TabIndex = 4;
            this.btUpload.Text = "Upload Data";
            this.btUpload.UseVisualStyleBackColor = true;
            this.btUpload.Click += new System.EventHandler(this.btUpload_Click);
            // 
            // lbStatus
            // 
            this.lbStatus.AutoSize = true;
            this.lbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStatus.Location = new System.Drawing.Point(15, 353);
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(51, 16);
            this.lbStatus.TabIndex = 6;
            this.lbStatus.Text = "label2";
            // 
            // lbFullPath
            // 
            this.lbFullPath.AutoSize = true;
            this.lbFullPath.Location = new System.Drawing.Point(1151, 9);
            this.lbFullPath.Name = "lbFullPath";
            this.lbFullPath.Size = new System.Drawing.Size(35, 13);
            this.lbFullPath.TabIndex = 7;
            this.lbFullPath.Text = "label3";
            // 
            // dgvDetail
            // 
            this.dgvDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetail.Location = new System.Drawing.Point(12, 114);
            this.dgvDetail.Name = "dgvDetail";
            this.dgvDetail.Size = new System.Drawing.Size(969, 225);
            this.dgvDetail.TabIndex = 8;
            this.dgvDetail.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.GridView_RowPostPaint);
            // 
            // groupBox2
            // 
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(975, 246);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Spreadsheet Details:";
            // 
            // btLoadFile
            // 
            this.btLoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btLoadFile.Location = new System.Drawing.Point(678, 12);
            this.btLoadFile.Name = "btLoadFile";
            this.btLoadFile.Size = new System.Drawing.Size(99, 75);
            this.btLoadFile.TabIndex = 10;
            this.btLoadFile.Text = "Load Excel File";
            this.btLoadFile.UseVisualStyleBackColor = true;
            this.btLoadFile.Click += new System.EventHandler(this.btLoadFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 376);
            this.Controls.Add(this.btLoadFile);
            this.Controls.Add(this.dgvDetail);
            this.Controls.Add(this.lbFullPath);
            this.Controls.Add(this.lbStatus);
            this.Controls.Add(this.btUpload);
            this.Controls.Add(this.btExit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "Form1";
            this.Text = "Vitals Loader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btOpenFileDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbFileName;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Button btUpload;
        private System.Windows.Forms.Label lbStatus;
        private System.Windows.Forms.Label lbDirectoryPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbFullPath;
        private System.Windows.Forms.DataGridView dgvDetail;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btLoadFile;

    }
}

