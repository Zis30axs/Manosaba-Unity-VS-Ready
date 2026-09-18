using System.Threading;
using UnityEngine;

namespace Naninovel
{
    public class CancelOnDestroy : MonoBehaviour
    {
        public CancellationToken Token => cts.Token;

        private readonly CancellationTokenSource cts = new();

        private void OnDestroy ()
        {
            cts.Cancel();
            cts.Dispose();
        }
    }
}
