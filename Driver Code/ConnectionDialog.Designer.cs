namespace SD.LLBLGen.Pro.LINQPadDriver
{
	partial class ConnectionDialog
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
			if(disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._browseASpecAssemblyButton = new System.Windows.Forms.Button();
            this._browseAGenAssemblyButton = new System.Windows.Forms.Button();
            this._browseSSAssemblyButton = new System.Windows.Forms.Button();
            this._aSpecAssemblyLabel = new System.Windows.Forms.Label();
            this._aGenAssemblyLabel = new System.Windows.Forms.Label();
            this._ssAssemblyLabel = new System.Windows.Forms.Label();
            this._aSpecAssemblyTextBox = new System.Windows.Forms.TextBox();
            this._aGenAssemblyTextBox = new System.Windows.Forms.TextBox();
            this._ssAssemblyTextBox = new System.Windows.Forms.TextBox();
            this._adapterRadioButton = new System.Windows.Forms.RadioButton();
            this._selfServicingRadioButton = new System.Windows.Forms.RadioButton();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._browseAppConfigFileButton = new System.Windows.Forms.Button();
            this._connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._appConfigFileTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this._mainErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this._browseOrmProfilerInterceptorDllButton = new System.Windows.Forms.Button();
            this._ormProfilerInterceptorLabel = new System.Windows.Forms.Label();
            this._ormprofilerInterceptorDllTextBox = new System.Windows.Forms.TextBox();
            this._enableORMProfilerCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this._browseASpecAssemblyButton);
            this.groupBox1.Controls.Add(this._browseAGenAssemblyButton);
            this.groupBox1.Controls.Add(this._browseSSAssemblyButton);
            this.groupBox1.Controls.Add(this._aSpecAssemblyLabel);
            this.groupBox1.Controls.Add(this._aGenAssemblyLabel);
            this.groupBox1.Controls.Add(this._ssAssemblyLabel);
            this.groupBox1.Controls.Add(this._aSpecAssemblyTextBox);
            this.groupBox1.Controls.Add(this._aGenAssemblyTextBox);
            this.groupBox1.Controls.Add(this._ssAssemblyTextBox);
            this.groupBox1.Controls.Add(this._adapterRadioButton);
            this.groupBox1.Controls.Add(this._selfServicingRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(590, 185);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Template group";
            // 
            // _browseASpecAssemblyButton
            // 
            this._browseASpecAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseASpecAssemblyButton.Enabled = false;
            this._browseASpecAssemblyButton.Location = new System.Drawing.Point(549, 143);
            this._browseASpecAssemblyButton.Name = "_browseASpecAssemblyButton";
            this._browseASpecAssemblyButton.Size = new System.Drawing.Size(24, 23);
            this._browseASpecAssemblyButton.TabIndex = 6;
            this._browseASpecAssemblyButton.Text = "...";
            this._browseASpecAssemblyButton.UseVisualStyleBackColor = true;
            this._browseASpecAssemblyButton.Click += new System.EventHandler(this._browseASpecAssemblyButton_Click);
            // 
            // _browseAGenAssemblyButton
            // 
            this._browseAGenAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseAGenAssemblyButton.Enabled = false;
            this._browseAGenAssemblyButton.Location = new System.Drawing.Point(549, 117);
            this._browseAGenAssemblyButton.Name = "_browseAGenAssemblyButton";
            this._browseAGenAssemblyButton.Size = new System.Drawing.Size(24, 23);
            this._browseAGenAssemblyButton.TabIndex = 4;
            this._browseAGenAssemblyButton.Text = "...";
            this._browseAGenAssemblyButton.UseVisualStyleBackColor = true;
            this._browseAGenAssemblyButton.Click += new System.EventHandler(this._browseAGenAssemblyButton_Click);
            // 
            // _browseSSAssemblyButton
            // 
            this._browseSSAssemblyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseSSAssemblyButton.Enabled = false;
            this._browseSSAssemblyButton.Location = new System.Drawing.Point(549, 43);
            this._browseSSAssemblyButton.Name = "_browseSSAssemblyButton";
            this._browseSSAssemblyButton.Size = new System.Drawing.Size(24, 23);
            this._browseSSAssemblyButton.TabIndex = 2;
            this._browseSSAssemblyButton.Text = "...";
            this._browseSSAssemblyButton.UseVisualStyleBackColor = true;
            this._browseSSAssemblyButton.Click += new System.EventHandler(this._browseSSAssemblyButton_Click);
            // 
            // _aSpecAssemblyLabel
            // 
            this._aSpecAssemblyLabel.AutoSize = true;
            this._aSpecAssemblyLabel.Enabled = false;
            this._aSpecAssemblyLabel.Location = new System.Drawing.Point(41, 148);
            this._aSpecAssemblyLabel.Name = "_aSpecAssemblyLabel";
            this._aSpecAssemblyLabel.Size = new System.Drawing.Size(110, 13);
            this._aSpecAssemblyLabel.TabIndex = 3;
            this._aSpecAssemblyLabel.Text = "DB Specific Assembly";
            // 
            // _aGenAssemblyLabel
            // 
            this._aGenAssemblyLabel.AutoSize = true;
            this._aGenAssemblyLabel.Enabled = false;
            this._aGenAssemblyLabel.Location = new System.Drawing.Point(41, 122);
            this._aGenAssemblyLabel.Name = "_aGenAssemblyLabel";
            this._aGenAssemblyLabel.Size = new System.Drawing.Size(109, 13);
            this._aGenAssemblyLabel.TabIndex = 3;
            this._aGenAssemblyLabel.Text = "DB Generic Assembly";
            // 
            // _ssAssemblyLabel
            // 
            this._ssAssemblyLabel.AutoSize = true;
            this._ssAssemblyLabel.Enabled = false;
            this._ssAssemblyLabel.Location = new System.Drawing.Point(41, 48);
            this._ssAssemblyLabel.Name = "_ssAssemblyLabel";
            this._ssAssemblyLabel.Size = new System.Drawing.Size(132, 13);
            this._ssAssemblyLabel.TabIndex = 3;
            this._ssAssemblyLabel.Text = "Generated Code Assembly";
            // 
            // _aSpecAssemblyTextBox
            // 
            this._aSpecAssemblyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._aSpecAssemblyTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._aSpecAssemblyTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this._aSpecAssemblyTextBox.Enabled = false;
            this._aSpecAssemblyTextBox.Location = new System.Drawing.Point(183, 145);
            this._aSpecAssemblyTextBox.Name = "_aSpecAssemblyTextBox";
            this._aSpecAssemblyTextBox.Size = new System.Drawing.Size(360, 20);
            this._aSpecAssemblyTextBox.TabIndex = 5;
            this._aSpecAssemblyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._aSpecAssemblyTextBox_Validating);
            // 
            // _aGenAssemblyTextBox
            // 
            this._aGenAssemblyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._aGenAssemblyTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._aGenAssemblyTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this._aGenAssemblyTextBox.Enabled = false;
            this._aGenAssemblyTextBox.Location = new System.Drawing.Point(183, 119);
            this._aGenAssemblyTextBox.Name = "_aGenAssemblyTextBox";
            this._aGenAssemblyTextBox.Size = new System.Drawing.Size(360, 20);
            this._aGenAssemblyTextBox.TabIndex = 3;
            this._aGenAssemblyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._aGenAssemblyTextBox_Validating);
            // 
            // _ssAssemblyTextBox
            // 
            this._ssAssemblyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ssAssemblyTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._ssAssemblyTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this._ssAssemblyTextBox.Enabled = false;
            this._ssAssemblyTextBox.Location = new System.Drawing.Point(183, 45);
            this._ssAssemblyTextBox.Name = "_ssAssemblyTextBox";
            this._ssAssemblyTextBox.Size = new System.Drawing.Size(360, 20);
            this._ssAssemblyTextBox.TabIndex = 1;
            this._ssAssemblyTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._ssAssemblyTextBox_Validating);
            // 
            // _adapterRadioButton
            // 
            this._adapterRadioButton.AutoSize = true;
            this._adapterRadioButton.Location = new System.Drawing.Point(12, 89);
            this._adapterRadioButton.Name = "_adapterRadioButton";
            this._adapterRadioButton.Size = new System.Drawing.Size(62, 17);
            this._adapterRadioButton.TabIndex = 1;
            this._adapterRadioButton.TabStop = true;
            this._adapterRadioButton.Text = "Adapter";
            this._adapterRadioButton.UseVisualStyleBackColor = true;
            this._adapterRadioButton.CheckedChanged += new System.EventHandler(this._adapterRadioButton_CheckedChanged);
            // 
            // _selfServicingRadioButton
            // 
            this._selfServicingRadioButton.AutoSize = true;
            this._selfServicingRadioButton.Location = new System.Drawing.Point(12, 19);
            this._selfServicingRadioButton.Name = "_selfServicingRadioButton";
            this._selfServicingRadioButton.Size = new System.Drawing.Size(90, 17);
            this._selfServicingRadioButton.TabIndex = 0;
            this._selfServicingRadioButton.TabStop = true;
            this._selfServicingRadioButton.Text = "Self-Servicing";
            this._selfServicingRadioButton.UseVisualStyleBackColor = true;
            this._selfServicingRadioButton.CheckedChanged += new System.EventHandler(this._selfServicingRadioButton_CheckedChanged);
            // 
            // _okButton
            // 
            this._okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._okButton.Location = new System.Drawing.Point(236, 390);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(61, 24);
            this._okButton.TabIndex = 3;
            this._okButton.Text = "OK";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this._cancelButton.CausesValidation = false;
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(317, 390);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(61, 24);
            this._cancelButton.TabIndex = 4;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._browseAppConfigFileButton);
            this.groupBox2.Controls.Add(this._connectionStringTextBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._appConfigFileTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(12, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(590, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connection string and configuration information";
            // 
            // _browseAppConfigFileButton
            // 
            this._browseAppConfigFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseAppConfigFileButton.Location = new System.Drawing.Point(549, 47);
            this._browseAppConfigFileButton.Name = "_browseAppConfigFileButton";
            this._browseAppConfigFileButton.Size = new System.Drawing.Size(24, 23);
            this._browseAppConfigFileButton.TabIndex = 2;
            this._browseAppConfigFileButton.Text = "...";
            this._browseAppConfigFileButton.UseVisualStyleBackColor = true;
            this._browseAppConfigFileButton.Click += new System.EventHandler(this._browseAppConfigFileButton_Click);
            // 
            // _connectionStringTextBox
            // 
            this._connectionStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._connectionStringTextBox.Location = new System.Drawing.Point(152, 24);
            this._connectionStringTextBox.Name = "_connectionStringTextBox";
            this._connectionStringTextBox.Size = new System.Drawing.Size(421, 20);
            this._connectionStringTextBox.TabIndex = 0;
            this._connectionStringTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._connectionStringTextBox_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Connection string to use";
            // 
            // _appConfigFileTextBox
            // 
            this._appConfigFileTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._appConfigFileTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._appConfigFileTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this._appConfigFileTextBox.Location = new System.Drawing.Point(152, 50);
            this._appConfigFileTextBox.Name = "_appConfigFileTextBox";
            this._appConfigFileTextBox.Size = new System.Drawing.Size(391, 20);
            this._appConfigFileTextBox.TabIndex = 1;
            this._appConfigFileTextBox.Validating += new System.ComponentModel.CancelEventHandler(this._appConfigFileTextBox_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = ".config file to use (optional)";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this._enableORMProfilerCheckBox);
            this.groupBox3.Controls.Add(this._browseOrmProfilerInterceptorDllButton);
            this.groupBox3.Controls.Add(this._ormProfilerInterceptorLabel);
            this.groupBox3.Controls.Add(this._ormprofilerInterceptorDllTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 294);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(590, 81);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Startup information";
            // 
            // _mainErrorProvider
            // 
            this._mainErrorProvider.ContainerControl = this;
            // 
            // _browseOrmProfilerInterceptorDllButton
            // 
            this._browseOrmProfilerInterceptorDllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._browseOrmProfilerInterceptorDllButton.Enabled = false;
            this._browseOrmProfilerInterceptorDllButton.Location = new System.Drawing.Point(549, 44);
            this._browseOrmProfilerInterceptorDllButton.Name = "_browseOrmProfilerInterceptorDllButton";
            this._browseOrmProfilerInterceptorDllButton.Size = new System.Drawing.Size(24, 23);
            this._browseOrmProfilerInterceptorDllButton.TabIndex = 2;
            this._browseOrmProfilerInterceptorDllButton.Text = "...";
            this._browseOrmProfilerInterceptorDllButton.UseVisualStyleBackColor = true;
            this._browseOrmProfilerInterceptorDllButton.Click += new System.EventHandler(this._browseOrmProfilerInterceptorDllButton_Click);
            // 
            // _ormProfilerInterceptorLabel
            // 
            this._ormProfilerInterceptorLabel.AutoSize = true;
            this._ormProfilerInterceptorLabel.Enabled = false;
            this._ormProfilerInterceptorLabel.Location = new System.Drawing.Point(9, 49);
            this._ormProfilerInterceptorLabel.Name = "_ormProfilerInterceptorLabel";
            this._ormProfilerInterceptorLabel.Size = new System.Drawing.Size(133, 13);
            this._ormProfilerInterceptorLabel.TabIndex = 6;
            this._ormProfilerInterceptorLabel.Text = "ORM Profiler interceptor dll";
            // 
            // _ormprofilerInterceptorDllTextBox
            // 
            this._ormprofilerInterceptorDllTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._ormprofilerInterceptorDllTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this._ormprofilerInterceptorDllTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this._ormprofilerInterceptorDllTextBox.Enabled = false;
            this._ormprofilerInterceptorDllTextBox.Location = new System.Drawing.Point(152, 46);
            this._ormprofilerInterceptorDllTextBox.Name = "_ormprofilerInterceptorDllTextBox";
            this._ormprofilerInterceptorDllTextBox.Size = new System.Drawing.Size(391, 20);
            this._ormprofilerInterceptorDllTextBox.TabIndex = 1;
            // 
            // _enableORMProfilerCheckBox
            // 
            this._enableORMProfilerCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._enableORMProfilerCheckBox.Location = new System.Drawing.Point(8, 20);
            this._enableORMProfilerCheckBox.Name = "_enableORMProfilerCheckBox";
            this._enableORMProfilerCheckBox.Size = new System.Drawing.Size(158, 24);
            this._enableORMProfilerCheckBox.TabIndex = 0;
            this._enableORMProfilerCheckBox.Text = "Enable ORM Profiler";
            this._enableORMProfilerCheckBox.UseVisualStyleBackColor = true;
            this._enableORMProfilerCheckBox.CheckedChanged += new System.EventHandler(this._enableORMProfilerCheckBox_CheckedChanged);
            // 
            // ConnectionDialog
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(614, 426);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ConnectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LLBLGen Pro LINQPad Connection Information";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mainErrorProvider)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button _browseASpecAssemblyButton;
		private System.Windows.Forms.Button _browseAGenAssemblyButton;
		private System.Windows.Forms.Button _browseSSAssemblyButton;
		private System.Windows.Forms.Label _aSpecAssemblyLabel;
		private System.Windows.Forms.Label _aGenAssemblyLabel;
		private System.Windows.Forms.Label _ssAssemblyLabel;
		private System.Windows.Forms.TextBox _aSpecAssemblyTextBox;
		private System.Windows.Forms.TextBox _aGenAssemblyTextBox;
		private System.Windows.Forms.TextBox _ssAssemblyTextBox;
		private System.Windows.Forms.RadioButton _adapterRadioButton;
		private System.Windows.Forms.RadioButton _selfServicingRadioButton;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button _browseAppConfigFileButton;
		private System.Windows.Forms.TextBox _connectionStringTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _appConfigFileTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ErrorProvider _mainErrorProvider;
        private System.Windows.Forms.Button _browseOrmProfilerInterceptorDllButton;
        private System.Windows.Forms.Label _ormProfilerInterceptorLabel;
        private System.Windows.Forms.TextBox _ormprofilerInterceptorDllTextBox;
        private System.Windows.Forms.CheckBox _enableORMProfilerCheckBox;
    }
}