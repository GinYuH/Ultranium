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
		// ((ModItem)this).DisplayName.SetDefault("Cxaxukluth");
		// ((ModItem)this).Tooltip.SetDefault("Has an uncommon chance to create an eldritch tentacle\nThe eldritch tentacle will deal twice the bow's damage");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 65;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 18;
		((ModItem)this).Item.useAnimation = 18;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 68);
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.UseSound = SoundID.Item5;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = 11;
		((ModItem)this).Item.useAmmo = AmmoID.Arrow;
		((ModItem)this).Item.shootSpeed = 18f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(4) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY).SafeNormalize(-Vector2.UnitY);
			Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
			vector = (vector * 4f + vector2).SafeNormalize(-Vector2.UnitY) * ((ModItem)this).Item.shootSpeed;
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
			Projectile.NewProjectile(position, vector, ((ModItem)this).Mod.Find<ModProjectile>("ShadeTentacle").Type, ((ModItem)this).Item.damage * 2, knockBack, player.whoAmI, num, num2);
			return false;
		}
		return true;
	}
}
