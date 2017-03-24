using System;
using System.Collections.Generic;
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

			var cumulativeLatittude = 0f;
			var cumulativeLongitude = 0f;

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
			float multiplier = squareRasterSize * scale;

			var x = (float)(lonDif * multiplier) + halfRastersize;
			var y = (float)(latDif * multiplier) + halfRastersize;

			var a = GetDistance(
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

			var b = GetDistance(
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

			var c = GetDistance(currentPosition, currentPosition);

			return new Point3d(x, y);
		}
	}

}
