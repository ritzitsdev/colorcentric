namespace printernet
{
  partial class config
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
      this.lblOrderFolderPath = new System.Windows.Forms.Label();
      this.orderFolderPath = new System.Windows.Forms.TextBox();
      this.btnOrderFolderBrowse = new System.Windows.Forms.Button();
      this.orderFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.btnCancel = new System.Windows.Forms.Button();
      this.btnSave = new System.Windows.Forms.Button();
      this.lblOrderSubmitURL = new System.Windows.Forms.Label();
      this.orderSubmitURL = new System.Windows.Forms.TextBox();
      this.lblSharedSecret = new System.Windows.Forms.Label();
      this.sharedSecret = new System.Windows.Forms.TextBox();
      this.lblFTPServer = new System.Windows.Forms.Label();
      this.ftpServer = new System.Windows.Forms.TextBox();
      this.lblcXmlSaveFolder = new System.Windows.Forms.Label();
      this.cXmlSaveFolderPath = new System.Windows.Forms.TextBox();
      this.cXmlSaveFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
      this.btncXmlSaveFolderBrowse = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.sqlServerName = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.sqlDatabaseName = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.sqlUsername = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.sqlPassword = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // lblOrderFolderPath
      // 
      this.lblOrderFolderPath.AutoSize = true;
      this.lblOrderFolderPath.Location = new System.Drawing.Point(8, 19);
      this.lblOrderFolderPath.Name = "lblOrderFolderPath";
      this.lblOrderFolderPath.Size = new System.Drawing.Size(90, 13);
      this.lblOrderFolderPath.TabIndex = 0;
      this.lblOrderFolderPath.Text = "Order Folder Path";
      // 
      // orderFolderPath
      // 
      this.orderFolderPath.Location = new System.Drawing.Point(8, 35);
      this.orderFolderPath.Name = "orderFolderPath";
      this.orderFolderPath.Size = new System.Drawing.Size(511, 20);
      this.orderFolderPath.TabIndex = 1;
      // 
      // btnOrderFolderBrowse
      // 
      this.btnOrderFolderBrowse.Location = new System.Drawing.Point(525, 34);
      this.btnOrderFolderBrowse.Name = "btnOrderFolderBrowse";
      this.btnOrderFolderBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnOrderFolderBrowse.TabIndex = 2;
      this.btnOrderFolderBrowse.Text = "Browse";
      this.btnOrderFolderBrowse.UseVisualStyleBackColor = true;
      this.btnOrderFolderBrowse.Click += new System.EventHandler(this.btnOrderFolderBrowse_Click);
      // 
      // orderFolderBrowserDialog
      // 
      this.orderFolderBrowserDialog.Description = "Select order folder to monitor";
      // 
      // btnCancel
      // 
      this.btnCancel.Location = new System.Drawing.Point(500, 615);
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size(75, 24);
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
      // 
      // btnSave
      // 
      this.btnSave.Location = new System.Drawing.Point(419, 615);
      this.btnSave.Name = "btnSave";
      this.btnSave.Size = new System.Drawing.Size(75, 24);
      this.btnSave.TabIndex = 4;
      this.btnSave.Text = "Save";
      this.btnSave.UseVisualStyleBackColor = true;
      this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
      // 
      // lblOrderSubmitURL
      // 
      this.lblOrderSubmitURL.AutoSize = true;
      this.lblOrderSubmitURL.Location = new System.Drawing.Point(7, 76);
      this.lblOrderSubmitURL.Name = "lblOrderSubmitURL";
      this.lblOrderSubmitURL.Size = new System.Drawing.Size(93, 13);
      this.lblOrderSubmitURL.TabIndex = 5;
      this.lblOrderSubmitURL.Text = "Order Submit URL";
      // 
      // orderSubmitURL
      // 
      this.orderSubmitURL.Location = new System.Drawing.Point(8, 93);
      this.orderSubmitURL.Name = "orderSubmitURL";
      this.orderSubmitURL.Size = new System.Drawing.Size(511, 20);
      this.orderSubmitURL.TabIndex = 6;
      // 
      // lblSharedSecret
      // 
      this.lblSharedSecret.AutoSize = true;
      this.lblSharedSecret.Location = new System.Drawing.Point(7, 129);
      this.lblSharedSecret.Name = "lblSharedSecret";
      this.lblSharedSecret.Size = new System.Drawing.Size(75, 13);
      this.lblSharedSecret.TabIndex = 7;
      this.lblSharedSecret.Text = "Shared Secret";
      // 
      // sharedSecret
      // 
      this.sharedSecret.Location = new System.Drawing.Point(8, 146);
      this.sharedSecret.Name = "sharedSecret";
      this.sharedSecret.Size = new System.Drawing.Size(511, 20);
      this.sharedSecret.TabIndex = 8;
      // 
      // lblFTPServer
      // 
      this.lblFTPServer.AutoSize = true;
      this.lblFTPServer.Location = new System.Drawing.Point(8, 188);
      this.lblFTPServer.Name = "lblFTPServer";
      this.lblFTPServer.Size = new System.Drawing.Size(61, 13);
      this.lblFTPServer.TabIndex = 9;
      this.lblFTPServer.Text = "FTP Server";
      // 
      // ftpServer
      // 
      this.ftpServer.Location = new System.Drawing.Point(10, 204);
      this.ftpServer.Name = "ftpServer";
      this.ftpServer.Size = new System.Drawing.Size(509, 20);
      this.ftpServer.TabIndex = 10;
      // 
      // lblcXmlSaveFolder
      // 
      this.lblcXmlSaveFolder.AutoSize = true;
      this.lblcXmlSaveFolder.Location = new System.Drawing.Point(8, 243);
      this.lblcXmlSaveFolder.Name = "lblcXmlSaveFolder";
      this.lblcXmlSaveFolder.Size = new System.Drawing.Size(146, 13);
      this.lblcXmlSaveFolder.TabIndex = 11;
      this.lblcXmlSaveFolder.Text = "Select folder to save cXml file";
      // 
      // cXmlSaveFolderPath
      // 
      this.cXmlSaveFolderPath.Location = new System.Drawing.Point(8, 259);
      this.cXmlSaveFolderPath.Name = "cXmlSaveFolderPath";
      this.cXmlSaveFolderPath.Size = new System.Drawing.Size(511, 20);
      this.cXmlSaveFolderPath.TabIndex = 12;
      // 
      // btncXmlSaveFolderBrowse
      // 
      this.btncXmlSaveFolderBrowse.Location = new System.Drawing.Point(528, 258);
      this.btncXmlSaveFolderBrowse.Name = "btncXmlSaveFolderBrowse";
      this.btncXmlSaveFolderBrowse.Size = new System.Drawing.Size(75, 23);
      this.btncXmlSaveFolderBrowse.TabIndex = 13;
      this.btncXmlSaveFolderBrowse.Text = "Browse";
      this.btncXmlSaveFolderBrowse.UseVisualStyleBackColor = true;
      this.btncXmlSaveFolderBrowse.Click += new System.EventHandler(this.btncXmlSaveFolderBrowse_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(5, 292);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(121, 13);
      this.label1.TabIndex = 14;
      this.label1.Text = "Enter SQL Server Name";
      // 
      // sqlServerName
      // 
      this.sqlServerName.Location = new System.Drawing.Point(8, 308);
      this.sqlServerName.Name = "sqlServerName";
      this.sqlServerName.Size = new System.Drawing.Size(511, 20);
      this.sqlServerName.TabIndex = 15;
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(5, 343);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(112, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "Enter Database Name";
      // 
      // sqlDatabaseName
      // 
      this.sqlDatabaseName.Location = new System.Drawing.Point(8, 359);
      this.sqlDatabaseName.Name = "sqlDatabaseName";
      this.sqlDatabaseName.Size = new System.Drawing.Size(511, 20);
      this.sqlDatabaseName.TabIndex = 17;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(5, 394);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(132, 13);
      this.label3.TabIndex = 18;
      this.label3.Text = "Enter Database Username";
      // 
      // sqlUsername
      // 
      this.sqlUsername.Location = new System.Drawing.Point(8, 410);
      this.sqlUsername.Name = "sqlUsername";
      this.sqlUsername.Size = new System.Drawing.Size(511, 20);
      this.sqlUsername.TabIndex = 19;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(5, 447);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(130, 13);
      this.label4.TabIndex = 20;
      this.label4.Text = "Enter Database Password";
      // 
      // sqlPassword
      // 
      this.sqlPassword.Location = new System.Drawing.Point(8, 463);
      this.sqlPassword.Name = "sqlPassword";
      this.sqlPassword.Size = new System.Drawing.Size(511, 20);
      this.sqlPassword.TabIndex = 21;
      // 
      // config
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(615, 651);
      this.Controls.Add(this.sqlPassword);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.sqlUsername);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.sqlDatabaseName);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.sqlServerName);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.btncXmlSaveFolderBrowse);
      this.Controls.Add(this.cXmlSaveFolderPath);
      this.Controls.Add(this.lblcXmlSaveFolder);
      this.Controls.Add(this.ftpServer);
      this.Controls.Add(this.lblFTPServer);
      this.Controls.Add(this.sharedSecret);
      this.Controls.Add(this.lblSharedSecret);
      this.Controls.Add(this.orderSubmitURL);
      this.Controls.Add(this.lblOrderSubmitURL);
      this.Controls.Add(this.btnSave);
      this.Controls.Add(this.btnCancel);
      this.Controls.Add(this.btnOrderFolderBrowse);
      this.Controls.Add(this.orderFolderPath);
      this.Controls.Add(this.lblOrderFolderPath);
      this.Name = "config";
      this.Text = "config";
      this.Load += new System.EventHandler(this.config_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lblOrderFolderPath;
    private System.Windows.Forms.TextBox orderFolderPath;
    private System.Windows.Forms.Button btnOrderFolderBrowse;
    private System.Windows.Forms.FolderBrowserDialog orderFolderBrowserDialog;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Label lblOrderSubmitURL;
    private System.Windows.Forms.TextBox orderSubmitURL;
    private System.Windows.Forms.Label lblSharedSecret;
    private System.Windows.Forms.TextBox sharedSecret;
    private System.Windows.Forms.Label lblFTPServer;
    private System.Windows.Forms.TextBox ftpServer;
    private System.Windows.Forms.Label lblcXmlSaveFolder;
    private System.Windows.Forms.TextBox cXmlSaveFolderPath;
    private System.Windows.Forms.FolderBrowserDialog cXmlSaveFolderBrowserDialog;
    private System.Windows.Forms.Button btncXmlSaveFolderBrowse;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox sqlServerName;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox sqlDatabaseName;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox sqlUsername;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.TextBox sqlPassword;
  }
}