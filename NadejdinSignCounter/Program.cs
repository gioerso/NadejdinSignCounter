namespace NadejdinSignCounter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SignatureParser sigParser = new SignatureParser();
            List<Signature> signList = sigParser.Collector();

            Console.Clear();
            Console.WriteLine("=== Отсортированные подписи Надеждина ===");

            int excess = 0;
            int notExcess = 0;

            for (int i = 0; i < signList.Count; i++)
            {
                Signature sig = signList[i];
                Console.WriteLine("В регионе " + sig.region + " собрано " + sig.signCount + " подписей.");
                if(sig.signCount > 2500)
                {
                    excess += sig.signCount;
                }
                else
                {
                    notExcess += sig.signCount;
                }
            }

            Console.WriteLine("\n\n\nОтсортированно всего: " + excess + " подписей.");
            Console.WriteLine("Отсортированно без превышения: " + notExcess + " подписей.");
            Console.WriteLine("С превышением до 100000 не хватает: " + (excess - 100000) * -1 + " подписей.");
            Console.WriteLine("Без превышения до 100000 не хватает: " + (notExcess - 100000) * -1 + " подписей.");

            Console.WriteLine("\n\n\n=== Отсортированные подписи Надеждина ===");
        }
    }
}