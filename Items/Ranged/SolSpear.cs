using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SolSpear : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Throws a fast moving sol spear\nThe spear will explode into short lived sparks when it touches an enemy or tile");
		// DisplayName.SetDefault("Spear of the Sol");
	}

	public override void SetDefaults()
	{
		Item.damage = 60;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 56;
		((Entity)(object)Item).height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 1;
		Item.knockBack = 8f;
		Item.noUseGraphic = true;
		Item.value = Item.buyPrice(0, 50);
		Item.rare = 8;
		Item.UseSound = SoundID.Item60;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("SolSpear").Type;
		Item.shootSpeed = 15f;
	}
}
