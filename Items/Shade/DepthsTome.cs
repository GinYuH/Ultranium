using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsTome : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Nyarlethotep");
		// ((ModItem)this).Tooltip.SetDefault("'Unleash the power of eldritch tentacle magic'");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 65;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 10;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((ModItem)this).Item.crit = 10;
		((Entity)(object)((ModItem)this).Item).height = 30;
		((ModItem)this).Item.useTime = 5;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 3.5f;
		((ModItem)this).Item.value = Item.buyPrice(0, 68);
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.UseSound = SoundID.Item103;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("ShadeTentacle").Type;
		((ModItem)this).Item.shootSpeed = 14f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
		Projectile.NewProjectile(position, vector, type, damage, knockBack, player.whoAmI, num, num2);
		return false;
	}
}
