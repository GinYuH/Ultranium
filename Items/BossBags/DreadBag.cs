using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Dread;

namespace Ultranium.Items.BossBags;

public class DreadBag : ModItem
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
		Item.width = 36;
		Item.height = 34;
		Item.rare = ItemRarityID.Expert;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadScale").Type, 1, 10, 15));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadFlame").Type, 1, 12, 31));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadHeart").Type));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<DreadSword>(), ModContent.ItemType<DreadBow>(), ModContent.ItemType<DreadStaff>(), ModContent.ItemType<DreadSummon>() }));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadTooth").Type, 3));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadBreadItem").Type, 15));
    }
}
