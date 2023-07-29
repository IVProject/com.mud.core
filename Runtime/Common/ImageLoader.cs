using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Mud
{
    public class ImageLoader: IAsyncResourceLoader<Texture2D>
    {
        private CancellationTokenSource _cancellationTokenSource;
        private int _maxRetryCount;
        private int _retryDelaySeconds;

        public ImageLoader()
        {
            _maxRetryCount = 3;
            _retryDelaySeconds = 5;
        }
        
        public ImageLoader(int maxRetryCount, int retryDelaySeconds)
        {
            _maxRetryCount = maxRetryCount;
            _retryDelaySeconds = retryDelaySeconds;
        }
        
        public async Task<Texture2D> LoadAsync(string address, CancellationToken cancellationToken = default)
        {
            _cancellationTokenSource ??= new CancellationTokenSource();

            try
            {
                return await LoadWithRetry(address, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                Debug.LogWarning($"Image loading from {address} was cancelled.");
                return null;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load image from {address}: {e.Message}");
                return null;
            }
        }

        private async Task<Texture2D> LoadWithRetry(string address, CancellationToken cancellationToken)
        {
            for (int retryCount = 0; retryCount <= _maxRetryCount; retryCount++)
            {
                try
                {
                    return await LoadImageInternal(address, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to load image from {address}: {e.Message}. Retrying...");
                    await Task.Delay(_retryDelaySeconds * 1000, cancellationToken);
                }
            }

            return null;
        }
        
        private async Task<Texture2D> LoadImageInternal(string address, CancellationToken cancellationToken)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(address))
            {
                var asyncOperation = www.SendWebRequest();

                while (!asyncOperation.isDone)
                {
                    if (cancellationToken.IsCancellationRequested || _cancellationTokenSource.IsCancellationRequested)
                    {
                        www.Abort();
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    await Task.Yield();
                }

                if (www.result != UnityWebRequest.Result.Success)
                    throw new Exception(www.error);

                return DownloadHandlerTexture.GetContent(www);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}