import axios from './index'

class DoctorApi {
  static async GetDoctorsBySpeciality(specialityId) {
    return await axios.get(`/Doctor/get_by_speciality/${specialityId}`)
  }
  static async GetSpecialities(){
    return await axios.get('/Speciality')
  }
  static async UpdateDoctor(data) {
    return await axios.put(`/Doctor/${data.userId}`, data)
  }
  static async DeleteDoctor(userId){
    return await axios.delete(`/Doctor/${userId}`)
  }
  static async SendSchedule(data){
    return await axios.post();
  }
}

export default DoctorApi