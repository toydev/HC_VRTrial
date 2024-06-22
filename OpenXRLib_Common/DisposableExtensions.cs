namespace System
{
    public class DisposableExtensions : IDisposable
    {
        private dynamic target;
        private DisposableExtensions(dynamic target)
        {
            this.target = target;
        }

        public void Dispose()
        {
            if (target != null && HasDisposeMethod(target))
            {
                target.Dispose();
            }
            else
            {
                throw new InvalidOperationException("The target object does not have a Dispose method.");
            }
        }

        private bool HasDisposeMethod(dynamic obj)
        {
            return obj.GetType().GetMethod("Dispose") != null;
        }

        public static DisposableExtensions CreateDisposable(dynamic target)
        {
            return new DisposableExtensions(target);
        }
    }
}
