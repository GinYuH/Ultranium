using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class Midnight : ModItem
{
	public override void SetStaticDefaults()
	{
		Tooltip.SetDefault("Turns normal bullets into midnight blasts");
		DisplayName.SetDefault("Midnight");
	}

	public override void SetDefaults()
	{
		Item.value = Item.buyPrice(0, 1);
		Item.damage = 13;
		Item.DamageType = DamageClass.Ranged;
		((Entity)(object)Item).width = 58;
		((Entity)(object)Item).height = 26;
		Item.useTime = 12;
		Item.useAnimation = 12;
		Item.useStyle = 5;
		Item.knockBack = 6f;
		Item.rare = 3;
		Item.UseSound = SoundID.Item40;
		Item.autoReuse = true;
		Item.shoot = 242;
		Item.shootSpeed = 2f;
		Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(4f));
		velocity.X = vector.X;
		velocity.Y = vector.Y;
		if (type == 14)
		{
			type = Mod.Find<ModProjectile>("MidnightPro").Type;
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
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(117, 10);
		val.AddIngredient((Mod)null, "ShadowEssence", 15);
		val.AddTile(16);
		val.Register();
	}
}
