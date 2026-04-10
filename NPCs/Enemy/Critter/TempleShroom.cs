using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class TempleShroom : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Temple Shroom");
		// ((ModItem)this).Tooltip.SetDefault("A strange mushroom that seems to have uprooted itself and begun to walk\nVery susceptible to heat");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.rare = 8;
		((ModItem)this).Item.maxStack = 1;
	}
}
