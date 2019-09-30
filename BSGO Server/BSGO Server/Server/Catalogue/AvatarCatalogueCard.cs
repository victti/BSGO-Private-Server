using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class AvatarCatalogueCard : Card
    {
        public List<AvatarIndex> AvatarIndexes = new List<AvatarIndex>();

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
            itemNames = new List<string>();
            itemNames.Add("volume_beard_empty");
            itemNames.Add("volume_beard_01_01");
            itemNames.Add("volume_beard_01_02");
            itemNames.Add("volume_beard_02_01");
            itemNames.Add("volume_beard_02_02");
            itemNames.Add("volume_beard_02_03");
            itemNames.Add("volume_beard_03_01");
            itemNames.Add("volume_beard_03_03");
            items.Add(itemName, itemNames);

            itemName = "helmet";
            itemNames = new List<string>();
            itemNames.Add("helmet_empty");
            itemNames.Add("helmet_01");
            itemNames.Add("helmet_02");
            itemNames.Add("helmet_03");
            itemNames.Add("helmet_04");
            itemNames.Add("helmet_05");
            items.Add(itemName, itemNames);

            itemName = "glasses";
            itemNames = new List<string>();
            itemNames.Add("glasses_empty");
            itemNames.Add("glasses_01");
            itemNames.Add("glasses_02");
            itemNames.Add("glasses_03");
            itemNames.Add("glasses_04");
            itemNames.Add("glasses_05");
            items.Add(itemName, itemNames);

            itemName = "head";
            itemNames = new List<string>();
            itemNames.Add("male_head_01");
            itemNames.Add("male_head_02");
            itemNames.Add("male_head_03");
            itemNames.Add("male_head_04");
            itemNames.Add("male_head_05");
            itemNames.Add("male_head_06");
            itemNames.Add("male_head_08");
            itemNames.Add("male_head_09");
            itemNames.Add("male_head_10");
            items.Add(itemName, itemNames);

            itemName = "suit";
            itemNames = new List<string>();
            itemNames.Add("male_suit_01");
            itemNames.Add("male_suit_02");
            itemNames.Add("male_suit_03");
            itemNames.Add("male_suit_04");
            itemNames.Add("male_suit_06");
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
            textureNames = new List<string>();
            textureNames.Add("male_hands1.png");
            textureNames.Add("male_hands2.png");
            textureNames.Add("male_hands3.png");
            textureNames.Add("male_hands4.png");
            textureNames.Add("male_hands5.png");
            textureNames.Add("male_hands6.png");
            textureNames.Add("male_hands7.png");
            textureNames.Add("male_hands8.png");
            textureNames.Add("male_hands9.png");
            textureNames.Add("male_hands10.png");
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

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_02_1.mat");
            submaterialNames.Add("male_hair_02_2.mat");
            submaterialNames.Add("male_hair_02_3.mat");
            submaterialNames.Add("male_hair_02_4.mat");
            submaterialNames.Add("male_hair_02_5.mat");
            submaterialNames.Add("male_hair_02_6.mat");
            submaterialNames.Add("male_hair_02_7.mat");
            submaterials.Add("male_hair_02", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_03_1.mat");
            submaterialNames.Add("male_hair_03_2.mat");
            submaterialNames.Add("male_hair_03_3.mat");
            submaterialNames.Add("male_hair_03_4.mat");
            submaterialNames.Add("male_hair_03_5.mat");
            submaterialNames.Add("male_hair_03_6.mat");
            submaterialNames.Add("male_hair_03_7.mat");
            submaterials.Add("male_hair_03", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_04_1.mat");
            submaterialNames.Add("male_hair_04_2.mat");
            submaterialNames.Add("male_hair_04_3.mat");
            submaterialNames.Add("male_hair_04_4.mat");
            submaterialNames.Add("male_hair_04_5.mat");
            submaterialNames.Add("male_hair_04_6.mat");
            submaterialNames.Add("male_hair_04_7.mat");
            submaterials.Add("male_hair_04", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_05_1.mat");
            submaterialNames.Add("male_hair_05_2.mat");
            submaterialNames.Add("male_hair_05_3.mat");
            submaterialNames.Add("male_hair_05_4.mat");
            submaterialNames.Add("male_hair_05_5.mat");
            submaterialNames.Add("male_hair_05_6.mat");
            submaterialNames.Add("male_hair_05_7.mat");
            submaterials.Add("male_hair_05", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_06_1.mat");
            submaterialNames.Add("male_hair_06_2.mat");
            submaterialNames.Add("male_hair_06_3.mat");
            submaterialNames.Add("male_hair_06_4.mat");
            submaterialNames.Add("male_hair_06_5.mat");
            submaterialNames.Add("male_hair_06_6.mat");
            submaterialNames.Add("male_hair_06_7.mat");
            submaterials.Add("male_hair_06", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_07_1.mat");
            submaterialNames.Add("male_hair_07_2.mat");
            submaterialNames.Add("male_hair_07_3.mat");
            submaterialNames.Add("male_hair_07_4.mat");
            submaterialNames.Add("male_hair_07_5.mat");
            submaterialNames.Add("male_hair_07_6.mat");
            submaterialNames.Add("male_hair_07_7.mat");
            submaterials.Add("male_hair_07", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_08_1.mat");
            submaterialNames.Add("male_hair_08_2.mat");
            submaterialNames.Add("male_hair_08_3.mat");
            submaterialNames.Add("male_hair_08_4.mat");
            submaterialNames.Add("male_hair_08_5.mat");
            submaterialNames.Add("male_hair_08_6.mat");
            submaterialNames.Add("male_hair_08_7.mat");
            submaterials.Add("male_hair_08", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_09_1.mat");
            submaterialNames.Add("male_hair_09_2.mat");
            submaterialNames.Add("male_hair_09_3.mat");
            submaterialNames.Add("male_hair_09_4.mat");
            submaterialNames.Add("male_hair_09_5.mat");
            submaterialNames.Add("male_hair_09_6.mat");
            submaterialNames.Add("male_hair_09_7.mat");
            submaterials.Add("male_hair_09", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("male_hair_10_1.mat");
            submaterialNames.Add("male_hair_10_2.mat");
            submaterialNames.Add("male_hair_10_3.mat");
            submaterialNames.Add("male_hair_10_4.mat");
            submaterialNames.Add("male_hair_10_5.mat");
            submaterialNames.Add("male_hair_10_6.mat");
            submaterialNames.Add("male_hair_10_7.mat");
            submaterials.Add("male_hair_10", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "beard_";
            submaterials = new Dictionary<string, List<string>>();
            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_01");
            submaterialNames.Add("volume_beard_01_01_1");
            submaterialNames.Add("volume_beard_01_01_2");
            submaterialNames.Add("volume_beard_01_01_3");
            submaterialNames.Add("volume_beard_01_01_4");
            submaterialNames.Add("volume_beard_01_01_5");
            submaterials.Add("volume_beard_01_01", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_01_02_1");
            submaterialNames.Add("volume_beard_01_02_2");
            submaterialNames.Add("volume_beard_01_02_3");
            submaterialNames.Add("volume_beard_01_02_4");
            submaterialNames.Add("volume_beard_01_02_5");
            submaterials.Add("volume_beard_01_02", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_02");
            submaterialNames.Add("volume_beard_02_01_1");
            submaterialNames.Add("volume_beard_02_01_2");
            submaterialNames.Add("volume_beard_02_01_3");
            submaterialNames.Add("volume_beard_02_01_4");
            submaterialNames.Add("volume_beard_02_01_5");
            submaterials.Add("volume_beard_02_01", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_02_02_1");
            submaterialNames.Add("volume_beard_02_02_2");
            submaterialNames.Add("volume_beard_02_02_3");
            submaterialNames.Add("volume_beard_02_02_4");
            submaterialNames.Add("volume_beard_02_02_5");
            submaterials.Add("volume_beard_02_02", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_02_03_1");
            submaterialNames.Add("volume_beard_02_03_2");
            submaterialNames.Add("volume_beard_02_03_3");
            submaterialNames.Add("volume_beard_02_03_4");
            submaterialNames.Add("volume_beard_02_03_5");
            submaterials.Add("volume_beard_02_03", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_03");
            submaterialNames.Add("volume_beard_03_01_1");
            submaterialNames.Add("volume_beard_03_01_2");
            submaterialNames.Add("volume_beard_03_01_3");
            submaterialNames.Add("volume_beard_03_01_4");
            submaterialNames.Add("volume_beard_03_01_5");
            submaterials.Add("volume_beard_03_01", submaterialNames);

            submaterialNames = new List<string>();
            submaterialNames.Add("volume_beard_03_03_1");
            submaterialNames.Add("volume_beard_03_03_2");
            submaterialNames.Add("volume_beard_03_03_3");
            submaterialNames.Add("volume_beard_03_03_4");
            submaterialNames.Add("volume_beard_03_03_5");
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
            {
                ai.Write(w);
            }
        }
    }
}
