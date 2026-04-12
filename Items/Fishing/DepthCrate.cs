using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Ultranium.Items.Shade;

namespace Ultranium.Items.Fishing;

public class DepthCrate : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Depths Crate");
		Tooltip.SetDefault("Right click to open");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 20;
		Item.rare = 7;
		Item.useStyle = 1;
		Item.createTile = Mod.Find<ModTile>("DepthCrateTile").Type;
		Item.maxStack = 999;
		Item.autoReuse = true;
		Item.useAnimation = 15;
		Item.useTime = 10;
		Item.consumable = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DepthGlowstoneItem").Type, 3, 3, 4).OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<NightmareBar>(), 2, 3, 4).OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<ShadowEssence>(), 1, 3, 4))));
		itemLoot.Add(ItemDropRule.Common(2674, 3, 3, 4).OnFailedRoll(ItemDropRule.Common(2675, 2, 2, 3).OnFailedRoll(ItemDropRule.Common(2676, 1))));
		itemLoot.Add(ItemDropRule.Common(499, 2, 3, 4).OnFailedRoll(ItemDropRule.Common(500, 1, 3, 4)));
    }
}
