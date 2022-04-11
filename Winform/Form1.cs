using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            //loadingGIF.Visible = true;

            //SyncVersion();

            //await AsyncVersion();

            //loadingGIF.Visible = false;

            //await new ConfigureAwait().btnStart_Click(btnCancel);



        }

        //UI blocking
        public void SyncVersion()
        {
            Thread.Sleep(5000);
        }

        //UI non blocking
        public async Task AsyncVersion()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}