using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/* 
 sources: 
 https://www.dotnetperls.com/memorystream  memoryStream()
 https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream.toarray?view=netframework-4.7.2 memorystream.toArray
 https://docs.microsoft.com/en-us/dotnet/api/system.io.memorystream.seek?view=netframework-4.7.2  memorystream.seek
 https://www.c-sharpcorner.com/article/serialization-and-deserialization-in-c-sharp/    Serializing object

    Challenge
    You need to write the information into a binary format so that everything you need to read it back will be inside the byte array.
    The byte array can be written to disk or sent over the wire via a socket, doesn't matter. 
    Once you have it in byte form, it can be processed in many ways.
    Write the two functions and then add two more if you like to actually write and read from a file.
    You can re-use the top two functions for the file functions, no need to rewrite the same thing. 
    I.e. write the byte array directly to disk once you have it.
    You have to define your own binary format (think: WOW!); how you are going to store each field and in which sequence. 
    Once you have this working properly you will develop a feeling of why XML is such a winner.
    But also why XML is a loser when it comes to storing millions of such customer records, where binary is clearly the winner.
    And there is more that sets XML and binary apart, we'll talk about it on Friday

    Hints
    Think big endian / little endian
    Think unicode encoding
    How to store a date in binary? My hint: think lime disease. You can use the 'ticks' field of DateTime, it's probably the best way to do it.
   
     */

namespace ConsoleApp3
{
  
    [Serializable()]
    public class Customer
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
        public Customer()
        {
            m_FirstName = "Name";
            m_LastName = "LastName";
            m_BirthdayUT = DateTime.Now;
            m_FullAddress = "Not registered";
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
        static public Customer Read(byte[] inputArray)
         {
            //throw if the inputArray is null
            if(inputArray == null)
            {
                throw new Exception();
            }
            Customer theCustomer = null;
            //deserialize the data
            try {
                //Set ups
                MemoryStream memoryStream = new MemoryStream(); //creates the memorystream
                BinaryFormatter binaryFormatter = new BinaryFormatter(); //create the binary formatter

                //Convert array to customer
                memoryStream.Write(inputArray, 0, inputArray.Length); //writes the array into memorystream
                memoryStream.Seek(0, SeekOrigin.Begin); //seek for the origin
                theCustomer = (Customer)binaryFormatter.Deserialize(memoryStream); //deserialize the data
            }
            catch(Exception error)
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
             try {

                //Set ups
                BinaryFormatter binaryFormatter = new BinaryFormatter(); //creates the binary formatter
                MemoryStream ms = new MemoryStream(); //creates the memory Stream


                //Convert Object to array
                binaryFormatter.Serialize(ms, this); //Serialize the object into binary form
                newByteArray = ms.ToArray();  //convert it into an array
        

                m_operation = true; //operation successed
             }
             catch(Exception error)
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
    }
}
