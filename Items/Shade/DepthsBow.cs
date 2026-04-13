using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsBow : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Cxaxukluth");
		//Tooltip.SetDefault("Has an uncommon chance to create an eldritch tentacle\nThe eldritch tentacle will deal twice the bow's damage");
	}

	public override void SetDefaults()
	{
		Item.damage = 65;
		Item.width = 20;
		Item.height = 40;
		Item.useTime = 18;
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Ranged;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 68);
		Item.rare = ItemRarityID.Lime;
		Item.UseSound = SoundID.Item5;
		Item.autoReuse = true;
		Item.shoot = ProjectileID.VilePowder;
		Item.useAmmo = AmmoID.Arrow;
		Item.shootSpeed = 18f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(4) == 0)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).SafeNormalize(-Vector2.UnitY);
			Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
			vector = (vector * 4f + vector2).SafeNormalize(-Vector2.UnitY) * Item.shootSpeed;
			float num = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num *= -1f;
			}
			float num2 = (float)Main.rand.Next(10, 80) * 0.001f;
			if (Main.rand.Next(2) == 0)
			{
				num2 *= -1f;
			}
			Projectile.NewProjectile(source, position, vector, Mod.Find<ModProjectile>("ShadeTentacle").Type, Item.damage * 2, knockback, player.whoAmI, num, num2);
			return false;
		}
		return true;
	}
}
