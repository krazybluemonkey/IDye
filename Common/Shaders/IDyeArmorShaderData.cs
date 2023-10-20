using Microsoft.Xna.Framework;
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
                if (iDyePlayer.iDyeUseImage != null && iDyePlayer.iDyeUseImage[dyeSlot])
                {
                    UseImage("Images/Misc/noise");
                }

                // here's where i'd put a shader swap. IF I HAD ONE

                if (iDyePlayer.iDyePasses != null)
                {
                    SwapProgram(iDyePlayer.iDyePasses[dyeSlot]);
                }
                if (iDyePlayer.iDyePrimaryColors != null)
                {
                    UseColor(iDyePlayer.iDyePrimaryColors[dyeSlot]);
                }

                //UseSecondaryColor(0f, 0f, 1f);
            }
            base.Apply(entity, drawData);
        }

        public IDyeArmorShaderData UseDyeSlot(int slot)
        {
            dyeSlot = slot;
            return this;
        }
    }
}