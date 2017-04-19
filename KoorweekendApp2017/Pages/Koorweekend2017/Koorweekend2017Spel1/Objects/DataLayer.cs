using System;
using System.Collections.Generic;
using System.Linq;
using CocosSharp;
using Geolocator.Plugin.Abstractions;
using KoorweekendApp2017.Models;
using Plugin.Compass.Abstractions;
using Xamarin.Forms;

namespace KoorweekendApp2017.Koorweekend2017Spel1.Objects
{
	public class DataLayer : CCLayer
	{
		public CCNode RotatingDataLayer { get; set; } = new CCNode();

		public List<DataPoint> DataPoints { get; set; } = new List<DataPoint>();

		public Position CurrentPosition { get; set; }

		public float CurrentScale { get; set; }

		public bool GameEnded { get; set;}

		public bool NodesMadeInvisible { get; set;}

		private bool _startPointAlertShowing { get; set; } = false;

		private bool _currentLocationFoundAlertShowing { get; set; } = false;



		public DataLayer()
		{
			CurrentScale = 1.0f;

			RotatingDataLayer.AnchorPoint = CCPoint.AnchorMiddle;
			RotatingDataLayer.Position = new CCPoint(500f, 750f);
			RotatingDataLayer.ContentSize = new CCSize(1000f, 1000f);
			//RotatingDataLayer.IgnoreAnchorPointForPosition = true;
			RotatingDataLayer.Color = CCColor3B.Blue;
			AddChild(RotatingDataLayer);
			NodesMadeInvisible = false;
			GameEnded = false;

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
			CurrentPosition = position;
			UpdateAllLocations();
		}

		public void UpdateAllLocations()
		{
			//foreach (var dataPoint in DataPoints)
			//{

			foreach (var dataPoint in DataPoints)
			{

				if (GameEnded)
				{
					if (dataPoint.OrignialAssignment.Settings.MaxScore != 0 && dataPoint.Node.Parent != null)
					{
						dataPoint.Node.Parent.RemoveChild(dataPoint.Node);
					}

				}
				else if (GameEnded && NodesMadeInvisible)
				{
					// Do nothing
				}
				else
				{
					var alreadyFound = dataPoint.OrignialAssignment.Result.Score != 0;
					if (!alreadyFound)
					{

						var baseCurrent = new ChoirWeekendBasePosition()
						{
							Longitude = CurrentPosition.Longitude,
							Lattitude = CurrentPosition.Latitude
						};

						var baseDataPoint = new ChoirWeekendBasePosition()
						{
							Longitude = dataPoint.OrignalGpsLocation.Longitude,
							Lattitude = dataPoint.OrignalGpsLocation.Latitude
						};

						var distance = GpsHelper.GetDistance(baseCurrent, baseDataPoint);

						if (distance <= 2)
						{
							var assignment = dataPoint.OrignialAssignment;
							assignment.Result.Score = assignment.Settings.MaxScore;


							if (assignment.Settings.MaxScore != 0)
							{
								if (!_currentLocationFoundAlertShowing)
								{
									_currentLocationFoundAlertShowing = true;
									App.Database.ChoirWeekend2017.Game1.UpdateOrInsert(assignment);
									Application.Current.MainPage.DisplayAlert("Gevonden!", "Je hebt deze lokatie gevonden.\r\n Ga snel verder naar de volgende!", "Oké");

									dataPoint.Node.Color = CCColor3B.White;
									dataPoint.Node.DrawSolidCircle(
										new CCPoint(0, 0),
										10,
										CCColor4B.White
									);
									_currentLocationFoundAlertShowing = false;
								}
							}
							else
							{
								if (!_startPointAlertShowing)
								{
									_startPointAlertShowing = true;
									Application.Current.MainPage.DisplayAlert("Begin-/Eindpunt!", "Welkom terug bij het begin-/eindpunt.\r\nVoor deze lokatie krijg je geen punten.", "Oké");
									_startPointAlertShowing = false;
								}
							}
						}
					}


				}

				var point3d = GetRelativePositionXY(dataPoint.OrignalGpsLocation);
				dataPoint.UpdatePosition(point3d);
			}
		}

		public Point3d GetRelativePositionXY(Position position)
		{
			var point3d = GpsHelper.GpsPositionToXY(
				currentPosition: new ChoirWeekendBasePosition()
				{
					Lattitude = (float)CurrentPosition.Latitude,
					Longitude = (float)CurrentPosition.Longitude
				},
				targetPosition: new ChoirWeekendBasePosition()
				{
					Lattitude = (float)position.Latitude,
					Longitude = (float)position.Longitude
				},
				scale: CurrentScale,
				squareRasterSize: 1000f


			);
			return point3d;
		}

		public void PlotNewNode(ChoirWeekendGame1Assignment assignment)
		{
			var position = new Position()
			{
				Longitude = assignment.Location.Position.Longitude,
				Latitude = assignment.Location.Position.Lattitude
			};
			var point3d = GetRelativePositionXY(position);
			var dataPoint = new DataPoint(point3d, position, _getColorForLocation(assignment));
			dataPoint.OrignialAssignment = assignment;

			RotatingDataLayer.AddChild(dataPoint.Node);
			DataPoints.Add(dataPoint);
		}

		private CCColor4B _getColorForLocation(ChoirWeekendGame1Assignment assignment)
		{
			var maxScore = assignment.Settings.MaxScore;
			var alreadyFound = assignment.Result.Score != 0;

			if (alreadyFound)
			{
				return CCColor4B.White;
			}
			else if (maxScore == 0)
			{
				return CCColor4B.Blue;
			}
			else if (maxScore == 1)
			{
				return CCColor4B.Green;
			}
			else if (maxScore == 5)
			{
				return CCColor4B.Orange;
			}
			else if (maxScore == 10)
			{
				return CCColor4B.Red;
			}
			else
			{
				return CCColor4B.Gray;
			}
		}

	}
}
