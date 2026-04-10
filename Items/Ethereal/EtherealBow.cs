using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealBow : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Ethereal Bow");
		// Tooltip.SetDefault("Has a 50% chance to shoot out Ethereal Bolts");
	}

	public override void SetDefaults()
	{
		Item.damage = 65;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 46;
		Item.height = 18;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 7f;
		Item.rare = 9;
		Item.value = Item.buyPrice(0, 30);
		Item.UseSound = SoundID.Item5;
		Item.shoot = 10;
		Item.autoReuse = true;
		Item.shootSpeed = 10f;
		Item.useAmmo = AmmoID.Arrow;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (Main.rand.Next(2) == 0)
		{
			Vector2 vector = new Vector2(speedX, speedY);
			Projectile.NewProjectile(null, position.X, position.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("EtherealBolt").Type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.Register();
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-4f, -4f);
	}
}
