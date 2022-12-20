import axios from './index'

class DoctorApi {
  static async GetDoctorsBySpeciality(specialityId) {
    return await axios.get(`/Doctors/get_by_speciality/${specialityId}`)
  }
  static async GetSpecialities(){
    return await axios.get('/Specialities')
  }
  static async UpdateDoctor(data) {
    return await axios.put(`/Doctors/${data.userId}`, data)
  }
  static async DeleteDoctor(userId){
    return await axios.delete(`/Doctors/${userId}`)
  }
  static async SendSchedule(data){
    return await axios.post();
  }
}

export default DoctorApi