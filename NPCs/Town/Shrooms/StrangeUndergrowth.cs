using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class StrangeUndergrowth : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Strange Undergrowth");
		// Tooltip.SetDefault("Apperantly an extremely difficult mushroom to grow.\nOnly those with enough perseverance and time could be able to grow them in mass.\nMakes sense the only guy who has a supply of them is dead.");
	}

	public override void SetDefaults()
	{
		Item.width = 20;
		Item.height = 30;
		Item.rare = -11;
		Item.maxStack = 1;
	}
}
