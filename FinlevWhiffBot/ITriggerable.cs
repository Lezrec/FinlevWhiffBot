using System;
using System.Collections.Generic;
using System.Text;

namespace FinlevWhiffBot
{
    interface ITriggerable
    {
        bool CheckForTrigger(object information);

        void OnTrigger();
    }
}
