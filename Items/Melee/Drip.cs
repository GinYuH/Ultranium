using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class Drip : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The Dripper");
		//Tooltip.SetDefault("Shoots Water bolts in random directions");
	}

	public override void SetDefaults()
	{
		Item.useStyle = 5;
		Item.width = 30;
		Item.height = 26;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.shoot = Mod.Find<ModProjectile>("DripProjectile").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 23;
		Item.value = Item.buyPrice(0, 20);
		Item.rare = 3;
	}
}
