import axios from './index'

class DoctorApi {
  static async GetDoctorsBySpeciality(specialityId) {
    return await axios.get(`/Doctors/get_by_speciality/${specialityId}`)
  }
  static async GetSpecialities(){
    return await axios.get('/Specialities')
  }
  static async UpdateDoctor(userId, data) {
    console.log(userId, data)
    return await axios.put(`/Doctors/${userId}`, data)
  }
  static async DeleteDoctor(userId){
    return await axios.delete(`/Doctors/${userId}`)
  }
  static async SendSchedule(data){
    return await axios.put(`/ScheduleIntervals`, data);
  }
  static async GetSchedule(userId){
    return await axios.get(`/ScheduleIntervals/${userId}`);
  }
  static async GetAvailableAppointmentSchedule(userId,data){
    return await axios.get(`Doctors/get_available_appointment_schedule/${userId}`)
  }
}

export default DoctorApi