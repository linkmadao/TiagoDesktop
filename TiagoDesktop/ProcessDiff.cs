using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace TiagoDesktop
{
    public class ProcessDiff : IEqualityComparer<Process>
    {

        public IEnumerable<Process> GetDiff(IEnumerable<Process> oldProcesses, IEnumerable<Process> newProcesses)
        {
            return newProcesses.Except(oldProcesses, this);
        }
        public bool Equals(Process x, Process y)
        {
            if (x == null && y == null) return true;
            if (x == null || y == null) return false;
            return x.Id == y.Id && x.ProcessName == y.ProcessName && x.SessionId == y.SessionId;
        }

        public int GetHashCode(Process obj)
        {
            if (obj == null) return 0;
            return obj.Id;
        }
    }
}
