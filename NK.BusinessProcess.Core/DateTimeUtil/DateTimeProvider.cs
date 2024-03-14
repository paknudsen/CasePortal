using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NK.Utils.DateTimeUtil
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTimeNow();
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
