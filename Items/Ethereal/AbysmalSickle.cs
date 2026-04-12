using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class AbysmalSickle : ModItem
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Abysmal Sickle");
		Tooltip.SetDefault("Fires a circle of abyssal scythes");
	}

	public override void SetDefaults()
	{
		Item.damage = 65;
		Item.scale = 0.8f;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 12;
		Item.width = 28;
		Item.height = 32;
		Item.useTime = 35;
		Item.useAnimation = 35;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.rare = 9;
		Item.value = Item.buyPrice(0, 30);
		Item.UseSound = SoundID.Item9;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("AbyssSickleInvisible").Type;
		Item.shootSpeed = 5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 3f;
		float num2 = 8f;
		float num3 = MathHelper.ToRadians(360f);
		int num4 = -1;
		for (int i = 0; (float)i < num2; i++)
		{
			Vector2 vector = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num3, num3, (float)i / (num2 - 1f))) * num;
			Projectile.NewProjectile(null, player.Center.X, player.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("AbyssSickle").Type, 55, 2f, Main.myPlayer, (float)num4, 0f);
		}
		return true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(272, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 5);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.Register();
	}
}
