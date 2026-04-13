using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class DukeYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("The Duke's Throw");
		//Tooltip.SetDefault("Fires homing typhoons in random directions");
	}

	public override void SetDefaults()
	{
		Item.useStyle = 5;
		Item.width = 24;
		Item.height = 24;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noMelee = true;
		Item.channel = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 25;
		Item.useTime = 25;
		Item.shoot = Mod.Find<ModProjectile>("DukeThrow").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 80;
		Item.value = Item.buyPrice(0, 60);
		Item.rare = 8;
	}
}
