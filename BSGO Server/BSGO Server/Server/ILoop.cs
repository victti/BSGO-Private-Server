using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSGO_Server
{
    public interface ILoop
    {
        Task Initialize(double rate);
    }
    public class Loop : ILoop
    {
        public Action<float> OnUpdated;
        public Loop()
        {

        }
        public Loop(Action<float> OnUpdated)
        {
            this.OnUpdated = OnUpdated;
        }
        public long CurrentTimeMillis
        {
            get
            {
                return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            }
        }

        public Task Initialize(double rate = 64d)
        {
            return Task.Factory.StartNew(async () =>
            {
                var last = CurrentTimeMillis;

                var interval = TimeSpan.FromSeconds(1d / rate);

                while (true)
                {
                    var now = CurrentTimeMillis;

                    float deltaTime = (float)(now - last) * 0.001f;

                    OnUpdated?.Invoke(deltaTime);
                                       
                    // use this for async
                    await Task.Delay(interval);

                    // comment this to use async
                    //Thread.Sleep(interval);

                    last = now;
                }
            });
        }
    }
}
