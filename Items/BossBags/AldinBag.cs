using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Cosmic;
using Ultranium.Items.Developer;
using Ultranium.Items.Pets;
using Ultranium.Items.Vanity.Aldin;

namespace Ultranium.Items.BossBags;

public class AldinBag : ModItem
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
		Item.rare = -12;
		Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}


    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("CosmicWings").Type));
		itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("CosmicIdol").Type, 10));
		itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<CosmicBlade>(), ModContent.ItemType<CosmicBow>(), ModContent.ItemType<CosmicStaff>() }));
        itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<AldinHood>(), ModContent.ItemType<AldinBody>(), ModContent.ItemType<AldinRobe>() }));
        itemLoot.Add(ItemDropRule.OneFromOptions(20, new int[] { ModContent.ItemType<RayGun>(), ModContent.ItemType<DevotedKatana>(), ModContent.ItemType<ShadowFlute>(), ModContent.ItemType<DemonicSingularity>(), ModContent.ItemType<Necromicon>() }));

    }
}
