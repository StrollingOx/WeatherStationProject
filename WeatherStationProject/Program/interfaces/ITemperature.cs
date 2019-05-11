namespace Program
{
    public interface ITemperature
    {
        void SetScale(int scale);
        string GetScaleName();
        string GetScaleSymbol();
        double GetTemperature();
    }
}