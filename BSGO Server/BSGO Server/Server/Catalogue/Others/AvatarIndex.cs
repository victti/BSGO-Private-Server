using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class AvatarIndex : IProtocolWrite
    {
        public string race { get; set; }

        public string sex { get; set; }

        public Dictionary<string, List<string>> items { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> textures { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, Dictionary<string, List<string>>> materials { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();

        public AvatarIndex(string race, string sex, Dictionary<string, List<string>> items, Dictionary<string, List<string>> textures, Dictionary<string, Dictionary<string, List<string>>> materials)
        {
            this.race = race;
            this.sex = sex;
            this.items = items;
            this.textures = textures;
            this.materials = materials;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(sex);
            w.Write(race);
            w.Write((ushort)items.Count);

            foreach (KeyValuePair<string, List<string>> pair in items)
            {
                w.Write(pair.Key);
                int pairCount = pair.Value.Count;
                w.Write((ushort)pairCount);
                for(int i = 0; i < pairCount; i++)
                {
                    w.Write(pair.Value[i]);
                }
            }

            w.Write((ushort)materials.Count);

            foreach (KeyValuePair<string, Dictionary<string, List<string>>> pair in materials)
            {
                w.Write(pair.Key);
                w.Write((ushort)pair.Value.Count);

                foreach (KeyValuePair<string, List<string>> pair2 in pair.Value)
                {
                    w.Write(pair2.Key);
                    int pair2Count = pair2.Value.Count;
                    w.Write((ushort)pair2Count);
                    for (int i = 0; i < pair2Count; i++)
                    {
                        w.Write(pair2.Value[i]);
                    }
                }
            }

            w.Write((ushort)textures.Count);

            foreach (KeyValuePair<string, List<string>> pair3 in textures)
            {
                w.Write(pair3.Key);
                int pairCount = pair3.Value.Count;
                w.Write((ushort)pairCount);
                for (int i = 0; i < pairCount; i++)
                {
                    w.Write(pair3.Value[i]);
                }
            }
        }
    }
}
