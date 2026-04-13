using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(EquipType.Head)]
public class AldinHood : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Mage's Hood");
		//Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = ItemRarityID.Cyan;
	}
}
