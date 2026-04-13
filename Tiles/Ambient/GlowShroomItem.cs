using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomItem : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glow Shroom");
		//Tooltip.SetDefault("A strange eldritch mushroom from an even stranger land\nMight have strange effects if eaten");
	}

	public override void SetDefaults()
	{
		Item.UseSound = SoundID.Item2;
		Item.width = 20;
		Item.height = 30;
		Item.rare = 0;
		Item.maxStack = 99;
		Item.useStyle = 2;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.consumable = true;
		Item.autoReuse = false;
		Item.buffType = Mod.Find<ModBuff>("GlowShroomed").Type;
		Item.buffTime = 3600;
	}
}
