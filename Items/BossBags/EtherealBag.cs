using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Developer;
using Ultranium.Items.Ethereal;
using Ultranium.Items.Pets;

namespace Ultranium.Items.BossBags;

public class EtherealBag : ModItem
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
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("XenanisFlesh").Type, 1, 15, 24));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EtherealCore").Type));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("EtherealDidgeridoo").Type, 8));
        itemLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("XenanisWings").Type, 15));
        itemLoot.Add(ItemDropRule.OneFromOptions(1, new int[] { ModContent.ItemType<EtherealSword>(), ModContent.ItemType<EtherealBow>(), ModContent.ItemType<EtherealTome>(), ModContent.ItemType<EtherealSummon>() }));
    }
}
