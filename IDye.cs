using IDye.Content;
using Terraria;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using static Terraria.ModLoader.ModContent;
using ReLogic.Content;

namespace IDye
{
    public class IDye : Mod
    {

        // public Ref<Effect> dyeRef;
        internal static IDye instance;
        public override void Load()
        {
            instance = this;

            // All of this loading needs to be client-side.

            //if (Main.netMode != NetmodeID.Server)
            //{
            //    dyeRef = new Ref<Effect>(ModContent.Request<Effect>("Assets/Effects/IDyeEffect", AssetRequestMode.ImmediateLoad).Value);
            //    GameShaders.Armor.BindShader(ItemType<DeterminationDye>(), new ArmorShaderData(dyeRef, "ArmorDetermination"));
            //}
        }
    }
}