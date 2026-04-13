using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Ultranium.Items.Shade;
using Ultranium.Tiles.ShadowBiome.Depths;

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
		IItemDropRule mat = ItemDropRule.Common(ModContent.ItemType<DepthGlowstoneItem>(), 3, 3, 4);
		IItemDropRule mat2 = ItemDropRule.Common(ModContent.ItemType<NightmareBar>(), 2, 3, 4);
		IItemDropRule mat3 = ItemDropRule.Common(ModContent.ItemType<ShadowEssence>(), 1, 3, 4);
		mat2.OnFailedRoll(mat3);
		mat.OnFailedRoll(mat2);
		itemLoot.Add(mat);

        IItemDropRule bait = ItemDropRule.Common(2674, 3, 3, 4);
        IItemDropRule bait2 = ItemDropRule.Common(2675, 2, 2, 3);
        IItemDropRule bait3 = ItemDropRule.Common(2676);
        bait2.OnFailedRoll(bait3);
        bait.OnFailedRoll(bait2);
        itemLoot.Add(bait);

		IItemDropRule potion = ItemDropRule.Common(499, 2, 3, 4);
		potion.OnFailedRoll(ItemDropRule.Common(500, 2, 3, 4));
		itemLoot.Add(potion);
    }
}
