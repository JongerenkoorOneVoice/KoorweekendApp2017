using System;
using CocosSharp;
using Plugin.Geolocator.Abstractions;
using Plugin.Compass;
using KoorweekendApp2017.Koorweekend2017Spel1.Objects;
//using GeoCoordinatePortable;
using Plugin.Geolocator;

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

			//CrossGeolocator.Current.
            //var a = 0;//new GeoCoordinate(52.224555, 4.953526);
            //var b = 0;//new GeoCoordinate(52.223713, 4.951803);
            //var test = 0;//a.GetDistanceTo(b);

			//
			//line = new CCDrawNode();
			//line.Position = new CCPoint(0, 0);
			//line.DrawRect(
				//new CCRect(45, 15, 10, 70),
				//CCColor4B.White
			//);
			//main.AddChild(line);

			/*
			CCDrawNode roundLineEndBottom = new CCDrawNode();
			roundLineEndBottom.DrawSolidCircle(
				new CCPoint(0, 0),d
				radius: 5,
				color: CCColor4B.White);
			roundLineEndBottom.PositionX = 50;
			roundLineEndBottom.PositionY = 15;
			layer.AddChild(roundLineEndBottom);

			CCDrawNode roundLineEndTop = new CCDrawNode();
			roundLineEndTop.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 5,
				color: CCColor4B.White);
			roundLineEndTop.PositionX = 50;
			roundLineEndTop.PositionY = 85;
			layer.AddChild(roundLineEndTop);

			innerCircle = new CCDrawNode();
			innerCircle.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 2.5f,
				color: CCColor4B.Red);
			innerCircle.PositionX = 50;
			innerCircle.PositionY = 15;
			layer.AddChild(innerCircle);

			outerCircle = new CCDrawNode();
			outerCircle.DrawCircle(
				new CCPoint(0, 0),
				radius: 10,
				color: CCColor4B.Red);
			outerCircle.PositionX = 50;
			outerCircle.PositionY = 15;
			layer.AddChild(outerCircle);
			*/
			/*
			outerCircle = new CCDrawNode();
			outerCircle.DrawSolidCircle(
				new CCPoint(50, 50),
				radius: 2.5f,
				color: CCColor4B.Green);
			main.AddChild(outerCircle);

			innerCircle = new CCDrawNode();
			innerCircle.DrawSolidCircle(
				new CCPoint(50, 50),
				radius: 2.5f,
				color: CCColor4B.Red);
			main.AddChild(innerCircle);

			label = new CCLabel("Init", "test", 8);
			label.AnchorPoint = new CCPoint(0, 0);
			label.Text ="init";
			label.Color = CCColor3B.White;
			main.AddChild(label);
			label.Text = String.Format("{0}||{1}", main.Position.X, main.Position.Y);
			*/
		}
		/*
		public void MoveCircleLeft()
		{
			var x = innerCircle.Position.X - 1;
			var y = innerCircle.Position.Y;

			CCPoint dot = new CCPoint(x, y);
			innerCircle.RunAction(new CCMoveTo(0.5f, dot));
			outerCircle.RunAction(new CCMoveTo(0.5f, dot));

			CCPoint innerCircleRightX = new CCPoint((innerCircle.Position.X + 1.5f), innerCircle.Position.Y);
			bool hit = line.BoundingRect.ContainsPoint(innerCircleRightX);
			if (!hit)
			{
				innerCircle.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 2.5f,
				color: CCColor4B.Green);
				

			}
			else
			{
				innerCircle.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 2.5f,
				color: CCColor4B.Red);
			}

		}

		public void MoveCircleRight()
		{
			
			var x = innerCircle.Position.X + 1;
			var y = innerCircle.Position.Y;
			/*var a = new CCCallFunc(() => { 
			
			});*/
		/*
			CCPoint dot = new CCPoint(x, y);
			innerCircle.RunAction(new CCMoveTo(0.5f, dot));
			outerCircle.RunAction(new CCMoveTo(0.5f, dot));

			CCPoint innerCircleLeftX = new CCPoint((innerCircle.Position.X - 1.5f), innerCircle.Position.Y);
			bool hit = line.BoundingRect.ContainsPoint(innerCircleLeftX);
			if (!hit)
			{
				innerCircle.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 2.5f,
				color: CCColor4B.Green);

			}
			else {
				innerCircle.DrawSolidCircle(
				new CCPoint(0, 0),
				radius: 2.5f,
				color: CCColor4B.Red);
			}

		}

		public void UpdatePosition(Position position)
		{
			double basicLattiude = 51.806454f;
			double basicLongitude = 4.628316f;

			double pixelLattitude = (position.Latitude - basicLattiude) * 111000;
			double pixelLongitude = (position.Longitude - basicLongitude) * 111000;


			innerCircle.PositionX = (float)pixelLattitude;
			innerCircle.PositionY = (float)pixelLongitude;

			//label.Text = position.Heading.ToDegrees().ToString();



			/*
			numberOfMeasurements = numberOfMeasurements + 1;
			SigmaLattitude = SigmaLattitude + position.Latitude;
			SigmaLongitude = SigmaLongitude + position.Longitude;
			if (lastUpdate == DateTime.MinValue) lastUpdate = DateTime.Now;

			if (DateTime.Now - lastUpdate > new TimeSpan(0, 0, 1))
			{
				double avarageLattitude = SigmaLattitude / numberOfMeasurements;
				double avarageLongitude = SigmaLongitude / numberOfMeasurements;



				lastUpdate = DateTime.Now;
				numberOfMeasurements = 1;
				SigmaLattitude = position.Latitude;
				SigmaLongitude = position.Longitude;

				label.Text = String.Format("lat:{0} | lon:{1} | head:{2}", pixelLattidude, pixelLongitude, position.Heading.ToDegrees());


			}
			    
*/







		/*
		if (!_initialPositionSet)
		{
			_initialPositionSet = true;
			_initialPosition = position;
		}

		double targetPositionLatitude = _initialPosition.Latitude + 1;
		double targetPositionLongitude = _initialPosition.Longitude + 1;

		double distanceInMeters = GeoCalculator.GetDistance(
			_initialPosition.Latitude,
			_initialPosition.Longitude,
			targetPositionLatitude,
			targetPositionLongitude,
			0
		);

		//double differenceInLattitude = Math.Abs(_initialPosition.Latitude - targetPositionLatitude);
		//double differenceInLongitude = Math.Abs(_initialPosition.Longitude - targetPositionLongitude);

		int pixelsInLine = 60;
		double meterPerPixel = Math.Round(((double)distanceInMeters / pixelsInLine));


		var x = innerCircle.Position.X + 1;
		var y = innerCircle.Position.Y;
		/*var a = new CCCallFunc(() => { 

		});*//*
		CCPoint dot = new CCPoint(x, y);
		innerCircle.RunAction(new CCMoveTo(0.5f, dot));
		outerCircle.RunAction(new CCMoveTo(0.5f, dot));

		CCPoint innerCircleLeftX = new CCPoint((innerCircle.Position.X - 1.5f), innerCircle.Position.Y);
		bool hit = line.BoundingRect.ContainsPoint(innerCircleLeftX);
		if (!hit)
		{
			innerCircle.DrawSolidCircle(
			new CCPoint(0, 0),
			radius: 2.5f,
			color: CCColor4B.Green);

		}
		else {
			innerCircle.DrawSolidCircle(
			new CCPoint(0, 0),
			radius: 2.5f,
			color: CCColor4B.Red);
		}
		*/
			 //}

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
			var RadarLayer = new CCLayer();
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
