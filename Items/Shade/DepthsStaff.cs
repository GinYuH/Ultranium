using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade;

public class DepthsStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Ny-Rakath");
		//Tooltip.SetDefault("Summons a shade demon that shoots demon scythes at nearby enemies");
	}

	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Summon;
		Item.mana = 20;
		Item.damage = 65;
		Item.width = 42;
		Item.height = 42;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.noMelee = true;
		Item.knockBack = 0f;
		Item.value = Item.buyPrice(0, 68);
		Item.rare = ItemRarityID.Lime;
		Item.UseSound = SoundID.Item44;
		Item.shoot = Mod.Find<ModProjectile>("DemonMinion").Type;
		Item.shootSpeed = 10f;
		Item.buffType = Mod.Find<ModBuff>("DemonBuff").Type;
		Item.buffTime = 3600;
	}
}
