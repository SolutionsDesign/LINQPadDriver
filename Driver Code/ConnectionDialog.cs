////////////////////////////////////////////////////////////////////////////////////////////////////////
// LLBLGen Pro LINQPad driver is (c) 2002-2012 Solutions Design. All rights reserved.
// http://www.llblgen.com
////////////////////////////////////////////////////////////////////////////////////////////////////////
// COPYRIGHTS:
// Copyright (c)2002-2012 Solutions Design. All rights reserved.
// http://www.llblgen.com
// 
// The LLBLGen Pro LINQPad driver sourcecode is released under the following license (BSD2):
// ----------------------------------------------------------------------
// Redistribution and use in source and binary forms, with or without modification, 
// are permitted provided that the following conditions are met: 
//
// 1) Redistributions of source code must retain the above copyright notice, this list of 
//    conditions and the following disclaimer. 
// 2) Redistributions in binary form must reproduce the above copyright notice, this list of 
//    conditions and the following disclaimer in the documentation and/or other materials 
//    provided with the distribution. 
// 
// THIS SOFTWARE IS PROVIDED BY SOLUTIONS DESIGN ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL SOLUTIONS DESIGN OR CONTRIBUTORS BE LIABLE FOR 
// ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
// NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, 
// STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
//
// The views and conclusions contained in the software and documentation are those of the authors 
// and should not be interpreted as representing official policies, either expressed or implied, 
// of Solutions Design. 
//
//////////////////////////////////////////////////////////////////////
// Contributers to the code:
//		- Frans Bouma [FB]
//////////////////////////////////////////////////////////////////////
// Special thanks to:
//		- Jeremy Thomas
//////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using LINQPad.Extensibility.DataContext;
using System.Xml.Linq;
using System.Xml;
using Microsoft.Win32;

namespace SD.LLBLGen.Pro.LINQPadDriver
{
	/// <summary>
	/// Dialog which is used to obtain the information for the assemblies and connection string to build a connection
	/// </summary>
	public partial class ConnectionDialog : Form
	{
		#region Class Member Declarations
		private IConnectionInfo _cxInfo;
		private bool _isNewConnection;
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="ConnectionDialog"/> class.
		/// </summary>
		/// <param name="cxInfo">The cx info.</param>
		/// <param name="isNewConnection">if set to <c>true</c> [is new connection].</param>
		public ConnectionDialog(IConnectionInfo cxInfo, bool isNewConnection)
		{
			_cxInfo = cxInfo;
			_isNewConnection = isNewConnection;
			InitializeComponent();

			CxInfoHelper.CreateDriverDataElements(_cxInfo);
		}


		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Form.Load"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
			if(!_isNewConnection)
			{
				FillControlsWithExistingData();
			}
			this.MinimumSize = this.Size;
            EnableDisableORMProfilerInterceptorControls();
		}


		/// <summary>
		/// Fills the controls with existing data.
		/// </summary>
		private void FillControlsWithExistingData()
		{
			if((_cxInfo == null) || _isNewConnection)
			{
				return;
			}
			var templateGroupValue = CxInfoHelper.GetTemplateGroup(_cxInfo);
			switch(templateGroupValue)
			{
				case TemplateGroup.None:
				case TemplateGroup.SelfServicing:
					_selfServicingRadioButton.Checked = true;
					break;
				case TemplateGroup.Adapter:
					_adapterRadioButton.Checked = true;
					break;
			}
			_ssAssemblyTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.SelfServicingAssemblyFilenameElement);
			_aGenAssemblyTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.AdapterDBGenericAssemblyFilenameElement);
			_aSpecAssemblyTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.AdapterDBSpecificAssemblyFilenameElement);
			_connectionStringTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.ConnectionStringElementName);
			_appConfigFileTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.ConfigFileFilenameElement);
            _enableORMProfilerCheckBox.Checked = XmlConvert.ToBoolean(CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.EnableORMProfilerElement));
            _ormprofilerInterceptorDllTextBox.Text = CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.ORMProfilerInterceptorLocationElement);
			_enableORMProfilerCheckBox.Checked = XmlConvert.ToBoolean(CxInfoHelper.GetDriverDataElementValue(_cxInfo, DriverDataElements.EnableORMProfilerElement));
		}

		
		/// <summary>
		/// Validates the specified filename text box, whether the textbox contains a valid filename or not.
		/// </summary>
		/// <param name="toValidate">The textbox.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		/// <param name="canBeEmpty">if set to <c>true</c> [can be empty].</param>
		/// <param name="parentIsEnabled">if set to <c>true</c> the parent control is enabled. If false, no active checks are performed</param>
		/// <param name="expectedExtension">The expected extension.</param>
		private void ValidateFilenameTextBox(TextBox toValidate, CancelEventArgs e, bool canBeEmpty, bool parentIsEnabled, string expectedExtension)
		{
			var realValue = toValidate.Text.Trim();
			bool cancel = false;
			string errorMessage = string.Empty;
			if(parentIsEnabled)
			{
				if(string.IsNullOrEmpty(realValue))
				{
					if(!canBeEmpty)
					{
						cancel = true;
						errorMessage = "Value is empty";
					}
				}
				else
				{
					if(File.Exists(realValue))
					{
						if(Path.GetExtension(realValue).ToLowerInvariant() != expectedExtension)
						{
							cancel = true;
							errorMessage = "The file has an invalid extension. Expected extension: " + expectedExtension;
						}
					}
					else
					{
						cancel = true;
						errorMessage = "File doesn't exist";
					}
				}
			}
			e.Cancel = cancel;
			_mainErrorProvider.SetError(toValidate, errorMessage);
		}


		/// <summary>
		/// Validates the entity assembly.
		/// </summary>
		/// <param name="toValidate">To validate.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		/// <param name="parentIsEnabled">if set to <c>true</c> [parent is enabled].</param>
		private void ValidateEntityAssembly(TextBox toValidate, CancelEventArgs e, bool parentIsEnabled)
		{
			var realValue = toValidate.Text.Trim();
			bool cancel = false;
			string errorMessage = string.Empty;
			if(parentIsEnabled)
			{
				var ilinqMetaDataType = CxInfoHelper.GetILinqMetaDataTypeFromEntityAssembly(_cxInfo, realValue);
				if(string.IsNullOrEmpty(ilinqMetaDataType))
				{
					cancel = true;
					errorMessage = string.Format("The assembly '{0}' doesn't contain an ILinqMetaData implementing class.", realValue);
				}
			}
			e.Cancel = cancel;
			_mainErrorProvider.SetError(toValidate, errorMessage);
		}


		/// <summary>
		/// Browses for a file.
		/// </summary>
		/// <param name="caption">The caption.</param>
		/// <param name="filter">The filter.</param>
		/// <param name="defaultExtension">The default extension.</param>
		/// <param name="targetTextBox">The target text box.</param>
		private void BrowseForFile(string caption, string filter, string defaultExtension, TextBox targetTextBox)
		{
			using(var dialog = new OpenFileDialog()
			{
				FileName = targetTextBox.Text,
				CheckFileExists = true,
				DefaultExt = defaultExtension,
				Title = caption,
				Filter = filter
			})
			{
				var result = dialog.ShowDialog();
				if(result == System.Windows.Forms.DialogResult.OK)
				{
					targetTextBox.Text = dialog.FileName;
				}
			}
		}


        private void EnableDisableORMProfilerInterceptorControls()
        {
            _ormprofilerInterceptorDllTextBox.Enabled = _enableORMProfilerCheckBox.Checked;
            _ormProfilerInterceptorLabel.Enabled = _enableORMProfilerCheckBox.Checked;
            _browseOrmProfilerInterceptorDllButton.Enabled = _enableORMProfilerCheckBox.Checked;
        }


		/// <summary>
		/// Fills the cx info with the data specified on the form
		/// </summary>
		private void FillCxInfo()
		{
			if(_cxInfo == null)
			{
				return;
			}
			_cxInfo.AppConfigPath = _appConfigFileTextBox.Text.Trim();
			var templateGroupSelected = _selfServicingRadioButton.Checked?TemplateGroup.SelfServicing : TemplateGroup.Adapter;
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.TemplateGroupElement, XmlConvert.ToString((int)templateGroupSelected));
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.AdapterDBGenericAssemblyFilenameElement, _aGenAssemblyTextBox.Text);
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.AdapterDBSpecificAssemblyFilenameElement, _aSpecAssemblyTextBox.Text);
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.SelfServicingAssemblyFilenameElement, _ssAssemblyTextBox.Text);
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.ConfigFileFilenameElement, _appConfigFileTextBox.Text);
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.ConnectionStringElementName, _connectionStringTextBox.Text);
			_cxInfo.DatabaseInfo.CustomCxString = _connectionStringTextBox.Text;
			CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.EnableORMProfilerElement, XmlConvert.ToString((_enableORMProfilerCheckBox.Enabled && _enableORMProfilerCheckBox.Checked)));
            CxInfoHelper.SetDriverDataElement(_cxInfo, DriverDataElements.ORMProfilerInterceptorLocationElement,  _ormprofilerInterceptorDllTextBox.Text);
			CxInfoHelper.SetCustomTypeInfo(_cxInfo, templateGroupSelected);
		}

		
		/// <summary>
		/// Handles the CheckedChanged event of the _selfServicingRadioButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _selfServicingRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			_ssAssemblyLabel.Enabled = _selfServicingRadioButton.Checked;
			_ssAssemblyTextBox.Enabled = _selfServicingRadioButton.Checked;
			_browseSSAssemblyButton.Enabled = _selfServicingRadioButton.Checked;
		}

		/// <summary>
		/// Handles the CheckedChanged event of the _adapterRadioButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _adapterRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			_aGenAssemblyLabel.Enabled = _adapterRadioButton.Checked;
			_aGenAssemblyTextBox.Enabled = _adapterRadioButton.Checked;
			_browseAGenAssemblyButton.Enabled = _adapterRadioButton.Checked;
			_aSpecAssemblyLabel.Enabled = _adapterRadioButton.Checked;
			_aSpecAssemblyTextBox.Enabled = _adapterRadioButton.Checked;
			_browseASpecAssemblyButton.Enabled = _adapterRadioButton.Checked;
		}


		/// <summary>
		/// Handles the Click event of the _okButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _okButton_Click(object sender, EventArgs e)
		{
			if(ValidateChildren(ValidationConstraints.Enabled))
			{
				FillCxInfo();
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		/// <summary>
		/// Handles the Click event of the _cancelButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _cancelButton_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Handles the Validating event of the _ssAssemblyTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		private void _ssAssemblyTextBox_Validating(object sender, CancelEventArgs e)
		{
			ValidateFilenameTextBox(_ssAssemblyTextBox, e, false, _selfServicingRadioButton.Checked, ".dll");
			if(!e.Cancel)
			{
				ValidateEntityAssembly(_ssAssemblyTextBox, e, _selfServicingRadioButton.Checked);
			}
		}

		/// <summary>
		/// Handles the Validating event of the _aGenAssemblyTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		private void _aGenAssemblyTextBox_Validating(object sender, CancelEventArgs e)
		{
			ValidateFilenameTextBox(_aGenAssemblyTextBox, e, false, _adapterRadioButton.Checked, ".dll");
			if(!e.Cancel)
			{
				ValidateEntityAssembly(_aGenAssemblyTextBox, e, _adapterRadioButton.Checked);
			}
		}

		/// <summary>
		/// Handles the Validating event of the _aSpecAssemblyTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		private void _aSpecAssemblyTextBox_Validating(object sender, CancelEventArgs e)
		{
			ValidateFilenameTextBox(_aSpecAssemblyTextBox, e, false, _adapterRadioButton.Checked, ".dll");
		}

		/// <summary>
		/// Handles the Validating event of the _appConfigFileTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		private void _appConfigFileTextBox_Validating(object sender, CancelEventArgs e)
		{
			// Pass true for 'can be empty' because it's an optional value.
			ValidateFilenameTextBox(_appConfigFileTextBox, e, true, true, ".config");
		}
		
		/// <summary>
		/// Handles the Validating event of the _connectionStringTextBox control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
		private void _connectionStringTextBox_Validating(object sender, CancelEventArgs e)
		{
			var realValue = _connectionStringTextBox.Text.Trim();
			if(string.IsNullOrEmpty(realValue))
			{
				var appConfigFile = _appConfigFileTextBox.Text.Trim();
				if(string.IsNullOrEmpty(appConfigFile))
				{
					e.Cancel = true;
					_mainErrorProvider.SetError(_connectionStringTextBox, "Connection string is empty and no config file has been specified.");
				}
			}
		}

		/// <summary>
		/// Handles the Click event of the _browseSSAssemblyButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _browseSSAssemblyButton_Click(object sender, EventArgs e)
		{
			BrowseForFile("Please select the generated code assembly", "Assemblies (*.dll)|*.dll", ".dll", _ssAssemblyTextBox);
		}

		/// <summary>
		/// Handles the Click event of the _browseAGenAssemblyButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _browseAGenAssemblyButton_Click(object sender, EventArgs e)
		{
			BrowseForFile("Please select the adapter db generic assembly", "Assemblies (*.dll)|*.dll", ".dll", _aGenAssemblyTextBox);
		}

		/// <summary>
		/// Handles the Click event of the _browseASpecAssemblyButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _browseASpecAssemblyButton_Click(object sender, EventArgs e)
		{
			BrowseForFile("Please select the adapter db specific assembly", "Assemblies (*.dll)|*.dll", ".dll", _aSpecAssemblyTextBox);
		}

		/// <summary>
		/// Handles the Click event of the _browseAppConfigFileButton control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void _browseAppConfigFileButton_Click(object sender, EventArgs e)
		{
			BrowseForFile("Please select the .config file to use.", ".config files (*.config)|*.config", ".config", _appConfigFileTextBox);
		}

        private void _browseOrmProfilerInterceptorDllButton_Click(object sender, EventArgs e)
        {
            BrowseForFile("Please select the ORM Profiler Intercept dll to use", "Assemblies (*.dll)|*.dll", ".dll", _ormprofilerInterceptorDllTextBox);
        }

        private void _enableORMProfilerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            EnableDisableORMProfilerInterceptorControls();
        }
    }
}
