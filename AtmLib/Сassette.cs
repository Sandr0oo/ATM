namespace ATM
{
    public class Сassette
    {
        public Сassette(int nominal, int count, bool isWork)
        {
            NominalValue = nominal;
            BanknoteCount = count;
            IsWork = isWork;

        }

        public int NominalValue { get; set; }
        public int BanknoteCount { get; set; }
        public bool IsWork { get; set; }
    }
}
