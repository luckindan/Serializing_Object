using System;



namespace Serialize_Object
{


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("######################################################\nTest1, XML Formatter\n");
            test1();
            Console.ReadKey();
            Console.WriteLine("######################################################\nTest2, Binary Formatter\n");
            test2();
            Console.ReadKey();
            Console.WriteLine("######################################################\nTest3, .Net Serializer");
            test3();
            Console.ReadKey();
        }
        //testing program for the XML method
        static void test1()
        {
            DateTime before = DateTime.Now;
            Console.WriteLine("Creating the custumer John Doe\n");
            Customer_xml newcutomer = new Customer_xml();
            newcutomer.m_FirstName = "John";
            newcutomer.m_LastName = "Doe";

            //newcutomer.printCustomerInfo();
            newcutomer.Write("C:\\Testing\\customer.xml");
            Console.WriteLine("\nJohn Doe created\n Now creating his clone by using his serialized Data\n");
            Customer_xml copyCustoer = new Customer_xml();

            //Console.WriteLine("\nBefore copy:\n");
            //copyCustoer.printCustomerInfo();
            //Console.WriteLine("\nAfter copy: \n");
            copyCustoer = Customer_xml.Read("C:\\Testing\\customer.xml");

            //copyCustoer.printCustomerInfo();

            DateTime after = DateTime.Now;
            Console.WriteLine("This program finished in " + (after - before).TotalSeconds + " seconds.");


        }
        //testing program for the binary formatter method
        static void test2()
        {
            string filepath = "C:\\Testing\\customer_binary.txt";
            DateTime before = DateTime.Now;
            byte[] dataInfo;
            Console.WriteLine("Creating the custumer John Doe\n");
            Customer_binary newcutomer = new Customer_binary();
            newcutomer.m_FirstName = "John";
            newcutomer.m_LastName = "Doe";

            //newcutomer.printCustomerInfo();
            newcutomer.Write(out dataInfo);
            newcutomer.Write(filepath);
            Console.WriteLine("\nJohn Doe created\n Now creating his clone by using his serialized Data\n");
            Customer_binary copyCustoer = new Customer_binary();

            //Console.WriteLine("\nBefore copy:\n");
            //copyCustoer.printCustomerInfo();
            //Console.WriteLine("\nAfter copy: \n");
            copyCustoer = Customer_binary.Read(dataInfo);

            //copyCustoer.printCustomerInfo();

            DateTime after = DateTime.Now;
            Console.WriteLine("This program finished in " + (after - before).TotalSeconds + " seconds.");
        }
        //testing program for the .NET serializer method
        static void test3()
        {
            string datapath = "C:\\Testing\\customer_net.txt";
            DateTime before = DateTime.Now;
            byte[] dataInfo;
            Console.WriteLine("Creating the custumer John Doe\n");
            Customer_net newcutomer = new Customer_net();
            newcutomer.m_FirstName = "John";
            newcutomer.m_LastName = "Doe";

            //newcutomer.printCustomerInfo();
            newcutomer.Write(out dataInfo);
            newcutomer.Write(datapath);
            Console.WriteLine("\nJohn Doe created\n Now creating his clone by using his serialized Data\n");
            Customer_net copyCustoer = new Customer_net();

            //Console.WriteLine("\nBefore copy:\n");
            //copyCustoer.printCustomerInfo();
            //Console.WriteLine("\nAfter copy: \n");
            copyCustoer = Customer_net.Read(dataInfo);

            //copyCustoer.printCustomerInfo();

            DateTime after = DateTime.Now;
            Console.WriteLine("This program finished in " + (after - before).TotalSeconds + " seconds.");
        }
    }

    
}
