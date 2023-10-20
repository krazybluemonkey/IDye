using Terraria;
using Terraria.UI;
using Terraria.GameInput;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.Audio;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.Localization;
using Terraria.Initializers;
using IDye.UI.Elements;

namespace IDye.UI.Systems
{
    class IDyeUISystem : ModSystem
    {
        internal UserInterface iDyeInterface;
        internal IDyeUI iDyeUI;
        private GameTime _lastUpdateUiGameTime;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                iDyeInterface = new UserInterface();

                iDyeUI = new IDyeUI();
                iDyeUI.Activate(); // Activate calls Initialize() on the UIState if not initialized and calls OnActivate, then calls Activate on every child element.
            }
        }

        internal void ShowIDyeUI()
        {
            SoundEngine.PlaySound(SoundID.MenuOpen);
            iDyeInterface?.SetState(iDyeUI);
        }

        internal void HideIDyeUI()
        {
            SoundEngine.PlaySound(SoundID.MenuClose);
            iDyeInterface?.SetState(null);
        }

        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (iDyeInterface?.CurrentState != null)
            {
                iDyeInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "DyeHard: iDyeInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && iDyeInterface?.CurrentState != null)
                        {
                            iDyeInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI));
            }
        }

        public override void Unload()
        {
            if (!Main.dedServ)
            {
                //IDyeUI?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
                iDyeUI = null;
            }
        }
    }
}