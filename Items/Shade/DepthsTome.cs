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
		//DisplayName.SetDefault("Nyarlethotep");
		//Tooltip.SetDefault("'Unleash the power of eldritch tentacle magic'");
	}

	public override void SetDefaults()
	{
		Item.damage = 65;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 10;
		Item.width = 28;
		Item.crit = 10;
		Item.height = 30;
		Item.useTime = 5;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 3.5f;
		Item.value = Item.buyPrice(0, 68);
		Item.rare = 7;
		Item.UseSound = SoundID.Item103;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("ShadeTentacle").Type;
		Item.shootSpeed = 14f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
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
		Projectile.NewProjectile(source, position, vector, type, damage, knockback, player.whoAmI, num, num2);
		return false;
	}
}
