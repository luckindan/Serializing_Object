using System;
using System.Text;
using System.IO;


namespace Serialize_Object
{
    class Customer_binary
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
        public Customer_binary()
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
        static public Customer_binary Read(byte[] inputArray)
        {
            //throw if the inputArray is null
            if (inputArray == null)
            {
                throw new Exception();
            }
            Customer_binary theCustomer = null;
            //deserialize the data
            try
            {
                //Set ups
                MemoryStream ms = new MemoryStream(inputArray);
                using (BinaryReader reader = new BinaryReader(ms, Encoding.UTF8))
                {
                    theCustomer = new Customer_binary();
                    //read the data
                    theCustomer.m_FirstName = reader.ReadString();
                    theCustomer.m_LastName = reader.ReadString();
                    theCustomer.m_BirthdayUT = DateTime.FromBinary(reader.ReadInt64());
                    theCustomer.m_FullAddress = reader.ReadString();
                    theCustomer.m_IsPainInTheAss = reader.ReadBoolean();
                    theCustomer.m_IsVIP = reader.ReadBoolean();
                    theCustomer.m_IsHot = reader.ReadBoolean();
                    theCustomer.m_AverageBodyTemperature = reader.ReadInt32();

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
                MemoryStream binaryStream = new MemoryStream();
                //Set ups
                using (BinaryWriter writer = new BinaryWriter(binaryStream, Encoding.UTF8))
                {
                    //Convert Object to arraym
                    writer.Write(m_FirstName);
                    writer.Write(m_LastName);
                    writer.Write(m_BirthdayUT.ToBinary());
                    writer.Write(m_FullAddress);
                    writer.Write(m_IsPainInTheAss);
                    writer.Write(m_IsVIP);
                    writer.Write(m_IsHot);
                    writer.Write(m_AverageBodyTemperature);
                    newByteArray = binaryStream.ToArray();

                    m_operation = true; //operation successed
                }


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
        //THis function will writes the current object into the file at the given filepath
        //returns the operation status
        public bool Write(string filepath)
        {
            bool m_operation = false; //operation statues
            byte[] newByteArray = null;

            // the new byte array
            try
            {
                //Set ups
                MemoryStream binaryStream = new MemoryStream();
                //writes the data into the binary array
                using (BinaryWriter writer = new BinaryWriter(binaryStream, Encoding.UTF8))
                {
                    //Convert Object to arraym
                    writer.Write(m_FirstName);
                    writer.Write(m_LastName);
                    writer.Write(m_BirthdayUT.ToBinary());
                    writer.Write(m_FullAddress);
                    writer.Write(m_IsPainInTheAss);
                    writer.Write(m_IsVIP);
                    writer.Write(m_IsHot);
                    writer.Write(m_AverageBodyTemperature);
                    newByteArray = binaryStream.ToArray();
                    writer.Close();
                }
                File.WriteAllBytes(filepath, newByteArray);
                m_operation = true; //operation successed
                   
                
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }


            return m_operation; //return the operation statues
        }
        //this function will read the bytearray from the filepath
        //retuns the Customer_binary object
        public Customer_binary Read(string filepath)
        {
            //throw if the inputArray is null
            Customer_binary theCustomer = null;
            //deserialize the data
            try
            {
                
                byte[] customerArray = File.ReadAllBytes(filepath); //read all the bytes from the file

                //set up
                MemoryStream ms = new MemoryStream(customerArray);
                using (BinaryReader reader = new BinaryReader(ms, Encoding.UTF8))
                {   
                    theCustomer = new Customer_binary();
                    //read the data
                    theCustomer.m_FirstName = reader.ReadString();
                    theCustomer.m_LastName = reader.ReadString();
                    theCustomer.m_BirthdayUT = DateTime.FromBinary(reader.ReadInt64());
                    theCustomer.m_FullAddress = reader.ReadString();
                    theCustomer.m_IsPainInTheAss = reader.ReadBoolean();
                    theCustomer.m_IsVIP = reader.ReadBoolean();
                    theCustomer.m_IsHot = reader.ReadBoolean();
                    theCustomer.m_AverageBodyTemperature = reader.ReadInt32();
                }

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            return theCustomer;// return object or throw exception if something sucked during the process
        }
    }
}
