namespace Program
{
    public class Generators //Day 15
    {
        public static long Run()
        {
            long genA = 722;
            long genB = 354;
            long judge = 0;
            for (long i = 0; i < 5000000; i++)
            {
                do
                {
                    genA = (genA * 16807) % 2147483647;
                } while (genA % 4 != 0);
                do
                {
                    genB = (genB * 48271) % 2147483647;
                } while (genB % 8 != 0);

                if ((genA & 65535) == (genB & 65535))
                {
                    judge++;
                }
            }
            return judge;
        }
    }
}