using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).DisplayName.SetDefault("Eldritch Pumpkin Staff");
		((ModItem)this).Tooltip.SetDefault("Casts a spread of pumpkin fire");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 40;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 15;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 10, 50);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.UseSound = SoundID.DD2_BetsysWrathShot;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchPumpkinFire");
		((ModItem)this).item.shootSpeed = 8f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "PumpkinStaff", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
