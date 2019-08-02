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

        public MainForm()
        {
            InitializeComponent();
            this.presenter = new MainPresenter(this);

            Disposed += MainForm_Disposed;
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
            fileNameTextBox.Text = name;
        }

        public void SetFileLength(long length)
        {
            fileSizeTextBox.Text = length.ToString("#,#", CultureInfo.InvariantCulture);
        }

        public void SetMaxBytes(long length)
        {
            bytesToReadNumericUpDown.Maximum = length;
            offsetNumericUpDown.Maximum = length;
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            presenter.ReadBytes((long)offsetNumericUpDown.Value, (long)bytesToReadNumericUpDown.Value);
        }

        public void SetText(string text)
        {
            hexTextBox.Text = text;
        }

        public void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
