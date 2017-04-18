using System;
using System.Collections.Generic;
using System.Linq;
using Geolocator.Plugin.Abstractions;
using KoorweekendApp2017.Models;


namespace KoorweekendApp2017
{
	public class GpsHelper
	{

		public static float GetDistance(ChoirWeekendBasePosition pos1, ChoirWeekendBasePosition pos2)
		{

			float R = 6378.137f; // Radius of earth in KM
			float dLat = (float)(pos2.Lattitude * Math.PI / 180 - pos1.Lattitude * Math.PI / 180);
			float dLon = (float)(pos2.Longitude * Math.PI / 180 - pos1.Longitude * Math.PI / 180);
			float a = (float)(Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
					   Math.Cos(pos1.Lattitude * Math.PI / 180) * Math.Cos(pos2.Lattitude * Math.PI / 180) *
					   Math.Sin(dLon / 2) * Math.Sin(dLon / 2));
			float c = (float)(2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a)));
			float d = R * c;
			return d * 1000; // meters
		}

		public static ChoirWeekendBasePosition GetAverageLocation(List<ChoirWeekendBasePosition> positionList)
		{
			var numberOfItems = positionList.Count;

			var cumulativeLatittude = 0.0;
			var cumulativeLongitude = 0.0;

			foreach (var pos in positionList)
			{
				cumulativeLatittude += pos.Lattitude;
				cumulativeLongitude += pos.Longitude;
			}

			var avarageLattitude = cumulativeLatittude / numberOfItems;
			var avarageLongitude = cumulativeLongitude / numberOfItems;

			var result = new ChoirWeekendBasePosition();
			result.Lattitude = avarageLattitude;
			result.Longitude = avarageLongitude;

			return result;
		}

		public static Point3d GpsPositionToXY(ChoirWeekendBasePosition currentPosition, ChoirWeekendBasePosition targetPosition, float scale, float squareRasterSize)
		{

			var latDif = targetPosition.Lattitude - currentPosition.Lattitude;
			var lonDif = targetPosition.Longitude - currentPosition.Longitude;

			float halfRastersize = squareRasterSize / 2;
			//float multiplier = squareRasterSize * scale;



			var distX = GetDistance(
				pos1: new ChoirWeekendBasePosition()
				{
					Lattitude = currentPosition.Lattitude,
					Longitude = 0f
				},
				pos2: new ChoirWeekendBasePosition()
				{
					Lattitude = targetPosition.Lattitude,
					Longitude = 0f
				}
			);

			var distY = GetDistance(
				pos1: new ChoirWeekendBasePosition()
				{
					Lattitude = 0f,
					Longitude = currentPosition.Longitude
				},
				pos2: new ChoirWeekendBasePosition()
				{
					Lattitude = 0f,
					Longitude = targetPosition.Longitude
				}
			);

			var x = (float)(distX * scale);
			var y = (float)(distY * scale);

			var c = GetDistance(targetPosition, currentPosition);


			var point3d = new Point3d(x, y);

			point3d.X = halfRastersize - x;
			if (currentPosition.Lattitude - targetPosition.Lattitude < 0)
			{
				point3d.X = x + halfRastersize;
			}

			point3d.Y = y + halfRastersize;
			if (currentPosition.Longitude - targetPosition.Longitude < 0)
			{
				point3d.Y = halfRastersize - y;
			}

			return point3d;
		}

		public static Position GetMostAccuratePosition(List<Position> measuredPositions)
		{
			if (measuredPositions.Count == 0) return new Position();

			Double lowestAccuracy = 1000000;
			foreach (var pos in measuredPositions)
			{
				if (pos.Accuracy < lowestAccuracy)
				{
					lowestAccuracy = pos.Accuracy;
				}
			}
			return measuredPositions.Find(x => x.Accuracy == lowestAccuracy);

			List<Position> bestMeasurements = measuredPositions.FindAll(
				x => x.Accuracy > Math.Ceiling(lowestAccuracy - 1) && x.Accuracy < Math.Floor(lowestAccuracy + 1)
           	).ToList();

			return GetAvaragePosition(bestMeasurements);
		}

		public static Position GetAvaragePosition(List<Position> measuredPositions)
		{
			if (measuredPositions.Count == 0) return new Position();

			var lastMeassuredTimeStamp = measuredPositions.First().Timestamp - measuredPositions.First().Timestamp;

			double cumulativeAccuracy = 0.0;
			double cumulativeAltitude = 0.0;
			double cumulativeAAltitudeAccuracy = 0.0;
			double cumulativeHeading = 0.0;
			double cumulativeLatitude = 0.0;
			double cumulativeLongitude = 0.0;
			double cumulativeSpeed = 0.0;


			foreach (var position in measuredPositions)
			{
				cumulativeAccuracy += Convert.ToDouble(position.Accuracy);
				cumulativeAltitude += Convert.ToDouble(position.Altitude);
				cumulativeAAltitudeAccuracy += Convert.ToDouble(position.AltitudeAccuracy);
				cumulativeHeading += Convert.ToDouble(position.Heading);
				cumulativeLatitude += Convert.ToDouble(position.Latitude);
				cumulativeLongitude += Convert.ToDouble(position.Longitude);
				cumulativeSpeed += Convert.ToDouble(position.Speed);
			}

			var numberOfMeasurements = measuredPositions.Count;
			var avaragePosition = new Position()
			{
				Accuracy = cumulativeAccuracy / numberOfMeasurements,
				Altitude = cumulativeAltitude / numberOfMeasurements,
				AltitudeAccuracy = cumulativeAAltitudeAccuracy / numberOfMeasurements,
				Heading = cumulativeHeading / numberOfMeasurements,
				Latitude = cumulativeLatitude / numberOfMeasurements,
				Longitude = cumulativeLongitude / numberOfMeasurements,
				Speed = cumulativeSpeed / numberOfMeasurements,
				Timestamp = measuredPositions.Last().Timestamp
			};
			return avaragePosition;
		}
	}

}
