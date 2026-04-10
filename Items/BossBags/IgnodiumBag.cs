using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Guardians.Hell;

namespace Ultranium.Items.BossBags;

public class IgnodiumBag : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Treasure Bag");
		// Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
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
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("HellShard").Type, 1, 30, 39));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("IgnodiumRelic").Type));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<HellFlail>(), ModContent.ItemType<HellThrow>(), ModContent.ItemType<HellGun>(), ModContent.ItemType<HellJavelin>(), ModContent.ItemType<HellStaff>(), ModContent.ItemType<HellTome>(), ModContent.ItemType<HellScepter>() }));
    }
}
