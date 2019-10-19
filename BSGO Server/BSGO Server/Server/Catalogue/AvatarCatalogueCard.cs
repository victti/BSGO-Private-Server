using System.Collections.Generic;

namespace BSGO_Server
{
    internal class AvatarCatalogueCard : Card
    {
        public List<AvatarIndex> AvatarIndexes { get; set; } = new List<AvatarIndex>();

        public AvatarCatalogueCard(uint cardGuid, CardView cardView)
            : base(cardGuid, cardView)
        {
            string race;
            string sex;
            Dictionary<string, List<string>> items = new Dictionary<string, List<string>>();
            string itemName;
            List<string> itemNames = new List<string>();
            Dictionary<string, List<string>> textures = new Dictionary<string, List<string>>();
            string textureName;
            List<string> textureNames = new List<string>();
            Dictionary<string, Dictionary<string, List<string>>> materials = new Dictionary<string, Dictionary<string, List<string>>>();
            string materialName;
            Dictionary<string, List<string>> submaterials = new Dictionary<string, List<string>>();
            List<string> submaterialNames = new List<string>();

            race = "human";
            sex = "male";

            itemName = "hair";
            itemNames.Add("male_hair_01");
            itemNames.Add("male_hair_02");
            itemNames.Add("male_hair_03");
            itemNames.Add("male_hair_04");
            itemNames.Add("male_hair_05");
            itemNames.Add("male_hair_06");
            itemNames.Add("male_hair_07");
            itemNames.Add("male_hair_08");
            itemNames.Add("male_hair_09");
            itemNames.Add("male_hair_10");
            items.Add(itemName, itemNames);

            itemName = "beard";
            itemNames = new List<string>
            {
                "volume_beard_empty",
                "volume_beard_01_01",
                "volume_beard_01_02",
                "volume_beard_02_01",
                "volume_beard_02_02",
                "volume_beard_02_03",
                "volume_beard_03_01",
                "volume_beard_03_03"
            };
            items.Add(itemName, itemNames);

            itemName = "helmet";
            itemNames = new List<string>
            {
                "helmet_empty",
                "helmet_01",
                "helmet_02",
                "helmet_03",
                "helmet_04",
                "helmet_05"
            };
            items.Add(itemName, itemNames);

            itemName = "glasses";
            itemNames = new List<string>
            {
                "glasses_empty",
                "glasses_01",
                "glasses_02",
                "glasses_03",
                "glasses_04",
                "glasses_05"
            };
            items.Add(itemName, itemNames);

            itemName = "head";
            itemNames = new List<string>
            {
                "male_head_01",
                "male_head_02",
                "male_head_03",
                "male_head_04",
                "male_head_05",
                "male_head_06",
                "male_head_08",
                "male_head_09",
                "male_head_10"
            };
            items.Add(itemName, itemNames);

            itemName = "suit";
            itemNames = new List<string>
            {
                "male_suit_01",
                "male_suit_02",
                "male_suit_03",
                "male_suit_04",
                "male_suit_06"
            };
            items.Add(itemName, itemNames);

            textureName = "faces_tex";
            textureNames.Add("male_face_1.tga");
            textureNames.Add("male_face_2.tga");
            textureNames.Add("male_face_3.tga");
            textureNames.Add("male_face_4.tga");
            textureNames.Add("male_face_5.tga");
            textureNames.Add("male_face_6.tga");
            textureNames.Add("male_face_7.tga");
            textureNames.Add("male_face_8.tga");
            textureNames.Add("male_face_9.tga");
            textureNames.Add("male_face_10.tga");
            textures.Add(textureName, textureNames);

            textureName = "hands_tex";
            textureNames = new List<string>
            {
                "male_hands1.png",
                "male_hands2.png",
                "male_hands3.png",
                "male_hands4.png",
                "male_hands5.png",
                "male_hands6.png",
                "male_hands7.png",
                "male_hands8.png",
                "male_hands9.png",
                "male_hands10.png"
            };
            textures.Add(textureName, textureNames);

            materialName = "hair_";
            submaterialNames.Add("male_hair_01_1.mat");
            submaterialNames.Add("male_hair_01_2.mat");
            submaterialNames.Add("male_hair_01_3.mat");
            submaterialNames.Add("male_hair_01_4.mat");
            submaterialNames.Add("male_hair_01_5.mat");
            submaterialNames.Add("male_hair_01_6.mat");
            submaterialNames.Add("male_hair_01_7.mat");
            submaterials.Add("male_hair_01", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_02_1.mat",
                "male_hair_02_2.mat",
                "male_hair_02_3.mat",
                "male_hair_02_4.mat",
                "male_hair_02_5.mat",
                "male_hair_02_6.mat",
                "male_hair_02_7.mat"
            };
            submaterials.Add("male_hair_02", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_03_1.mat",
                "male_hair_03_2.mat",
                "male_hair_03_3.mat",
                "male_hair_03_4.mat",
                "male_hair_03_5.mat",
                "male_hair_03_6.mat",
                "male_hair_03_7.mat"
            };
            submaterials.Add("male_hair_03", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_04_1.mat",
                "male_hair_04_2.mat",
                "male_hair_04_3.mat",
                "male_hair_04_4.mat",
                "male_hair_04_5.mat",
                "male_hair_04_6.mat",
                "male_hair_04_7.mat"
            };
            submaterials.Add("male_hair_04", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_05_1.mat",
                "male_hair_05_2.mat",
                "male_hair_05_3.mat",
                "male_hair_05_4.mat",
                "male_hair_05_5.mat",
                "male_hair_05_6.mat",
                "male_hair_05_7.mat"
            };
            submaterials.Add("male_hair_05", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_06_1.mat",
                "male_hair_06_2.mat",
                "male_hair_06_3.mat",
                "male_hair_06_4.mat",
                "male_hair_06_5.mat",
                "male_hair_06_6.mat",
                "male_hair_06_7.mat"
            };
            submaterials.Add("male_hair_06", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_07_1.mat",
                "male_hair_07_2.mat",
                "male_hair_07_3.mat",
                "male_hair_07_4.mat",
                "male_hair_07_5.mat",
                "male_hair_07_6.mat",
                "male_hair_07_7.mat"
            };
            submaterials.Add("male_hair_07", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_08_1.mat",
                "male_hair_08_2.mat",
                "male_hair_08_3.mat",
                "male_hair_08_4.mat",
                "male_hair_08_5.mat",
                "male_hair_08_6.mat",
                "male_hair_08_7.mat"
            };
            submaterials.Add("male_hair_08", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_09_1.mat",
                "male_hair_09_2.mat",
                "male_hair_09_3.mat",
                "male_hair_09_4.mat",
                "male_hair_09_5.mat",
                "male_hair_09_6.mat",
                "male_hair_09_7.mat"
            };
            submaterials.Add("male_hair_09", submaterialNames);

            submaterialNames = new List<string>
            {
                "male_hair_10_1.mat",
                "male_hair_10_2.mat",
                "male_hair_10_3.mat",
                "male_hair_10_4.mat",
                "male_hair_10_5.mat",
                "male_hair_10_6.mat",
                "male_hair_10_7.mat"
            };
            submaterials.Add("male_hair_10", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "beard_";
            submaterials = new Dictionary<string, List<string>>();
            submaterialNames = new List<string>
            {
                "volume_beard_01",
                "volume_beard_01_01_1",
                "volume_beard_01_01_2",
                "volume_beard_01_01_3",
                "volume_beard_01_01_4",
                "volume_beard_01_01_5"
            };
            submaterials.Add("volume_beard_01_01", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_01_02_1",
                "volume_beard_01_02_2",
                "volume_beard_01_02_3",
                "volume_beard_01_02_4",
                "volume_beard_01_02_5"
            };
            submaterials.Add("volume_beard_01_02", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_02",
                "volume_beard_02_01_1",
                "volume_beard_02_01_2",
                "volume_beard_02_01_3",
                "volume_beard_02_01_4",
                "volume_beard_02_01_5"
            };
            submaterials.Add("volume_beard_02_01", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_02_02_1",
                "volume_beard_02_02_2",
                "volume_beard_02_02_3",
                "volume_beard_02_02_4",
                "volume_beard_02_02_5"
            };
            submaterials.Add("volume_beard_02_02", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_02_03_1",
                "volume_beard_02_03_2",
                "volume_beard_02_03_3",
                "volume_beard_02_03_4",
                "volume_beard_02_03_5"
            };
            submaterials.Add("volume_beard_02_03", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_03",
                "volume_beard_03_01_1",
                "volume_beard_03_01_2",
                "volume_beard_03_01_3",
                "volume_beard_03_01_4",
                "volume_beard_03_01_5"
            };
            submaterials.Add("volume_beard_03_01", submaterialNames);

            submaterialNames = new List<string>
            {
                "volume_beard_03_03_1",
                "volume_beard_03_03_2",
                "volume_beard_03_03_3",
                "volume_beard_03_03_4",
                "volume_beard_03_03_5"
            };
            submaterials.Add("volume_beard_03_03", submaterialNames);

            materials.Add(materialName, submaterials);

            AvatarIndex colonialHuman = new AvatarIndex(race, sex, items, textures, materials);
            AvatarIndexes.Add(colonialHuman);
        }

        public override void Write(BgoProtocolWriter w)
        {
            base.Write(w);
            w.Write((ushort)AvatarIndexes.Count);
            foreach(AvatarIndex ai in AvatarIndexes)
                ai.Write(w);
        }
    }
}
