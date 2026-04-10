using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[((ModItem)this).Item.type] = true;
		// ((ModItem)this).DisplayName.SetDefault("Eldritch Pumpkin Staff");
		// ((ModItem)this).Tooltip.SetDefault("Casts a spread of pumpkin fire");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 40;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 15;
		((Entity)(object)((ModItem)this).Item).width = 80;
		((Entity)(object)((ModItem)this).Item).height = 80;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 2f;
		((ModItem)this).Item.value = Item.buyPrice(0, 10, 50);
		((ModItem)this).Item.rare = 4;
		((ModItem)this).Item.UseSound = SoundID.DD2_BetsysWrathShot;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("EldritchPumpkinFire").Type;
		((ModItem)this).Item.shootSpeed = 8f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num = 2 + Main.rand.Next(4);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20f));
			float num2 = 1f - Main.rand.NextFloat() * 0.3f;
			vector *= num2;
			Projectile.NewProjectile(position.X, position.Y, vector.X, vector.Y, type, damage, knockBack, player.whoAmI, 0f, 0f);
		}
		return false;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "PumpkinStaff", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.Register();
	}
}
