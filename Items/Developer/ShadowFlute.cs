using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Developer;

public class ShadowFlute : ModItem
{
	private Color[] itemNameCycleColors = new Color[2]
	{
		new Color(34, 166, 162),
		new Color(138, 7, 163)
	};

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Despairides");
		Tooltip.SetDefault("When played, the flute will create a phantom claw that will home in on enemies\n~Developer Item~");
	}

	public override void SetDefaults()
	{
		Item.damage = 250;
		Item.mana = 20;
		Item.noMelee = true;
		Item.DamageType = DamageClass.Magic;
		Item.width = 42;
		Item.height = 58;
		Item.useTime = 21;
		Item.useAnimation = 21;
		Item.useStyle = 5;
		Item.knockBack = 1f;
		Item.rare = 11;
		Item.value = Item.buyPrice(2);
		Item.UseSound = new Terraria.Audio.SoundStyle("Ultranium/Sounds/Item/Flute");
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("PhantomClaw").Type;
		Item.shootSpeed = 15f;
	}

	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-7f, -4f);
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			if (tooltip.Mod == "Terraria" && tooltip.Name == "ItemName")
			{
				float amount = (float)(Main.GameUpdateCount % 60) / 60f;
				int num = (int)(Main.GameUpdateCount / 60 % 2);
				tooltip.OverrideColor = Color.Lerp(itemNameCycleColors[num], itemNameCycleColors[(num + 1) % 2], amount);
			}
		}
	}
}
