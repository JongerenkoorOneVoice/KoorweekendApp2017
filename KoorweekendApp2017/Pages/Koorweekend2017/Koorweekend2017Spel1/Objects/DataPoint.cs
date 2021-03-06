﻿using System;
using System.Collections.Generic;
using CocosSharp;
using Geolocator.Plugin.Abstractions;
using KoorweekendApp2017.Models;

namespace KoorweekendApp2017.Koorweekend2017Spel1.Objects
{
	public class DataPoint 
	{
		public CCDrawNode Node { get; set; } = new CCDrawNode();

		private Point3d _nodePosition { get; set; } = new Point3d();

		//private CCLayer _currentLayer { get; set; }

		//private CCNode _currentParent { get; set; }

		public Position OrignalGpsLocation { get; set; }

		public ChoirWeekendGame1Assignment OrignialAssignment { get; set; }

		public DataPoint(Point3d point, Position position, CCColor4B color)
		{
			
			Node.DrawSolidCircle(
				new CCPoint(0, 0),
				10,
				color
			);
			Node.PositionX = point.X;
			Node.PositionY = point.Y;

			_nodePosition = point;

			OrignalGpsLocation = position;


		}

		public void UpdatePosition(Point3d point)
		{
			

			var moveAction = new CCMoveTo(2, new CCPoint(
				x: point.X,
				y: point.Y
			));
			//Node.PositionX = point.X;
			//Node.PositionY = point.Y;

			Node.AddAction(moveAction);

			_nodePosition = point;
		}


	}
}
