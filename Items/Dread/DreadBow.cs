using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread;

public class DreadBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Bow of Distress");
		// Tooltip.SetDefault("Converts arrows into dread bolts");
	}

	public override void SetDefaults()
	{
		Item.damage = 45;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 46;
		Item.height = 18;
		Item.useTime = 25;
		Item.useAnimation = 25;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 4f;
		Item.rare = 4;
		Item.value = Item.buyPrice(0, 12);
		Item.UseSound = SoundID.Item5;
		Item.shoot = 10;
		Item.autoReuse = true;
		Item.shootSpeed = 9f;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		new Vector2(speedX, speedY).RotatedBy(Math.PI / (double)(Main.rand.Next(72, 1800) / 10));
		Projectile.NewProjectile(null, position.X, position.Y, speedX, speedY, Mod.Find<ModProjectile>("DreadBowBolt").Type, damage, knockBack, player.whoAmI, 0f, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "DreadFlame", 10);
		val.AddIngredient((Mod)null, "DreadScale", 5);
		val.AddTile(134);
		val.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, -4f);
	}
}
