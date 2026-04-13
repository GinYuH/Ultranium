using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Guardians.Nature;

namespace Ultranium.Items.BossBags;

public class UltrumBag : ModItem
{

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Treasure Bag");
		//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		Item.maxStack = 999;
		Item.consumable = true;
		Item.width = 46;
		Item.height = 36;
		Item.rare = ItemRarityID.Expert;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("UltrumShard").Type, 1, 30, 39));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("UltrumRelic").Type));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<UltraFlail>(), ModContent.ItemType<UltraniumBow>(), ModContent.ItemType<UltraniumKunai>(), ModContent.ItemType<UltraniumStaff>(), ModContent.ItemType<UltraniumSword>(), ModContent.ItemType<UltraTome>(), ModContent.ItemType<UltraniumScepter>() }));
    }
}
