using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {

        

       
        [HttpGet]
        [Route("All",Name = "Getstudents")]
        public ActionResult<IEnumerable<StudentDto>>  Getstudents()
        {
            /*var students = new List<StudentDto>();
            foreach (var item in CollegeReposettory.Students)
            {
                var studentDto = new StudentDto()
                {
                    Id = item.Id,
                    StudentName = item.StudentName,
                    Address= item.Address, 
                    Email=item.Email  
                };
                students.Add(studentDto);
            } 

            
            return Ok(students);*/


            //using LINQ
           // _logger.LogInformation("get student metho is stARTED");

            var students = CollegeReposettory.Students.Select(s => new StudentDto()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });
              return Ok(students);
        }


        [HttpGet("{id:int}", Name = "GetStudntById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Student> GetStudntById(int id)
        {
            if(id <= 0)
            {
               
                return BadRequest();
            }
                

            var student = CollegeReposettory.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                //_logger.LogError("Student not found with log id");
                return NotFound();
            }
            return Ok(student);
        }


      
        [HttpGet("{name:alpha}", Name = "GetStudntByName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<Student> GetStudntByName(string name)
        {

            if (string.IsNullOrEmpty(name))
            {
               // _logger.LogWarning("Provide the name");
                return BadRequest();
            }
               

            var student = CollegeReposettory.Students.Where(n => n.StudentName == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpDelete("{id:min(1):max(100)}",Name = "DeleteStudntById")]
        public bool DeleteStudntById(int id)
        {  
            var student = CollegeReposettory.Students.Where(n => n.Id == id).FirstOrDefault();
            CollegeReposettory.Students.Remove(student);
            return true;
        }



        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<StudentDto> UpdateStudent([FromBody] StudentDto model)
        {
            if(model==null|| model.Id <= 0)
            {
                return BadRequest();
            }
            var Existingstudennt=CollegeReposettory.Students.Where(n =>n.Id == model.Id).FirstOrDefault();
            if(Existingstudennt == null)
            {
                return BadRequest();
            }
            Existingstudennt.StudentName = model.StudentName;
            Existingstudennt.Email= model.Email;
            Existingstudennt.Address= model.Address;

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<StudentDto> UpdatePartialStudent(int id,[FromBody] JsonPatchDocument<StudentDto> patchdocment)
        {
            if (patchdocment == null || id <= 0)
            {
                return BadRequest();
            }
            var Existingstudennt = CollegeReposettory.Students.Where(n => n.Id ==id).FirstOrDefault();
            if (Existingstudennt == null)
            {
                return BadRequest();
            }
            var newStudentDto = new StudentDto()
            {
                Id=Existingstudennt.Id,
                StudentName=Existingstudennt.StudentName,
                Email=Existingstudennt.Email,
                Address=Existingstudennt.Address,
            };

            patchdocment.ApplyTo(newStudentDto,ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Existingstudennt.StudentName = newStudentDto.StudentName;
            Existingstudennt.Email = newStudentDto.Email;
            Existingstudennt.Address = newStudentDto.Address;



            return NoContent();
        }



        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto model)
        {
            if (model == null) { 
                return BadRequest();
            }

            int newId = CollegeReposettory.Students.LastOrDefault().Id + 1;

            Student std= new Student()
            {
                Id = newId,
                StudentName=model.StudentName,
                Email=model.Email,
                Address=model.Address,
            };
            CollegeReposettory.Students.Add(std);
            model.Id = std.Id;
            return CreatedAtRoute("GetStudntById", new { id = model.Id},model);
            return Ok(model);
        }

        
    }
}
