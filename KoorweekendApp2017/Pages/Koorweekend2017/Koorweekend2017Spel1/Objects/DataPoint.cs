using System;
using System.Collections.Generic;
using CocosSharp;
using KoorweekendApp2017.Models;

namespace KoorweekendApp2017.Koorweekend2017Spel1.Objects
{
	public class DataPoint 
	{
		private CCDrawNode _node { get; set; } = new CCDrawNode();

		private Point3d _nodePosition { get; set; } = new Point3d();

		private CCLayer _currentLayer { get; set; }

		public DataPoint(CCLayer currentLayer, Point3d point)
		{
			_currentLayer = currentLayer;

			_currentLayer.AddChild(_node);

			_node.DrawSolidCircle(
				new CCPoint(point.X, point.Y),
				10,
				CCColor4B.Green
			);
		}




	}
}
