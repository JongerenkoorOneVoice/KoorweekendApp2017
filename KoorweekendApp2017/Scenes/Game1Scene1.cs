using System;
using CocosSharp;
using Plugin.Geolocator.Abstractions;
using Plugin.Compass;
using KoorweekendApp2017.Koorweekend2017Spel1.Objects;
//using GeoCoordinatePortable;
using Plugin.Geolocator;
using KoorweekendApp2017.Models;
using System.Collections.Generic;

namespace KoorweekendApp2017.Scenes
{
	public class Game1Scene1 : CCScene
	{
		public CCLayer RadarLayer { get; set; }

		public DataLayer DataLayer { get; set; }

		/*
		CCDrawNode innerCircle;
		CCDrawNode outerCircle;
		CCDrawNode line;
		public CCLabel label { get; set;}
*/
		/*
		private double SigmaLattitude { get; set; }
		private double SigmaLongitude { get; set; }

		private DateTime lastUpdate { get; set; }

		private int numberOfMeasurements { get; set;}

		private bool _initialPositionSet {get; set;} = false;

		private Position _initialPosition { get; set;}
		*/
		public Game1Scene1(CCGameView gameView) : base(gameView)
		{
			// Setup scene
			RadarLayer = SetupRadarLayer();
			AddLayer(RadarLayer);

			DataLayer = new DataLayer();
			AddLayer(DataLayer);

			// Setup events
			CrossCompass.Current.CompassChanged += DataLayer.OnCompassChange;


			DateTime timeLastMeasurementSend = DateTime.Now;
			List<Position> routeLog = new List<Position>();
			List<Position> lastMeasuredPositions = new List<Position>();

			// Set some position so the value isn't null
			/*
			var currentAssignment = gameAssignments.Find(x => x.Location.Description == "Thuis");
			DataLayer.SetCurrentPosition(new Position()
			{
				Longitude = currentAssignment.Location.Position.Longitude,
				Latitude = currentAssignment.Location.Position.Lattitude
			});*/

			// Get the real position
			CrossGeolocator.Current.PositionChanged += (object sender, PositionEventArgs e) =>
			{
				var position = e.Position as Position;
				if (routeLog.Count == 0)
				{
					DataLayer.SetCurrentPosition(position);
					SetupDataLayer();
				}

				routeLog.Add(position);
				lastMeasuredPositions.Add(position);
				if (DateTime.Now - timeLastMeasurementSend >= new TimeSpan(0, 0, 2))
				{
					timeLastMeasurementSend = DateTime.Now;
					var numberOfMeasurements = lastMeasuredPositions.Count;
					var avaragePosition = GpsHelper.GetAvaragePosition(lastMeasuredPositions);
					lastMeasuredPositions.RemoveRange(0, numberOfMeasurements);
					DataLayer.SetCurrentPosition(avaragePosition);
				}
			};
		}

		public void SetupDataLayer()
		{
			var gameAssignments = App.Database.ChoirWeekend2017.Game1.GetAll();
			foreach (var assignment in gameAssignments)
			{
				/*
				DataLayer.PlotNewNode(new Position()
				{
					Longitude = currentAssignment.Location.Position.Longitude,
					Latitude = currentAssignment.Location.Position.Lattitude
				});
*/
				//if (assignment.Location.Name == "Locatie 1")
				//{
					DataLayer.PlotNewNode(
						new Position()
						{
							Longitude = assignment.Location.Position.Longitude,
							Latitude = assignment.Location.Position.Lattitude
						}
					);

				//}
			}
		}

		public CCDrawNode CenterCircle(Int32 radius)
		{
			var circle = new CCDrawNode();
			circle.DrawCircle(
				new CCPoint(0, 0),
				radius: radius,
				color: CCColor4B.Red);
			circle.PositionX = 500;
			circle.PositionY = 750;
			return circle;
		}

		public CCDrawNode Line(CCPoint point1, CCPoint point2)
		{
			var node = new CCDrawNode();
			node.DrawLine(
				point1,
				point2,
				1.0f,
				CCColor4B.Red,
				CCLineCap.Butt
			);

			return node;		
		}

		public CCLabel Text(CCPoint position)
		{
			var text = new CCLabel("Schaal 1:1000", "Arial", 50f, CCLabelFormat.SystemFont);
			text.AnchorPoint = CCPoint.AnchorLowerRight;
			text.Position = position;
			text.Color = CCColor3B.White;

			return text;
		}

		public CCLayer SetupRadarLayer()
		{

			// create layer
			RadarLayer = new CCLayer();
			AddLayer(RadarLayer);

			// add radar circles
			RadarLayer.AddChild(CenterCircle(50));
			RadarLayer.AddChild(CenterCircle(150));
			RadarLayer.AddChild(CenterCircle(250));
			RadarLayer.AddChild(CenterCircle(350));
			RadarLayer.AddChild(CenterCircle(450));

			// add radar diagonal lines
			RadarLayer.AddChild(Line(new CCPoint(150f, 400f), new CCPoint(850f, 1100f)));
			RadarLayer.AddChild(Line(new CCPoint(150f, 1100f), new CCPoint(850f, 400f)));

			// add radar horizontal and vertical lines
			RadarLayer.AddChild(Line(new CCPoint(0f, 750f), new CCPoint(1000f, 750f)));
			RadarLayer.AddChild(Line(new CCPoint(500f, 250f), new CCPoint(500f, 1250f)));

			RadarLayer.AddChild(Text(new CCPoint(975f, 250f)));

			return RadarLayer;
		}

	}
}
