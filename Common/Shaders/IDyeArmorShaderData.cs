using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework.Graphics;

namespace IDye.Common.Shaders
{
    public class IDyeArmorShaderData : ArmorShaderData
    {
        private int dyeSlot;

        public IDyeArmorShaderData(Ref<Effect> shader, string passName)
        : base(shader, passName)
        {}

        public override void Apply(Entity entity, DrawData? drawData)
        {

            IDyePlayer iDyePlayer = null;
            if (entity is Player)
            {
                Player player = entity as Player;
                if (player != null && !player.isDisplayDollOrInanimate && !player.isHatRackDoll)
                {
                    iDyePlayer = player.GetModPlayer<IDyePlayer>();
                }
                else
                {
                    return;
                }
            }
            if (iDyePlayer != null)
            {
                //*should* allow for stuff like metallic shader data to work... hopfully
                if (iDyePlayer.iDyeItemID != null && GameShaders.Armor.GetShaderFromItemId(iDyePlayer.iDyeItemID[dyeSlot]) != null)
                {
                    GameShaders.Armor.GetShaderFromItemId(iDyePlayer.iDyeItemID[dyeSlot]).Apply(entity, drawData);
                }

                // might benefit to have a way to derive this from the iDyeItemID?
                if (iDyePlayer.iDyeUseImage != null && iDyePlayer.iDyeUseImage[dyeSlot])
                {
                    UseImage("Images/Misc/noise");
                }

                // shader swap here; get via iDyeItemID maybe?

                // same deal as UseImage; way to get from iDyeItemID?
                if (iDyePlayer.iDyePasses != null)
                {
                    SwapProgram(iDyePlayer.iDyePasses[dyeSlot]);
                }

                // this is fine 
                if (iDyePlayer.iDyePrimaryColors != null)
                {
                    UseColor(iDyePlayer.iDyePrimaryColors[dyeSlot]);
                }

                // have not started on this yet
                //UseSecondaryColor(0f, 0f, 1f);

                base.Apply(entity, drawData);
            }
        }

        public IDyeArmorShaderData UseDyeSlot(int slot)
        {
            dyeSlot = slot;
            return this;
        }
    }
}