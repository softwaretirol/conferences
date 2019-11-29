using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DDC2019Workshop
{
    class Program
    {
        public static IEnumerable<int> WhereEquals(IEnumerable<int> source, 
            Func<int, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
        public static IEnumerable<string> WhereEquals2(IEnumerable<string> source,
            Expression<Func<string, bool>> predicate)
        {
            return source;
        }
        static async Task Main(string[] args)
        {
            int[] numbers = {1, 2, 3, 4};
            //WhereEquals(numbers, x => x % 2 == 0);
            //WhereEquals2(new[]{"a","b","c"}, x => x?.Length > 10);


            await using (var connection = new SqlConnection())
            {
                connection.Open();
            }


            await using var connection2 = new SqlConnection();
            connection2.Open();

            await using var connection3 = new SqlConnection();
            await connection3.OpenAsync();

            await using var command = connection3.CreateCommand();
            command.CommandText = "SELECT * FROM IRGENDWAS";
            using var reader = command.ExecuteReaderAsync();



            Console.WriteLine("Hello World!");
        }
    }
}
