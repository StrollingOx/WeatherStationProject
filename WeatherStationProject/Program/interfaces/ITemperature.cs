namespace Program
{
    public interface ITemperature
    {
        void setScale(int scale);
        string getScaleName();
        double getTemperature();
    }
}