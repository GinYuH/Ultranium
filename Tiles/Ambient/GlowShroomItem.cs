using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomItem : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Glow Shroom");
		((ModItem)this).Tooltip.SetDefault("A strange eldritch mushroom from an even stranger land\nMight have strange effects if eaten");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.UseSound = SoundID.Item2;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.rare = 0;
		((ModItem)this).item.maxStack = 99;
		((ModItem)this).item.useStyle = 2;
		((ModItem)this).item.useTime = 20;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.consumable = true;
		((ModItem)this).item.autoReuse = false;
		((ModItem)this).item.buffType = ((ModItem)this).mod.BuffType("GlowShroomed");
		((ModItem)this).item.buffTime = 3600;
	}
}
