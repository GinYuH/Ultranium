using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Dread.TrueDread;

namespace Ultranium.Items.BossBags;

public class TrueDreadBag : ModItem
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
		Item.width = 36;
		Item.height = 34;
		Item.rare = -12;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("NightmareFuel").Type, 1, 30, 44));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("TrueDreadHeart").Type));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<DreadSpear>(), ModContent.ItemType<DreadYoyo>(), ModContent.ItemType<DreadDisc>(), ModContent.ItemType<DreadFlameBlaster>(), ModContent.ItemType<FearStaff>(), ModContent.ItemType<DreadTome>(), ModContent.ItemType<DreadScepter>() }));
    }
}
