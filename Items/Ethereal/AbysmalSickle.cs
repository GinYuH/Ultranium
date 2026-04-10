using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class AbysmalSickle : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Abysmal Sickle");
		((ModItem)this).Tooltip.SetDefault("Fires a circle of abyssal scythes");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 65;
		((ModItem)this).item.scale = 0.8f;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 12;
		((Entity)(object)((ModItem)this).item).width = 28;
		((Entity)(object)((ModItem)this).item).height = 32;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.UseSound = SoundID.Item9;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("AbyssSickleInvisible");
		((ModItem)this).item.shootSpeed = 5f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		float num = 3f;
		float num2 = 8f;
		float num3 = MathHelper.ToRadians(360f);
		int num4 = -1;
		for (int i = 0; (float)i < num2; i++)
		{
			Vector2 vector = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num3, num3, (float)i / (num2 - 1f))) * num;
			Projectile.NewProjectile(player.Center.X, player.Center.Y, vector.X, vector.Y, ((ModItem)this).mod.ProjectileType("AbyssSickle"), 55, 2f, Main.myPlayer, (float)num4, 0f);
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(272, 1);
		val.AddIngredient((Mod)null, "XenanisFlesh", 5);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
