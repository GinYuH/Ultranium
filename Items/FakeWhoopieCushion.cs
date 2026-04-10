using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items;

public class FakeWhoopieCushion : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Whoopie Cushion?");
		// Tooltip.SetDefault("'May annoy others'\nLegends say that it's farts can be harvested...");
		ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
	}

	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 14;
		Item.maxStack = 1;
		Item.useStyle = 4;
		Item.useTime = 30;
		Item.useAnimation = 30;
		Item.UseSound = SoundID.Item16;
		Item.rare = 2;
		Item.useTurn = true;
		Item.autoReuse = false;
	}

	public override void ExtractinatorUse(int extractinatorBlockType, ref int resultType, ref int resultStack)
	{
		string[] array = new string[1] { "TheFart" };
		int num = Main.rand.Next(array.Length);
		resultType = Mod.Find<ModItem>(array[num]).Type;
		resultStack = 1;
		if (!UltraniumWorld.TheFart)
		{
			UltraniumWorld.TheFart = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		int num2 = Main.LocalPlayer.FindItem(Mod.Find<ModItem>("FakeWhoopieCushion").Type);
		Main.LocalPlayer.inventory[num2].TurnToAir();
	}
}
