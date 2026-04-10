using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin.Eldritch;

public class EldritchPumpkinBlade : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Eldritch Pumpkin Buster");
		((ModItem)this).Tooltip.SetDefault("Fires flaming eldritch seeds\nHas a chance to shoot out a spread of eldritch bolts that deal twice the sword's damage");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 55;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 35;
		((ModItem)this).item.useAnimation = 35;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.value = Item.buyPrice(0, 10, 50);
		((ModItem)this).item.rare = 4;
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EldritchSeed");
		((ModItem)this).item.shootSpeed = 6.5f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		if (Main.rand.Next(7) == 0)
		{
			float num = 5f;
			float num2 = MathHelper.ToRadians(25f);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; (float)i < num; i++)
			{
				Vector2 vector = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(0f - num2, num2, (float)i / (num - 1f))) * 0.2f;
				Projectile.NewProjectile(position.X, position.Y, vector.X * 10f, vector.Y * 10f, ((ModItem)this).mod.ProjectileType("EldritchPumpkinTentacle"), damage * 2, knockBack, player.whoAmI, 0f, 0f);
			}
			return false;
		}
		return true;
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
		val.AddIngredient((Mod)null, "PumpkinBlade", 1);
		val.AddIngredient((Mod)null, "ShadowEssence", 20);
		val.AddIngredient(521, 10);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
