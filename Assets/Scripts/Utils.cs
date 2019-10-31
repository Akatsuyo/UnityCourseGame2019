public static class Utils
{
    public static float Clamp(float value, float min, float max) 
    {
        if (value < min) {
            return min;
        } else if (value > max) {
            return max;
        } else {
            return value;
        }
    }

    public static int GetInputDirection(float axis) {
        if (axis > 0) {
            return 1;
        } else if (axis < 0) {
            return -1;
        } else {
            return 0;
        }
    }
}