using System;
using System.Windows.Forms;
using Task2_GreetingLibrary;

namespace Task1_WinForms
{
    public partial class Mainform : Form
    {
        private readonly IGreetingService _greetingService = new GreetingService();
        public Mainform()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var username = InputName.Text;
            LabelOutput.Text = _greetingService.GetMessage(username);
        }
    }
}
