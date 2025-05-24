// See https://aka.ms/new-console-template for more information


public class Student
{
    public string IndexNumber { get; set; }
    public string Name { get; set; }
    public double GPA { get; set; }
    public int AdmissionYear { get; set; }
    public string NIC { get; set; }

    public Student(string indexNumber, string name, double gpa, int admissionYear, string nic)
    {
        IndexNumber = indexNumber;
        Name = name;
        GPA = gpa;
        AdmissionYear = admissionYear;
        NIC = nic;
    }

    public override string ToString()
    {
        return $"Index: {IndexNumber}, Name: {Name}, GPA: {GPA}, Year: {AdmissionYear}, NIC: {NIC}";
    }
}

public class StudentNode
{
    public Student Student { get; set; }
    public StudentNode Next { get; set; }

    public StudentNode(Student student)
    {
        Student = student;
        Next = null;
    }
}

public class StudentList
{
    private StudentNode head;

    public StudentList()
    {
        head = null;
    }

    private int CompareIndices(string index1, string index2)
    {
        return string.Compare(index1, index2, StringComparison.Ordinal);
    }

    public void AddStudent(Student newStudent)
    {
        if (FindStudent(newStudent.IndexNumber) != null)
        {
            Console.WriteLine($"Student with index {newStudent.IndexNumber} already exists.");
            return;
        }

        StudentNode newNode = new StudentNode(newStudent);

        if (head == null || CompareIndices(newStudent.IndexNumber, head.Student.IndexNumber) < 0)
        {
            newNode.Next = head;
            head = newNode;
        }
        else
        {
            StudentNode current = head;
            while (current.Next != null && CompareIndices(newStudent.IndexNumber, current.Next.Student.IndexNumber) > 0)
            {
                current = current.Next;
            }
            newNode.Next = current.Next;
            current.Next = newNode;
        }
        Console.WriteLine($"Student {newStudent.IndexNumber} added.");
    }

    public Student FindStudent(string indexNumber)
    {
        StudentNode current = head;
        while (current != null)
        {
            if (current.Student.IndexNumber.Equals(indexNumber, StringComparison.OrdinalIgnoreCase))
            {
                return current.Student;
            }
            current = current.Next;
        }
        return null;
    }

    public void DeleteStudent(string indexNumber)
    {
        if (head == null)
        {
            Console.WriteLine("List is empty. Nothing to remove.");
            return;
        }

        if (head.Student.IndexNumber.Equals(indexNumber, StringComparison.OrdinalIgnoreCase))
        {
            head = head.Next;
            Console.WriteLine($"Student {indexNumber} removed.");
            return;
        }

        StudentNode current = head;
        while (current.Next != null && !current.Next.Student.IndexNumber.Equals(indexNumber, StringComparison.OrdinalIgnoreCase))
        {
            current = current.Next;
        }

        if (current.Next != null)
        {
            current.Next = current.Next.Next;
            Console.WriteLine($"Student {indexNumber} removed.");
        }
        else
        {
            Console.WriteLine($"Student with index {indexNumber} not found.");
        }
    }

    public void ShowAllStudents()
    {
        if (head == null)
        {
            Console.WriteLine("No students in the list.");
            return;
        }

        StudentNode current = head;
        Console.WriteLine("\n--- All Students ---");
        while (current != null)
        {
            Console.WriteLine(current.Student);
            current = current.Next;
        }
        Console.WriteLine("--------------------\n");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        StudentList students = new StudentList();
        bool running = true;

        while (running)
        {
            Console.WriteLine("--- Student Menu ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Search Student");
            Console.WriteLine("3. Remove Student");
            Console.WriteLine("4. Show All Students");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Index Number: ");
                    string indexNum = Console.ReadLine();
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter GPA: ");
                    double gpa;
                    while (!double.TryParse(Console.ReadLine(), out gpa))
                    {
                        Console.WriteLine("Invalid GPA. Enter a number.");
                        Console.Write("Enter GPA: ");
                    }
                    Console.Write("Enter Admission Year: ");
                    int admissionYear;
                    while (!int.TryParse(Console.ReadLine(), out admissionYear))
                    {
                        Console.WriteLine("Invalid year. Enter a number.");
                        Console.Write("Enter Admission Year: ");
                    }
                    Console.Write("Enter NIC: ");
                    string nic = Console.ReadLine();

                    Student newStudent = new Student(indexNum, name, gpa, admissionYear, nic);
                    students.AddStudent(newStudent);
                    break;

                case "2":
                    Console.Write("Enter Index Number to search: ");
                    string searchIndex = Console.ReadLine();
                    Student found = students.FindStudent(searchIndex);
                    if (found != null)
                    {
                        Console.WriteLine("Found: " + found);
                    }
                    else
                    {
                        Console.WriteLine($"Student {searchIndex} not found.");
                    }
                    break;

                case "3":
                    Console.Write("Enter Index Number to remove: ");
                    string removeIndex = Console.ReadLine();
                    students.DeleteStudent(removeIndex);
                    break;

                case "4":
                    students.ShowAllStudents();
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
