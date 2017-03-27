﻿using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using KoorweekendApp2017.Models;
using Plugin.Compass.Abstractions;
using Plugin.Geolocator.Abstractions;


namespace KoorweekendApp2017.Koorweekend2017Spel1.Objects
{
	public class DataLayer : CCLayer
	{
		public CCNode RotatingDataLayer { get; set; } = new CCNode();

		public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();

		private Position _currentPosition { get; set; }

		public float CurrentScale { get; set; }



		public DataLayer()
		{
			

			RotatingDataLayer.AnchorPoint = CCPoint.AnchorMiddle;
			RotatingDataLayer.Position = new CCPoint(500f, 750f);
			RotatingDataLayer.ContentSize = new CCSize(1000f, 1000f);
			//RotatingDataLayer.IgnoreAnchorPointForPosition = true;
			RotatingDataLayer.Color = CCColor3B.Blue;
			AddChild(RotatingDataLayer);


		}

		public void OnCompassChange(object sender, CompassChangedEventArgs e)
		{
			RotatingDataLayer.Rotation = -(float)e.Heading - 90;
			//var h = DataLayer.AnchorPointInPoints;
		}

		// Leave this method here as an example in case I need it later
		public void OnCurrentLocationChange(object sender, PositionEventArgs e)
		{
			//RotatingDataLayer.Rotation = -(float)e.Heading;
			//var h = DataLayer.AnchorPointInPoints;
		}

		public void SetCurrentPosition(Position position)
		{
			_currentPosition = position;
			UpdateAllLocations();
		}

		public void UpdateAllLocations()
		{
			//foreach (var dataPoint in DataPoints)
			//{
			foreach (var dataPoint in DataPoints)
			{

				var point3d = GetRelativePositionXY(dataPoint.OrignalGpsLocation);
				dataPoint.UpdatePosition(point3d);

				var baseCurrent = new ChoirWeekendBasePosition()
				{
					Longitude = _currentPosition.Longitude,
					Lattitude = _currentPosition.Latitude
				};

				var baseDataPoint = new ChoirWeekendBasePosition()
				{
					Longitude = dataPoint.OrignalGpsLocation.Longitude,
					Lattitude = dataPoint.OrignalGpsLocation.Latitude
				};

				if (GpsHelper.GetDistance(baseCurrent, baseDataPoint) <= 5)
				{
					dataPoint.Node.Color = CCColor3B.Orange;
					dataPoint.Node.UpdateColor();
					//dataPoint.Node.UpdateDisplayedColor(CCColor3B.Magenta);

				}

				if (GpsHelper.GetDistance(baseCurrent, baseDataPoint) <= 2)
				{
					dataPoint.Node.Color = CCColor3B.Red;
					dataPoint.Node.UpdateColor();
					//dataPoint.Node.UpdateDisplayedColor(CCColor3B.White);
				}
			
			}
			//}
		}

		public Point3d GetRelativePositionXY(Position position)
		{
			var point3d = GpsHelper.GpsPositionToXY(
				currentPosition: new ChoirWeekendBasePosition()
				{
					Lattitude = (float)_currentPosition.Latitude,
					Longitude = (float)_currentPosition.Longitude
				},
				targetPosition: new ChoirWeekendBasePosition()
				{
					Lattitude = (float)position.Latitude,
					Longitude = (float)position.Longitude
				},
				scale: 1.0f,
				squareRasterSize: 1000f


			);
			return point3d;
		}

		public void PlotNewNode(Position position)
		{

			var point3d = GetRelativePositionXY(position);
			var dataPoint = new DataPoint(point3d, position);

			RotatingDataLayer.AddChild(dataPoint.Node);
			DataPoints.Add(dataPoint);
		}



	}
}
