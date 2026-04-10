using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class SolThrow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Sol Throw");
		// Tooltip.SetDefault("Fires solar laser beams at nearby enemies");
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
		Item.shoot = Mod.Find<ModProjectile>("SolProjectile").Type;
		Item.shootSpeed = 16f;
		Item.knockBack = 2.5f;
		Item.damage = 76;
		Item.value = Item.buyPrice(0, 50);
		Item.rare = 8;
	}
}
