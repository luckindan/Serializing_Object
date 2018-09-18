using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApp3
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating the custumer John Doe\n");
            Customer newcutomer = new Customer();
            newcutomer.m_FirstName = "John";
            newcutomer.m_LastName = "Doe";
            newcutomer.printCustomerInfo();
            byte[] customerInfo;
            newcutomer.Write(out customerInfo);

            Console.WriteLine("\nJohn Doe created\n Now creating his clone by using his serialized Data");
            Customer copyCustoer = new Customer();

            Console.WriteLine("\nBefore copy:\n");
            copyCustoer.printCustomerInfo();
            Console.WriteLine("\nAfter copy: \n");
            copyCustoer = Customer.Read(customerInfo);

            copyCustoer.printCustomerInfo();

            Console.ReadKey();
            
        }
    }

    
}
