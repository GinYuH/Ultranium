using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Melee;

public class DukeYoyo : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("The Duke's Throw");
		// ((ModItem)this).Tooltip.SetDefault("Fires homing typhoons in random directions");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.useStyle = 5;
		((Entity)(object)((ModItem)this).Item).width = 24;
		((Entity)(object)((ModItem)this).Item).height = 24;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.channel = true;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DukeThrow").Type;
		((ModItem)this).Item.shootSpeed = 16f;
		((ModItem)this).Item.knockBack = 2.5f;
		((ModItem)this).Item.damage = 80;
		((ModItem)this).Item.value = Item.buyPrice(0, 60);
		((ModItem)this).Item.rare = 8;
	}
}
