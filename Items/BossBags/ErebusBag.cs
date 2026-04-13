using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Cosmic;
using Ultranium.Items.Eldritch;

namespace Ultranium.Items.BossBags;

public class ErebusBag : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Treasure Bag");
		//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		ItemID.Sets.BossBag[Type] = true;
    }

	public override void SetDefaults()
	{
		Item.maxStack = 999;
		Item.consumable = true;
		Item.width = 24;
		Item.height = 24;
		Item.rare = -12;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("NightmareScale").Type, 1, 30, 49));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DarkMatter").Type, 1, 20, 29));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ShadowHeart").Type));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("ErebusGuitar").Type, 20));
        itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<Noctis>(), ModContent.ItemType<SolibusOrba>(), ModContent.ItemType<Exitium>(), ModContent.ItemType<Crepus>(), ModContent.ItemType<Inanis>(), ModContent.ItemType<CavumNigrum>(), ModContent.ItemType<Umbra>(), ModContent.ItemType<Nihil>(), ModContent.ItemType<Caliginus>() }));
    }
}
