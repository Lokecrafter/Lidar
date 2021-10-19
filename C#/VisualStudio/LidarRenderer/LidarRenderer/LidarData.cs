public struct LidarData
{
    public int angle;
    public int distance;
    public bool rendered;

    public LidarData(int angle, int distance)
    {
        this.angle = angle;
        this.distance = distance;
        this.rendered = false;
    }
}