using System.Collections.Generic;

namespace BSGO_Server
{
    internal class AvatarIndex : IProtocolWrite
    {
        public string Race { get; set; }

        public string Sex { get; set; }

        public Dictionary<string, List<string>> Items { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, List<string>> Textures { get; set; } = new Dictionary<string, List<string>>();

        public Dictionary<string, Dictionary<string, List<string>>> Materials { get; set; } = new Dictionary<string, Dictionary<string, List<string>>>();

        public AvatarIndex(string race, string sex, Dictionary<string, List<string>> items, Dictionary<string, List<string>> textures, Dictionary<string, Dictionary<string, List<string>>> materials)
        {
            Race = race;
            Sex = sex;
            Items = items;
            Textures = textures;
            Materials = materials;
        }

        public void Write(BgoProtocolWriter w)
        {
            w.Write(Sex);
            w.Write(Race);
            w.Write((ushort)Items.Count);
            foreach (KeyValuePair<string, List<string>> pair in Items)
            {
                w.Write(pair.Key);
                int pairCount = pair.Value.Count;
                w.Write((ushort)pairCount);
                for(int i = 0; i < pairCount; i++)
                    w.Write(pair.Value[i]);
                
            }

            w.Write((ushort)Materials.Count);

            foreach (KeyValuePair<string, Dictionary<string, List<string>>> pair in Materials)
            {
                w.Write(pair.Key);
                w.Write((ushort)pair.Value.Count);

                foreach (KeyValuePair<string, List<string>> pair2 in pair.Value)
                {
                    w.Write(pair2.Key);
                    int pair2Count = pair2.Value.Count;
                    w.Write((ushort)pair2Count);
                    for (int i = 0; i < pair2Count; i++)
                        w.Write(pair2.Value[i]);
                    
                }
            }

            w.Write((ushort)Textures.Count);

            foreach (KeyValuePair<string, List<string>> pair3 in Textures)
            {
                w.Write(pair3.Key);
                int pairCount = pair3.Value.Count;
                w.Write((ushort)pairCount);
                for (int i = 0; i < pairCount; i++)
                    w.Write(pair3.Value[i]);
                
            }
        }
    }
}
