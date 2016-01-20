using ColorPicker.Windows.Base;

using System;
using System.Windows.Forms;

namespace ColorPicker.Windows
{
    public partial class InputWindow : SkinWindow
    {
        public static string Result;

        public string _input;
        public string Input
        {
            get
            {
                return _input;
            }
        }

        public InputWindow(string msg = "", string input = "")
        {
            InitializeComponent();

            lblMsg.Text = msg;
            inputBox.Text = input;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _input = inputBox.Text;

            this.DialogResult = DialogResult.OK;
        }

        private void inputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOk.PerformClick();
            }
        }

        public static DialogResult Show(string msg = "", string input = "")
        {
            InputWindow iw = new InputWindow(msg, input);
            DialogResult r = iw.ShowDialog();

            if (r == DialogResult.OK)
            {
                Result = iw.Input;
            }

            return r;
        }

    }
}