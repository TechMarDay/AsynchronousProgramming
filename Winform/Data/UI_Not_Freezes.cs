using System;
using System.Threading;
using System.Threading.Tasks;

namespace Winform
{
    public class UI_Not_Freezes
    {
        public void SyncFunction()
        {
            Thread.Sleep(5000);
        }

        public async Task AsyncFunction()
        {
            //await does not mean that the thread will have to be blocked. Waitting for the operation means the thread
            //is free to go to do another thing and then will be come back when the operation is done.
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
