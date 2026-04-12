using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Vanity.Futaba;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class FutabaBody : ModItem
{
	public override void SetStaticDefaults()
	{
		SetStaticDefaults();
		DisplayName.SetDefault("Futaba Body");
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
