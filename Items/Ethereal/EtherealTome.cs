using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealTome : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Ethereal Spell");
		// ((ModItem)this).Tooltip.SetDefault("Casts a spread of ethereal blasts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 65);
		((ModItem)this).Item.damage = 55;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 13;
		((Entity)(object)((ModItem)this).Item).width = 40;
		((Entity)(object)((ModItem)this).Item).height = 40;
		((ModItem)this).Item.useTime = 25;
		((ModItem)this).Item.useAnimation = 25;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.rare = 9;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("BigEtherealBlast").Type;
		((ModItem)this).Item.shootSpeed = 36f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		float num = 4f;
		float num2 = MathHelper.ToRadians(12f);
		position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
		for (int i = 0; (float)i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
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
}
