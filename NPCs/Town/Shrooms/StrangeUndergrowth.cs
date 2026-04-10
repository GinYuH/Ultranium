using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Town.Shrooms;

public class StrangeUndergrowth : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Strange Undergrowth");
		((ModItem)this).Tooltip.SetDefault("Apperantly an extremely difficult mushroom to grow.\nOnly those with enough perseverance and time could be able to grow them in mass.\nMakes sense the only guy who has a supply of them is dead.");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.rare = -11;
		((ModItem)this).item.maxStack = 1;
	}
}
