using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement.Shared.Presentation
{
    internal sealed class AsyncDataLoader<T> : IDisposable
    {
        private readonly Control owner;
        private CancellationTokenSource current;
        private bool disposed;

        public AsyncDataLoader(Control owner)
        {
            this.owner = owner;
        }

        public async Task LoadAsync(Func<CancellationToken, Task<T>> load, Action<T> apply,
            int delayMilliseconds = 0)
        {
            if (disposed)
                return;

            var previous = current;
            var request = new CancellationTokenSource();
            current = request;
            previous?.Cancel();

            try
            {
                if (delayMilliseconds > 0)
                    await Task.Delay(delayMilliseconds, request.Token);

                var data = await load(request.Token);
                if (!request.IsCancellationRequested && !owner.IsDisposed && !owner.Disposing)
                    apply(data);
            }
            catch (OperationCanceledException) when (request.IsCancellationRequested)
            {
            }
            catch (Exception exception)
            {
                if (!request.IsCancellationRequested && !owner.IsDisposed && !owner.Disposing)
                    MessageBox.Show(owner, exception.Message, "Falha ao carregar dados",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (ReferenceEquals(current, request))
                    current = null;
                request.Dispose();
            }
        }

        public void Dispose()
        {
            disposed = true;
            current?.Cancel();
        }
    }
}
