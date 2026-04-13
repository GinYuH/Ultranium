using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class PumpkinStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		Item.staff[Item.type] = true;
		//DisplayName.SetDefault("Pumpkin Staff");
		//Tooltip.SetDefault("Casts a small amount of short lived pumpkin seeds");
	}

	public override void SetDefaults()
	{
		Item.damage = 10;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 4;
		Item.width = 80;
		Item.height = 80;
		Item.useTime = 18;
		Item.useAnimation = 18;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.knockBack = 2f;
		Item.value = Item.buyPrice(0, 0, 50);
		Item.rare = ItemRarityID.Blue;
		Item.UseSound = SoundID.Item8;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("PumpkinSeed").Type;
		Item.shootSpeed = 7f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		int num = 1 + Main.rand.Next(2);
		for (int i = 0; i < num; i++)
		{
			Vector2 vector = new Vector2(velocity.X, velocity.Y).RotatedByRandom(MathHelper.ToRadians(20f));
			float num2 = 1f - Main.rand.NextFloat() * 0.3f;
			vector *= num2;
			Projectile.NewProjectile(source, position.X, position.Y, vector.X, vector.Y, type, damage, knockback, player.whoAmI, 0f, 0f);
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
		Recipe val = Recipe.Create(Type, 1);
		val.AddIngredient(ItemID.Pumpkin, 10);
        val.AddRecipeGroup(RecipeGroupID.Wood, 20);
        val.AddTile(TileID.WorkBenches);
		val.Register();
	}
}
