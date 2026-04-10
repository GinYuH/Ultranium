using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ice;

public class GlacialGun : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Has a chance to shoot ice bolts");
		// DisplayName.SetDefault("Glacial Pistol");
	}

	public override void SetDefaults()
	{
		Item.damage = 20;
		Item.DamageType = DamageClass.Ranged;
		Item.width = 58;
		Item.height = 26;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 3;
		Item.value = Item.buyPrice(0, 20);
		Item.UseSound = SoundID.Item40;
		Item.autoReuse = true;
		Item.shoot = 242;
		Item.shootSpeed = 12f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		if (type == 14 && Main.rand.Next(5) == 0)
		{
			type = 119;
		}
		return true;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-6f, 0f);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(664, 10);
		val.AddIngredient((Mod)null, "IcePelt", 7);
		val.AddTile(16);
		val.Register();
	}
}
