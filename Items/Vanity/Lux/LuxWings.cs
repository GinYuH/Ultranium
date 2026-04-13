using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Lux;

[AutoloadEquip(EquipType.Wings)]
public class LuxWings : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Lux's Wings");
		//Tooltip.SetDefault("Allows flight and slow fall\n~Developer item~");
		ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new Terraria.DataStructures.WingStats(150, 7f, 2f);
    }

	public override void SetDefaults()
	{
		Item.width = 34;
		Item.height = 26;
		Item.value = Item.buyPrice(1, 20);
		Item.rare = 9;
		Item.accessory = true;
	}

	public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
	{
		ascentWhenFalling = 0.75f;
		ascentWhenRising = 0.11f;
		maxCanAscendMultiplier = 1f;
		maxAscentMultiplier = 2.6f;
		constantAscend = 0.135f;
	}
}
