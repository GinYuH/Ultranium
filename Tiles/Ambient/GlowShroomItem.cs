using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Glow Shroom");
		// ((ModItem)this).Tooltip.SetDefault("A strange eldritch mushroom from an even stranger land\nMight have strange effects if eaten");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.UseSound = SoundID.Item2;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.rare = 0;
		((ModItem)this).Item.maxStack = 99;
		((ModItem)this).Item.useStyle = 2;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.consumable = true;
		((ModItem)this).Item.autoReuse = false;
		((ModItem)this).Item.buffType = ((ModItem)this).Mod.Find<ModBuff>("GlowShroomed").Type;
		((ModItem)this).Item.buffTime = 3600;
	}
}
