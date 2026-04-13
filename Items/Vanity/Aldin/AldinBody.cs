using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Aldin;

[AutoloadEquip(EquipType.Body)]
public class AldinBody : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cosmic Mage's Body");
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
