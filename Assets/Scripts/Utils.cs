using UnityEngine;
using System.Collections;

public static class Utils {
    /// <summary>
    /// Convert from Cartesian (x, y) to Polar (r, phi)
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
	public static Vector2 CartesianToPolar (Vector2 coord) {
        return new Vector2(Mathf.Sqrt(coord.x * coord.x + coord.y * coord.y), Mathf.Atan2(coord.y, coord.x));
	}

    /// <summary>
    /// Convert from Polar (r, phi) to Cartesian (x, y)
    /// </summary>
    /// <param name="coord"></param>
    /// <returns></returns>
    public static Vector2 PolarToCartesian (Vector2 coord) {
        return new Vector2(coord.x * Mathf.Cos(coord.y), coord.x * Mathf.Sin(coord.y));
    }

    public static Vector2 PolarLerp(Vector2 a, Vector2 b, float t) {
        return new Vector2(
            Mathf.Lerp(a.x, b.x, t),
            Mathf.Deg2Rad*Mathf.LerpAngle(Mathf.Rad2Deg*a.y, Mathf.Rad2Deg*b.y, t)
        );
    }
}
