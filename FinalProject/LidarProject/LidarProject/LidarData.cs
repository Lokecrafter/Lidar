namespace LidarProject
{
    public struct LidarData
    {
        public int angle;
        public int distance;

        public LidarData(int angle, int distance)
        {
            this.angle = angle;
            this.distance = distance;
        }

        public LidarData(string dataMessage)
        {
            string[] datas = dataMessage.Split(',');

            angle = int.Parse(datas[0]);
            distance = int.Parse(datas[1]);
        }
    }
}
