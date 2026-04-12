using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Pois1x;

[AutoloadEquip(EquipType.Body)]
public class PoisonBody : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Pois1x's Chestmail");
		Tooltip.SetDefault("~Developer item~");
	}

	public override void SetDefaults()
	{
		Item.width = 18;
		Item.height = 18;
		Item.vanity = true;
		Item.rare = 9;
	}
}
