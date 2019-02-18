using System;
using System.Collections.Generic;
using System.Text;

namespace GMAP1
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
