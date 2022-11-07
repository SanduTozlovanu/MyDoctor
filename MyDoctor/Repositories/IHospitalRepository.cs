﻿using MyDoctor.Bussiness.Entities;

namespace MyDoctor.Bussiness.Repositories
{
    public interface IHospitalRepository
    {
        void Add(Hospital hospital);
        void Delete(int id);
        void Update(Hospital hospital);
    }
}
