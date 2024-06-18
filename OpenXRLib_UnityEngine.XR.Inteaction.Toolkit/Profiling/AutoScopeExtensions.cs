using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenXRLib_UnityEngine.XR.Inteaction.Toolkit.Profiling
{
    public class AutoScopeExtensions : IDisposable
    {
        public AutoScopeExtensions(Unity.Profiling.ProfilerMarker.AutoScope autoScope) {
            AutoScope = autoScope;
        }
        private Unity.Profiling.ProfilerMarker.AutoScope AutoScope { get; set; }

        public void Dispose()
        {
            AutoScope.Dispose();
        }
    }
}
