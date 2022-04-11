using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Winform
{
    public class ConfigureAwait
    {
        public async Task btnStart_Click(Button btnCancel)
        {
            btnCancel.Text = "before";

            Console.WriteLine($"Thread before the await: {Thread.CurrentThread.ManagedThreadId}");

            await Task.Delay(1000).ConfigureAwait(continueOnCapturedContext: false);

            Console.WriteLine($"Thread after the await: {Thread.CurrentThread.ManagedThreadId}");

            btnCancel.Text = "after";
        }
    }
}
