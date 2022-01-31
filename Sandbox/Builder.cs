namespace Sandbox;

public static class Builder
{
    public static uint[] GeneratePlaneIndices()
    {
		uint[] indices = new uint[2 * 3] {
			0, 1, 3,
			1, 2, 3
		};

		return indices;
    }

    public static float[] GeneratePlaneVertices()
    {
        float[] vertices = new float[4 * (3 + 2)] 
		{
             0.5f,  0.5f, 0.0f, 1.0f, 1.0f,
			 0.5f, -0.5f, 0.0f, 1.0f, 0.0f,
			-0.5f, -0.5f, 0.0f, 0.0f, 0.0f,
			-0.5f,  0.5f, 0.0f, 0.0f, 1.0f
		};

        return vertices;
    }

    public static uint[] GenerateCubeIndices()
    {
        uint[] indices = new uint[12 * 3]
        {
			// Back
			0, 1, 2,
            2, 3, 0,

			// Front
			4, 5, 6,
            6, 7, 4,

			// Right
			8, 9, 10,
            10, 11, 8,

			// Left
			12, 13, 14,
            14, 15, 12,

			// Top
			16, 17, 18,
            18, 19, 16,

			// Bottom
			20, 21, 22,
            22, 23, 20,
        };
        return indices;
    }

    public static float[] GenerateCubeVertices()
    {
        float[] vertices = new float[24 * (3 + 2)]
        {
			// POSITION			  TEX COORDS
			-0.5f, -0.5f, -0.5f,  0.0f, 0.0f, //0
			 0.5f, -0.5f, -0.5f,  1.0f, 0.0f, //1
			 0.5f,  0.5f, -0.5f,  1.0f, 1.0f, //2
			-0.5f,  0.5f, -0.5f,  0.0f, 1.0f, //3

			// POSITION			  TEX COORDS
			-0.5f, -0.5f,  0.5f,  0.0f, 0.0f, //4 
			 0.5f, -0.5f,  0.5f,  1.0f, 0.0f, //5
			 0.5f,  0.5f,  0.5f,  1.0f, 1.0f, //6 
			-0.5f,  0.5f,  0.5f,  0.0f, 1.0f, //7

			// POSITION			  TEX COORDS
			-0.5f,  0.5f,  0.5f,  0.0f, 0.0f, //8
			-0.5f,  0.5f, -0.5f,  1.0f, 0.0f, //9
			-0.5f, -0.5f, -0.5f,  1.0f, 1.0f, //10
			-0.5f, -0.5f,  0.5f,  0.0f, 1.0f, //11
								  
			// POSITION			  TEX COORDS
			 0.5f,  0.5f,  0.5f,  0.0f, 0.0f, //12
			 0.5f,  0.5f, -0.5f,  1.0f, 0.0f, //13
			 0.5f, -0.5f, -0.5f,  1.0f, 1.0f, //14
			 0.5f, -0.5f,  0.5f,  0.0f, 1.0f, //15

			 // POSITION		  TEX COORDS
			 -0.5f, -0.5f, -0.5f, 0.0f, 0.0f, //16
			 0.5f, -0.5f, -0.5f,  1.0f, 0.0f, //17
			 0.5f, -0.5f,  0.5f,  1.0f, 1.0f, //18
			-0.5f, -0.5f,  0.5f,  0.0f, 1.0f, //19

			// POSITION			  TEX COORDS
			-0.5f,  0.5f, -0.5f,  0.0f, 0.0f, //20
			 0.5f,  0.5f, -0.5f,  1.0f, 0.0f, //21
			 0.5f,  0.5f,  0.5f,  1.0f, 1.0f, //22
			-0.5f,  0.5f,  0.5f,  0.0f, 1.0f, //23
        };
        return vertices;
    }
}
