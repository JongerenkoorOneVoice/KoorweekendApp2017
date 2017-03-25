using System;
using System.Collections.Generic;
using CocosSharp;

namespace KoorweekendApp2017.Koorweekend2017Spel1.Objects
{
	public class DataLayer : CCLayer
	{
		public CCNode RotatingDataLayer { get; set; } = new CCNode();

		public List<CCNode> DataPoints { get; set; } = new List<CCNode>();

		public DataLayer()
		{
			

			RotatingDataLayer.AnchorPoint = CCPoint.AnchorMiddle;
			RotatingDataLayer.Position = new CCPoint(500f, 750f);
			RotatingDataLayer.ContentSize = new CCSize(1000f, 1000f);
			RotatingDataLayer.Color = CCColor3B.Blue;
			AddChild(RotatingDataLayer);


		}


	}
}
