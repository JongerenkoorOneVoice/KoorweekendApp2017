using System;
using CocosSharp;

namespace KoorweekendApp2017.Scenes
{
	public class Game1Scene1 : CCScene
	{
		CCDrawNode circle;

		public Game1Scene1(CCGameView gameView) : base(gameView)
		{
			var layer = new CCLayer();
			this.AddLayer(layer);
			circle = new CCDrawNode();
			layer.AddChild(circle);
			circle.DrawCircle(
				// The center to use when drawing the circle,
				// relative to the CCDrawNode:
				new CCPoint(0, 0),
				radius: 15,
				color: CCColor4B.White);
			circle.PositionX = 20;
			circle.PositionY = 50;
		}

		public void MoveCircleLeft()
		{
			circle.PositionX -= 10;
		}

		public void MoveCircleRight()
		{
			circle.PositionX += 10;
		}
	}
}
