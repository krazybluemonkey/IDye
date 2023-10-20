using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace IDye
{
    public static class Resources
    {
        //    public static ShaderCache Shaders { get; internal set; }
        //    public class ShaderCache
        //    {
        //        public ShaderCache()
        //        {
        //            DyeHard mod = DyeHard.instance;
        //            DyeHardShaders = mod.Assets.Request<Effect>("Assets/Effects/DyeHardEffect").Value;

        //            jadeDyeShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "JadeConst");
        //            fireDyeShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Firewave", AssetRequestMode.ImmediateLoad).Value), "Firewave");
        //            fireMiscShader = new MiscShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Firewave", AssetRequestMode.ImmediateLoad).Value), "Firewave");
        //            starlightShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Starlight", AssetRequestMode.ImmediateLoad).Value), "Starlight");
        //            dimStarlightShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "Starlight");
        //            brightStarlightShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "BrightStarlight");

        //            nebulaShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Nebula", AssetRequestMode.ImmediateLoad).Value), "Nebula");
        //            nebulaDistortionTexture = mod.Assets.Request<Texture2D>("Textures/Starry_Noise", AssetRequestMode.ImmediateLoad);
        //            nebulaShader.UseNonVanillaImage(nebulaDistortionTexture);

        //            retroShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "Retro");
        //            retroShader.UseOpacity(0.75f);
        //            retroShader.UseSaturation(0.65f);

        //            retroShaderRed = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "Retro");
        //            retroShaderRed.UseOpacity(-0.25f);
        //            retroShaderRed.UseSaturation(-0.5f);

        //            distortMiscShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Distort", AssetRequestMode.ImmediateLoad).Value), "Distort");
        //            testDistortionTexture = mod.Assets.Request<Texture2D>("Textures/40x40Dist", AssetRequestMode.ImmediateLoad);
        //            distortMiscShader.UseNonVanillaImage(testDistortionTexture);

        //            laserBowOverlayShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/LaserBow", AssetRequestMode.ImmediateLoad).Value), "LaserBow");
        //            chimeraShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "Chimerebos");
        //            opaqueChimeraShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "ChimerebosOpaque");

        //            GameShaders.Armor.BindShader(ItemType<Jade_Dye>(), jadeDyeShader);
        //            GameShaders.Armor.BindShader(ItemType<Heatwave_Dye>(), fireDyeShader);
        //            GameShaders.Armor.BindShader(ItemType<Starlight_Dye>(), starlightShader);
        //            GameShaders.Armor.BindShader(ItemType<Dim_Starlight_Dye>(), dimStarlightShader);
        //            GameShaders.Armor.BindShader(ItemType<Bright_Starlight_Dye>(), brightStarlightShader);
        //            GameShaders.Armor.BindShader(ItemType<Hydra_Staff>(), nebulaShader);
        //            GameShaders.Armor.BindShader(ItemType<Retro_Dye>(), retroShader);
        //            GameShaders.Armor.BindShader(ItemType<Red_Retro_Dye>(), retroShaderRed);

        //            GameShaders.Armor.BindShader(ItemType<GPS_Dye>(), new GPSArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/GPS", AssetRequestMode.ImmediateLoad).Value), "GPS"));

        //            EpikV2.jadeShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Jade_Dye>());
        //            EpikV2.starlightShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Starlight_Dye>());
        //            EpikV2.dimStarlightShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Dim_Starlight_Dye>());
        //            EpikV2.brightStarlightShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Bright_Starlight_Dye>());
        //            EpikV2.nebulaShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Hydra_Staff>());

        //            GameShaders.Armor.BindShader(ItemType<GraphicsDebugger>(), distortMiscShader);
        //            EpikV2.distortShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<GraphicsDebugger>());

        //            EpikV2.alphaMapShader = new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "AlphaMap");
        //            GameShaders.Armor.BindShader(ItemType<Chroma_Dummy_Dye>(), EpikV2.alphaMapShader);
        //            EpikV2.alphaMapShaderID = ItemType<Chroma_Dummy_Dye>();

        //            GameShaders.Armor.BindShader(ItemType<Cursed_Hades_Dye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades"))
        //                .UseColor(0.2f, 1.5f, 0.2f).UseSecondaryColor(0.2f, 1.5f, 0.2f);
        //            GameShaders.Armor.BindShader(ItemType<Ichor_Dye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame"))
        //                .UseColor(1.12f, 1f, 0f).UseSecondaryColor(1.25f, 0.8f, 0f);
        //            EpikV2.ichorShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Ichor_Dye>());
        //            GameShaders.Armor.BindShader(ItemType<Golden_Flame_Dye>(), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades"))
        //                .UseColor(1f, 1f, 1f).UseSecondaryColor(1.5f, 1.25f, 0.2f);

        //            GameShaders.Armor.BindShader(ItemType<Laser_Bow>(), laserBowOverlayShader);
        //            EpikV2.laserBowShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Laser_Bow>());

        //            GameShaders.Armor.BindShader(ItemType<Chimera_Dye>(), chimeraShader);
        //            EpikV2.chimeraShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Chimera_Dye>());

        //            GameShaders.Armor.BindShader(ItemType<Opaque_Chimera_Dye>(), opaqueChimeraShader);
        //            EpikV2.opaqueChimeraShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Opaque_Chimera_Dye>());

        //            GameShaders.Armor.BindShader(ItemType<Inverted_Chimera_Dye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "ChimerebosInverted"));
        //            int invertedChimeraShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Inverted_Chimera_Dye>());

        //            GameShaders.Armor.BindShader(ItemType<Opaque_Inverted_Chimera_Dye>(), new ArmorShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/Armor", AssetRequestMode.ImmediateLoad).Value), "ChimerebosInvertedOpaque"));
        //            int opaqueInvertedChimeraShaderID = GameShaders.Armor.GetShaderIdFromItemId(ItemType<Opaque_Inverted_Chimera_Dye>());

        //            Filters.Scene["EpikV2:LSD"] = new Filter(new ScreenShaderData(new Ref<Effect>(mod.Assets.Request<Effect>("Effects/LSD", AssetRequestMode.ImmediateLoad).Value), EpikClientConfig.Instance.reduceJitter ? "LessD" : "LSD"), EffectPriority.High);

        //            InvalidArmorShaders = new List<InvalidArmorShader> {
        //                new InvalidArmorShader(EpikV2.starlightShaderID, EpikV2.dimStarlightShaderID),
        //                new InvalidArmorShader(EpikV2.brightStarlightShaderID, EpikV2.dimStarlightShaderID),
        //                new InvalidArmorShader(EpikV2.chimeraShaderID, EpikV2.opaqueChimeraShaderID),
        //                new InvalidArmorShader(invertedChimeraShaderID, opaqueInvertedChimeraShaderID)
        //            };

        //        }
        //        public Effect DyeHardShaders;
        //        public ArmorShaderData jadeDyeShader;
        //        public ArmorShaderData fireDyeShader;
        //        public MiscShaderData fireMiscShader;
        //        public ArmorShaderData starlightShader;
        //        public ArmorShaderData dimStarlightShader;
        //        public ArmorShaderData brightStarlightShader;
        //        public ArmorShaderData nebulaShader;
        //        public ArmorShaderData retroShader;
        //        public ArmorShaderData retroShaderRed;
        //        public ArmorShaderData distortMiscShader;
        //        public ArmorShaderData laserBowOverlayShader;
        //        public ArmorShaderData chimeraShader;
        //        public ArmorShaderData opaqueChimeraShader;
        //        public Asset<Texture2D> nebulaDistortionTexture;
        //        public Asset<Texture2D> testDistortionTexture;
        //    }
    }

}