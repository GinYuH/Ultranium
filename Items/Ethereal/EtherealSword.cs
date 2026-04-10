using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ethereal;

public class EtherealSword : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Ethereal Buster");
		((ModItem)this).Tooltip.SetDefault("Fires ethereal fire blasts");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 75;
		((ModItem)this).item.melee = true;
		((Entity)(object)((ModItem)this).item).width = 80;
		((Entity)(object)((ModItem)this).item).height = 80;
		((ModItem)this).item.useTime = 22;
		((ModItem)this).item.useAnimation = 44;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.knockBack = 6f;
		((ModItem)this).item.rare = 9;
		((ModItem)this).item.value = Item.buyPrice(0, 30);
		((ModItem)this).item.UseSound = SoundID.Item1;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EtherealFlame");
		((ModItem)this).item.shootSpeed = 6.5f;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		Vector2 vector = Vector2.Normalize(new Vector2(speedX, speedY)) * 100f;
		if (Collision.CanHit(position, 0, 0, position + vector, 0, 0))
		{
			position += vector;
		}
		int num = Main.rand.Next(3, 4);
		for (int i = 0; i < num; i++)
		{
			Projectile.NewProjectile(position, new Vector2(speedX, speedY).RotatedByRandom(0.19634954631328583), type, damage, knockBack, player.whoAmI, 0f, 0f);
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
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "XenanisFlesh", 10);
		val.AddIngredient((Mod)null, "ShadowFlame", 5);
		val.AddTile(134);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
