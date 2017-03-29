using System;
namespace KoorweekendApp2017.Models
{
	public class Point3d
	{



		public float X { get; set; }

		public float Y { get; set; }

		public float Z { get; set; }

		public Point3d(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public Point3d(float x, float y)
			:this(x, y, 0f)
		{

		}

		public Point3d()
			: this(0f, 0f, 0f)
		{
			
		}


	}

}
