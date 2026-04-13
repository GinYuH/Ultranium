using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags.Acc;

[AutoloadEquip(EquipType.Wings)]
public class CosmicWings : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Wings");
		//Tooltip.SetDefault("Gives infinite flight time and very fast flight speed");
		ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(999999, 10f, 5.5f);
    }

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.value = Item.buyPrice(1, 20);
		Item.rare = ItemRarityID.Purple;
		Item.accessory = true;
		Item.expert = true;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.85f;
		ascentWhenRising = 0.15f;
		maxCanAscendMultiplier = 1.1f;
		maxAscentMultiplier = 3f;
		constantAscend = 0.095f;
	}
}
