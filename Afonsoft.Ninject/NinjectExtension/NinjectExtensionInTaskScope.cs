using Ninject.Activation;
using Ninject.Syntax;
using System;
#if NET
using System.Runtime.Remoting.Messaging;
#endif

namespace Afonsoft.Ninject.NinjectExtension
{
    public static class NinjectExtensionInTaskScope
    {
        public const string InTaskScopeName = "InTaskScope";

        public static IBindingNamedWithOrOnSyntax<T> InTaskScope<T>(this IBindingInSyntax<T> syntax)
        {
            return syntax.InScope(new Func<IContext, object>(NinjectExtensionInTaskScope.GetScope));
        }

        private static object GetScope(IContext ctx)
        {
#if NET
            return CallContext.GetData(InTaskScopeName);
#else
            return ctx.GetScope();
#endif
        }
    }
}
