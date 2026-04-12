using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Ocean;

namespace Ultranium.Items.BossBags;

public class SquidBag : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Treasure Bag");
		Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		ItemID.Sets.BossBag[Type] = true;
		ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
	}

	public override void SetDefaults()
	{
		Item.maxStack = 999;
		Item.consumable = true;
		Item.width = 46;
		Item.height = 36;
		Item.rare = -12;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("OceanScale").Type, 1, 12, 15));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("MysticTentacle").Type, 1));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<ZephyrBlade>(), ModContent.ItemType<ZephyrTrident>(), ModContent.ItemType<ZephyrKnife>() }));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("WormPet").Type, 20));
    }
}
