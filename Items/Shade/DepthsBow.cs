using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsBow : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Cxaxukluth");
		((ModItem)this).Tooltip.SetDefault("Has an uncommon chance to create an eldritch tentacle\nThe eldritch tentacle will deal twice the bow's damage");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 65;
		((Entity)(object)((ModItem)this).item).width = 20;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 18;
		((ModItem)this).item.useAnimation = 18;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.ranged = true;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 68);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.UseSound = SoundID.Item5;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = 11;
		((ModItem)this).item.useAmmo = AmmoID.Arrow;
		((ModItem)this).item.shootSpeed = 18f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-3f, 0f);
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(4) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY).SafeNormalize(-Vector2.UnitY);
			Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101)).SafeNormalize(-Vector2.UnitY);
			vector = (vector * 4f + vector2).SafeNormalize(-Vector2.UnitY) * ((ModItem)this).item.shootSpeed;
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
			Projectile.NewProjectile(position, vector, ((ModItem)this).mod.ProjectileType("ShadeTentacle"), ((ModItem)this).item.damage * 2, knockBack, player.whoAmI, num, num2);
			return false;
		}
		return true;
	}
}
