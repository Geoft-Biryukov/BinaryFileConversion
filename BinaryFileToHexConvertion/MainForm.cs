using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryFileToHexConvertion
{
    public partial class MainForm : Form, IMainView
    {
        private readonly MainPresenter presenter;
        private readonly ReadBytesOptions options = new ReadBytesOptions();

        public MainForm()
        {
            InitializeComponent();
            this.presenter = new MainPresenter(this, options);

            Disposed += MainForm_Disposed;
            viewProperties.SelectedObject = options;
        }

        private void MainForm_Disposed(object sender, EventArgs e)
        {
            presenter.Dispose();
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            presenter.OpenFile();
        }

        public string AskUserOpenFileName()
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog(this) == DialogResult.OK)
                    return openFileDialog.FileName;
                else
                    return null;
            }
        }

        public void SetFileName(string name)
        {         
            options.FileName = name;
            viewProperties.Refresh();
        }

        public void SetFileLength(long length)
        {           
            options.FileSize = length.ToString("#,#", CultureInfo.InvariantCulture);
            viewProperties.Refresh();
        }
      

        private void readButton_Click(object sender, EventArgs e)
        {
            presenter.ReadBytes();
        }

        public void SetText(string text)
        {
            hexTextBox.Text = text;
        }

        public void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void SetMaxBytes(long length)
        {
            options.SetMaxBytes(length);
        }

        private void viewProperties_Click(object sender, EventArgs e)
        {

        }
    }
}
