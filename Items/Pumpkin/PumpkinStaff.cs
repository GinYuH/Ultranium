using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class PumpkinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[((ModItem)this).item.type] = true;
		((ModItem)this).DisplayName.SetDefault("Pumpkin Staff");
		((ModItem)this).Tooltip.SetDefault("Casts a small amount of short lived pumpkin seeds");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 10;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 4;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 18;
		((ModItem)this).item.useAnimation = 18;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.knockBack = 2f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item8;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("PumpkinSeed");
		((ModItem)this).item.shootSpeed = 7f;
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient(1725, 10);
		val.AddIngredient(9, 20);
		((Recipe)val).anyWood = true;
		val.AddTile(18);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
