using System.Drawing;
using BSGO_Server._3dAlgorithm;

namespace BSGO_Server
{
    internal class BackgroundDesc : IProtocolWrite
    {
        public string prefabName { get; set; }
        public Quaternion rotation { get; set; }
        public Color color { get; set; }
        public Vector3 position { get; set; }

        public BackgroundDesc(string prefabName, Quaternion rotation, Color color, Vector3 position)
        {
            this.prefabName = prefabName;
            this.rotation = rotation;
            this.color = color;
            this.position = position;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(prefabName);
            w.Write(rotation);
            w.Write(color);
            w.Write(position);
        }
    }
}
