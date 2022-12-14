import axios from './index'

class DoctorApi {
  static async GetDoctorsBySpeciality(specialityId) {
    return await axios.get(`/Doctors/get_by_speciality/${specialityId}`)
  }
  static async GetDoctorById(doctorId) {
    return await axios.get(`/Doctors/${doctorId}`)
  }
  static async UpdateDoctor(userId, data) {
    console.log(userId, data)
    return await axios.put(`/Doctors/${userId}`, data)
  }
  static async DeleteDoctor(userId) {
    return await axios.delete(`/Doctors/${userId}`)
  }
  static async GetAvailableAppointmentSchedule(userId, data) {
    return await axios.get(
      `Doctors/get_available_appointment_schedule/${userId}?` + new URLSearchParams(data)
    )
  }
}

export default DoctorApi
