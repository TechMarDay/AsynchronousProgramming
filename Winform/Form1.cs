using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Winform.Data;

namespace Winform
{
    public partial class Form1 : Form
    {
        private string apiURL;
        private CancellationTokenSource cancellationTokenSource;

        public Form1()
        {
            InitializeComponent();
            apiURL = "https://localhost:7222/api";
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            loadingGIF.Visible = true;

            //Non-Freezing UI
            //new UI_Not_Freezes().SyncFunction();
            //await new UI_Not_Freezes().AsyncFunction();


            //SynchronizationContext, configAwait
            //await new ConfigureAwait(apiURL).btnStart_Click(btnCancel);

            //Task.WhenAll
            //await new TaskWhenAll(apiURL).btnStart_Click();

            //Cancelling Tasks
            //cancellationTokenSource = new CancellationTokenSource();
            //cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
            //await new Cancelling_Tasks(apiURL, cancellationTokenSource).btnStart_Click();

            //Cancelling Non-Cancellable Tasks with TaskCompletionSource
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Cancelling_Non_Cancellable_Tasks(cancellationTokenSource).btnStart_Click();

            //Only - One Pattern
            //cancellationTokenSource = new CancellationTokenSource();
            //await new Only_One_Pattern(apiURL, cancellationTokenSource).btnStart_Click();

            //Retry Pattern
            //await new RetryPattern(apiURL).btnStart_Click();

            //sync - over - async
            //await new sync_over_async().btnStart_Click();

            //Avoid Task.Factory.StartNew
            await new StartNew().btnStart_Click();

            loadingGIF.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancellationTokenSource?.Cancel();
        }
    }
}