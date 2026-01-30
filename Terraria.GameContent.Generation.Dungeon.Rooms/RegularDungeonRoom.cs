using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.Utilities;

namespace Terraria.GameContent.Generation.Dungeon.Rooms;

public class RegularDungeonRoom : DungeonRoom
{
	public int _innerBoundsSize;

	public RegularDungeonRoom(DungeonRoomSettings settings)
		: base(settings)
	{
	}

	public override void CalculateRoom(DungeonData data)
	{
		calculated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		RegularRoom(data, x, y, generating: false);
		calculated = true;
	}

	public override bool GenerateRoom(DungeonData data)
	{
		generated = false;
		int x = settings.RoomPosition.X;
		int y = settings.RoomPosition.Y;
		RegularRoom(data, x, y, generating: true);
		generated = true;
		return true;
	}

	public void RegularRoom(DungeonData data, int i, int j, bool generating)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		UnifiedRandom unifiedRandom = new UnifiedRandom(settings.RandomSeed);
		RegularDungeonRoomSettings regularDungeonRoomSettings = (RegularDungeonRoomSettings)settings;
		ushort brickTileType = settings.StyleData.BrickTileType;
		ushort brickWallType = settings.StyleData.BrickWallType;
		Point center = default(Point);
		((Point)(ref center))._002Ector(i, j);
		if (base.Processed)
		{
			center = InnerBounds.Center;
		}
		int num = 6 + unifiedRandom.Next(7);
		int num2 = 8;
		if (regularDungeonRoomSettings.OverrideInnerBoundsSize > 0)
		{
			num = regularDungeonRoomSettings.OverrideInnerBoundsSize;
		}
		if (regularDungeonRoomSettings.OverrideOuterBoundsSize > 0)
		{
			num2 = regularDungeonRoomSettings.OverrideOuterBoundsSize;
		}
		if (base.Processed)
		{
			num = _innerBoundsSize;
		}
		int num3 = num + num2;
		InnerBounds.SetBounds(center.X, center.Y, center.X, center.Y);
		OuterBounds.SetBounds(center.X, center.Y, center.X, center.Y);
		OuterBounds.UpdateBounds(center.X - num3, center.Y - num3, center.X + num3, center.Y + num3);
		InnerBounds.UpdateBounds(OuterBounds.Left + num2, OuterBounds.Top + num2, OuterBounds.Right - num2, OuterBounds.Bottom - num2);
		GenerateDungeonSquareRoom(data, InnerBounds, OuterBounds, Vector2D.op_Implicit(center), brickTileType, brickWallType, num, num3, generating, generating);
		_innerBoundsSize = num;
		InnerBounds.CalculateHitbox();
		OuterBounds.CalculateHitbox();
	}
}
