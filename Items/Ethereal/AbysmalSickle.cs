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
		// ((ModItem)this).DisplayName.SetDefault("Abysmal Sickle");
		// ((ModItem)this).Tooltip.SetDefault("Fires a circle of abyssal scythes");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 65;
		((ModItem)this).Item.scale = 0.8f;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 12;
		((Entity)(object)((ModItem)this).Item).width = 28;
		((Entity)(object)((ModItem)this).Item).height = 32;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.rare = 9;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.UseSound = SoundID.Item9;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("AbyssSickleInvisible").Type;
		((ModItem)this).Item.shootSpeed = 5f;
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
			Projectile.NewProjectile(player.Center.X, player.Center.Y, vector.X, vector.Y, ((ModItem)this).Mod.Find<ModProjectile>("AbyssSickle").Type, 55, 2f, Main.myPlayer, (float)num4, 0f);
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(272, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 5);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.Register();
	}
}
