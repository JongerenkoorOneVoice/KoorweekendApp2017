using System;
using CocosSharp;
//using Plugin.Geolocator.Abstractions;
using Plugin.Compass;
using KoorweekendApp2017.Koorweekend2017Spel1.Objects;
//using GeoCoordinatePortable;
//using Plugin.Geolocator;
using KoorweekendApp2017.Models;
using System.Collections.Generic;
using Xamarin.Forms;
using Geolocator.Plugin;
using Geolocator.Plugin.Abstractions;

namespace KoorweekendApp2017.Scenes
{
	public class Game1Scene1 : CCScene
	{
		public CCLayer RadarLayer { get; set; }

		public DataLayer DataLayer { get; set; }

		public CCLayer ControlsLayer { get; set; }

		public CCLabel ScaleLabel { get; set; }

		public CCLabel LongitudeLabel { get; set; }

		public CCLabel LatitudeLabel { get; set; }

		public CCLabel AccuracyLabel { get; set; }

		public Game1Scene1(CCGameView gameView) : base(gameView)
		{
			

			// Setup scene
			RadarLayer = SetupRadarLayer();
			AddLayer(RadarLayer);

			ScaleLabel = Text(new CCPoint(25f, 25f));
			ScaleLabel.Text = "Zoomfactor: 1.00";
			ScaleLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
			RadarLayer.AddChild(ScaleLabel);

			LongitudeLabel = Text(new CCPoint(25f, 175f));
			LongitudeLabel.Text = "Lon: 0.000000";
			LongitudeLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
			RadarLayer.AddChild(LongitudeLabel);

			LatitudeLabel = Text(new CCPoint(25f, 125f));
			LatitudeLabel.Text = "Lat: 0.000000";
			LatitudeLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
			RadarLayer.AddChild(LatitudeLabel);

			AccuracyLabel = Text(new CCPoint(25f, 75f));
			AccuracyLabel.Text = "Nauwkeurigheid: 0.0";
			AccuracyLabel.AnchorPoint = CCPoint.AnchorLowerLeft;
			RadarLayer.AddChild(AccuracyLabel);

			DataLayer = new DataLayer();
			AddLayer(DataLayer);

			ControlsLayer = SetupControlLayer();
			RadarLayer.AddChild(ControlsLayer);

			// Setup events
			if (CrossCompass.Current.IsSupported)
			{
				CrossCompass.Current.CompassChanged += DataLayer.OnCompassChange;
			}

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

					var bestPosition = GpsHelper.GetMostAccuratePosition(lastMeasuredPositions);

					lastMeasuredPositions.RemoveRange(0, numberOfMeasurements);
					DataLayer.SetCurrentPosition(bestPosition);
					LatitudeLabel.Text =  String.Format("Lat: {0}", bestPosition.Latitude.ToString());
					LongitudeLabel.Text = String.Format("Lon: {0}", bestPosition.Longitude.ToString());
					AccuracyLabel.Text =  String.Format("Nauwkeurigheid: {0}M", bestPosition.Accuracy.ToString());
					//Application.Current.MainPage.DisplayAlert("test", numberOfMeasurements.ToString(), "test");
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
			var text = new CCLabel("Zoomfactor: 1.00", "Arial", 50f, CCLabelFormat.SystemFont);
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



			return RadarLayer;
		}

		public CCLayer SetupControlLayer()
		{

			// create layer
			ControlsLayer = new CCLayer();

			CCDrawNode scaleUpButton = new CCDrawNode();
			scaleUpButton.DrawCircle(
				new CCPoint(0, 0),
				radius: 75,
				color: CCColor4B.White);
			scaleUpButton.PositionX = 700;
			scaleUpButton.PositionY = 100;
		



			ControlsLayer.AddChild(scaleUpButton);

			//scaleUpButton.AddChild(zoominImage, 999);
			ControlsLayer.AddChild(scaleUpButton);

			CCDrawNode scaleDownButton = new CCDrawNode();
			scaleDownButton.DrawCircle(
				new CCPoint(0, 0),
				radius: 75,
				color: CCColor4B.White);
			scaleDownButton.PositionX = 875;
			scaleDownButton.PositionY = 100;



			try
			{
				CCSprite zoominImage = new CCSprite("zoomin.png");
				zoominImage.AnchorPoint = CCPoint.AnchorMiddle;
				zoominImage.Position = new CCPoint(200, 200);
				CCSprite.DefaultTexelToContentSizeRatio = 1.0f;
				scaleDownButton.AddChild(zoominImage);
			}
			catch (Exception ex)
			{

			}
			ControlsLayer.AddChild(scaleDownButton);
			//scaleUpButton

			var touchListener = new CCEventListenerTouchOneByOne();
			touchListener.OnTouchBegan = TouchBegan;
			AddEventListener(touchListener, ControlsLayer);

			B1 = scaleUpButton;
			B2 = scaleDownButton;

			return ControlsLayer;
		}

		public CCNode B1 { get; set; }

		public CCNode B2 { get; set; }


		public bool TouchBegan(CCTouch touch, CCEvent e){


			var point = new CCPoint(touch.Location.X, touch.Location.Y + 100);

			if (B1.BoundingBoxTransformedToWorld.ContainsPoint(point))
			{

				DataLayer.CurrentScale = DataLayer.CurrentScale / 2;
				ScaleLabel.Text = String.Format("Zoomfactor: {0}", DataLayer.CurrentScale.ToString("F2"));
				DataLayer.UpdateAllLocations();
			}

			if (B2.BoundingBoxTransformedToWorld.ContainsPoint(point))
			{
				DataLayer.CurrentScale = DataLayer.CurrentScale * 2;
				ScaleLabel.Text = String.Format("Zoomfactor: {0}", DataLayer.CurrentScale.ToString("F2"));
				DataLayer.UpdateAllLocations();
			}

			return true;

		}
	}
}
