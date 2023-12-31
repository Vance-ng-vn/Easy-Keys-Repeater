using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easy_key_repeater
{
    class TaskDelay
    {
        public static async Task PutTaskDelay(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
