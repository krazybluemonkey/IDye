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
using IDye.UI.Systems;
using Terraria.Graphics.Shaders;
using System.Linq;
using Terraria.ModLoader.Config.UI;
using System.Net.Mail;
using Terraria.IO;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.States;
using Terraria.ModLoader.UI;
using Terraria.UI.Gamepad;

namespace IDye.UI
{
    class IDyeUI : UIState
    {
        private UIText primaryColorBarRText;
        private UIText primaryColorBarGText;
        private VanillaItemSlotWrapper _researchSlot;
        internal IDyeUISystem interfaceRef;
        private Vector3 _tempPrimaryColor = new Vector3(0.5f, 0.5f, 0.5f);
        private UIList _dyeList;
        private UIScrollbar _dyeListScrollBar;
        private Player tempPlayer = new Player();
        private CustomUIText selectedDye = new CustomUIText("");
        private enum SliderIds
        {
            PrimaryR,
            PrimaryG,
            PrimaryB,
            Default
        }

        public override void OnInitialize()
        {
            // invisible UI container
            UIElement UIContainer = new UIElement();
            UIContainer.Width.Set(600, 0);
            UIContainer.Height.Set(675, 0);
            UIContainer.VAlign = 0.5f;
            UIContainer.HAlign = 0.5f;
            Append(UIContainer);

            // main BG
            UIPanel BG = new UIPanel();
            BG.Width.Set(600, 0);
            BG.Height.Set(500, 0);
            BG.BackgroundColor = new Color(33, 15, 91, 255) * 0.685f;
            BG.HAlign = 0.5f;
            BG.VAlign = 0.5f;
            UIContainer.Append(BG);

            // main panel (holds color sliders)
            UIPanel mainPanel = new UIPanel();
            mainPanel.Width.Set(590, 0);
            mainPanel.Height.Set(200, 0);
            mainPanel.HAlign = 0.5f;
            mainPanel.VAlign = 1f;
            BG.Append(mainPanel);

            // WIP dye reserch slot container 
            _researchSlot = new VanillaItemSlotWrapper(ItemSlot.Context.ChestItem, ItemSlot.Context.DisplayDollDye, 0.85f) // taken from example mod, ExamplePersonUI.cs in \Old\UI\
            {
                Left = { Pixels = 0 },
                Top = { Pixels = 90 },
                ValidItemFunc = item => item.IsAir || !item.IsAir && !(item.dye == 0), //&& !(item.ModItem is not null)
                HoverText = "Insert dyes to research"
            };

            //_researchItem[0] = new Item();
            //UIItemSlot researchSlot = new UIItemSlot(_researchItem, 0, ItemSlot.Context.ChestItem);
            //researchSlot.VAlign = 0.5f;
            //researchSlot.HAlign = 0.5f;
            //researchSlot.Width = new StyleDimension(85f, 0f);
            //researchSlot.Height = new StyleDimension(85f, 0f);

            mainPanel.Append(_researchSlot);

            // dye preview 
            UICharacter uiPlayer = new UICharacter(tempPlayer, false, false, 3f);
            uiPlayer.HAlign = 1f;
            uiPlayer.VAlign = 0f;
            mainPanel.Append(uiPlayer);

            // apply text
            UIText apply = new UIText("Apply", 0.8f, true);
            apply.Width.Set(100, 0);
            apply.Height.Set(50, 0);
            apply.HAlign = 0.5f;
            apply.Top.Set(150, 0);
            apply.TextColor = Color.Gray;
            apply.OnMouseOver += onTextHover;
            apply.OnMouseOut += onTextFade;
            apply.OnLeftClick += OnApplyClick;
            mainPanel.Append(apply);

            // R Bar -
            UIText barRMinus = new UIText("-", 1.4f);
            barRMinus.HAlign = 0f;
            barRMinus.VAlign = 0.04f;
            barRMinus.TextColor = Color.Gray;
            mainPanel.Append(barRMinus);

            // primary R bar (taken from Terraria.GameContent.UI.States.UICharacterCreation)
            UIElement primaryColorBarR = new UIColoredSlider(LocalizedText.Empty, () => _tempPrimaryColor.X, delegate (float x)
            {
                UpdateHSLValue(SliderIds.PrimaryR, x);
            }, UpdateSlider_PrimaryR, (float x) => Color.Lerp(new Color(-10f, 0f, 0f), new Color(10f, 0f, 0f), x), Color.Transparent);
            primaryColorBarR.VAlign = 0f;
            primaryColorBarR.HAlign = 0f;
            primaryColorBarR.Width = StyleDimension.FromPixelsAndPercent(-10f, 0.4f);
            primaryColorBarR.SetSnapPoint("Middle", (int)SliderIds.PrimaryR, (Vector2?)null, (Vector2?)new Vector2(0f, 20f));
            mainPanel.Append(primaryColorBarR);

            // R bar number 
            primaryColorBarRText = new UIText(Math.Round(-10 - (-20 * _tempPrimaryColor.X), 2).ToString());
            primaryColorBarRText.HAlign = 0.45f;
            primaryColorBarRText.VAlign = 0.05f;
            mainPanel.Append(primaryColorBarRText);

            // R Bar +
            UIText barRPlus = new CustomUIText("+", 1.4f);
            barRPlus.HAlign = 0.38f;
            barRPlus.VAlign = 0.04f;
            barRPlus.OnMouseOver += onTextHover;
            barRPlus.OnMouseOut += onTextFade;
            barRPlus.OnLeftMouseDown += onPlusRClick;
            barRPlus.TextColor = Color.Gray;
            mainPanel.Append(barRPlus);

            // primary G bar (smae as primaryColorBarR)
            UIElement primaryColorBarG = new UIColoredSlider(LocalizedText.Empty, () => _tempPrimaryColor.Y, delegate (float x)
            {
                UpdateHSLValue(SliderIds.PrimaryG, x);
            }, UpdateSlider_PrimaryG, (float x) => Color.Lerp(new Color(0f, -10f, 0f), new Color(0f, 10f, 0f), x), Color.Transparent);
            primaryColorBarG.VAlign = 0.15f;
            primaryColorBarG.HAlign = -0.05f;
            primaryColorBarG.Width = StyleDimension.FromPixelsAndPercent(-10f, 0.4f);
            primaryColorBarG.SetSnapPoint("Middle", (int)SliderIds.PrimaryG, (Vector2?)null, (Vector2?)new Vector2(0f, 20f));
            mainPanel.Append(primaryColorBarG);

            // G bar number 
            primaryColorBarGText = new UIText(Math.Round(-10 - (-20 * _tempPrimaryColor.Y), 2).ToString());
            primaryColorBarGText.HAlign = 0.4f;
            primaryColorBarGText.VAlign = 0.23f; 
            mainPanel.Append(primaryColorBarGText);

            // dye list scroll area
            UIPanel dyeListPanel = new UIPanel();
            dyeListPanel.Width.Set(220, 0);
            dyeListPanel.Height.Set(260, 0);
            dyeListPanel.HAlign = 0f;
            dyeListPanel.VAlign = 0f;
            BG.Append(dyeListPanel);

            // dye list
            _dyeList = new UIList();
            _dyeList.HAlign = 0f;
            _dyeList.VAlign = 0f;
            _dyeList.Width.Set(0f, 1f);
            _dyeList.Height.Set(0f, 1f);
            _dyeList.ListPadding = 6f;
            dyeListPanel.Append(_dyeList);

            //dye list scroll bar
            _dyeListScrollBar = new UIScrollbar();
            _dyeListScrollBar.SetView(100f, 1000f);
            _dyeListScrollBar.Height.Set(0f, 1f);
            _dyeListScrollBar.HAlign = 1.05f;
            _dyeList.SetScrollbar(_dyeListScrollBar);
            dyeListPanel.Append(_dyeListScrollBar);

            //exit button (taken from Terraria.GameContent.UI.States.UIBestiaryTest)
            UITextPanel<LocalizedText> ExitButton = new UITextPanel<LocalizedText>(Language.GetText("UI.Back"), 0.7f, large: true)
            {
                Width = StyleDimension.FromPixelsAndPercent(-10f, 0.5f),
                Height = StyleDimension.FromPixels(50f),
                VAlign = 1f,
                HAlign = 0.5f,
                Top = StyleDimension.FromPixels(-25f)
            };
            ExitButton.OnMouseOver += ExitFadedMouseOver;
            ExitButton.OnMouseOut += ExitFadedMouseOut;
            ExitButton.OnLeftMouseDown += OnExitButtonClick;
            ExitButton.SetSnapPoint("ExitButton", 0);
            UIContainer.Append(ExitButton);
        }
        private void OnApplyClick(UIMouseEvent evt, UIElement listeningElement)
        {
            // currently only tests functionality
            var iDyePlayer = Main.LocalPlayer.GetModPlayer<IDyePlayer>();
            iDyePlayer.iDyePrimaryColors[1] = new Microsoft.Xna.Framework.Color(0f, 0f, 1f);
            iDyePlayer.iDyePasses[1] = "ArmorLivingRainbow";
            Main.NewText($"primary color: r{iDyePlayer.iDyePrimaryColors[1].R}, g{iDyePlayer.iDyePrimaryColors[1].G}, b{iDyePlayer.iDyePrimaryColors[1].B}");
            Main.NewText($"saturation: {iDyePlayer.iDyeSaturation[1]}f");
            Main.NewText($"pass: {iDyePlayer.iDyePasses[1]}");
            SoundEngine.PlaySound(SoundID.MenuTick);
        }
        private void OnDyeListClick(UIMouseEvent evt, UIElement listeningElement)
        {
            // idea here would be copy over the items' defaults (colors, sat, ect) to the player/set the players' "iDyeItemID" to this item
            // for use in IDyeArmorShaderData.
            // thing is, this isn't possible yet

            selectedDye.TextColor = Color.Gray;
            SoundEngine.PlaySound(SoundID.MenuTick);
            selectedDye = ((CustomUIText)evt.Target);
            selectedDye.TextColor = Color.Yellow;
            
            // various reasearch bits
            //var iDyePlayer = Main.LocalPlayer.GetModPlayer<IDyePlayer>();
            //iDyePlayer.iDyePrimaryColors[1] = new Microsoft.Xna.Framework.Color(0f, 0f, 1f);
            //iDyePlayer.iDyePasses[1] = "ArmorLivingRainbow";
            //Main.NewText($"primary color: r{iDyePlayer.iDyePrimaryColors[1].R}, g{iDyePlayer.iDyePrimaryColors[1].G}, b{iDyePlayer.iDyePrimaryColors[1].B}");
            //Main.NewText($"saturation: {iDyePlayer.iDyeSaturation[1]}f");
            //Main.NewText($"pass: {iDyePlayer.iDyePasses[1]}");
            //Main.NewText($"e: {GameShaders.Armor.GetShaderFromItemId(selectedDye.ItemID)}");
            //IDyePlayer.passes[1] = "ArmorLivingRainbow";
            //Main.NewText($"pass: {IDyePlayer.passes[1]}");
        }
        private void OnExitButtonClick(UIMouseEvent evt, UIElement listeningElement)
        {
            interfaceRef = ModContent.GetInstance<IDyeUISystem>();
            SoundEngine.PlaySound(SoundID.MenuClose);
            if (Main.gameMenu)
            {
                Main.menuMode = 0;
            }
            else
            {
                interfaceRef.HideIDyeUI();
            }
        }
        private void ExitFadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ((UIPanel)evt.Target).BackgroundColor = new Color(73, 94, 171);
            ((UIPanel)evt.Target).BorderColor = Colors.FancyUIFatButtonMouseOver;
        }
        private void DyeListMouseOver(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            if (((CustomUIText)evt.Target) != selectedDye)
            {
                ((CustomUIText)evt.Target).TextColor = Color.White;
            }
        }
        private void DyeListMouseFade(UIMouseEvent evt, UIElement listeningElement)
        {
            if (((CustomUIText)evt.Target) != selectedDye)
            {
                ((CustomUIText)evt.Target).TextColor = Color.Gray;
            }
        }

        private void ExitFadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
        {
            ((UIPanel)evt.Target).BackgroundColor = new Color(63, 82, 151) * 0.8f;
            ((UIPanel)evt.Target).BorderColor = Color.Black;
        }
        private void onPlusRClick(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            _tempPrimaryColor.X += 0.0005f;
            UpdateHSLValue(SliderIds.PrimaryR, _tempPrimaryColor.X);
        }
        private void onTextHover(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(SoundID.MenuTick);
            ((UIText)evt.Target).TextColor = Color.White;
        }
        private void onTextFade(UIMouseEvent evt, UIElement listeningElement)
        {
            ((UIText)evt.Target).TextColor = Color.Gray;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            interfaceRef = ModContent.GetInstance<IDyeUISystem>();
            if (interfaceRef.iDyeInterface.CurrentState == interfaceRef.iDyeUI)
            {
                Main.LocalPlayer.releaseInventory = false;
            }

            if (Main.LocalPlayer.controlInv && interfaceRef.iDyeInterface.CurrentState == interfaceRef.iDyeUI)
            {
                interfaceRef.HideIDyeUI();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //seems to need to be set constantly to actually work
            tempPlayer.direction = -1;
            tempPlayer.gravDir = 1f;
            tempPlayer.PlayerFrame();
            tempPlayer.socialIgnoreLight = true;
            base.Draw(spriteBatch);
        }

        public override void OnActivate()
        {
            // likely going to fill slots with dyes from default slot (0) later
            tempPlayer.armor = Main.LocalPlayer.armor;
            tempPlayer.dye = Main.LocalPlayer.dye;
            //var iDyeTempPlayer = _tempPlayer.GetModPlayer<IDyePlayer>();
            //if (iDyeTempPlayer != null)
            //{
            //    iDyeTempPlayer.iDyePasses[1] = "ColorOnly";
            //}

            UpdateDyesList();
        }
        public override void OnDeactivate()
        {
            if (_researchSlot.Item.IsAir)
            {
                return;
            }

            // allows items to be returned without pickup text 
            GetItemSettings getItemInDropItemCheck = GetItemSettings.GetItemInDropItemCheck;
            Item oldItem = Main.LocalPlayer.GetItem(Main.LocalPlayer.whoAmI, _researchSlot.Item, getItemInDropItemCheck);
            Item newItem = Main.LocalPlayer.QuickSpawnClonedItemDirect(Main.LocalPlayer.GetSource_Misc("PlayerDropItemCheck"), oldItem, oldItem.stack);
            newItem.newAndShiny = false;


            _researchSlot.Item = new Item();
        }

        private void UpdateDyesList()
        {
            _dyeList.Clear();

            // until research is done, going to just fill the list with all dye items.
            // nothing here does much yet though
            List<CustomUIText> TempList = new List<CustomUIText>();
            for (int i = 0; i < ItemLoader.ItemCount; i++)
            {
                var item = new Item();
                if (i < ContentSamples.ItemsByType.Count)
                {
                    item = ContentSamples.ItemsByType[i];
                }
                if (item != null)
                {
                    if (item.dye != 0) // && item.ModItem is null; was going to check for modded items but may not be feasible
                    {
                        TempList.Add(new CustomUIText("[i:" + item.type + "] " + item.Name, 1.1f).SetItemID(item.type));
                    }
                }  
            }

            // trying to order list alphabetically, no dice as far as i can tell though
            // likely related to the "icons"
            TempList.Sort(delegate (CustomUIText x, CustomUIText y)
            {
                if (x.Text == null && y.Text == null) return 0;
                else if (x.Text == null) return -1;
                else if (y.Text == null) return 1;
                else return x.Text.CompareTo(y.Text);
            });

            for (int i = 0; i < TempList.Count; i++)
            {
                TempList[i].OnLeftClick += OnDyeListClick;
                TempList[i].TextColor = Color.Gray;
                TempList[i].OnMouseOver += DyeListMouseOver;
                TempList[i].OnMouseOut += DyeListMouseFade;
                _dyeList.Add(TempList[i]);
            }

            //_dyeList.UpdateOrder();
        }

        private void UpdateSlider_PrimaryR()
        {
            float value = UILinksInitializer.HandleSliderHorizontalInput(_tempPrimaryColor.X, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.05f);
            UpdateHSLValue(SliderIds.PrimaryR, value);
        }

        private void UpdateSlider_PrimaryG()
        {
            float value = UILinksInitializer.HandleSliderHorizontalInput(_tempPrimaryColor.Y, 0f, 1f, PlayerInput.CurrentProfile.InterfaceDeadzoneX, 0.05f);
            UpdateHSLValue(SliderIds.PrimaryG, value);
        }

        private void UpdateHSLValue(SliderIds id, float value)
        {
            switch (id)
            {
                case SliderIds.PrimaryR:
                    _tempPrimaryColor.X = value;
                    break;
                case SliderIds.PrimaryG:
                    _tempPrimaryColor.Y = value;
                    break;
                case SliderIds.PrimaryB:
                    _tempPrimaryColor.Z = value;
                    break;
            }
            // Math.Round(10.75, 2)
            //Color color = ScaledHslToRgb(_tempPrimaryColor.X, _tempPrimaryColor.Y, _tempPrimaryColor.Z);
            primaryColorBarRText.SetText(Math.Round(-10 - (-20 * _tempPrimaryColor.X), 2).ToString());
            primaryColorBarGText.SetText(Math.Round(-10 - (-20 * _tempPrimaryColor.Y), 2).ToString());
            //ApplyPendingColor(color);
            //_colorPickers[(int)_selectedPicker]?.SetColor(color);
            //if (_selectedPicker == CategoryId.HairColor)
            //{
            //    _hairStylesCategoryButton.SetColor(color);
            //}
            //UpdateHexText(color);
        }
    }
}