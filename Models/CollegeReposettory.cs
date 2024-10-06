namespace CollegeApp.Models
{
    public static class CollegeReposettory
    {
        public static List<Student> Students { get; set; } = new List<Student>{
                new Student
                {
                    Id= 1,
                    StudentName="student1",
                    Email="Suraj@gmail.com",
                    Address="india"

                },
                 new Student
                 {
                     Id = 2,
                     StudentName = "student2",
                     Email = "Suraj@gmail.com",
                     Address = "brazil"

                 },
            };
    }
}
