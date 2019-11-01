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

            AvatarIndex colonialMaleHuman = new AvatarIndex(race, sex, items, textures, materials);
            AvatarIndexes.Add(colonialMaleHuman);

            race = "human";
            sex = "female";

            items = new Dictionary<string, List<string>>();
            textures = new Dictionary<string, List<string>>();
            materials = new Dictionary<string, Dictionary<string, List<string>>>();

            itemNames = new List<string>();
            textureNames = new List<string>();
            submaterials = new Dictionary<string, List<string>>();
            submaterialNames = new List<string>();

            itemName = "beard";
            items.Add(itemName, itemNames);

            itemName = "hair";
            itemNames.Add("female_hair_01");
            itemNames.Add("female_hair_02");
            itemNames.Add("female_hair_03");
            itemNames.Add("female_hair_04");
            itemNames.Add("female_hair_05");
            itemNames.Add("female_hair_06");
            itemNames.Add("female_hair_07");
            itemNames.Add("female_hair_08");
            itemNames.Add("female_hair_09");
            itemNames.Add("female_hair_10");
            items.Add(itemName, itemNames);

            itemName = "helmet";
            itemNames = new List<string>
            {
                "helmet_empty",
                "helmet_01",
                "helmet_02",
                "helmet_03_open",
                "helmet_04",
                "helmet_05"
            };
            items.Add(itemName, itemNames);

            itemName = "glasses";
            itemNames = new List<string>
            {
                "female_glasses_empty",
                "female_glasses_01",
                "female_glasses_02",
                "female_glasses_03",
                "female_glasses_04",
                "female_glasses_05"
            };
            items.Add(itemName, itemNames);

            itemName = "head";
            itemNames = new List<string>
            {
                "female_head_01",
                "female_head_02",
                "female_head_03",
                "female_head_04",
                "female_head_05",
                "female_head_06",
                "female_head_08",
                "female_head_09",
                "female_head_10"
            };
            items.Add(itemName, itemNames);

            itemName = "suit";
            itemNames = new List<string>
            {
                "female_suit_01",
                "female_suit_02",
                "female_suit_03",
                "female_suit_04",
                "female_suit_05"
            };
            items.Add(itemName, itemNames);

            textureName = "faces_tex";
            textureNames.Add("female_face_1.tga");
            textureNames.Add("female_face_2.tga");
            textureNames.Add("female_face_3.tga");
            textureNames.Add("female_face_4.tga");
            textureNames.Add("female_face_5.tga");
            textureNames.Add("female_face_6.tga");
            textureNames.Add("female_face_7.tga");
            textureNames.Add("female_face_8.tga");
            textureNames.Add("female_face_9.tga");
            textureNames.Add("female_face_10.tga");
            textures.Add(textureName, textureNames);

            textureName = "hands_tex";
            textureNames = new List<string>
            {
                "female_hands1.png",
                "female_hands2.png",
                "female_hands3.png",
                "female_hands4.png",
                "female_hands5.png",
                "female_hands6.png",
                "female_hands7.png",
                "female_hands8.png",
                "female_hands9.png",
                "female_hands10.png"
            };
            textures.Add(textureName, textureNames);

            materialName = "hair_";
            submaterialNames.Add("female_hair_01.mat");
            submaterialNames.Add("female_hair_01_2.mat");
            submaterialNames.Add("female_hair_01_3.mat");
            submaterialNames.Add("female_hair_01_4.mat");
            submaterialNames.Add("female_hair_01_5.mat");
            submaterialNames.Add("female_hair_01_6.mat");
            submaterialNames.Add("female_hair_01_7.mat");
            submaterials.Add("female_hair_01", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_02.mat",
                "female_hair_02_2.mat",
                "female_hair_02_3.mat",
                "female_hair_02_4.mat",
                "female_hair_02_5.mat",
                "female_hair_02_6.mat",
                "female_hair_02_7.mat"
            };
            submaterials.Add("female_hair_02", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_03.mat",
                "female_hair_03_2.mat",
                "female_hair_03_3.mat",
                "female_hair_03_4.mat",
                "female_hair_03_5.mat",
                "female_hair_03_6.mat",
                "female_hair_03_7.mat"
            };
            submaterials.Add("female_hair_03", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_04.mat",
                "female_hair_04_2.mat",
                "female_hair_04_3.mat",
                "female_hair_04_4.mat",
                "female_hair_04_5.mat",
                "female_hair_04_6.mat",
                "female_hair_04_7.mat"
            };
            submaterials.Add("female_hair_04", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_05.mat",
                "female_hair_05_2.mat",
                "female_hair_05_3.mat",
                "female_hair_05_4.mat",
                "female_hair_05_5.mat",
                "female_hair_05_6.mat",
                "female_hair_05_7.mat"
            };
            submaterials.Add("female_hair_05", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_06.mat",
                "female_hair_06_2.mat",
                "female_hair_06_3.mat",
                "female_hair_06_4.mat",
                "female_hair_06_5.mat",
                "female_hair_06_6.mat",
                "female_hair_06_7.mat"
            };
            submaterials.Add("female_hair_06", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_07.mat",
                "female_hair_07_2.mat",
                "female_hair_07_3.mat",
                "female_hair_07_4.mat",
                "female_hair_07_5.mat",
                "female_hair_07_6.mat",
                "female_hair_07_7.mat"
            };
            submaterials.Add("female_hair_07", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_08.mat",
                "female_hair_08_2.mat",
                "female_hair_08_3.mat",
                "female_hair_08_4.mat",
                "female_hair_08_5.mat",
                "female_hair_08_6.mat",
                "female_hair_08_7.mat"
            };
            submaterials.Add("female_hair_08", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_09.mat",
                "female_hair_09_2.mat",
                "female_hair_09_3.mat",
                "female_hair_09_4.mat",
                "female_hair_09_5.mat",
                "female_hair_09_6.mat",
                "female_hair_09_7.mat"
            };
            submaterials.Add("female_hair_09", submaterialNames);

            submaterialNames = new List<string>
            {
                "female_hair_10.mat",
                "female_hair_10_2.mat",
                "female_hair_10_3.mat",
                "female_hair_10_4.mat",
                "female_hair_10_5.mat",
                "female_hair_10_6.mat",
                "female_hair_10_7.mat"
            };
            submaterials.Add("female_hair_10", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "beard_";
            submaterials = new Dictionary<string, List<string>>();
            materials.Add(materialName, submaterials);

            AvatarIndex colonialFemaleHuman = new AvatarIndex(race, sex, items, textures, materials);
            AvatarIndexes.Add(colonialFemaleHuman);

            race = "cylon";
            sex = "centurion";

            items = new Dictionary<string, List<string>>();
            textures = new Dictionary<string, List<string>>();
            materials = new Dictionary<string, Dictionary<string, List<string>>>();

            itemNames = new List<string>();
            textureNames = new List<string>();
            submaterials = new Dictionary<string, List<string>>();
            submaterialNames = new List<string>();

            itemName = "head";
            itemNames = new List<string>
            {
                "centurion_head_v1",
                "centurion_head_v2"
            };
            items.Add(itemName, itemNames);

            itemName = "arms";
            itemNames = new List<string>
            {
                "centurion_arms_v1",
                "centurion_arms_v2"
            };
            items.Add(itemName, itemNames);

            itemName = "body";
            itemNames = new List<string>
            {
                "centurion_body_v1",
                "centurion_body_v2"
            };
            items.Add(itemName, itemNames);

            itemName = "legs";
            itemNames = new List<string>
            {
                "centurion_legs_v1",
                "centurion_legs_v2"
            };
            items.Add(itemName, itemNames);

            materialName = "head_";
            submaterialNames.Add("centurion_head_v1_black_1.mat");
            submaterialNames.Add("centurion_head_v1_black_2.mat");
            submaterialNames.Add("centurion_head_v1_black_3.mat");
            submaterialNames.Add("centurion_head_v1_black_4.mat");
            submaterials.Add("centurion_head_v1_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v1_brown_1.mat",
                "centurion_head_v1_brown_2.mat",
                "centurion_head_v1_brown_3.mat",
                "centurion_head_v1_brown_4.mat",
            };
            submaterials.Add("centurion_head_v1_brown", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v1_green_1.mat",
                "centurion_head_v1_green_2.mat",
                "centurion_head_v1_green_3.mat",
                "centurion_head_v1_green_4.mat",
            };
            submaterials.Add("centurion_head_v1_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v1_grey_1.mat",
                "centurion_head_v1_grey_2.mat",
                "centurion_head_v1_grey_3.mat",
                "centurion_head_v1_grey_4.mat",
            };
            submaterials.Add("centurion_head_v1_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v1_white_1.mat",
                "centurion_head_v1_white_2.mat",
                "centurion_head_v1_white_3.mat",
                "centurion_head_v1_white_4.mat",
            };
            submaterials.Add("centurion_head_v1_white", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v2_black_1.mat",
                "centurion_head_v2_black_2.mat",
                "centurion_head_v2_black_3.mat",
                "centurion_head_v2_black_4.mat",
            };
            submaterials.Add("centurion_head_v2_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v2_green_1.mat",
                "centurion_head_v2_green_2.mat",
                "centurion_head_v2_green_3.mat",
                "centurion_head_v2_green_4.mat",
            };
            submaterials.Add("centurion_head_v2_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v2_grey_1.mat",
                "centurion_head_v2_grey_2.mat",
                "centurion_head_v2_grey_3.mat",
                "centurion_head_v2_grey_4.mat",
            };
            submaterials.Add("centurion_head_v2_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_head_v2_white_1.mat",
                "centurion_head_v2_white_2.mat",
                "centurion_head_v2_white_3.mat",
                "centurion_head_v2_white_4.mat",
            };
            submaterials.Add("centurion_head_v2_white", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "legs_";
            submaterialNames = new List<string>
            {
                "centurion_legs_v1_black_1.mat",
            };
            submaterials.Add("centurion_legs_v1_black", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v1_brown_1.mat",
            };
            submaterials.Add("centurion_legs_v1_brown", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v1_green_1.mat",
            };
            submaterials.Add("centurion_legs_v1_green", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v1_grey_1.mat",
            };
            submaterials.Add("centurion_legs_v1_grey", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v1_white_1.mat",
            };
            submaterials.Add("centurion_legs_v1_white", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_legs_v2_black_1.mat",
            };
            submaterials.Add("centurion_legs_v2_black", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v2_brown_1.mat",
            };
            submaterials.Add("centurion_legs_v2_brown", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v2_green_1.mat",
            };
            submaterials.Add("centurion_legs_v2_green", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v2_grey_1.mat",
            };
            submaterials.Add("centurion_legs_v2_grey", submaterialNames);
            submaterialNames = new List<string>
            {
                "centurion_legs_v2_white_1.mat",
            };
            submaterials.Add("centurion_legs_v2_white", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "body_";
            submaterialNames.Add("centurion_body_v1_black_1.mat");
            submaterialNames.Add("centurion_body_v1_black_2.mat");
            submaterialNames.Add("centurion_body_v1_black_3.mat");
            submaterialNames.Add("centurion_body_v1_black_4.mat");
            submaterials.Add("centurion_body_v1_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v1_brown_1.mat",
                "centurion_body_v1_brown_2.mat",
                "centurion_body_v1_brown_3.mat",
                "centurion_body_v1_brown_4.mat",
            };
            submaterials.Add("centurion_body_v1_brown", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v1_green_1.mat",
                "centurion_body_v1_green_2.mat",
                "centurion_body_v1_green_3.mat",
                "centurion_body_v1_green_4.mat",
            };
            submaterials.Add("centurion_body_v1_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v1_grey_1.mat",
                "centurion_body_v1_grey_2.mat",
                "centurion_body_v1_grey_3.mat",
                "centurion_body_v1_grey_4.mat",
            };
            submaterials.Add("centurion_body_v1_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v1_white_1.mat",
                "centurion_body_v1_white_2.mat",
                "centurion_body_v1_white_3.mat",
                "centurion_body_v1_white_4.mat",
            };
            submaterials.Add("centurion_body_v1_white", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v2_black_1.mat",
                "centurion_body_v2_black_2.mat",
                "centurion_body_v2_black_3.mat",
                "centurion_body_v2_black_4.mat",
            };
            submaterials.Add("centurion_body_v2_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v2_green_1.mat",
                "centurion_body_v2_green_2.mat",
                "centurion_body_v2_green_3.mat",
                "centurion_body_v2_green_4.mat",
            };
            submaterials.Add("centurion_body_v2_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v2_grey_1.mat",
                "centurion_body_v2_grey_2.mat",
                "centurion_body_v2_grey_3.mat",
                "centurion_body_v2_grey_4.mat",
            };
            submaterials.Add("centurion_body_v2_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_body_v2_white_1.mat",
                "centurion_body_v2_white_2.mat",
                "centurion_body_v2_white_3.mat",
                "centurion_body_v2_white_4.mat",
            };
            submaterials.Add("centurion_body_v2_white", submaterialNames);

            materials.Add(materialName, submaterials);

            materialName = "arms_";
            submaterialNames.Add("centurion_arms_v1_black_1.mat");
            submaterialNames.Add("centurion_arms_v1_black_2.mat");
            submaterialNames.Add("centurion_arms_v1_black_3.mat");
            submaterialNames.Add("centurion_arms_v1_black_4.mat");
            submaterials.Add("centurion_arms_v1_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v1_brown_1.mat",
                "centurion_arms_v1_brown_2.mat",
                "centurion_arms_v1_brown_3.mat",
                "centurion_arms_v1_brown_4.mat",
            };
            submaterials.Add("centurion_arms_v1_brown", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v1_green_1.mat",
                "centurion_arms_v1_green_2.mat",
                "centurion_arms_v1_green_3.mat",
                "centurion_arms_v1_green_4.mat",
            };
            submaterials.Add("centurion_arms_v1_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v1_grey_1.mat",
                "centurion_arms_v1_grey_2.mat",
                "centurion_arms_v1_grey_3.mat",
                "centurion_arms_v1_grey_4.mat",
            };
            submaterials.Add("centurion_arms_v1_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v1_white_1.mat",
                "centurion_arms_v1_white_2.mat",
                "centurion_arms_v1_white_3.mat",
                "centurion_arms_v1_white_4.mat",
            };
            submaterials.Add("centurion_arms_v1_white", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v2_black_1.mat",
                "centurion_arms_v2_black_2.mat",
                "centurion_arms_v2_black_3.mat",
                "centurion_arms_v2_black_4.mat",
            };
            submaterials.Add("centurion_arms_v2_black", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v2_green_1.mat",
                "centurion_arms_v2_green_2.mat",
                "centurion_arms_v2_green_3.mat",
                "centurion_arms_v2_green_4.mat",
            };
            submaterials.Add("centurion_arms_v2_green", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v2_grey_1.mat",
                "centurion_arms_v2_grey_2.mat",
                "centurion_arms_v2_grey_3.mat",
                "centurion_arms_v2_grey_4.mat",
            };
            submaterials.Add("centurion_arms_v2_grey", submaterialNames);

            submaterialNames = new List<string>
            {
                "centurion_arms_v2_white_1.mat",
                "centurion_arms_v2_white_2.mat",
                "centurion_arms_v2_white_3.mat",
                "centurion_arms_v2_white_4.mat",
            };
            submaterials.Add("centurion_arms_v2_white", submaterialNames);

            materials.Add(materialName, submaterials);

            AvatarIndex Cylon = new AvatarIndex(race, sex, items, textures, materials);
            AvatarIndexes.Add(Cylon);
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
