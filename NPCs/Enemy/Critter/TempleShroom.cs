using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class TempleShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Temple Shroom");
		Tooltip.SetDefault("A strange mushroom that seems to have uprooted itself and begun to walk\nVery susceptible to heat");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = 8;
		Item.maxStack = 1;
	}
}
