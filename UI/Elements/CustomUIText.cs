using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using IDye.Common.Shaders;
using Terraria.Localization;

namespace IDye.UI.Elements
{
    internal class CustomUIText : UIText
    {
        public int ItemID;

        public CustomUIText(string text, float textScale = 1f, bool large = false)
        : base(text, textScale, large)
        { }

        public CustomUIText(LocalizedText text, float textScale = 1f, bool large = false)
        : base(text, textScale, large)
        { }

        public CustomUIText SetItemID(int itemID)
        {
            ItemID = itemID;
            return this;
        }
    }
}