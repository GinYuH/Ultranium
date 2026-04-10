using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsTome : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Nyarlethotep");
		((ModItem)this).Tooltip.SetDefault("'Unleash the power of eldritch tentacle magic'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 65;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 10;
		((Entity)(object)((ModItem)this).item).width = 28;
		((ModItem)this).item.crit = 10;
		((Entity)(object)((ModItem)this).item).height = 30;
		((ModItem)this).item.useTime = 5;
		((ModItem)this).item.useAnimation = 20;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 3.5f;
		((ModItem)this).item.value = Item.buyPrice(0, 68);
		((ModItem)this).item.rare = 7;
		((ModItem)this).item.UseSound = SoundID.Item103;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ShadeTentacle");
		((ModItem)this).item.shootSpeed = 14f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
		Projectile.NewProjectile(position, vector, type, damage, knockBack, player.whoAmI, num, num2);
		return false;
	}
}
