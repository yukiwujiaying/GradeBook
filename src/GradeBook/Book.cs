using System;
using System.Collections.Generic;
namespace GradeBook
{
    public delegate void GradeAddedDelegated(object sender, EventArgs args);

    public class NameObject
    {
        public NameObject(string name)
        {
            Name = name;
        }

        public string Name{ get; set;}
    }
    public class Book : NameObject
    {
        
        // this is a constructor that initialise the name and grades
        public Book(string name)
        {
            grades= new List<double>();
            //Name is field, name is parameter
            Name=name;

        }
        public void AddLetter(char letter)
        {
            switch(letter)
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
        public void AddGrade( double grade)
        {
            
           if (grade <= 100 && grade >=0)
           {
               grades.Add(grade);
               if(GradeAdded != null)
               {
                   GradeAdded(this, new EventArgs());
               }
           } 
           else
           {
               throw new ArgumentException($"Invalid {nameof(grade)}");
           }    

        }
        public event GradeAddedDelegated GradeAdded;
        public Statistics GetStatistics()
        {
            
            var result=new Statistics();
            result.Average = 0.0;
            //just a property double type and start at the lowest possible double type value
            result.High= double.MinValue;
            result.Low= double.MaxValue;

           
            
            for(var index=0; index < grades.Count; index+=1 )
            {
                
                result.High= Math.Max(grades[index],result.High);
                result.Low=Math.Min(grades[index],result.Low);
                result.Average+=grades[index];
               
            }       
            
             
            result.Average/=grades.Count;
            switch(result.Average)
            {
                case var d when d>=90.0:
                    result.letter ='A';
                    break;
                case var d when d>=80.0:
                    result.letter ='B';
                    break;

                case var d when d>=70.0:
                    result.letter ='C';
                    break;

                case var d when d>=60.0:
                    result.letter ='D';
                    break;
                
                default:
                    result.letter='A';
                    break;
                
            }

            return result;
         
           

        }
        //this is a field, so the method can access it
        //private mean this can only be assessible by the book class
        private List<double> grades;

        public string Name{ get;  set;}

        readonly string category="Science";
              
    }

}