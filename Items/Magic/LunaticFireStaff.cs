using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticFireStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Solar Flare Staff");
		// Tooltip.SetDefault("Shoots solar flare bolts");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 80;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 22;
		Item.width = 58;
		Item.height = 56;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(1);
		Item.rare = 8;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("CultistFire").Type;
		Item.shootSpeed = 10f;
	}
}
