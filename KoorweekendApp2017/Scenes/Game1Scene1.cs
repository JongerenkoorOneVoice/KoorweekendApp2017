using System;
using CocosSharp;
using Plugin.Geolocator.Abstractions;
using Plugin.Compass;
using GeoCoordinatePortable;

namespace KoorweekendApp2017.Scenes
{
	public class Game1Scene1 : CCScene
	{
		CCLayer layer;
		CCDrawNode innerCircle;
		CCDrawNode outerCircle;
		CCDrawNode line;
		public CCLabel label { get; set;}

		private double SigmaLattitude { get; set; }
		private double SigmaLongitude { get; set; }

		private DateTime lastUpdate { get; set; }

		private int numberOfMeasurements { get; set;}

		private bool _initialPositionSet {get; set;} = false;

		private Position _initialPosition { get; set;}

		public Game1Scene1(CCGameView gameView) : base(gameView)
		{

			layer = new CCLayer();

			//layer.Position = new CCPoint(layer.ContentSize.Center.X, layer.ContentSize.Center.Y);


			//layer.IgnoreAnchorPointForPosition = false;
			//layer.AnchorPoint = new CCPoint(0.5f, -0.5f);

			this.AddLayer(layer);

			CCNode main = new CCNode();
			main.AnchorPoint = new CCPoint(0.5f, 0.5f);
			main.Position = new CCPoint(50f, 50f);
			main.ContentSize = new CCSize(100f, 100f);

			layer.AddChild(main);

			CrossCompass.Current.CompassChanged += (s, e) =>
			{

				main.Rotation = -(float)e.Heading;
				var h = main.AnchorPointInPoints;

			};

			var a = new GeoCoordinate(52.224555, 4.953526);
			var b = new GeoCoordinate(52.223713, 4.951803);
			var test = a.GetDistanceTo(b);


			line = new CCDrawNode();
			line.Position = new CCPoint(0, 0);
			line.DrawRect(
				new CCRect(45, 15, 10, 70),
				CCColor4B.White
			);
			main.AddChild(line);

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
		}

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
		}
	}
}
