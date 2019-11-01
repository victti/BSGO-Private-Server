using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class TimeSync
    {
        private const int syncStepCount = 3;

        private const double syncInterval = 10.0;

        private double serverDelta;

        private double[] timeDeltas = new double[3];

        private double[] latencies = new double[3];

        private double lastClientSendTime;

        private int syncStep = -1;

        private bool nextSyncTimerActive;

        private double nextSyncTimer;

        private double shift;

        private DateTime clientTime;

        public double ClientTime
        {
            get
            {
                return DateTime.UtcNow.ToUniversalTime().Subtract(clientTime).TotalSeconds;
            }
        }

        public double ServerTime
        {
            get
            {
                return ClientTime + serverDelta - shift;
            }
        }

        public TimeSync()
        {
            clientTime = DateTime.Now;
        }

        private double GetAverageDelta(double[] deltas)
        {
            double num = 0.0;
            foreach (double num2 in deltas)
            {
                num += num2;
            }
            return num / 3.0;
        }

        public void ReceiveSync()
        {
            if (syncStep < 0)
            {
                syncStep = 0;
            }
            lastClientSendTime = ClientTime;
            syncStep++;
        }

        public void ClientReply(long currentServerMilliseconds)
        {
            if (syncStep <= 3)
            {
                double num = ClientTime - lastClientSendTime;
                double num2 = lastClientSendTime + num / 2.0;
                double num3 = 0.001 * (double)currentServerMilliseconds - num2;
                timeDeltas[syncStep - 1] = num3;
                latencies[syncStep - 1] = num;
                if (syncStep >= 3)
                {
                    serverDelta = GetAverageDelta(timeDeltas);
                    syncStep = -1;
                    nextSyncTimer = ClientTime + 10.0;
                    nextSyncTimerActive = true;
                    shift = GetAverageDelta(latencies) + 0.1;
                }
            }
        }
    }
}
