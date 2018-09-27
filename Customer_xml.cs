using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace ConsoleApp3
{

    [Serializable()]
    public class Customer_xml
    {
        public string m_FirstName;
        public string m_LastName;
        public DateTime m_BirthdayUT; // UT for universal time. We haven't covered the beauties of international date and time handling, have we ? (^_^)
        public string m_FullAddress;
        public bool m_IsPainInTheAss;
        public bool m_IsVIP;
        public bool m_IsHot; // I leave it up to you to interpret the meaning of this flag. It doesn't have to be perverted but it's up to you (^_^)
        public float m_AverageBodyTemperature; // could range anywhere from 0 °C to 45 (^_^)


        //default constructor
        public Customer_xml()
        {
            m_FirstName = "Name";
            m_LastName = "LastName";
            m_BirthdayUT = DateTime.Now;
            m_FullAddress = "Somewhere on the Earth";
            m_IsPainInTheAss = true;
            m_IsVIP = false;
            m_IsHot = false;
            m_AverageBodyTemperature = 0;
        }

        //this function prints the basic customer info
        public void printCustomerInfo()
        {
            Console.Write("Your First name is: \t{0}\n" +
                " Your Last Name is: \t{1} \n" +
                " Your Birthday is: \t{2} \n" +
                "Your Address is: \t{3} \n" +
                "Are you pain in the ass: \t{4} \n" +
                "Are you VIP: \t{5} \n" +
                "Are you Hot: \t{6} \n" +
                "Your average body temperature \t {7} \n"
                , m_FirstName, m_LastName, m_BirthdayUT.ToString(), m_FullAddress, m_IsPainInTheAss, m_IsVIP, m_IsHot, m_AverageBodyTemperature);

        }

        //THis function will read the customer infomation from a serialized byte array
        //return: the customer info with provided inputArray
        static public Customer_xml Read(byte[] inputArray)
        {
            //throw if the inputArray is null
            if (inputArray == null)
            {
                throw new Exception();
            }
            Customer_xml theCustomer = null;
            //deserialize the data
            try
            {
                MemoryStream memoryStream = new MemoryStream(inputArray);
                //Set ups

                XmlSerializer binaryFormatter = new XmlSerializer(typeof(Customer_xml)); //create the binary formatter

                //Convert array to customer
                memoryStream.Write(inputArray, 0, inputArray.Length); //writes the array into memorystream
                memoryStream.Seek(0, SeekOrigin.Begin); //seek for the origin
                theCustomer = (Customer_xml)binaryFormatter.Deserialize(memoryStream); //deserialize the data

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            return theCustomer;// return object or throw exception if something sucked during the process

        }


        static public Customer_xml Read(string filepath)
        {
            //throw if the inputArray is null
            Customer_xml theCustomer = null;
            //deserialize the data
            try
            {
                using(TextReader reader = new StreamReader(filepath))
                {
                    theCustomer = new Customer_xml();
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer_xml));
                    theCustomer = (Customer_xml)xmlSerializer.Deserialize(reader);
                    reader.Close();

                }  
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            return theCustomer;// return object or throw exception if something sucked during the process

        }

        //this function will convert the customer data into outputArray
        //returns the operations statues, true or false
        public bool Write(out byte[] outputArray)
        {
            bool m_operation = false; //operation statues
            byte[] newByteArray = null;

            // the new byte array
            try
            {
                //Set ups
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(Customer_xml)); //creates the binary formatter
                MemoryStream ms = new MemoryStream(); //creates the memory Stream


                //Convert Object to array
                xmlFormatter.Serialize(ms, this); //Serialize the object into binary form
                newByteArray = ms.ToArray();  //convert it into an array


                m_operation = true; //operation successed

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            finally
            {
                //check if the created array is null
                //must assign outputArray to something
                if (newByteArray != null)
                {
                    outputArray = newByteArray;
                }
                else
                {
                    outputArray = null;
                }
            }

            return m_operation; //return the operation statues
        }

        //this function will writes the current object into the file with given filepath
        public bool Write(string filepath)
        {
            bool m_operation = false; //operation statues


            // the new byte array
            try
            {
                //Set ups
                using (TextWriter writer = new StreamWriter(filepath,false, UTF8Encoding.UTF8))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer_xml)); //creates the binary formatter
                    

                    xmlSerializer.Serialize(writer, this); //Serialize the object

                    m_operation = true; //operation successed
                    writer.Close();
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
          

            return m_operation; //return the operation statues
        }
    }
}

