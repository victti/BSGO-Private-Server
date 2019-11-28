using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server._3dAlgorithm
{
    internal class ManeuverController : IMovementController
    {
        private enum FrameIndex
        {
            Old3,
            Old2,
            Old1,
            Current,
            Next,
            Count
        }

        private int clientIndex;

        private MovementFrame[] frames;

        private Tick oldTick;

        private Dictionary<Tick, MovementFrame> syncFrames = new Dictionary<Tick, MovementFrame>();

        private readonly List<Maneuver> maneuvers = new List<Maneuver>();

        private readonly Stack<Maneuver> maneuverStack = new Stack<Maneuver>();

        protected float currentSpeed;

        private Vector3 currentStrafingSpeed;

        private MovementCard card;

        public Gear Gear
        {
            get
            {
                Maneuver lastManeuverAtTick = GetLastManeuverAtTick(Server.GetSectorById(Server.GetClientByIndex(clientIndex).Character.PlayerShip.sectorId).Tick.Current);
                return (lastManeuverAtTick != null) ? lastManeuverAtTick.GetGear() : Gear.Regular;
            }
        }

        public float MarchSpeed
        {
            get
            {
                Maneuver lastManeuverAtTick = GetLastManeuverAtTick(Server.GetSectorById(Server.GetClientByIndex(clientIndex).Character.PlayerShip.sectorId).Tick.Current);
                return (lastManeuverAtTick != null) ? lastManeuverAtTick.GetMarchSpeed() : 0f;
            }
        }

        public float CurrentSpeed
        {
            get
            {
                return currentSpeed;
            }
        }

        public Vector3 CurrentStrafingSpeed
        {
            get
            {
                return currentStrafingSpeed;
            }
        }

        public ManeuverController(int clientIndex, MovementCard card)
        {
            frames = new MovementFrame[5];
            oldTick = Server.GetSectorById(Server.GetClientByIndex(clientIndex).Character.PlayerShip.sectorId).Tick.Current - 3;
            this.clientIndex = clientIndex;
            this.card = card;
        }

        public void Advance(Tick tick)
        {
            Build(tick);
        }

        protected void Build(Tick tick)
        {
            int num = TickIndex(tick);
            if (num >= 0 && num < 5)
            {
                BuildFrame(tick, out frames[num]);
            }
        }

        private void BuildFrame(Tick tick, out MovementFrame frame)
        {
            frame = MovementFrame.Invalid;
            Stack<Maneuver> stack = GetManeuvers(tick);
            if (stack == null)
            {
                frame.valid = false;
                return;
            }
            Maneuver maneuver = stack.Pop();
            MovementFrame frame2 = GetTickFrame(tick - 1);
            if (!frame2.valid && tick > oldTick)
            {
                BuildFrame(tick - 1, out frame2);
            }
            frame2.valid = true;
            frame = maneuver.NextFrame(tick, frame2);
            Vector3 position = frame.position;
            Euler3 euler = frame.euler3;
            foreach (Maneuver item in stack)
            {
                frame = item.NextFrame(tick, frame);
            }
            frame.position = position;
            frame.euler3 = euler;
        }

        private int TickIndex(Tick tick)
        {
            return tick - oldTick;
        }

        public void PostAdvance()
        {
            ++oldTick;
            for (int i = 0; i < 4; i++)
            {
                frames[i] = frames[i + 1];
            }
            frames[4].valid = false;
            DropOldSyncFrames();
            DropOldManeuvers();
        }

        private void DropOldSyncFrames()
        {
            List<Tick> list = new List<Tick>();
            foreach (Tick key in syncFrames.Keys)
            {
                if (key < oldTick)
                {
                    list.Add(key);
                }
            }
            foreach (Tick item in list)
            {
                syncFrames.Remove(item);
            }
        }

        private void DropOldManeuvers()
        {
            int num = maneuvers.FindLastIndex((Maneuver maneuver) => maneuver.GetStartTick() < oldTick);
            if (num > 0)
            {
                maneuvers.RemoveRange(0, num);
            }
        }

        public Maneuver GetLastManeuverAtTick(Tick tick)
        {
            Maneuver result = null;
            if (maneuvers.Count == 0)
            {
                return null;
            }
            for (int i = 0; i < maneuvers.Count; i++)
            {
                if (maneuvers[i].GetStartTick() <= tick)
                {
                    result = maneuvers[i];
                }
            }
            return result;
        }

        public Stack<Maneuver> GetManeuvers(Tick tick)
        {
            Maneuver lastManeuverAtTick = GetLastManeuverAtTick(tick);
            if (lastManeuverAtTick == null)
            {
                return null;
            }
            Tick startTick = lastManeuverAtTick.GetStartTick();
            maneuverStack.Clear();
            for (int i = 0; i < maneuvers.Count; i++)
            {
                Maneuver maneuver = maneuvers[i];
                if (maneuver.GetStartTick() == startTick)
                {
                    maneuverStack.Push(maneuver);
                }
            }
            return maneuverStack;
        }

        public bool GetFrame(double time, out Vector3 position, out Quaternion rotation, out float speed, out Vector3 strafingSpeed, out MovementFrame frame)
        {
            Tick prevTick = Tick.Previous(time);
            float dt = (float)(time - prevTick.Time);
            return GetFrame(prevTick, dt, out position, out rotation, out speed, out strafingSpeed, out frame);
        }

        public bool GetFrameFromOldFrame(double time, out Vector3 position, out Quaternion rotation, out float speed, out Vector3 strafingSpeed, out MovementFrame frame)
        {
            Tick prevTick = Tick.Previous(time - 0.10000000149011612);
            float dt = (float)(time - prevTick.Time);
            return GetFrame(prevTick, dt, out position, out rotation, out speed, out strafingSpeed, out frame);
        }

        private bool GetFrame(Tick prevTick, float dt, out Vector3 position, out Quaternion rotation, out float speed, out Vector3 strafingSpeed, out MovementFrame frame)
        {
            frame = GetTickFrame(prevTick);
            position = frame.GetFuturePosition(dt);
            rotation = frame.GetFutureRotation(dt);
            speed = frame.linearSpeed.magnitude;
            strafingSpeed = frame.strafeSpeed;
            return frame.valid;
        }

        public MovementFrame GetTickFrame(Tick tick)
        {
            int num = TickIndex(tick);
            if (num >= 0 && num < 5)
            {
                return frames[num];
            }
            return MovementFrame.Invalid;
        }

        public MovementFrame GetNextTickFrame()
        {
            return GetTickFrame(Server.GetSectorById(Server.GetClientByIndex(clientIndex).Character.PlayerShip.sectorId).Tick.Next);
        }

        private bool NewManeuverEliminatesExistingManeuver(Maneuver existingManeuver, Maneuver newManeuver)
        {
            switch (existingManeuver.GetStartTick() >= newManeuver.GetStartTick())
            {
                case false:
                    return false;
                case true:
                    switch (existingManeuver.IsExclusive + newManeuver.IsExclusive)
                    {
                        case 2:
                            return true;
                        case 1:
                            return false;
                        case 0:
                            return existingManeuver.GetType() == newManeuver.GetType();
                    }
                    break;
            }
            //Debug.LogError("We should never get here.");
            return true;
        }

        public void AddManeuver(Maneuver newManeuver)
        {
            newManeuver.Card = card;
            maneuvers.RemoveAll((Maneuver man) => NewManeuverEliminatesExistingManeuver(man, newManeuver));
            maneuvers.Add(newManeuver);
            maneuvers.Sort();
        }

        public void AddSyncFrame(Tick tick, MovementFrame frame)
        {
            int num = TickIndex(tick);
            if (num >= 0 && num < 5)
            {
                frames[num] = frame;
            }
            if (num >= 4)
            {
                syncFrames[tick] = frame;
            }
        }

        public override string ToString()
        {
            string text = string.Empty;
            foreach (Maneuver maneuver in maneuvers)
            {
                text += maneuver.ToString();
            }
            return string.Format("MovementController {0}", text);
        }

        public virtual bool Move(double time)
        {
            Vector3 position;
            Quaternion rotation;
            MovementFrame frame;
            if (!GetFrame(time, out position, out rotation, out currentSpeed, out currentStrafingSpeed, out frame))
            {
                if (!GetFrameFromOldFrame(time, out position, out rotation, out currentSpeed, out currentStrafingSpeed, out frame))
                {
                    return false;
                }
                //spaceObject.Position = position;
                //spaceObject.Rotation = rotation;
                return false;
            }
            if (frame.valid)
            {
                //spaceObject.UpdateThrusters(frame.ActiveThrusterEffects);
            }
            //spaceObject.Position = position;
            //spaceObject.Rotation = rotation;
            Server.GetClientByIndex(clientIndex).Character.PlayerShip.MovementFrame = frame;
            return true;
        }
    }
}
