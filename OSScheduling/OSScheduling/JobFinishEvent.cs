using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSScheduling
{
    public interface JobFinishEvent
    {
        void onFinish(Job j);
    }
}
