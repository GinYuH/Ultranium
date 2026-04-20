using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Ice;

namespace Ultranium.Items.BossBags;

public class IceDragonBag : ModItem
{

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Treasure Bag");
		//Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
		ItemID.Sets.BossBag[Type] = true;
		ItemID.Sets.PreHardmodeLikeBossBag[Type] = true;
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
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("IcePelt").Type, 1, 15, 21));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("IceTalon").Type));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<GlacialFlail>(), ModContent.ItemType<GlacialGun>(), ModContent.ItemType<GlacialWand>() }));
    }
}
