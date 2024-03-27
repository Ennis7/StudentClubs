namespace StudentClubs;
public class Student
{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
    public double Tuition { get; set; }
}
public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}
public class StudentGPA
{
    public int StudentID { get; set; }
    public double GPA { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        // Student collection
        IList<Student> studentList = new List<Student>() {
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
        };
        // Student GPA Collection
        IList<StudentGPA> studentGPAList = new List<StudentGPA>() {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
        };
        // Club collection
        IList<StudentClubs> studentClubList = new List<StudentClubs>() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        Console.WriteLine("a.)");
        Console.WriteLine("Group by GPA and display the student's IDs.)");
        Console.WriteLine();
        var groupedGPA = studentGPAList.GroupBy(a => a.GPA);

        foreach (var gpaGroup in groupedGPA)
        {
            Console.WriteLine("GPA Group: " + gpaGroup.Key);
            foreach (StudentGPA student in gpaGroup)
            {
                Console.WriteLine("Student ID: " + student.StudentID);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();




        Console.WriteLine("b.)");
        Console.WriteLine("Sort by Club, then group by Club and display the student's IDs.");
        Console.WriteLine();

        var groupedClub = studentClubList.GroupBy(b => b.ClubName);

        foreach (var clubGroup in groupedClub.OrderBy(g => g.Key))
        {
            Console.WriteLine("Club Name: " + clubGroup.Key);
            foreach (StudentClubs studentClub in clubGroup)
            {
                Console.WriteLine("Student ID: " + studentClub.StudentID);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();



        Console.WriteLine("c.)");
        Console.WriteLine("Count the number of students with a GPA between 2.5 and 4.0.");
        Console.WriteLine();
        int countGPA = studentGPAList.Count(c => c.GPA > 2.5 && c.GPA < 4.0);
        Console.WriteLine(countGPA);
        Console.WriteLine();
        Console.WriteLine();



        Console.WriteLine("d.)");
        Console.WriteLine("Average all student's tuition.");
        Console.WriteLine();
        var avgTuition = studentList.Average(d => d.Tuition);
        Console.WriteLine(avgTuition);
        Console.WriteLine();
        Console.WriteLine();



        Console.WriteLine("e.)");
        Console.WriteLine("The student with the highest tuition:");
        Console.WriteLine();
        double maxTuition = studentList.Max(e => e.Tuition);
        var highTuitionStudent = studentList.Where(e => e.Tuition == maxTuition);
        foreach (var student in highTuitionStudent)
        {
            Console.WriteLine("Name: " + student.StudentName);
            Console.WriteLine("Major: " + student.Major);
            Console.WriteLine("Tuition: $" + student.Tuition);
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine();



        Console.WriteLine("f.)");
        Console.WriteLine("Joining the student list and student GPA list on student ID and display the student's name, major and gpa:");
        Console.WriteLine();
        //LINQ Query Method Syntax using Join to combine Students and StudentClubs
        var JoinGPA = studentList.Join(studentGPAList,
                                student => student.StudentID,
                                GPA => GPA.StudentID,
                                (student, GPA) => new
                                {
                                    StudentName = student.StudentName,
                                    Major = student.Major,
                                    GPA = GPA.GPA
                                });
        foreach (var f in JoinGPA)
        {
            Console.WriteLine($"Name: {f.StudentName}   " +
                $"\tGPA: {f.GPA} \t\tMajor: {f.Major}");
        }
        Console.WriteLine();
        Console.WriteLine();



        Console.WriteLine("g.)");
        Console.WriteLine("Joining the student list and student club list. Display the names of only those students who are in the Game club:");
        Console.WriteLine();
        //LINQ Query Method Syntax using Join to combine Students and StudentClubs
        var JoinClub = studentList.Join(studentClubList,
                                student => student.StudentID,
                                club => club.StudentID,
                                (student, club) => new
                                {
                                    StudentName = student.StudentName,
                                    Major = student.Major,
                                    ClubName = club.ClubName
                                })
            .Where(game => game.ClubName == "Game");

        Console.WriteLine("Students who belong to the club Game:");
        Console.WriteLine();
        foreach (var g in JoinClub)
        {
            Console.WriteLine($"{g.StudentName}");
        }

        Console.ReadKey();
    }
}