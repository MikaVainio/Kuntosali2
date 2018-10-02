using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Classes needed for serialization
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kuntosali2
{
    // Person is a super class for class Member
    [Serializable]
    class Person
    {
        // Class attributes are protected to allow inheritace
        protected string givenName;
        protected string surName;
        protected double height;
        protected double weight;
        protected uint age;
        protected uint sex; // 1 male, 0 female

        // Default constructor, no parameters
        public Person()
        {
            this.givenName = "Unknown";
            this.surName = "Unknown";
            this.height = 0;
            this.weight = 0;
            this.age = 0;
            this.sex = 0;

        }

        // Constructor with all attributes as parameters
        public Person(string givenName, string surName, double height, double weight, uint age, uint sex)
        {
            this.givenName = givenName;
            this.surName = surName;
            this.height = height;
            this.weight = weight;
            this.age = age;
            this.sex = sex;
        }

        // Method for calculating the body mass index (BMI), returns double
        public double calculateBMI()
        {
            double bmi = this.weight / (this.height * this.height);
            return bmi;
        }

        // Method for calculating body fat pecentage, returns double
        public double fatPercent()
        {
            double bmi = this.calculateBMI();
            if (age < 17) // Fat % for children
            {
                if (sex == 1) // For boys
                {
                    double fatPercent = (1.51f * bmi) - (0.70f * this.age) - 3.6f + 1.4f;
                    return fatPercent;
                }
                else // For girls
                {
                    double fatPercent = (1.51f * bmi) - (0.70f * this.age) + 1.4f;
                    return fatPercent;
                }
            }
            else // Fat % for adults
            {
                if (sex == 1) // For men
                {
                    // Aikuisen rasvaprosentti = (1.20 × painoindeksi) + (0.23 × ikä) − (10.8 × sukupuoli) − 5.4
                    double fatPercent = (1.2f * bmi) + (0.23f * this.age) - (10.8f * this.sex) -5.4f;
                    return fatPercent;
                }
                else // For women
                {
                    double fatPercent = (1.2f * bmi) + (0.23f * this.age) - 5.4f;
                    return fatPercent;
                }
            }
        }
    }

    // Member class inherits attributes and methods from Pernson class
    [Serializable]
    class Member : Person
    {
        // Attributes for Member not in Person class
        string memberId;
        double membershipFee;
        double bonus;

        // Default constructor for Member objects
        public Member() : base()
        {
            this.memberId = "new member";
            this.membershipFee = 100.0;
            this.bonus = 0.0;
        }

        // Constructor with all attributes
        public Member(string givenName, string surName, double height, double weight, uint age, uint sex, string memberId, double membershipFee, double bonus) : base(givenName, surName, height, weight, age, sex)
        {
            {
                this.memberId = memberId;
                this.membershipFee = membershipFee;
                this.bonus = bonus;
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                // Variables for UI
                string filePath;
                string firstName;
                string lastName;
                double height;
                double weight;
                uint age;
                uint sex;
                string memberId;
                double fee;
                double bonus;
                string heightAsText;
                string weigthAsText;
                string ageAsText;
                string sexAsText;
                string feeAsText;
                string bonusAsText;

                // Create array for objects to store
                Member[] members = new Member[4];
                for (int i = 0; i < 4; i++)
                {
                    // Asking for user input
                    Console.Write("Type members first name: ");
                    firstName = Console.ReadLine();
                    Console.Write("Type members lastname: ");
                    lastName = Console.ReadLine();
                    Console.Write("Insert height: ");
                    heightAsText = Console.ReadLine();
                    Console.Write("Insert weight: ");
                    weigthAsText = Console.ReadLine();
                    Console.Write("Insert age: ");
                    ageAsText = Console.ReadLine();
                    Console.Write("sex male 1, female 0: ");
                    sexAsText = Console.ReadLine();
                    Console.Write("Type member id: ");
                    memberId = Console.ReadLine();
                    Console.Write("Insert membership fee: ");
                    feeAsText = Console.ReadLine();
                    Console.Write("Insert bonus: ");
                    bonusAsText = Console.ReadLine();

                    // Convert input strings to numeric values
                    height = Double.Parse(heightAsText);
                    weight = Double.Parse(weigthAsText);
                    age = UInt32.Parse(ageAsText);
                    sex = UInt32.Parse(sexAsText);
                    fee = Double.Parse(feeAsText);
                    bonus = Double.Parse(bonusAsText);

                    members[i] = new Member(firstName, lastName, height, weight, age, sex, memberId, fee, bonus);
                }
                Console.WriteLine("Last name is " + members[2].surName);
                Console.ReadLine();

                //// Ask filename and path
                //Console.Write("Enter path and filename to save member data");
                //filePath = Console.ReadLine();

                // File path for dat file 2 backlashes needed for a single backslash
                filePath = "C:\\Users\\admin\\Documents\\members.dat";

                // Create filestream and binary formatter for serialization
                FileStream fileStream = File.OpenWrite(filePath);
                BinaryFormatter binaryFormatter = new BinaryFormatter();

                // Write every element in array to filestream
                for (int i = 0; i < members.Length; i++)
                {
                    binaryFormatter.Serialize(fileStream, members[i]);
                }

                //Close filestream
                fileStream.Flush();
                fileStream.Close();
            }
        }
    }
}
