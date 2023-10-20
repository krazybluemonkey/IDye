using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Shaders;
using Terraria.GameContent.Creative;
using IDye.UI.Systems;
using IDye.Common.Shaders;

namespace IDye.Content.Items.Dyes.Intelligent
{
    public class IntelligentDyeSlot1 : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(
                    Item.type,
                    new IDyeArmorShaderData(Main.PixelShaderRef, "ColorOnly")
                ).UseDyeSlot(1);
            }

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.HiddenAnimation;
            Item.useTime = 15;
            Item.useAnimation = 0;
            Item.autoReuse = false;
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 9999;
            Item.value = Item.sellPrice(0, 3, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            Item.dye = Item.dye;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool? UseItem(Player player)
        {
            IDyeUISystem uiRef = ModContent.GetInstance<IDyeUISystem>();
            if (player.altFunctionUse == 2)
            {

            }
            else
            {
                if (uiRef.iDyeInterface.CurrentState != uiRef.iDyeUI)
                {
                    uiRef.ShowIDyeUI();
                }
            }
            return false;
        }
    }
}