using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using System.Linq;
using Terraria.ModLoader.IO;
using Terraria.ID;

namespace IDye
{
    public class IDyePlayer : ModPlayer
    {
        // experimental; none of this is final
        public Microsoft.Xna.Framework.Color[] iDyePrimaryColors;
        public Microsoft.Xna.Framework.Color[] iDyeSecondaryColors;
        public float[] iDyeSaturation;
        public string[] iDyePasses;
        public bool[] iDyeUseImage;
        public int[] iDyeItemID;

        public override void Initialize()
        {
            iDyePrimaryColors = new Microsoft.Xna.Framework.Color[32];
            iDyeSecondaryColors = new Microsoft.Xna.Framework.Color[32];
            iDyeSaturation = new float[32];
            iDyePasses = new string[32];
            iDyeUseImage = new bool[32];
            iDyeItemID = new int[32];

            for (int i = 0; i < 32; i++)
            {
                iDyePrimaryColors[i] = new Microsoft.Xna.Framework.Color(0f, 0f, 0f);
                iDyeSecondaryColors[i] = new Microsoft.Xna.Framework.Color(0f, 0f, 0f);
                iDyeSaturation[i] = 0f;
                iDyePasses[i] = "ArmorColored";
                iDyeUseImage[i] = false;
                iDyeItemID[i] = ItemID.RedDye;
            }
        }

        public override void SaveData(TagCompound tag)
        {
            tag["IDyePrimaryColors"] = iDyePrimaryColors;
            tag["IDyeSecondaryColors"] = iDyeSecondaryColors;
            tag["IDyeSaturation"] = iDyeSaturation.ToList();
            tag["IDyePasses"] = iDyePasses.ToList();
            tag["IDyeUseImage"] = iDyeUseImage.ToList();
            tag["IDyeItemID"] = iDyeItemID.ToList();
        }

        public override void LoadData(TagCompound tag)
        {
            iDyePrimaryColors = tag.Get<Microsoft.Xna.Framework.Color[]>("IDyePrimaryColors");
            iDyeSecondaryColors = tag.Get<Microsoft.Xna.Framework.Color[]>("IDyeSecondaryColors");
            iDyeSaturation = tag.Get<List<float>>("IDyeSaturation").ToArray();
            iDyePasses = tag.Get<List<string>>("IDyePasses").ToArray();
            iDyeUseImage = tag.Get<List<bool>>("IDyeUseImage").ToArray();
            iDyeItemID = tag.Get<List<int>>("IDyeItemID").ToArray();
        }

        public override void Unload()
        {
            iDyePrimaryColors = null;
            iDyeSecondaryColors = null;
            iDyeSaturation = null;
            iDyePasses = null;
            iDyeUseImage = null;
            iDyeItemID = null;
        }
    }
}
