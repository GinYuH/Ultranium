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
		// ((ModItem)this).Tooltip.SetDefault("Turns normal bullets into midnight blasts");
		// ((ModItem)this).DisplayName.SetDefault("Midnight");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.value = Item.buyPrice(0, 1);
		((ModItem)this).Item.damage = 13;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.useTime = 12;
		((ModItem)this).Item.useAnimation = 12;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.UseSound = SoundID.Item40;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = 242;
		((ModItem)this).Item.shootSpeed = 2f;
		((ModItem)this).Item.useAmmo = AmmoID.Bullet;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4f));
		speedX = vector.X;
		speedY = vector.Y;
		if (type == 14)
		{
			type = ((ModItem)this).Mod.Find<ModProjectile>("MidnightPro").Type;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(117, 10);
		val.AddIngredient((Mod)null, "ShadowEssence", 15);
		val.AddTile(16);
		val.Register();
	}
}
