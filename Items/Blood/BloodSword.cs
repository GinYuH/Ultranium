using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodSword : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Bloodthorn Blade");
		((ModItem)this).Tooltip.SetDefault("Shoots a random spread of blood thorns");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.scale = 1.2f;
		((ModItem)this).item.damage = 12;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 1, 35);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.UseSound = SoundID.Item7;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("BloodThorn");
		((ModItem)this).item.shootSpeed = 5f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		if (Utils.NextBool(Main.rand, 2))
		{
			Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 5);
		}
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		int num = 1 + Main.rand.Next(2);
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
