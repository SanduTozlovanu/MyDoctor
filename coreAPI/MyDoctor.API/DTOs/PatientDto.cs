﻿namespace MyDoctor.API.DTOs
{
    public class CreatePatientDto
    {
        public CreateUserDto UserDetails { get; set; }
        public uint Age { get; set; }
    }
    public class DisplayPatientDto
    {
        public DisplayPatientDto(Guid id, string firstName, string lastName, string email, uint age) 
        {
            this.Id = id;
            this.LastName = lastName;
            this.FirstName = firstName; 
            this.Email = email; 
            this.Age = age;
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public uint Age { get; set; }       

    }
}