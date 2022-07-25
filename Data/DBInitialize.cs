namespace LearnMathRu_0._1.Data
{
    public class DBInitialize
    {
        public static void Initialize(LavandaDB context)
        {
          //  context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Order.Any())
            {
                return;   // DB has been seeded
            }

            context.SaveChanges();
        }
    }
}
