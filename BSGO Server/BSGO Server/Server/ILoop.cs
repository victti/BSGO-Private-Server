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
            return Task.Factory.StartNew(() =>
            {
                var last = CurrentTimeMillis;

                var interval = TimeSpan.FromSeconds(1d / rate);

                while (true)
                {
                    var now = CurrentTimeMillis;


                    float deltaTime = (float)(now - last) * 0.001f;

                    //Console.WriteLine("delta time: " + deltaTime);


                    OnUpdated?.Invoke(deltaTime);

                    Thread.Sleep(interval);

                    last = now;

                }
            });
        }
    }
}
