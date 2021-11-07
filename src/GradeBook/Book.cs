using System;
using System.Collections.Generic;
using System.IO;
namespace GradeBook
{
    public delegate void GradeAddedDelegated(object sender, EventArgs args);

    public class NameObject
    {
        public NameObject(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
    }

    public interface IBook
    {
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegated GradeAdded;
    }
    public abstract class Book : NameObject, IBook
    {
        public Book(string name) : base(name)
        {
        }
        public abstract event GradeAddedDelegated GradeAdded;
        //public virtural event GradeAddedDelegated GradeAdded;
        //virtural allow the derived class to override the implementation for this detail for this method
        public abstract Statistics GetStatistics();
        //abstract is already a virtual class
        public abstract void AddGrade(double grade);
    }
    public class DiskBook : Book
    {
        public DiskBook(string name) : base(name)
        {
        }

        public override event GradeAddedDelegated GradeAdded;

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using(var reader = File.OpenText($"{Name}.txt"))
            {
               var line = reader.ReadLine();

               while(line != null)
               {
                   var number = double.Parse(line);
                   result.Add(number);
                   line = reader.ReadLine();
               }
            }

            return result;
        }

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }

            //dispose() and close() clean up and free the underlying resource
            //(when you write grade to a file and quite it clean up the txt file in order for the next writer to write in file)
            //writer.Dispose();
            // here we use using statement instead of dispose(),so it can automatically close and clean up  as soon as we are
            //finishing working with them
        }

    }
    public class InMemoryBook : Book
    {

        // this is a constructor that initialise the name and grades
        //base() means accessing the constructor of the base class
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            //Name is field, name is parameter
            Name = name;

        }
        public void AddLetter(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;

            }
        }
        public override void AddGrade(double grade)
        {

            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }

        }
        public override event GradeAddedDelegated GradeAdded;
        public override Statistics GetStatistics()
        {

            var result = new Statistics();



            for (var index = 0; index < grades.Count; index += 1)
            {
                result.Add(grades[index]);
            }

            return result;



        }
        //this is a field, so the method can access it
        //private mean this can only be assessible by the book class
        private List<double> grades;

        public string Name { get; set; }

        readonly string category = "Science";

    }

}