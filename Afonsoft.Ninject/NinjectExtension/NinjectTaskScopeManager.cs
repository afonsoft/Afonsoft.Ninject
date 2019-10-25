using Afonsoft.Ninject.TaskExecutor.Interface;
using Ninject;
using Ninject.Parameters;
#if NET
using System.Runtime.Remoting.Messaging;
#endif

namespace Afonsoft.Ninject.NinjectExtension
{
    public class NinjectTaskScopeManager : ITask
    {
        private readonly IKernel kernel;
        private readonly string taskName;
        private static readonly object lockObject = new object();

        public NinjectTaskScopeManager(string taskName, IKernel kernel)
        {
            this.taskName = taskName;
            this.kernel = kernel;
        }

        public void Execute()
        {
            NinjectTaskScopeObject ninjectTaskScopeObject = new NinjectTaskScopeObject();
#if NET
            CallContext.SetData(NinjectExtensionInTaskScope.InTaskScopeName, ninjectTaskScopeObject);
#endif
            try
            {
                lock (lockObject)
                {
                    ResolutionExtensions.Get<ITask>(this.kernel, this.taskName, new IParameter[0]).Execute();
                }
            }
            finally
            {
                ninjectTaskScopeObject.Dispose();
#if NET
                CallContext.FreeNamedDataSlot(NinjectExtensionInTaskScope.InTaskScopeName);
#endif
            }
        }
    }
}
