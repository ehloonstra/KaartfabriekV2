using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaartfabriek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var surferApp = new Surfer.Application();
            surferApp.Visible = true;
            label1.Text = surferApp.Version;

            await Task.Delay(10_000);

            surferApp.Visible = false;

            surferApp.Quit();
        }
    }
}
