using Ninject.Infrastructure.Disposal;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Afonsoft.Ninject.NinjectExtension
{
    public class NinjectTaskScopeObject : INotifyWhenDisposed, IDisposableObject, IDisposable
    {
        private bool isDisposed;

        [method: CompilerGenerated]
        [CompilerGenerated]
        public event EventHandler Disposed;

        public bool IsDisposed
        {
            get
            {
                return isDisposed;
            }
        }

        public void Dispose()
        {
            isDisposed = true;
            if (Disposed != null)
            {
                Disposed(this, EventArgs.Empty);
            }
        }
    }
}
